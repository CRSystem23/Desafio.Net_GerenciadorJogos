using GerenciadorJogos.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorJogos.AcessoDados.ConfigEntidades
{
    public class JogoConfiguration : IEntityTypeConfiguration<Jogo>
    {
        public void Configure(EntityTypeBuilder<Jogo> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.Id);

            entityTypeBuilder.Property(x => x.Nome)
                 .IsRequired();

            entityTypeBuilder.HasMany(c => c.ControleEmprestimoJogos).WithOne(x => x.Jogo);
        }
    }
}
