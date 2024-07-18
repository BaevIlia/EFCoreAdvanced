using EFCoreAdvanced.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreAdvanced.Configuration
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder) 
        {
            
                builder.ToTable("Students").HasKey(s => s.Id);
                builder.Property(s => s.Id).HasColumnName("StudentID");
                builder.ComplexProperty(s => s.Name, nameBuilder =>
                {
                    nameBuilder.Property(n => n.First).HasColumnName("FirstName").HasMaxLength(100);
                    nameBuilder.Property(n => n.Last).HasColumnName("LastName").HasMaxLength(100);

                });
                builder.HasOne(s => s.FavouriteCourse)
                    .WithMany();

                builder.HasMany(s => s.Enrollments)
                    .WithOne(e => e.Student);


        
        }
    }
}
