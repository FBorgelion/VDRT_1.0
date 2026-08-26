using Domain.Entities;
using Domain.Imports;
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

        public DbSet<ImportBatch> ImportBatches => Set<ImportBatch>();

        public DbSet<ImportSourceFile> ImportSourceFiles => Set<ImportSourceFile>();

        public DbSet<ImportedTrace> ImportedTraces => Set<ImportedTrace>();

        public DbSet<ImportedTraceProperty> ImportedTraceProperties => Set<ImportedTraceProperty>();

        public DbSet<ImportError> ImportErrors => Set<ImportError>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Driver --> Missions
            modelBuilder.Entity<Mission>()
                .HasOne(m => m.Driver)
                .WithMany(d => d.Missions)
                .HasForeignKey(m => m.DriverId)
                .OnDelete(DeleteBehavior.Restrict);

            //Vehicle --> Missions
            modelBuilder.Entity<Mission>()
                .HasOne(m => m.Vehicle)
                .WithMany(v => v.Missions)
                .HasForeignKey(m => m.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            //Site --> Missions
            modelBuilder.Entity<Mission>()
                .HasOne(m => m.Site)
                .WithMany(s => s.Missions)
                .HasForeignKey(m => m.SiteId)
                .OnDelete(DeleteBehavior.Restrict);

            // MISSION → ACTIVITIES
            modelBuilder.Entity<Activity>()
                .HasOne(a => a.Mission)
                .WithMany(m => m.Activities)
                .HasForeignKey(a => a.MissionId)
                .OnDelete(DeleteBehavior.Restrict);

            // ACTIVITY TYPE → ACTIVITIES
            modelBuilder.Entity<Activity>()
                .HasOne(a => a.ActivityType)
                .WithMany(at => at.Activities)
                .HasForeignKey(a => a.ActivityTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // DRIVER → ACTIVITIES
            modelBuilder.Entity<Activity>()
                .HasOne(a => a.Driver)
                .WithMany(d => d.Activities)
                .HasForeignKey(a => a.DriverId)
                .OnDelete(DeleteBehavior.Restrict);

            // USER → ACTIVITIES VALIDATED
            modelBuilder.Entity<Activity>()
                .HasOne(a => a.Validator)
                .WithMany(u => u.ValidatedActivities)
                .HasForeignKey(a => a.ValidatorId)
                .OnDelete(DeleteBehavior.Restrict);

            // ACTIVITY → TRIP ANOMALIES
            modelBuilder.Entity<TripAnomaly>()
                .HasOne(t => t.Activity)
                .WithMany(a => a.TripAnomalies)
                .HasForeignKey(t => t.ActivityId)
                .OnDelete(DeleteBehavior.Restrict);

            // MISSION → TRIP ANOMALIES
            modelBuilder.Entity<TripAnomaly>()
            .HasOne(t => t.Mission)
            .WithMany(m => m.TripAnomalies)
            .HasForeignKey(t => t.MissionId)
            .OnDelete(DeleteBehavior.Restrict);

            //ACTIVITY → TRIP ANOMALIES
            modelBuilder.Entity<TripAnomaly>()
                .HasOne(t => t.Activity)
                .WithMany(a => a.TripAnomalies)
                .HasForeignKey(t => t.ActivityId)
                .OnDelete(DeleteBehavior.Restrict);

            // VEHICLE → TRIP ANOMALIES
            modelBuilder.Entity<TripAnomaly>()
                .HasOne(t => t.Vehicle)
                .WithMany(v => v.TripAnomalies)
                .HasForeignKey(t => t.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            // DRIVER → TRIP ANOMALIES
            modelBuilder.Entity<TripAnomaly>()
                .HasOne(t => t.Driver)
                .WithMany(d => d.TripAnomalies)
                .HasForeignKey(t => t.DriverId)
                .OnDelete(DeleteBehavior.Restrict);

            // USER → TRIP ANOMALIES REVIEWED
            modelBuilder.Entity<TripAnomaly>()
                .HasOne(t => t.Reviewer)
                .WithMany(u => u.ReviewedTripAnomalies)
                .HasForeignKey(t => t.ReviewerId)
                .OnDelete(DeleteBehavior.Restrict);

            // ACTIVITY → INVOICE LINE
            modelBuilder.Entity<InvoiceLine>()
                .HasOne(i => i.Activity)
                .WithOne(a => a.InvoiceLine)
                .HasForeignKey<InvoiceLine>(i => i.ActivityId)
                .OnDelete(DeleteBehavior.Restrict);

            //DRIVER → TIMESHEET
            modelBuilder.Entity<Timesheet>()
                .HasOne(t => t.Driver)
                .WithMany(d => d.Timesheets)
                .HasForeignKey(t => t.DriverId)
                .OnDelete(DeleteBehavior.Restrict);

            // USER (APPROVER) → TIMESHEET
            modelBuilder.Entity<Timesheet>()
                .HasOne(t => t.Approver)
                .WithMany()
                .HasForeignKey(t => t.ApproverId)
                .OnDelete(DeleteBehavior.Restrict);

            // VEHICLE → POSITIONS
            modelBuilder.Entity<Position>()
                .HasOne(p => p.Vehicle)
                .WithMany(v => v.Positions)
                .HasForeignKey(p => p.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(AppDbContext).Assembly);

        }


    }

}
