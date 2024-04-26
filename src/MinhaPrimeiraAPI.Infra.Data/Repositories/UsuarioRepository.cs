using Dapper;
using MinhaPrimeiraAPI.Domain.Entities;
using MinhaPrimeiraAPI.Domain.Interfaces.Repositories;
using MinhaPrimeiraAPI.Infra.Data.UoW;
using System;
using System.Text;
using System.Threading.Tasks;

namespace MinhaPrimeiraAPI.Infra.Data.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        #region Public Constructors

        public UsuarioRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<bool> VerificarDuplicidadeAsync(Guid id, string nome, string email)
        {
            using (var conn = new MinhaPrimeiraApiConnection())
            {
                var query = new StringBuilder(@"
                        select 1
                        from usuario_tb u
                        u.usuario_id <> @id and
                        (u.nome = @nome or
                        u.email = @email);");

                return await conn.Connection.QueryFirstOrDefaultAsync<bool>(query.ToString(),
                    new
                    {
                        id,
                        nome,
                        email
                    });
            }
        }

        #endregion Public Methods
    }
}