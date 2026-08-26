using Domain.Imports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations.Imports
{
    public sealed class ImportedTraceConfiguration : IEntityTypeConfiguration<ImportedTrace>
    {
        public void Configure(EntityTypeBuilder<ImportedTrace> builder)
        {
            builder.ToTable("ImportedTraces");

            builder.HasKey(trace => trace.Id);

            builder.Property(trace => trace.TraceTypeRaw)
                .HasMaxLength(50);

            builder.Property(trace => trace.SourceRaw)
                .HasMaxLength(500);

            builder.Property(trace => trace.TechnicalTimeRaw)
                .HasMaxLength(100);

            builder.Property(trace => trace.LatitudeRaw)
                .HasMaxLength(100);

            builder.Property(trace => trace.Latitude)
                .HasPrecision(10, 7);

            builder.Property(trace => trace.LongitudeRaw)
                .HasMaxLength(100);

            builder.Property(trace => trace.Longitude)
                .HasPrecision(10, 7);

            builder.Property(trace => trace.MileageRaw)
                .HasMaxLength(100);

            builder.Property(trace => trace.HeadingRaw)
                .HasMaxLength(100);

            builder.Property(trace => trace.Heading)
                .HasPrecision(18, 6);

            builder.Property(trace => trace.SpeedRaw)
                .HasMaxLength(100);

            builder.Property(trace => trace.Speed)
                .HasPrecision(18, 6);

            builder.Property(trace => trace.LinkId)
                .HasMaxLength(500);

            builder.Property(trace => trace.ActivityCode)
                .HasMaxLength(500);

            builder.Property(trace => trace.DriverIdsRaw)
                .HasMaxLength(500);

            builder.Property(trace => trace.SequenceRaw)
                .HasMaxLength(100);

            builder.Property(trace => trace.ActivityStartTimeRaw)
                .HasMaxLength(100);

            builder.Property(trace => trace.ActivityLengthMillisecondsRaw)
                .HasMaxLength(100);

            builder.Property(trace => trace.DrivingLengthMillisecondsRaw)
                .HasMaxLength(100);

            builder.Property(trace => trace.DeviceRaw)
                .HasMaxLength(500);

            builder.Property(trace => trace.ActivityReportRaw)
                .HasColumnType("nvarchar(max)");

            builder.Property(trace => trace.ActivityFinalReportRaw)
                .HasColumnType("nvarchar(max)");

            builder.Property(trace => trace.TraceHash)
                .IsRequired()
                .HasColumnType("char(64)");

            builder.Property(trace => trace.RawXml)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.HasIndex(trace => trace.TraceHash)
                .IsUnique();

            builder.HasIndex(trace => new
            {
                trace.ImportSourceFileId,
                trace.Position
            })
                .IsUnique();

            builder.HasOne(trace => trace.ImportSourceFile)
                .WithMany(sourceFile => sourceFile.Traces)
                .HasForeignKey(trace => trace.ImportSourceFileId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}