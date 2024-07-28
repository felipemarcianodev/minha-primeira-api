using MinhaPrimeiraAPI.Domain.Dto.Usuarios;
using MinhaPrimeiraAPI.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace MinhaPrimeiraAPI.Domain.Interfaces.Services
{
    public interface IUsuarioService
    {
        #region Public Methods

        Task AdicionarAsync(UsuarioCadastrarDto usuarioCadastrarDto);

        Task<Usuario> ObterPorIdAsync(Guid id);

        #endregion Public Methods
    }
}