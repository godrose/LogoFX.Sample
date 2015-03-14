using LogoFX.Practices.IoC;

namespace LogoFX.Practices.Composition.Web
{
    public class BootstrapperInitializationFacade : BootstrapperInitializationFacadeBase
    {
        public BootstrapperInitializationFacade(IIocContainer iocContainer) : base(iocContainer)
        {
        }

        protected override IAssembliesReadOnlyResolver CreateAssembliesResolver()
        {
            return new AssembliesResolver(CompositionContainer);
        }
    }
}
