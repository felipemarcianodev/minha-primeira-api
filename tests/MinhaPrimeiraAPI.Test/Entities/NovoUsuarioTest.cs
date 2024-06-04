using Bogus;
using MinhaPrimeiraAPI.Domain.Entities;
using MinhaPrimeiraAPI.Domain.Interfaces.Repositories;
using MinhaPrimeiraAPI.Domain.Interfaces.Services;
using MinhaPrimeiraAPI.Domain.Validators.Entities;
using Moq;
using Moq.AutoMock;
using FluentAssertions;
using MinhaPrimeiraAPI.Domain.Dto.Usuarios;
using MinhaPrimeiraAPI.Domain.Notifications;
using MinhaPrimeiraAPI.Domain.Services;

namespace MinhaPrimeiraAPI.Test.Entities
{
    public class NovoUsuarioTest
    {
        #region Public Methods

        [Fact(DisplayName = "Novo usuario sucesso")]
        [Trait(nameof(Usuario), "Teste de unidade")]
        public async Task Novo_Usuario_Sucesso()
        {
            //Arrange
            var mocker = new AutoMocker();
            var usuarioCadastrarDto = new Faker<UsuarioCadastrarDto>("pt_BR")
                 .CustomInstantiator(c =>
                     new UsuarioCadastrarDto
                     {
                         Nome = c.Name.FullName(),
                         Email = c.Internet.Email(),
                         Senha = c.Internet.Password(),
                     }
                 ).Generate(10).First();

            var usuario = new Usuario(usuarioCadastrarDto.Nome, usuarioCadastrarDto.Email,
                usuarioCadastrarDto.Senha);

            usuarioCadastrarDto.ConfirmacaoSenha = usuario.EncryptPassword(usuarioCadastrarDto.Senha);

            var fluentValidator = new UsuarioValidator(usuario, usuarioCadastrarDto.ConfirmacaoSenha);
            var erros = fluentValidator.ValidationResult.Errors.Select(x => x.ErrorMessage);
            erros.Should().BeEmpty()
                          .And.NotContain(fluentValidator.MensagemIdInvalido)
                          .And.NotContain(fluentValidator.MensagemNomeInvalido)
                          .And.NotContain(fluentValidator.MensagemEmailInvalido)
                          .And.NotContain(fluentValidator.MensagemSenhaInvalida)
                          .And.NotContain(fluentValidator.MensagemSenhaMaximoCaracteres);

            var notification = new Notification();

            var usuarioRepository = new Mock<IUsuarioRepository>();
            var usuarioService = new UsuarioService(notification, usuarioRepository.Object);
            usuarioRepository.Setup(s => s.CreateAsync(usuario));

            //Act
            await usuarioService.AdicionarAsync(usuarioCadastrarDto);

            //Assert
            await usuarioRepository.Object.CreateAsync(usuario);
            usuarioRepository.Verify(v => v.CreateAsync(usuario), Times.Once);
        }

        #endregion Public Methods
    }
}