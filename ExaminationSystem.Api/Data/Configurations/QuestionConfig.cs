using ExaminationSystem.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExaminationSystem.Api.Data.Configurations;

public class QuestionConfig : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        //builder.Property(q => q.Status)
        //              .HasConversion(

        //              qStatus => qStatus.ToString() /*In Database*/,

        //              qStatus => (QStatus)Enum.Parse(typeof(QStatus), qStatus) /*In App*/

        //              );
    }
}
