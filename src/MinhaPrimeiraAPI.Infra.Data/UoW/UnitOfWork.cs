using MinhaPrimeiraAPI.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaPrimeiraAPI.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Public Constructors

        public UnitOfWork(MinhaPrimeiraApiContext minhaPrimeiraApiContext)
        {
            Context = minhaPrimeiraApiContext;
        }

        #endregion Public Constructors

        #region Public Properties

        public MinhaPrimeiraApiContext Context { get; }

        #endregion Public Properties

        #region Public Methods

        public async Task BeginTransactionAsync()
        {
            if (Context.Database.CurrentTransaction is null)
            {
                await Context.Database.BeginTransactionAsync();
            }
        }

        public async Task CommitAsync()
        {
            if (Context.Database.CurrentTransaction is null)
                return;

            await Context.Database.CommitTransactionAsync();
        }

        public async Task RollbackAsync()
        {
            if (Context.Database.CurrentTransaction is null)
                return;

            await Context.Database.RollbackTransactionAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync();
        }

        #endregion Public Methods
    }
}