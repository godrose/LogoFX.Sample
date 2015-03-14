using System.Net.Http;
using Caliburn.Micro;
using Sample.Client.Shell.Model;
using Solid.Practices.IoC;

namespace Sample.Client.Shell
{
    public class AppBootstrapper : SimpleInjectorBootstrapper<ShellViewModel>
    {
        protected override void OnConfigure(IIocContainer container)
        {
            base.OnConfigure(container);
            container.RegisterTransient<IDataService, DataService>();
            container.RegisterSingleton<IUserInteractionService, UserInteractionService>();
            container.RegisterSingleton<IServerAgent, ServerAgent>();
            container.RegisterSingleton<IFileUploader, HttpClientUploader>();
            container.RegisterInstance(new HttpClient());
        }
    }
}
