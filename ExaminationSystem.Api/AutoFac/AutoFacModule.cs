using Autofac;
using ExaminationSystem.Api.Interfaces;
using ExaminationSystem.Api.Repositories;
using ExaminationSystem.Api.Services.ExamQuestionService;
using ExaminationSystem.Api.Services.ExamService;
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

    }
}
