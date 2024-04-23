using System;

namespace MinhaPrimeiraAPI.Domain.Entities
{
    public class Cliente
    {
        public Cliente(string nome, string email, string celular, Guid usuarioCadastroId)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Email = email;
            UsuarioCadastroId = usuarioCadastroId;
            Celular = celular;
            DataCadastro = DateTime.UtcNow;
            Ativo = true;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Celular { get; private set; }
        public Guid UsuarioCadastroId { get; }
        public DateTime DataCadastro { get; }
        public DateTime? DataAtualizacao { get; private set; }
        public Guid? UsuarioAtualizacaoId { get; private set; }
        public bool Ativo { get; private set; }
    }
}
