using AppDbContext.Data;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using AcademicService.Consumers;
using AcademicService.Interface;
using AcademicService.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ISenateList, SenateListService>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AcademicDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add MassTransit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<StudentCreatedEventConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost");

        cfg.ReceiveEndpoint("StudentCreatedEventQueue", ep =>
        {
            ep.ConfigureConsumer<StudentCreatedEventConsumer>(context);
        });
    });
});
builder.Services.AddMassTransitHostedService();

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
