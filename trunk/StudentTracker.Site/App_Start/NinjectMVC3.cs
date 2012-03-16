using System.Data.Entity;
using StudentTracker.Mappings;
using StudentTracker.Models;
using StudentTracker.Repository;
using StudentTracker.Repository.Sql;
using StudentTracker.Services.Account;
using StudentTracker.Services.Appointment;
using StudentTracker.Services.Authentication;
using StudentTracker.Services.Core;

[assembly: WebActivator.PreApplicationStartMethod(typeof(StudentTracker.Site.App_Start.NinjectMVC3), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(StudentTracker.Site.App_Start.NinjectMVC3), "Stop")]

namespace StudentTracker.Site.App_Start {
    using System.Reflection;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Mvc;

    public static class NinjectMVC3 {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestModule));
            DynamicModuleUtility.RegisterModule(typeof(HttpApplicationInitializationModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop() {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel() {
            var kernel = new StandardKernel();
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel) {
            kernel.Bind<DbContext>().To<StudentTrackerDb>().InRequestScope();
            kernel.Bind<CoreService>().ToSelf();
            kernel.Bind<AccountService>().ToSelf();
            //  kernel.Bind<AppointmentService>().ToSelf();
            kernel.Bind<StudentTracker.Services.Authentication.RoleService>().ToSelf();
            kernel.Bind<StaticDataService>().ToSelf();
            kernel.Bind<Services.Student.StudentService>().ToSelf();
            kernel.Bind<Services.Teacher.TeacherService>().ToSelf();
            kernel.Bind<Services.User.UserService>().ToSelf();
            kernel.Bind<Service.Topic.TopicService>().ToSelf();
            kernel.Bind<Repository.IRepository<Student>>().To
                <StudentTracker.Repository.Sql.SqlRepository<Student>>();
            kernel.Bind<Repository.IRepository<Teacher>>().To
                <StudentTracker.Repository.Sql.SqlRepository<Teacher>>();
            kernel.Bind<Repository.IRepository<Course>>().To
                <StudentTracker.Repository.Sql.SqlRepository<Course>>();
            kernel.Bind<Repository.IRepository<Appointment>>().To
              <StudentTracker.Repository.Sql.SqlRepository<Appointment>>();
            kernel.Bind<Repository.IRepository<StudyCenter>>().To
                <StudentTracker.Repository.Sql.SqlRepository<StudyCenter>>();
            kernel.Bind<Repository.IRepository<Time>>().To
                <StudentTracker.Repository.Sql.SqlRepository<Time>>();
            kernel.Bind<Repository.IRepository<TimeSlot>>().To
                <StudentTracker.Repository.Sql.SqlRepository<TimeSlot>>();
            kernel.Bind<Repository.IRepository<User>>().To
                <StudentTracker.Repository.Sql.SqlRepository<User>>();
            kernel.Bind<ISqlUnitOfWork>().To<SqlUnitOfWork>();
            kernel.Bind<StudentTracker.Repository.IRepository<Topic>>().To
                <StudentTracker.Repository.Sql.SqlRepository<Topic>>();

            MapperExtensions.ConfigureMappings();
        }
    }
}
