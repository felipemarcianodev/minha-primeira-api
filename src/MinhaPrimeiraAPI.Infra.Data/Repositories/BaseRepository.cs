using Microsoft.EntityFrameworkCore;
using MinhaPrimeiraAPI.Domain.Interfaces.Repositories;
using MinhaPrimeiraAPI.Infra.Data.UoW;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhaPrimeiraAPI.Infra.Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class
    {
        #region Private Fields

        private bool _disposedValue;

        #endregion Private Fields

        #region Protected Constructors

        protected BaseRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        #endregion Protected Constructors

        #region Public Properties

        public IUnitOfWork UnitOfWork { get; }

        #endregion Public Properties

        #region Public Methods

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            UnitOfWork.Context.Attach(entity).State = EntityState.Added;
            var obj = await UnitOfWork.Context.Set<TEntity>().AddAsync(entity);
            await UnitOfWork.SaveChangesAsync();

            return obj.Entity;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var obj = await UnitOfWork.Context.Set<TEntity>().AsNoTracking().ToListAsync();

            return obj;
        }

        public async Task<TEntity> GetByIdAsync<TKey>(TKey id) where TKey : IEquatable<TKey>
        {
            var obj = await UnitOfWork.Context.Set<TEntity>().FindAsync(id);

            return obj;
        }

        public async Task<int> RemoveAsync(TEntity entity)
        {
            UnitOfWork.Context.Set<TEntity>().Remove(entity);

            return await UnitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            UnitOfWork.Context.Attach(entity).State = EntityState.Modified;
            UnitOfWork.Context.Set<TEntity>().Update(entity);
            await UnitOfWork.SaveChangesAsync();
        }

        #endregion Public Methods

        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    UnitOfWork.Context?.Dispose();
                }

                _disposedValue = true;
            }
        }

        #endregion Protected Methods

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~BaseRepository()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }
    }
}