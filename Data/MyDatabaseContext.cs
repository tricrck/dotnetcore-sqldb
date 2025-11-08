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
            // Convert CreatedDate to string for storage, and back to DateTime on read
            var dateTimeConverter = new ValueConverter<DateTime, string>(
                v => v.ToString("yyyy-MM-dd HH:mm:ss"),  // write as string
                v => DateTime.Parse(v)                   // read as DateTime
            );

            modelBuilder.Entity<Todo>()
                .Property(t => t.CreatedDate)
                .HasConversion(dateTimeConverter)
                .HasColumnType("TEXT"); // explicitly TEXT in SQL
        }
    }
}
