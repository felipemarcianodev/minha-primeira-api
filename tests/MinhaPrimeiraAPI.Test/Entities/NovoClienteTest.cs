using Bogus;
using MinhaPrimeiraAPI.Domain.Entities;
using MinhaPrimeiraAPI.Domain.Interfaces.Repositories;
using MinhaPrimeiraAPI.Domain.Interfaces.Services;
using MinhaPrimeiraAPI.Domain.Validators.Entities;
using Moq;
using Moq.AutoMock;
using FluentAssertions;
using MinhaPrimeiraAPI.Domain.Dto.Clientes;
using MinhaPrimeiraAPI.Domain.Notifications;
using MinhaPrimeiraAPI.Domain.Services;

namespace MinhaPrimeiraAPI.Test.Entities
{
    public class NovoClienteTest
    {
        #region Public Methods

        [Fact(DisplayName = "Novo cliente sucesso")]
        [Trait(nameof(Cliente), "Teste de unidade")]
        public async Task Novo_Cliente_Sucesso()
        {
            //Arrange
            var mocker = new AutoMocker();
            var clienteCadastrarDto = new Faker<ClienteCadastrarDto>("pt_BR")
                 .CustomInstantiator(c =>
                     new ClienteCadastrarDto
                     {
                         Email = c.Internet.Email(),
                         Celular = c.Phone.PhoneNumber("##########"),
                         Nome = c.Name.FullName()
                     }
                 ).Generate(10).First();

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

        #endregion Public Methods
    }
}