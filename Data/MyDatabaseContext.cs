#nullable disable
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace DotNetCoreSqlDb.Models
{
    public class MyDatabaseContext : DbContext
    {
        public MyDatabaseContext(DbContextOptions<MyDatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<Todo> Todo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Convert CreatedDate (DateTime) to string for storage in TEXT column
            var dateTimeConverter = new ValueConverter<DateTime, string>(
                v => v.ToString("yyyy-MM-dd HH:mm:ss"),  // store format
                v => DateTime.Parse(v)                   // read back as DateTime
            );

            modelBuilder.Entity<Todo>()
                .Property(t => t.CreatedDate)
                .HasConversion(dateTimeConverter)
                .HasColumnType("TEXT"); // ensure it maps to TEXT in SQL
        }
    }
}
