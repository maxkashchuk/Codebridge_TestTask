using Azure;
using Codebridge_TestTask.Models;
using Microsoft.EntityFrameworkCore;

namespace Codebridge_TestTask
{
    public class ApplicationContext : DbContext
    {
        public DbSet<DogDB> Dogs { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<DogDB>().HasIndex(x => x.Id).IsUnique();
            builder.Entity<DogDB>().Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Entity<DogDB>().Property(x => x.Name).IsRequired();
            builder.Entity<DogDB>().Property(x => x.Color).IsRequired();
            builder.Entity<DogDB>().Property(x => x.TailLength).IsRequired();
            builder.Entity<DogDB>().Property(x => x.Weight).IsRequired();
        }
    }
}
