using GerenciadorJogos.AcessoDados.Contexto;
using GerenciadorJogos.Dominio.Entidades;
using GerenciadorJogos.Generico.Repositorio;

namespace GerenciadorJogos.Repositorio.Entidades
{
    public class ControleEmprestimoJogoRepositorio : RepositorioGenerico<ControleEmprestimoJogo, int>
    {
        protected GerenciadorJogosDbContext _contexto;
        public ControleEmprestimoJogoRepositorio(GerenciadorJogosDbContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }
    }
}
