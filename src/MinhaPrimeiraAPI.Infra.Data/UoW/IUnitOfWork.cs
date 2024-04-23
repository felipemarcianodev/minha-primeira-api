using MinhaPrimeiraAPI.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaPrimeiraAPI.Infra.Data.UoW
{
    public interface IUnitOfWork
    {
        #region Public Properties

        MinhaPrimeiraApiContext Context { get; }

        #endregion Public Properties

        #region Public Methods

        Task BeginTransactionAsync();

        Task CommitAsync();

        Task RollbackAsync();

        Task<int> SaveChangesAsync();

        #endregion Public Methods
    }
}