using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MinhaPrimeiraAPI.Domain.Entities;
using MinhaPrimeiraAPI.Infra.Data.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaPrimeiraAPI.Infra.Data.Context
{
    public class MinhaPrimeiraApiContext : DbContext
    {
        #region Public Constructors

        public MinhaPrimeiraApiContext(DbContextOptions<MinhaPrimeiraApiContext> options)
            : base(options)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.EnableDetailedErrors();
            optionsBuilder.UseLoggerFactory(LoggerFactory.Create((log) => log.AddConsole()));

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMapConfig());
            modelBuilder.ApplyConfiguration(new ClienteMapConfig());

            base.OnModelCreating(modelBuilder);
        }

        #endregion Protected Methods
    }
}