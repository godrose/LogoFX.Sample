namespace LogoFX.Web.Core.Owin
{
    public interface IAppBuilderProxy
    {
        IAppBuilderProxy UseWebApi(IHttpConfigurationProxy httpConfigurationProxy);
        IAppBuilderProxy UseOAuth();
        IAppBuilderProxy UseErrorPage();
        IAppBuilderProxy UseCors();
    }
}