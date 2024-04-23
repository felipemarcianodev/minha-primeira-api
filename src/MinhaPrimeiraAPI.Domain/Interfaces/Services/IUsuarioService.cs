using MinhaPrimeiraAPI.Domain.Dto.Usuarios;
using System.Threading.Tasks;

namespace MinhaPrimeiraAPI.Domain.Interfaces.Services
{
    public interface IUsuarioService
    {
        #region Public Methods

        Task AdicionarAsync(UsuarioCadastrarDto usuarioCadastrarDto);

        #endregion Public Methods
    }
}