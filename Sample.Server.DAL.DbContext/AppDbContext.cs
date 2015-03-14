using System.Data.Entity;
using LogoFX.DAL.DbContext;
using Sample.Server.DAL.Entities;

namespace Sample.Server.DAL.DbContext
{
    public class AppDbContext : System.Data.Entity.DbContext,IDbContext
    {
        public AppDbContext()
            : base("name=appEntities")
        {
            Database.SetInitializer<AppDbContext>(null);

        }
       public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
       {
           return base.Set<TEntity>();
       }

       protected override void OnModelCreating(DbModelBuilder modelBuilder)
       {
           modelBuilder.Entity<ImageEntity>().ToTable("Image");
           modelBuilder.Entity<UserEntity>().ToTable("Users");
       }
    }
}
