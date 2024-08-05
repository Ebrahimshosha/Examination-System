using Autofac;
using ExaminationSystem.Api.Interfaces;
using ExaminationSystem.Api.Repositories;
using ExaminationSystem.Api.Services;
using ExaminationSystem.Api.Services.ChoiceService;
using ExaminationSystem.Api.Services.ExamQuestionService;
using ExaminationSystem.Api.Services.ExamService;
using ExaminationSystem.Api.Services.QuestionService;
using ExaminationSystem.Api.Services.StudentExamService;
using ExaminationSystem.Api.Services.StudentService;
//using System.Reflection;

namespace ExaminationSystem.Api.AutoFac;

public class AutoFacModule : Module
{

    protected override void Load(ContainerBuilder builder)
    {
        //builder.RegisterType<Context>().InstancePerLifetimeScope();
        builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
        builder.RegisterAssemblyTypes(typeof(IExamService).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();
        builder.RegisterAssemblyTypes(typeof(IExamQuestionService).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();
        builder.RegisterAssemblyTypes(typeof(IQuestionService).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();
        builder.RegisterAssemblyTypes(typeof(IChoiceService).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();
        builder.RegisterAssemblyTypes(typeof(IStudentCourseService).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();
        builder.RegisterAssemblyTypes(typeof(IValidateIfTakenFinalExam).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();
        builder.RegisterAssemblyTypes(typeof(IStudentExamService).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();

    }
}
