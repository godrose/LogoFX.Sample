using System;

namespace LogoFX.DAL.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
        IRepositoryBase<T> Repository<T>() where T : class;
    }
}