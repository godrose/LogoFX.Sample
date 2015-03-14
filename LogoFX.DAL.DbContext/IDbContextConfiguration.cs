using System.Data.Entity.Infrastructure;

namespace LogoFX.DAL.DbContext
{
    public interface IDbContextConfiguration
    {
        DbContextConfiguration ContextConfiguration { get; }
    }    
}
