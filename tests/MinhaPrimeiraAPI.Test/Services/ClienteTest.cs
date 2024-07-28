using Bogus;
using MinhaPrimeiraAPI.Domain.Entities;
using MinhaPrimeiraAPI.Domain.Interfaces.Repositories;
using MinhaPrimeiraAPI.Domain.Validators.Entities;
using Moq;
using FluentAssertions;
using MinhaPrimeiraAPI.Domain.Dto.Clientes;
using MinhaPrimeiraAPI.Domain.Notifications;
using MinhaPrimeiraAPI.Domain.Services;

namespace MinhaPrimeiraAPI.Test.Services
{
    public static class ClienteFixture
    {
        #region Public Constructors

        static ClienteFixture()
        {
            ListaClientes = new List<object[]>();

            var clientes = new Faker<ClienteCadastrarDto>("pt_BR")
              .CustomInstantiator(c =>
                  new ClienteCadastrarDto
                  {
                      Email = c.Internet.Email(),
                      Celular = c.Phone.PhoneNumber("##########"),
                      Nome = c.Name.FullName()
                  }
              ).Generate(10);

            var idsClientes = clientes.Select(x => Guid.NewGuid()).ToList();
            
            idsClientes.ForEach(x => Guid.NewGuid());

            ListaClientes = clientes.Select(u => new object[] { u }).ToList();
            IdsClientes = idsClientes.Select(u => new object[] { u }).ToList();
        }

        #endregion Public Constructors

        #region Public Properties

        public static IList<object[]> IdsClientes { get; }
        public static IList<object[]> ListaClientes { get; }

        #endregion Public Properties
    }

    public class ClienteTest
    {
        #region Public Methods

        [Theory(DisplayName = "Novo cliente sucesso")]
        [Trait(nameof(Cliente), "Teste de unidade")]
        [MemberData(nameof(ClienteFixture.ListaClientes), MemberType = typeof(ClienteFixture))]
        public async Task Novo_Cliente_Sucesso(ClienteCadastrarDto clienteCadastrarDto)
        {
            //Arrange
            var cliente = new Cliente(clienteCadastrarDto.Nome, clienteCadastrarDto.Email,
                clienteCadastrarDto.Celular, Guid.NewGuid());

            var fluentValidator = new ClienteValidator(cliente);
            var erros = fluentValidator.ValidationResult.Errors.Select(x => x.ErrorMessage);
            erros.Should().BeEmpty()
                          .And.NotContain(fluentValidator.MensagemIdInvalido)
                          .And.NotContain(fluentValidator.MensagemNomeInvalido)
                          .And.NotContain(fluentValidator.MensagemEmailInvalido)
                          .And.NotContain(fluentValidator.MensagemCelularMaximoCaracteres);

            var notification = new Notification();

            var clienteRepository = new Mock<IClienteRepository>();
            var clienteService = new ClienteService(notification, clienteRepository.Object);
            clienteRepository.Setup(s => s.CreateAsync(cliente));

            //Act
            await clienteService.AdicionarAsync(clienteCadastrarDto);

            //Assert
            await clienteRepository.Object.CreateAsync(cliente);
            clienteRepository.Verify(v => v.CreateAsync(cliente), Times.Once);
        }

        [Theory(DisplayName = "Obter cliente sucesso")]
        [Trait(nameof(Cliente), "Teste de unidade")]
        [MemberData(nameof(ClienteFixture.IdsClientes), MemberType = typeof(ClienteFixture))]
        public async Task Obter_Cliente_Por_Id_Sucesso(Guid id)
        {
            Assert.False(id.Equals(Guid.Empty));
            var notification = new Notification();

            var clienteRepository = new Mock<IClienteRepository>();
            var clienteService = new ClienteService(notification, clienteRepository.Object);
            clienteRepository.Setup(s => s.GetByIdAsync(id));

            //Act
            await clienteService.ObterPorIdAsync(id);

            //Assert
            clienteRepository.Verify(v => v.GetByIdAsync(id), Times.Once);
        }

        #endregion Public Methods
    }
}