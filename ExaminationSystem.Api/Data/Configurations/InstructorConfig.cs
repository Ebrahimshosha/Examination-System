
namespace ExaminationSystem.Api.Data.Configurations;

public class InstructorConfig : IEntityTypeConfiguration<Instructor>
{
    public void Configure(EntityTypeBuilder<Instructor> builder)
    {
        builder.HasMany(i => i.Exams)
            .WithOne(e => e.Instructor)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
