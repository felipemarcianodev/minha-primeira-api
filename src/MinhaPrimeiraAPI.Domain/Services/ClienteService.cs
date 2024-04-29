using MinhaPrimeiraAPI.Domain.Dto.Clientes;
using MinhaPrimeiraAPI.Domain.Entities;
using MinhaPrimeiraAPI.Domain.Interfaces.Repositories;
using MinhaPrimeiraAPI.Domain.Interfaces.Services;
using MinhaPrimeiraAPI.Domain.Notifications;
using MinhaPrimeiraAPI.Domain.Validators.Entities;
using System;
using System.Threading.Tasks;

namespace MinhaPrimeiraAPI.Domain.Services
{
    public class ClienteService : IClienteService
    {
        #region Private Fields

        private readonly IClienteRepository _clienteRepository;
        private readonly Notification _notification;

        #endregion Private Fields

        #region Public Constructors

        public ClienteService(
            Notification notification,
            IClienteRepository clienteRepository)
        {
            _notification = notification;
            _clienteRepository = clienteRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task AdicionarAsync(ClienteCadastrarDto clienteCadastrarDto)
        {
            if (clienteCadastrarDto is null)
            {
                _notification.AddNotifications("Os dados do formulário estão inválido!");
                return;
            }

            var clienteExiste = await _clienteRepository.VerificarDuplicidadeAsync(Guid.Empty, clienteCadastrarDto.Nome, clienteCadastrarDto.Email);
            if (clienteExiste)
            {
                _notification.AddNotifications("Já existe um usuário com esses esses dados!");
                return;
            }

            var cliente = new Cliente(clienteCadastrarDto.Nome, clienteCadastrarDto.Email, clienteCadastrarDto.Celular, Guid.NewGuid());
            var fluentValidator = new ClienteValidator(cliente);
            _notification.AddNotifications(fluentValidator.ValidationResult);

            if (!_notification.Success)
                return;

            await _clienteRepository.CreateAsync(cliente).ConfigureAwait(false);

            _notification.SetCreated("Cliente cadastrado com sucesso!");
        }

        #endregion Public Methods
    }
}