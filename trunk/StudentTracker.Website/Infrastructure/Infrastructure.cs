using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using Ninject.Modules;
using StudentTracker.Service.Abstract;
using StudentTracker.Service.Concrete;

namespace StudentTracker.Website.Infrastructure {
    public class NinjectControllerFactory : DefaultControllerFactory {

        // A Ninject "kernel" is the thing that can supply object instances
        private readonly IKernel _kernel = new StandardKernel(new StudentTrackerServices());

        // ASP.NET MVC calls this to get the controller for each request
        protected override IController GetControllerInstance(RequestContext context,
        Type controllerType) {
            if (controllerType == null)
                return null;
            return (IController)_kernel.Get(controllerType);
        }

        // Configures how abstract service types are mapped to concrete implementations
        private class StudentTrackerServices : NinjectModule {
            public override void Load() {
                //                Bind<IUserService>()
                //.To<SqlProductsRepository>()
                //.WithConstructorArgument("connectionString",
                //ConfigurationManager.ConnectionStrings["AppDb"].ConnectionString
                //);
                Bind<IUserService>().To<UserService>();
                Bind<IStudentService>().To<StudentService>();

            }
        }
    }
}