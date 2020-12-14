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
    }
}
