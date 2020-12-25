using GerenciadorJogos.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorJogos.AcessoDados.ConfigEntidades
{
    public class ControleEmprestimoJogoConfiguration : IEntityTypeConfiguration<ControleEmprestimoJogo>
    {
        public void Configure(EntityTypeBuilder<ControleEmprestimoJogo> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.Id);

            entityTypeBuilder.Property(x => x.PessoaId)
                 .IsRequired();

            entityTypeBuilder.Property(x => x.JogoId)
                 .IsRequired();

            entityTypeBuilder.Property(x => x.DataEmprestimo)
                 .IsRequired();

            entityTypeBuilder.Property(x => x.Pessoa).IsRequired();
            entityTypeBuilder.HasOne(x => x.Pessoa).WithMany(x => x.ControleEmprestimoJogos).HasForeignKey(x => x.PessoaId);
            entityTypeBuilder.Property(x => x.Jogo).IsRequired();
            entityTypeBuilder.HasOne(x => x.Jogo).WithMany(x => x.ControleEmprestimoJogos).HasForeignKey(x => x.JogoId);
        }
    }
}
