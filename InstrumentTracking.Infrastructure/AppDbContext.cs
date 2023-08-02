using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InstrumentsTracking.Domain.Models;
using InstrumentTracking.Domain.Models;


namespace InstrumentTracking.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<Engineer> Engineers { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Request> Request { get; set; }
        public DbSet<SamplingAct> SamplingAct { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<RequestHistory> RequestHistory { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
          // Database.EnsureDeleted();
           Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Engineer>()
                               .Property(p => p.ID)
                               .ValueGeneratedOnAdd();
            modelBuilder.Entity<Equipment>()
                               .Property(p => p.ID)
                               .ValueGeneratedOnAdd();
            modelBuilder.Entity<Request>()
                               .Property(p => p.ID)
                               .ValueGeneratedOnAdd();
            modelBuilder.Entity<SamplingAct>()
                               .Property(p => p.ID)
                               .ValueGeneratedOnAdd();
            modelBuilder.Entity<RequestHistory>()
                               .Property(p => p.ID)
                               .ValueGeneratedOnAdd();
            modelBuilder.Entity<Status>()
                               .Property(p => p.ID)
                               .ValueGeneratedOnAdd();

            modelBuilder.Entity<Engineer>()
            .HasOne(a => a.User)
            .WithOne(a => a.Engineer)
            .HasForeignKey<User>(c => c.EngineerID);
        }
    }
}
