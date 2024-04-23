using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinhaPrimeiraAPI.Domain.Entities;

namespace MinhaPrimeiraAPI.Infra.Data.Configurations
{
    internal class UsuarioMapConfig : IEntityTypeConfiguration<Usuario>
    {
        public virtual void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("usuario_tb");

            builder.HasKey(x => x.Id)
                .HasName("PRIMARY");

            builder.Property(x => x.Id).HasColumnName("usuario_id");

            builder.Property(x => x.Nome)
                   .HasColumnName("nome")
                   .HasColumnType("varchar(100)");

            builder.Property(x => x.Email)
                   .HasColumnName("email")
                   .HasColumnType("varchar(100)");

            builder.Property(x => x.Senha)
                  .HasColumnName("senha")
                  .HasColumnType("varchar(1000)");

            builder.Property(x => x.DataCadastro)
                  .IsRequired()
                  .HasColumnName("dt_cadastro")
                  .HasColumnType("datetime");

            builder.Property(x => x.DataAtualizacao)
                  .HasColumnName("dt_atualizacao")
                  .HasColumnType("datetime");
        }
    }
}
