using Domain.Imports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations.Imports
{
    public sealed class ImportErrorConfiguration : IEntityTypeConfiguration<ImportError>
    {
        public void Configure(EntityTypeBuilder<ImportError> builder)
        {
            builder.ToTable("ImportErrors");

            builder.HasKey(error => error.Id);

            builder.Property(error => error.Code)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(100);

            builder.Property(error => error.Message)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(error => error.FileName)
                .HasMaxLength(500);

            builder.Property(error => error.Severity)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(error => error.CreatedAtUtc)
                .IsRequired();

            builder.HasOne(error => error.ImportBatch)
                .WithMany(batch => batch.Errors)
                .HasForeignKey(error => error.ImportBatchId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(error => error.ImportSourceFile)
                .WithMany(sourceFile => sourceFile.Errors)
                .HasForeignKey(error => error.ImportSourceFileId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}