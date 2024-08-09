
using Autofac.Extensions.DependencyInjection;
using Autofac;
using ExaminationSystem.Api.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ExaminationSystem.Api.AutoFac;
using AutoMapper;
using ExaminationSystem.Api.profiles;
using ExaminationSystem.Api.Helpers;
using ExaminationSystem.Api.Middlewares;

namespace ExaminationSystem.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        builder.Services.AddDbContext<StoreContext>(Options =>
        {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                  .UseLoggerFactory(MyLoggerFactory)
                  .LogTo(log => Debug.WriteLine(log), LogLevel.Information)
                    .EnableSensitiveDataLogging();
        });

        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
            builder.RegisterModule(new AutoFacModule()));

        builder.Services.AddAutoMapper(typeof(Questionprofile));
        builder.Services.AddAutoMapper(typeof(ExamProfile));
        builder.Services.AddAutoMapper(typeof(CourseProfile));
        builder.Services.AddAutoMapper(typeof(StudentProfile));

        var app = builder.Build();

        //app.UseMiddleware<GlobalErrorHandlerMiddleware>();
        app.UseMiddleware<TransactionMiddleware>();

        MapperHelper.Mapper = app.Services.GetService<IMapper>();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
