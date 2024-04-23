using System;
using System.Text;

namespace MinhaPrimeiraAPI.Domain.Entities
{
    public class Usuario
    {
        #region Public Constructors

        public Usuario(string nome, string email, string senha)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Email = email;
            Senha = EncryptPassword(senha);
            DataCadastro = DateTime.UtcNow;
        }

        #endregion Public Constructors

        #region Protected Constructors

        protected Usuario()
        {
        }

        #endregion Protected Constructors

        #region Public Properties

        public DateTime? DataAtualizacao { get; private set; }
        public DateTime DataCadastro { get; }
        public string Email { get; private set; }
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Senha { get; private set; }

        #endregion Public Properties

        #region Public Methods

        public virtual void Atualizar(string nome, string email)
        {
            Nome = nome;
            Email = email;
            DataAtualizacao = DateTime.UtcNow;
        }

        public virtual string EncryptPassword(string pass)
        {
            if (string.IsNullOrEmpty(pass)) return "";
            var password = (pass += "|8CCC0260-ED24-4E54-A4CD-EEECDCAFD33C");
            var md5 = System.Security.Cryptography.MD5.Create();
            var data = md5.ComputeHash(Encoding.Default.GetBytes(password));
            var sbString = new StringBuilder();
            foreach (var t in data)
                sbString.Append(t.ToString("x2"));

            return sbString.ToString();
        }

        #endregion Public Methods
    }
}