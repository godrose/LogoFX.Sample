// Partial Copyright (c) LogoUI Software Solutions LTD
// Author: Vladislav Spivak
// This source file is the part of LogoFX Framework http://logofx.codeplex.com
// See accompanying licences and credits.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Threading;
using LogoFX.Practices.IoC.SimpleInjector;
using Solid.Practices.Composition.Desktop;
using Solid.Practices.IoC;

namespace Caliburn.Micro
{
    /// <summary>
    /// Bootstrapper that uses simple container
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SimpleInjectorBootstrapper<T> : BootstrapperBase<T> where T : class
    {        
        private IServiceLocator _serviceLocator;
        private object _defaultLifetimeScope;
        private SimpleInjectorContainer _iocContainer;

        /// <summary>
        /// Configures this instance.
        /// </summary>
        protected override void Configure()
        {            
            base.Configure();            
            _serviceLocator = _iocContainer;

            RegisterCommon(_iocContainer);
            RegisterViewsAndViewModels(_iocContainer);
            OnConfigure(_iocContainer);            
        }

        private void RegisterCommon(IIocContainer iocContainer)
        {
            iocContainer.RegisterSingleton<IWindowManager, WindowManager>();
            iocContainer.RegisterSingleton<T, T>();
            iocContainer.RegisterInstance(iocContainer);
        }

        /// <summary>
        /// Called on configure.
        /// </summary>
        /// <param name="container">The container.</param>
        protected virtual void OnConfigure(IIocContainer container)
        {
        }

        protected override void StartRuntime()
        {
            base.StartRuntime();
            Dispatch.InitializeDispatch();
        }

        #region Overrides
        /// <summary>
        /// Override this to provide an <c>IoC</c> specific implementation.
        /// </summary>
        /// <param name="service">The service to locate.</param>
        /// <param name="key">The key to locate.</param>
        /// <returns>The located service.</returns>
        protected override object GetInstance(Type service, string key)
        {
            return _serviceLocator.GetInstance(service);
        }


        /// <summary>
        /// Override this to provide an <c>IoC</c> specific implementation
        /// </summary>
        /// <param name="service">The service to locate.</param>
        /// <returns>The located services.</returns>
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _serviceLocator.GetAllInstances(service);
        }

        /// <summary>
        /// Override this to provide an <c>IoC</c> specific implementation.
        /// </summary>
        /// <param name="instance">The instance to perform injection on.</param>
        protected override void BuildUp(object instance)
        {
            _serviceLocator.BuildUp(instance);
        }

        /// <summary>
        /// Override to tell the framework where to find assemblies to inspect for views, etc.
        /// </summary>
        /// <returns>A list of assemblies to inspect.</returns>
        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            _iocContainer = new SimpleInjectorContainer();
            var initializationFacade = new BootstrapperInitializationFacade(GetType(), _iocContainer);
            initializationFacade.Initialize(ModulesPath);
            return initializationFacade.AssembliesResolver.GetAssemblies();
        }

        #endregion        

        /// <summary>
        /// Gets the modules path that MEF need to search.
        /// </summary>
        /// <value>The modules path.</value>
        public virtual string ModulesPath
        {
            get { return "."; }
        }

        #region private implementation
        private void RegisterViewsAndViewModels(IIocContainer iocContainer)
        {
            //  register view models
            AssemblySource.Instance.ToArray()
              .SelectMany(ass => ass.GetTypes())
                //  must be a type that ends with ViewModel
              .Where(type => type != typeof(T) && type.Name.EndsWith("ViewModel"))
                //  must be in a namespace ending with ViewModels
              .Where(type => !(string.IsNullOrWhiteSpace(type.Namespace)) && type.Namespace!=null && type.Namespace.EndsWith("ViewModels"))
                //  must implement INotifyPropertyChanged (deriving from PropertyChangedBase will statisfy this)
              .Where(type => type.GetInterface(typeof(INotifyPropertyChanged).Name, false) != null)
                //  registered as self
              .Apply(a => iocContainer.RegisterTransient(a, a));

            ////  register views
            //AssemblySource.Instance.ToArray()
            //  .SelectMany(ass => ass.GetTypes())
            //    //  must be a type that ends with View
            //  .Where(type => type.Name.EndsWith("View"))
            //    //  must be in a namespace ending with ViewModels
            //  .Where(type => !(string.IsNullOrWhiteSpace(type.Namespace)) && type.Namespace != null && type.Namespace.EndsWith("Views"))
            //    //  registered as self
            //  .Apply(a => _iocContainer.RegisterPerRequest(a, null, a));
            
        }
        #endregion
    }
}
