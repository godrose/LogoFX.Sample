using Microsoft.Owin.Diagnostics;
using Owin;

namespace LogoFX.Web.Core.Owin
{    
    public class AppBuilderProxy : IAppBuilderProxy
    {
        private readonly IAppBuilder _appBuilder;

        public AppBuilderProxy(IAppBuilder appBuilder)
        {
            _appBuilder = appBuilder;
        }

        public IAppBuilderProxy UseWebApi(IHttpConfigurationProxy httpConfigurationProxy)
        {
            _appBuilder.UseWebApi(httpConfigurationProxy.HttpConfiguration);
            return this;
        }

        public IAppBuilderProxy UseOAuth()
        {
            throw new System.NotImplementedException();
        }

        public IAppBuilderProxy UseErrorPage()
        {
            _appBuilder.UseErrorPage(new ErrorPageOptions
            {
                ShowCookies = true,
                ShowEnvironment = true,
                ShowExceptionDetails = true,
                ShowHeaders = true,
                ShowQuery = true,
                ShowSourceCode = true,
                SourceCodeLineCount = 7
            });
            return this;
        }

        public IAppBuilderProxy UseCors()
        {
            _appBuilder.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            return this;
        }
    }
}