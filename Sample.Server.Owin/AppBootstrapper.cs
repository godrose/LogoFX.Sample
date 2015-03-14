using Groopfie.Storage.Azure;
using LogoFX.Web.Core.Owin;
using Solid.Practices.IoC;

namespace Sample.Server.Owin
{
    public class AppBootstrapper : OwinBootstrapper
    {
        public AppBootstrapper(IIocContainer iocContainer) : base(iocContainer)
        {
        }

        protected override void Configure()
        {
            base.Configure();
            new StorageInitializer().InitAsync();
        }
    }
}