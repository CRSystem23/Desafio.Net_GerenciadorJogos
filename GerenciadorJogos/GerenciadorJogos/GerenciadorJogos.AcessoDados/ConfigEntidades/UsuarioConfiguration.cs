using GerenciadorJogos.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorJogos.AcessoDados.ConfigEntidades
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.Id);

            entityTypeBuilder.Property(x => x.Login)
                 .IsRequired();

            entityTypeBuilder.Property(x => x.Senha)
                 .IsRequired();

            entityTypeBuilder.Property(x => x.PessoaId)
                   .IsRequired();

        }
    }
}
