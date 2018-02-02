using System.Configuration;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using ProductManager.Data;
using ProductManager.Data.Repositories;
using ProductManager.Service;
using ProductManager.WebApi.Mappings;

namespace ProductManager.WebApi
{
    public static class Bootstrapper
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

        public static void Run()
        {
            // Initialize AutoFac
            SetAutofacContainer();

            // Configure AutoMapper
            AutoMapperConfiguration.Configure();
        }

        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();

            // Register all Web API controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<ProductManagerContext>()
                .As<IProductManagerContext>()
                .WithParameter("connectionString", ConnectionString)
                .InstancePerRequest();

            // Repositories
            builder.Register<IProductRepository>(context =>
            {
                return context.Resolve<IProductManagerContext>().ProductRepository;
            }).InstancePerRequest();
            builder.Register<IProductCategoryRepository>(context =>
            {
                return context.Resolve<IProductManagerContext>().ProductCategoryRepository;
            }).InstancePerRequest();
            builder.Register<IProductSubCategoryRepository>(context =>
            {
                return context.Resolve<IProductManagerContext>().ProductSubCategoryRepository;
            }).InstancePerRequest();

            // Services
            builder.RegisterAssemblyTypes(typeof(ProductService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(ProductCategoryService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(ProductSubCategoryService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerRequest();

            IContainer container = builder.Build();

            // Get HttpConfiguration
            var config = GlobalConfiguration.Configuration;
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}