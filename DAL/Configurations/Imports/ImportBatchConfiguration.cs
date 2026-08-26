using Domain.Imports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations.Imports
{
    public sealed class ImportBatchConfiguration : IEntityTypeConfiguration<ImportBatch>
    {
        public void Configure(EntityTypeBuilder<ImportBatch> builder)
        {
            builder.ToTable("ImportBatches");

            builder.HasKey(batch => batch.Id);

            builder.Property(batch => batch.OriginalFileName)
                .IsRequired()
                .HasMaxLength(260);

            builder.Property(batch => batch.OriginalFileSizeBytes)
                .IsRequired();

            builder.Property(batch => batch.FileHash)
                .IsRequired()
                .HasColumnType("char(64)");

            builder.Property(batch => batch.Status)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(batch => batch.CreatedAtUtc)
                .IsRequired();

            builder.Property(batch => batch.TechnicalMessage)
                .HasMaxLength(2000);

            builder.HasIndex(batch => batch.FileHash)
                .IsUnique();

            builder.Navigation(batch => batch.SourceFiles)
                .HasField("_sourceFiles")
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Navigation(batch => batch.Errors)
                .HasField("_errors")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}