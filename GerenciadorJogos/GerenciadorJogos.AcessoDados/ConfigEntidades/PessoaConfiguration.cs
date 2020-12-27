using GerenciadorJogos.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorJogos.AcessoDados.ConfigEntidades
{
    public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.Id);

            entityTypeBuilder.Property(x => x.Nome)
                 .IsRequired();


            entityTypeBuilder.HasMany(c => c.ControleEmprestimoJogos).WithOne(x => x.Pessoa);
        }
    }
}
