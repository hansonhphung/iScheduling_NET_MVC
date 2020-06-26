using Autofac;
using Autofac.Integration.Mvc;
using iScheduling.Repositories.Context;
using iScheduling.Repositories.Implementation;
using iScheduling.Repositories.Interface;
using iScheduling.Services.Implementation;
using iScheduling.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace iScheduling.App_Start
{
    public class IoCConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            //Register dbcontext
            builder.Register(c => new iSchedulingContext()).InstancePerLifetimeScope();

            //Register Controller
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            //Regiser repositories/
            var repositoryAsssembly = typeof(BaseRepositories).Assembly;
            builder.RegisterAssemblyTypes(repositoryAsssembly)
                .Where(t => t.Namespace.Contains("iScheduling.Repositories.Implementation")
                || t.IsAssignableTo<IRepositories>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            //Regiser services/
            var serviceAsssembly = typeof(BaseServices).Assembly;
            builder.RegisterAssemblyTypes(serviceAsssembly)
                .Where(t => t.Namespace.Contains("iScheduling.Services.Implementation")
                || t.IsAssignableTo<IServices>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}