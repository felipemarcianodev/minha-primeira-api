namespace MinhaPrimeiraAPI.Domain.Dto.Usuarios
{
    public class UsuarioCadastrarDto
    {
        #region Public Properties
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string ConfirmacaoSenha { get; set; }

        #endregion Public Properties
    }
}