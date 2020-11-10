using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Principal;
using TicketsReselling.DAL.Models;

namespace TicketsReselling.DAL
{
    public class TicketsResellingContext : IdentityDbContext<User>
    {
        public TicketsResellingContext(DbContextOptions<TicketsResellingContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Venue> Venues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder.Entity<Ticket>().ToTable("Tickets");
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<Event>().ToTable("Events");
            modelBuilder.Entity<City>().ToTable("Cityes");
            modelBuilder.Entity<Venue>().ToTable("Venues");
        }
    }
}
