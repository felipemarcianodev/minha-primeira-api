using MinhaPrimeiraAPI.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace MinhaPrimeiraAPI.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        #region Public Methods

        Task<bool> VerificarDuplicidadeAsync(Guid id, string nome, string email);

        #endregion Public Methods
    }
}