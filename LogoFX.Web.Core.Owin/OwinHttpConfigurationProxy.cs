using System.Web.Http;

namespace LogoFX.Web.Core.Owin
{
    public class OwinHttpConfigurationProxy : IHttpConfigurationProxy
    {
        private readonly IHttpConfigurationProxy _decorated;

        public OwinHttpConfigurationProxy(IHttpConfigurationProxy httpConfigurationProxy)
        {
            _decorated = httpConfigurationProxy;
        }        

        public IHttpConfigurationProxy MapHttpRoutes()
        {
            return _decorated.MapHttpRoutes();
        }

        public IHttpConfigurationProxy ReplaceService<TService>(TService service)
        {
            return _decorated.ReplaceService(service);
        }

        public HttpConfiguration HttpConfiguration
        {
            get { return _decorated.HttpConfiguration; }
        }
    }
}