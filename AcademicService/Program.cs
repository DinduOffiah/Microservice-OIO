using AppDbContext.Data;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using AcademicService.Consumers;
using AcademicService.Interface;
using AcademicService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IStudentService, StudentService>();

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
