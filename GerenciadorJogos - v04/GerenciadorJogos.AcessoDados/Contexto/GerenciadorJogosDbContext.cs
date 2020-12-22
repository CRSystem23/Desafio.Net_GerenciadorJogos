using GerenciadorJogos.AcessoDados.ConfigEntidades;
using GerenciadorJogos.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorJogos.AcessoDados.Contexto
{
    public class GerenciadorJogosDbContext : DbContext
    {
        public GerenciadorJogosDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Jogo> Jogo { get; set; }
        public DbSet<ControleEmprestimoJogo> ControleEmprestimoJogo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PessoaConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
        }
    }
}
