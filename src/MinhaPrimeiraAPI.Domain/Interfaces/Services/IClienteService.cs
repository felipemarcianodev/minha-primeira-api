using MinhaPrimeiraAPI.Domain.Dto.Clientes;
using System.Threading.Tasks;

namespace MinhaPrimeiraAPI.Domain.Interfaces.Services
{
    public interface IClienteService
    {
        #region Public Methods

        Task AdicionarAsync(ClienteCadastrarDto clienteCadastrarDto);

        #endregion Public Methods
    }
}