using Microsoft.EntityFrameworkCore;
using AcademicService.Models;

namespace AppDbContext.Data
{
    public class AcademicDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public AcademicDbContext(DbContextOptions<AcademicDbContext> options)
          : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        //For correctly mapping "Decimal" data type to the corresponding column in the database.
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Bid>().Property(b => b.Amount).HasPrecision(10, 2);
        //    base.OnModelCreating(modelBuilder);

        //    // This tells Entity Framework to use the User table instead of AspNetUsers for the User entity.
        //    modelBuilder.Entity<User>().ToTable("User");
        //}

    }
}
