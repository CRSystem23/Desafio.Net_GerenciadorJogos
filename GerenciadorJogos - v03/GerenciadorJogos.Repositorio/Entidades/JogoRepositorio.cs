using GerenciadorJogos.AcessoDados.Contexto;
using GerenciadorJogos.Dominio.Entidades;
using GerenciadorJogos.Generico.Repositorio;

namespace GerenciadorJogos.Repositorio.Entidades
{
    public class JogoRepositorio : RepositorioGenerico<Jogo, int>
    {
        protected GerenciadorJogosDbContext _contexto;
        public JogoRepositorio(GerenciadorJogosDbContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }
    }
}
