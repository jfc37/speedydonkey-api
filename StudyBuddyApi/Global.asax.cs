using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ActionHandlers;
using Autofac;
using Autofac.Core;
using Autofac.Integration.WebApi;
using Data;
using Data.Repositories;
using Data.Searches;
using log4net.Config;
using SpeedyDonkeyApi.Models;
using SpeedyDonkeyApi.Services;
using Validation;
using Validation.Validators;

namespace SpeedyDonkeyApi
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            // Create the container builder.
            var builder = new ContainerBuilder();

            // Register the Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Register other dependencies.
            builder.RegisterAssemblyTypes(typeof(ValidatorOverlord).Assembly)
                .AsClosedTypesOf(typeof(IActionValidator<,>)).AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(ActionHandlerOverlord).Assembly)
                .AsClosedTypesOf(typeof(IActionHandler<,>)).AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(PersonRepository<>).Assembly)
                .AsClosedTypesOf(typeof(IPersonRepository<>)).AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(CourseWorkRepository).Assembly)
                .AsClosedTypesOf(typeof(ICourseWorkRepository<>)).AsImplementedInterfaces();

            builder.RegisterGeneric(typeof(EntitySearch<>))
                .As(typeof(IEntitySearch<>))
                .InstancePerDependency();

            builder.RegisterType<SpeedyDonkeyDbContext>().As<ISpeedyDonkeyDbContext>().InstancePerLifetimeScope();

            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<CourseRepository>().As<ICourseRepository>();
            builder.RegisterType<CourseGradeRepository>().As<ICourseGradeRepository>();
            builder.RegisterType<CourseWorkGradeRepository>().As<ICourseWorkGradeRepository>();
            builder.RegisterType<NoticeRepository>().As<INoticeRepository>();
            builder.RegisterType<LectureRepository>().As<ILectureRepository>();
            builder.RegisterType<AssignmentRepository>().As<IAssignmentRepository>();
            builder.RegisterType<ExamRepository>().As<IExamRepository>();
            builder.RegisterType<ActionHandlerOverlord>().As<IActionHandlerOverlord>();
            builder.RegisterType<ValidatorOverlord>().As<IValidatorOverlord>();
            builder.RegisterType<Container>().As<IContainer>();
            builder.RegisterType<ModelFactory>().As<IModelFactory>();
            builder.RegisterType<UrlConstructor>().As<IUrlConstructor>();
            builder.RegisterType<PasswordHasher>().As<IPasswordHasher>();
            builder.RegisterType<SearchQueryParser>().As<ISearchQueryParser>();
            builder.RegisterType<QueryFilterModifier>().As<IQueryModifier>();
            builder.RegisterType<ConditionExpressionHandlerFactory>().As<IConditionExpressionHandlerFactory>();
            builder.RegisterType<QueryModifierFactory>().As<IQueryModifierFactory>();

            // Build the container.
            var container = builder.Build();

            // Create the depenedency resolver.
            var resolver = new AutofacWebApiDependencyResolver(container);

            // Configure Web API with the dependency resolver.
            GlobalConfiguration.Configuration.DependencyResolver = resolver;

            //Setup log4net
            XmlConfigurator.Configure();
        }
    }
}
