using System.Reflection;
using LogoFX.Practices.IoC.SimpleInjector;
using LogoFX.Web.Core;
using LogoFX.Web.Core.Owin;
using Owin;
using Solid.Practices.IoC;

namespace Sample.Server.Owin
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            try
            {
                var iocContainer = new SimpleInjectorContainer();
                ServiceLocator.Current = iocContainer;
                IHttpConfigurationProvider bootstrapper = new AppBootstrapper(iocContainer);
                IHttpConfigurationProxy httpConfigurationProxy = new OwinHttpConfigurationProxy(bootstrapper.GetConfiguration());                
               
                IAppBuilderProxy appBuilderProxy = new AppBuilderProxy(appBuilder);
                appBuilderProxy.UseErrorPage().UseCors().UseWebApi(httpConfigurationProxy);
            }
            catch (ReflectionTypeLoadException loadException)
            {                
                throw loadException.LoaderExceptions[0];
            }            
        }
    }
}