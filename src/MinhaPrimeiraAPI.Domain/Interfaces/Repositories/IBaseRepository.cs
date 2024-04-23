using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhaPrimeiraAPI.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> : IDisposable
        where TEntity : class
    {
        #region Public Methods

        Task<TEntity> CreateAsync(TEntity entity);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync<TKey>(TKey id) where TKey : IEquatable<TKey>;

        Task<int> RemoveAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        #endregion Public Methods
    }
}