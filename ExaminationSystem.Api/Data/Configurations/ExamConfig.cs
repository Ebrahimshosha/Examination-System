
namespace ExaminationSystem.Api.Data.Configurations;

public class ExamConfig : IEntityTypeConfiguration<Exam>
{
    public void Configure(EntityTypeBuilder<Exam> builder)
    {
        //builder.HasOne(e => e.Instructor)
        //    .WithMany(i => i.Exams)
        //    .OnDelete(DeleteBehavior.NoAction);


        builder.Property(O => O.ExamStatus)
               .HasConversion(

               EStatus => EStatus.ToString() /*In Database*/,

               EStatus => (ExamStatus)Enum.Parse(typeof(ExamStatus), EStatus) /*In App*/

               );
    }
}
