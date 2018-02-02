using System.Web.Http;

namespace ProductManager.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // Autofac and Automapper configurations
            Bootstrapper.Run();
        }
    }
}
