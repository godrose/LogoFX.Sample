using System.ComponentModel.Composition;
using Groopfie.Storage;
using Groopfie.Storage.Azure;
using LogoFX.DAL.DbContext;
using LogoFX.DAL.Repository;
using Sample.Server.DAL.DbContext;
using Solid.Practices.Composition;
using Solid.Practices.IoC;

namespace Sample.Server.Composition
{
    [Export(typeof(ICompositionModule))]
    class Module : ICompositionModule
    {
        public void RegisterModule(IIocContainer container)
        {
            container.RegisterSingleton<IUnitOfWork, UnitOfWork>();
            container.RegisterSingleton<IDbContext, AppDbContext>();
            container.RegisterTransient<ITransactionFactory, TransactionConcreteFactory>();
            container.RegisterTransient<IDbContextFactory, DbContextFactory>();
            container.RegisterSingleton<IStorageStreamProvider, StorageStreamProvider>();
        }
    }
}
