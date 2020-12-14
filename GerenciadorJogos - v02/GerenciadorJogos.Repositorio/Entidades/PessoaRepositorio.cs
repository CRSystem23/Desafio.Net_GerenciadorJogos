using GerenciadorJogos.AcessoDados.Contexto;
using GerenciadorJogos.Dominio.Entidades;
using GerenciadorJogos.Generico.Repositorio;

namespace GerenciadorJogos.Repositorio.Entidades
{
    public class PessoaRepositorio : RepositorioGenerico<Pessoa, int>
    {
        protected GerenciadorJogosDbContext _contexto;

        public PessoaRepositorio(GerenciadorJogosDbContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }
    }
}
