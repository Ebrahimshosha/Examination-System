
using System.Reflection;

namespace ExaminationSystem.Api.Data;

public class StoreContext : DbContext
{
    public StoreContext(DbContextOptions<StoreContext> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Choice> Choices { get; set; }
    public DbSet<ExamQuestion> ExamQuestions { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Result> Results { get; set; }

}
