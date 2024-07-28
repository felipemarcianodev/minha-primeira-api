using Bogus;
using MinhaPrimeiraAPI.Domain.Entities;
using MinhaPrimeiraAPI.Domain.Interfaces.Repositories;
using MinhaPrimeiraAPI.Domain.Validators.Entities;
using Moq;
using FluentAssertions;
using MinhaPrimeiraAPI.Domain.Dto.Usuarios;
using MinhaPrimeiraAPI.Domain.Notifications;
using MinhaPrimeiraAPI.Domain.Services;

namespace MinhaPrimeiraAPI.Test.Services
{
    public static class UsuarioFixture
    {
        public static IList<object[]> ListaUsuarios { get; }
        public static IList<object[]> IdsUsuarios { get; }
        static UsuarioFixture()
        {
            ListaUsuarios = new List<object[]>();

            var usuarios = new Faker<UsuarioCadastrarDto>("pt_BR")
             .CustomInstantiator(c =>
                 new UsuarioCadastrarDto
                 {
                     Nome = c.Name.FullName(),
                     Email = c.Internet.Email(),
                     Senha = c.Internet.Password(),
                 }
             ).Generate(10);

            var idsUsuarios = usuarios.Select(x => Guid.NewGuid()).ToList();

            ListaUsuarios = usuarios.Select(u => new object[] { u }).ToList();
            IdsUsuarios = idsUsuarios.Select(u => new object[] { u }).ToList();
        }
    }

    public class UsuarioTest
    {
        #region Public Methods

        [Theory(DisplayName = "Novo usuário sucesso")]
        [Trait(nameof(Usuario), "Teste de unidade")]
        [MemberData(nameof(UsuarioFixture.ListaUsuarios), MemberType = typeof(UsuarioFixture))]
        public async Task Novo_Usuario_Sucesso(UsuarioCadastrarDto usuarioCadastrarDto)
        {
            //Arrange
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

        [Theory(DisplayName = "Obter usuário sucesso")]
        [Trait(nameof(Usuario), "Teste de unidade")]
        [MemberData(nameof(UsuarioFixture.IdsUsuarios), MemberType = typeof(UsuarioFixture))]
        public async Task Obter_Usuario_Por_Id_Sucesso(Guid id)
        {
            Assert.False(id.Equals(Guid.Empty));
            var notification = new Notification();

            var usuarioRepository = new Mock<IUsuarioRepository>();
            var usuarioService = new UsuarioService(notification, usuarioRepository.Object);
            usuarioRepository.Setup(s => s.GetByIdAsync(id));

            //Act
            await usuarioService.ObterPorIdAsync(id);

            //Assert
            usuarioRepository.Verify(v => v.GetByIdAsync(id), Times.Once);
        }
        #endregion Public Methods
    }
}