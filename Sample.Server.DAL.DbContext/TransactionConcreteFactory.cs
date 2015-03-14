using LogoFX.DAL.DbContext;

namespace Sample.Server.DAL.DbContext
{
    public class TransactionConcreteFactory : TransactionAbstractFactory<AppDbContext>
    {
        private readonly IDbContextFactory _dbContextFactory;

        public TransactionConcreteFactory(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        protected override AppDbContext CreateDbContext()
        {
            return _dbContextFactory.CreateDbContext<AppDbContext>();
        }
    }
}
