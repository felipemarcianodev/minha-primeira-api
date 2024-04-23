using FluentValidation;
using FluentValidation.Results;
using MinhaPrimeiraAPI.Domain.Entities;
using System;

namespace MinhaPrimeiraAPI.Domain.Validators.Entities
{
    public class ClienteValidator : AbstractValidator<Cliente>
    {
        #region Public Fields

        public string MensagemCelularMaximoCaracteres = "Informe no máximo 11 caracteres para o número do celular!";
        public string MensagemDataAtualizacaoInvalida = "Data de atualização inválida!";
        public string MensagemDataCadastroInvalida = "Data de cadastro inválida!";
        public string MensagemEmailInvalido = "Informe no máximo 100 caracteres para o email do cliente!";
        public string MensagemEmailMaximoCaracteres = "Email inválido!";
        public string MensagemIdInvalido = "Id de usuário inválido!";
        public string MensagemNomeInvalido = "Nome de cliente inválido!";
        public string MensagemNomeMaximoCaracteres = "Informe no máximo 100 caracteres para o nome do cliente!";
        public string MensagemUsuarioAtualizacaoInvalido = "Id de usuário inválido!";

        #endregion Public Fields

        #region Public Constructors

        public ClienteValidator(Cliente cliente)
        {
            RuleFor(x => x.Id)
              .NotEmpty().WithMessage(MensagemIdInvalido);

            RuleFor(x => x.Nome)
               .NotEmpty().WithMessage(MensagemNomeInvalido)
               .MaximumLength(100).WithMessage(MensagemNomeMaximoCaracteres);

            RuleFor(x => x.Celular)
               .MaximumLength(11).WithMessage(MensagemCelularMaximoCaracteres);

            RuleFor(x => x.Email)
               .NotEmpty().WithMessage(MensagemEmailInvalido)
               .EmailAddress().WithMessage(MensagemEmailInvalido)
               .MaximumLength(100).WithMessage(MensagemEmailMaximoCaracteres);

            RuleFor(x => x.DataCadastro)
                .NotNull().WithMessage(MensagemDataCadastroInvalida);

            if (cliente.UsuarioAtualizacaoId.HasValue)
            {
                RuleFor(x => x.DataAtualizacao)
                .NotNull().WithMessage(MensagemDataAtualizacaoInvalida);

                RuleFor(x => x.UsuarioAtualizacaoId)
                .NotNull().WithMessage(MensagemUsuarioAtualizacaoInvalido);
            }

            ValidationResult = Validate(cliente);
        }

        #endregion Public Constructors

        #region Public Properties

        public ValidationResult ValidationResult { get; }

        #endregion Public Properties
    }
}