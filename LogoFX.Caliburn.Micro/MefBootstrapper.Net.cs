// Partial Copyright (c) LogoUI Software Solutions LTD
// Author: Vladislav Spivak
// This source file is the part of LogoFX Framework http://logofx.codeplex.com
// See accompanying licences and credits.


using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using AttributedModelServices = System.ComponentModel.Composition.AttributedModelServices;

namespace Caliburn.Micro
{
    /// <summary>
    /// Bootstrapper with MEF as <c>IoC</c>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TModule">modules type</typeparam>
    public class MefBootstrapper<T,TModule> : BootstrapperBase<T>
    {
        private CompositionContainer _mefContainer;


        /// <summary>
        /// Override to configure the framework and setup your <c>IoC</c> container.
        /// </summary>
        protected sealed override void Configure()
        {            
            _mefContainer = new CompositionContainer(
                         new AggregateCatalog(
                         AssemblySource.Instance
                        .Select(x => new AssemblyCatalog(x))
                        .OfType<System.ComponentModel.Composition.Primitives.ComposablePartCatalog>()));

            var batch = new CompositionBatch();
            batch.AddExportedValue<IWindowManager>(new WindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            batch.AddExportedValue(_mefContainer);
            
            OnConfigure(batch);

            _mefContainer.Compose(batch);

            OnEndConfigure(_mefContainer);

        }

        /// <summary>
        /// Called when configure ends.
        /// </summary>
        /// <param name="container">The container.</param>
        protected virtual void OnEndConfigure(CompositionContainer container)
        {
            
        }

        /// <summary>
        /// Called when configure end default pass.
        /// </summary>
        /// <param name="compositionBatch">The composition batch.</param>
        protected virtual void OnConfigure(CompositionBatch compositionBatch)
        {
            
        }

        /// <summary>
        /// Override this to provide an <c>IoC</c> specific implementation.
        /// </summary>
        /// <param name="serviceType">The service to locate.</param>
        /// <param name="key">The key to locate.</param>
        /// <returns>The located service.</returns>
        protected override object GetInstance(Type serviceType, string key)
        {
            string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
            var exports = _mefContainer.GetExportedValues<object>(contract);

            if (exports.Count() > 0)
                return exports.First();

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }


        /// <summary>
        /// Override this to provide an <c>IoC</c> specific implementation
        /// </summary>
        /// <param name="serviceType">The service to locate.</param>
        /// <returns>The located services.</returns>
        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _mefContainer.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
        }

        /// <summary>
        /// Override this to provide an <c>IoC</c> specific implementation.
        /// </summary>
        /// <param name="instance">The instance to perform injection on.</param>
        protected override void BuildUp(object instance)
        {
            _mefContainer.SatisfyImportsOnce(instance);
        }

        /// <summary>
        /// Override to tell the framework where to find assemblies to inspect for views, etc.
        /// </summary>
        /// <returns>A list of assemblies to inspect.</returns>
        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            if (!Directory.Exists(ModulesPath))
                Directory.CreateDirectory(ModulesPath);

            CompositionHost.Initialize(new DirectoryCatalog(ModulesPath));
            CompositionInitializer.SatisfyImports(this);

            IEnumerable<Assembly> local = 
                GetType().Assembly
                .GetReferencedAssemblies()
                .Select(Assembly.Load)
                .Concat(new[] { GetType().Assembly }).Distinct();
            if (Modules != null)
            {
                return local.Concat(Modules.Select(a => a.GetType().Assembly)).Distinct();
            }
            return local;
        }


        /// <summary>
        /// Gets or sets the modules.
        /// </summary>
        /// <value>The modules.</value>
        [ImportMany]
        public IEnumerable<TModule> Modules { get; set; } 

        /// <summary>
        /// Gets the modules path that MEF need to search.
        /// </summary>
        /// <value>The modules path.</value>
        public string ModulesPath
        {
            get { return "."; }
        }

    }
}
