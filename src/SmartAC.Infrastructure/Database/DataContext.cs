using Microsoft.EntityFrameworkCore;
using SmartAC.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Infrastructure.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Device> Devices { get; set; }
        public DbSet<SensorReading> SensorReadings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>()
                .HasIndex(d => d.SerialNumber)
                .IsUnique();
            modelBuilder.Entity<Client>()
                .HasIndex(d => d.SerialNumber)
                .IsUnique();
            modelBuilder.Entity<Client>().HasData(SeedClients());

            modelBuilder.Entity<User>().HasData(SeedUsers());
        }

        private List<User> SeedUsers()
        {
            return new List<User>
            {
                new User
                {
                    UserId = Guid.Parse("47479da2-7641-445b-9186-15c5e6590ef5"),
                    Email = "admin@gmail.com",
                    PasswordHash = "X03MO1qnZdYdgyfeuILPmQ=="
                }
            };
        }

        private static List<Client> SeedClients()
        {
            return new List<Client> {
                new Client {
                    ClientId = Guid.Parse("8bd99aa7-539a-46f3-821b-dfce8ec57762"),
                    SerialNumber = "Device-001",
                    Secret = "a79b6fc9-3884-4232-84ef-0bd8a687fa12"
                },
                new Client
                {
                    ClientId = Guid.Parse("69aef910-b87e-415a-91d2-8bed0f54c545"),
                    SerialNumber = "Device-002",
                    Secret = "b17a76fc-5754-4422-8aa5-7c9b0db4401f"
                },
                new Client
                {
                    ClientId = Guid.Parse("2244f0ba-6e36-4b78-813f-021c749bfe47"),
                    SerialNumber = "Device-003",
                    Secret = "5733e2b1-bab9-442a-b671-59423e7a2b4c"
                }
            };
        }
    }
}
