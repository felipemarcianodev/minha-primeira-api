using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinhaPrimeiraAPI.Domain.Entities;


namespace MinhaPrimeiraAPI.Infra.Data.Configurations
{
    internal class ClienteMapConfig : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("cliente_tb");

            builder.HasKey(x => x.Id)
                .HasName("PRIMARY");

            builder.Property(x => x.Id).HasColumnName("cliente_id");

            builder.Property(x => x.Nome)
                   .HasColumnName("nome")
                   .HasColumnType("varchar(100)");

            builder.Property(x => x.Email)
                   .IsRequired()
                   .HasColumnName("email")
                   .HasColumnType("varchar(100)");

            builder.Property(x => x.Celular)
                  .HasColumnName("celular")
                  .HasColumnType("varchar(11)");

            builder.Property(x => x.DataCadastro)
                  .IsRequired()
                  .HasColumnName("dt_cadastro")
                  .HasColumnType("datetime");

            builder.Property(x => x.DataAtualizacao)
                  .HasColumnName("dt_atualizacao")
                  .HasColumnType("datetime");

            builder.Property(x => x.UsuarioCadastroId)
                  .IsRequired()
                  .HasColumnName("usuario_cadastro_id");

            builder.Property(x => x.UsuarioAtualizacaoId)
                  .HasColumnName("usuario_atualizacao_id");
        }
    }
}
