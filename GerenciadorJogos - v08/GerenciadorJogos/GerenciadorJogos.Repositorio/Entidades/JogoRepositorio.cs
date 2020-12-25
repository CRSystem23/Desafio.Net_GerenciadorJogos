using GerenciadorJogos.AcessoDados.Contexto;
using GerenciadorJogos.Dominio.Entidades;
using GerenciadorJogos.Generico.Repositorio;
using System.Collections.Generic;
using System.Linq;

namespace GerenciadorJogos.Repositorio.Entidades
{
    public class JogoRepositorio : RepositorioGenerico<Jogo, int>
    {
        protected GerenciadorJogosDbContext _contexto;
        public JogoRepositorio(GerenciadorJogosDbContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public IEnumerable<Jogo> SelecionarJogosComUltimoControleEmprestimo()
        {
            var jogos = _contexto.Jogo;

            foreach (var jogo in jogos)
            {
                ControleEmprestimoJogo teste = (from c in _contexto.ControleEmprestimoJogo where c.JogoId == jogo.Id select c).Max();
            }

            return jogos;
        }
    }
}
