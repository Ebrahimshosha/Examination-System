using Autofac;
using ExaminationSystem.Api.Services.CourseService;
using ExaminationSystem.Api.Services.ResultService;

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
        builder.RegisterAssemblyTypes(typeof(IStudentExamService).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();
        builder.RegisterAssemblyTypes(typeof(ICourseService).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();
        builder.RegisterAssemblyTypes(typeof(IResultService).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();

    }
}