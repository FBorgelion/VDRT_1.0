using Domain.Imports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations.Imports
{
    public sealed class ImportedTracePropertyConfiguration : IEntityTypeConfiguration<ImportedTraceProperty>
    {
        public void Configure(EntityTypeBuilder<ImportedTraceProperty> builder)
        {
            builder.ToTable("ImportedTraceProperties");

            builder.HasKey(property => property.Id);

            builder.Property(property => property.KeyRaw)
                .HasMaxLength(500);

            builder.Property(property => property.ValueRaw)
                .HasColumnType("nvarchar(max)");

            builder.HasIndex(property => new
            {
                property.ImportedTraceId,
                property.Position
            })
                .IsUnique();

            builder.HasOne(property => property.ImportedTrace)
                .WithMany(trace => trace.Properties)
                .HasForeignKey(property => property.ImportedTraceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}