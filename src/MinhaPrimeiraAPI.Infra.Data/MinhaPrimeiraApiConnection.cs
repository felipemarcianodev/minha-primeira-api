using MinhaPrimeiraAPI.Domain;
using MySql.Data.MySqlClient;
using System;
using System.Data.Common;

namespace MinhaPrimeiraAPI.Infra.Data
{
    public class MinhaPrimeiraApiConnection : IDisposable
    {
        #region Private Fields

        private bool _disposedValue;

        #endregion Private Fields

        #region Public Properties

        public DbConnection Connection => new MySqlConnection(ApiConnection.ConnectionString);

        #endregion Public Properties

        #region Public Methods

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion Public Methods

        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    Connection?.Close();
                    Connection?.Dispose();
                }

                _disposedValue = true;
            }
        }

        #endregion Protected Methods

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~MinhaPrimeiraApiConnection()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }
    }
}