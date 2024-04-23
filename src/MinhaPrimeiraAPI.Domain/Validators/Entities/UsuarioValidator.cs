using FluentValidation;
using FluentValidation.Results;
using MinhaPrimeiraAPI.Domain.Entities;
using System;

namespace MinhaPrimeiraAPI.Domain.Validators.Entities
{
    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        #region Public Fields

        public string MensagemDataAtualizacaoInvalida = "Data de atualização inválida!";
        public string MensagemDataCadastroInvalida = "Data de cadastro inválida!";
        public string MensagemEmailInvalido = "Informe no máximo 100 caracteres para o email do usuário!";
        public string MensagemEmailMaximoCaracteres = "Email inválido!";
        public string MensagemIdInvalido = "Id de usuário inválido!";
        public string MensagemNomeInvalido = "Nome de usuário inválido!";
        public string MensagemNomeMaximoCaracteres = "Informe no máximo 100 caracteres para o nome do usuário!";
        public string MensagemSenhaConfirmacaoNaoConferem = "As senhas não conferem!";
        public string MensagemSenhaInvalida = "Senha inválida!";
        public string MensagemSenhaMaximoCaracteres = "Quantidade de caracteres excedido para a senha!";

        #endregion Public Fields

        #region Public Constructors

        public UsuarioValidator(Usuario usuario, string confirmacaoSenha)
        {
            RuleFor(x => x.Id)
               .NotEmpty().WithMessage(MensagemIdInvalido);

            RuleFor(x => x.Nome)
               .NotEmpty().WithMessage(MensagemNomeInvalido)
               .MaximumLength(100).WithMessage(MensagemNomeMaximoCaracteres);

            RuleFor(x => x.Email)
               .NotEmpty().WithMessage(MensagemEmailInvalido)
               .EmailAddress().WithMessage(MensagemEmailInvalido)
               .MaximumLength(100).WithMessage(MensagemEmailMaximoCaracteres);

            RuleFor(x => x.Senha)
               .NotEmpty().WithMessage(MensagemSenhaInvalida)
               .Equal(confirmacaoSenha).WithMessage(MensagemSenhaConfirmacaoNaoConferem)
               .MaximumLength(1000).WithMessage(MensagemSenhaMaximoCaracteres);

            RuleFor(x => x.DataCadastro)
                .NotNull().WithMessage(MensagemDataCadastroInvalida);

            if(usuario.DataAtualizacao.HasValue)
            {
                RuleFor(x => x.DataAtualizacao)
                .NotNull().WithMessage(MensagemDataAtualizacaoInvalida);
            }

            ValidationResult = Validate(usuario);
        }

        #endregion Public Constructors

        #region Public Properties

        public ValidationResult ValidationResult { get; }

        #endregion Public Properties
    }
}