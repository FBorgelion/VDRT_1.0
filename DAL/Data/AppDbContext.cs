using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Mission> Missions { get; set; }
        public DbSet<Sites> Sites { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceLine> InvoiceLines { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Timesheet> Timesheets { get; set; }
        public DbSet<TripAnomaly> TripAnomalies { get; set; }
        public DbSet<VehicleAlert> VehicleAlerts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Mission>()
                .HasOne(m => m.Driver)
                .WithMany(d => d.Missions)
                .HasForeignKey(m => m.DriverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Mission>()
                .HasOne(m => m.Vehicle)
                .WithMany(v => v.Missions)
                .HasForeignKey(m => m.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Mission>()
                .HasOne(m => m.Site)
                .WithMany(s => s.Missions)
                .HasForeignKey(m => m.SiteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Activity>()
                .HasOne(a => a.TripAnomaly)
                .WithOne(ta => ta.Activity)
                .HasForeignKey<TripAnomaly>(ta => ta.Activity_Id)
                .IsRequired(false);

            modelBuilder.Entity<Activity>()
                .HasOne(a => a.InvoiceLine)
                .WithOne(il => il.Activity)
                .HasForeignKey<InvoiceLine>(il => il.Activity_Id)
                .IsRequired(false);
        }


    }

}
