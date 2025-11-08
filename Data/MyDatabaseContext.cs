using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace DotNetCoreSqlDb.Models
{
    public class MyDatabaseContext : DbContext
    {
        public MyDatabaseContext(DbContextOptions<MyDatabaseContext> options)
            : base(options)
        { }

        public DbSet<Todo> Todo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var converter = new ValueConverter<DateTime, string>(
                v => v.ToString("yyyy-MM-dd HH:mm:ss"), // write as string
                v => DateTime.Parse(v)                  // read back as DateTime
            );

            modelBuilder.Entity<Todo>()
                .Property(t => t.CreatedDate)
                .HasConversion(converter)
                .HasColumnType("TEXT"); // matches your SQLite TEXT column
        }
    }
}
