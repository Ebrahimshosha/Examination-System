using ExaminationSystem.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExaminationSystem.Api.Data.Configurations;

public class ExamConfig : IEntityTypeConfiguration<Exam>
{
    public void Configure(EntityTypeBuilder<Exam> builder)
    {
        //builder.HasOne(e => e.Instructor)
        //    .WithMany(i => i.Exams)
        //    .OnDelete(DeleteBehavior.NoAction);
    }
}
