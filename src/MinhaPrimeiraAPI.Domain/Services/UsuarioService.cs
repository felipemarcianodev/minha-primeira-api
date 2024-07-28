using MinhaPrimeiraAPI.Domain.Dto.Usuarios;
using MinhaPrimeiraAPI.Domain.Entities;
using MinhaPrimeiraAPI.Domain.Interfaces.Repositories;
using MinhaPrimeiraAPI.Domain.Interfaces.Services;
using MinhaPrimeiraAPI.Domain.Notifications;
using MinhaPrimeiraAPI.Domain.Validators.Entities;
using System;
using System.Threading.Tasks;

namespace MinhaPrimeiraAPI.Domain.Services
{
    public class UsuarioService : IUsuarioService
    {
        #region Private Fields

        private readonly Notification _notification;
        private readonly IUsuarioRepository _usuarioRepository;

        #endregion Private Fields

        #region Public Constructors

        public UsuarioService(
            Notification notification,
            IUsuarioRepository usuarioRepository)
        {
            _notification = notification;
            _usuarioRepository = usuarioRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task AdicionarAsync(UsuarioCadastrarDto usuarioCadastrarDto)
        {
            if (usuarioCadastrarDto is null)
            {
                _notification.AddNotifications("Os dados do formulário estão inválido!");
                return;
            }

            var usuarioExiste = await _usuarioRepository.VerificarDuplicidadeAsync(Guid.Empty, usuarioCadastrarDto.Nome, usuarioCadastrarDto.Email);
            if (usuarioExiste)
            {
                _notification.AddNotifications("Já existe um usuário com esses esses dados!");
                return;
            }

            var usuario = new Usuario(usuarioCadastrarDto.Nome, usuarioCadastrarDto.Email, usuarioCadastrarDto.Senha);
            var fluentValidator = new UsuarioValidator(usuario, usuarioCadastrarDto.ConfirmacaoSenha);
            _notification.AddNotifications(fluentValidator.ValidationResult);

            if (!_notification.Success)
                return;

            await _usuarioRepository.CreateAsync(usuario).ConfigureAwait(false);

            _notification.SetCreated("Usuário cadastrado com sucesso!");
        }

        public async Task<Usuario> ObterPorIdAsync(Guid id)
        {
            if (id.Equals(Guid.Empty))
            {
                _notification.AddNotifications("Informe o id pra consultar o usuário!");
                return await Task.FromResult<Usuario>(null);
            }
            return await _usuarioRepository.GetByIdAsync(id);
        }

        #endregion Public Methods
    }
}