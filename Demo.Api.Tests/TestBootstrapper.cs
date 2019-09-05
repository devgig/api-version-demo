using Autofac;
using Demo.Api.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Demo.Api.Tests
{
    public class TestBootstrapper
    {
        public IContainer Configure(ContainerBuilder builder)
        {
            //builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();

            var context = new DbContextOptionsBuilder<RentalDbContext>();
            context.UseInMemoryDatabase("RentalList");

            builder.RegisterType<RentalDbContext>().WithParameter("options", context.Options);

            var assemblies = Assembly.GetAssembly(typeof(Program));
            builder.RegisterAssemblyTypes(assemblies).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.Name.EndsWith("Controller"));
            

            return builder.Build();
        }

    }
}
