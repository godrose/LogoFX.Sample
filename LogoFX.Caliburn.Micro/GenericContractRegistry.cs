// Partial Copyright (c) LogoUI Software Solutions LTD
// Autor: Vladislav Spivak
// This source file is the part of LogoFX Framework http://logofx.codeplex.com
// See accompanying licences and credits.

#if SILVERLIGHT
using MefContrib.Hosting.Generics;
namespace Caliburn.Micro
{
    /// <summary>
    /// Support for Mef Generics
    /// </summary>
    public class GenericContractRegistry : GenericContractRegistryBase
    {
        protected override void Initialize()
        {            
            Register(typeof(IObservableCollection<>), typeof(BindableCollection<>));
        }
    }
}
#endif