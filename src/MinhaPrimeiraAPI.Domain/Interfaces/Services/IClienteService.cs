using MinhaPrimeiraAPI.Domain.Dto.Clientes;
using MinhaPrimeiraAPI.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace MinhaPrimeiraAPI.Domain.Interfaces.Services
{
    public interface IClienteService
    {
        #region Public Methods

        Task AdicionarAsync(ClienteCadastrarDto clienteCadastrarDto);

        Task<Cliente> ObterPorIdAsync(Guid id);

        #endregion Public Methods
    }
}