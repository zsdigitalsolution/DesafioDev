using DesafioDevApi.Domain.Entities;
using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DesafioDevApi.Infrastructure.Data.Common
{
    public class ApiDBContext : DbContext
    {
        public ApiDBContext(DbContextOptions<ApiDBContext> options) : base(options)
        {
        }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Ignore(typeof(Notification));
            modelBuilder.Ignore(typeof(Notifiable));
            modelBuilder.Entity<Transaction>()
                .HasKey(t => t.Id);
        }

    }
}
