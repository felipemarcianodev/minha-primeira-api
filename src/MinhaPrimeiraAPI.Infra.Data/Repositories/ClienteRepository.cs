using Dapper;
using MinhaPrimeiraAPI.Domain.Entities;
using MinhaPrimeiraAPI.Domain.Interfaces.Repositories;
using MinhaPrimeiraAPI.Infra.Data.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaPrimeiraAPI.Infra.Data.Repositories
{
    public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
    {
        #region Public Constructors

        public ClienteRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
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
                        from cliente_tb c
                        c.cliente_id <> @id and
                        (c.nome = @nome or
                        c.email = @email);");

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