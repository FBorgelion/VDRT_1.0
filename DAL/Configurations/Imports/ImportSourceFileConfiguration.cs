using Domain.Imports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations.Imports
{
    public sealed class ImportSourceFileConfiguration : IEntityTypeConfiguration<ImportSourceFile>
    {
        public void Configure(EntityTypeBuilder<ImportSourceFile> builder)
        {
            builder.ToTable("ImportSourceFiles");

            builder.HasKey(sourceFile => sourceFile.Id);

            builder.Property(sourceFile => sourceFile.OriginalFileName)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(sourceFile => sourceFile.FileSizeBytes)
                .IsRequired();

            builder.Property(sourceFile => sourceFile.ContentHash)
                .HasColumnType("char(64)");

            builder.Property(sourceFile => sourceFile.Status)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(sourceFile => sourceFile.TechnicalMessage)
                .HasMaxLength(2000);

            builder.HasIndex(sourceFile => sourceFile.ContentHash);

            builder.HasOne(sourceFile => sourceFile.ImportBatch)
                .WithMany(batch => batch.SourceFiles)
                .HasForeignKey(sourceFile => sourceFile.ImportBatchId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(sourceFile => sourceFile.Traces)
                .HasField("_traces")
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Navigation(sourceFile => sourceFile.Errors)
                .HasField("_errors")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}