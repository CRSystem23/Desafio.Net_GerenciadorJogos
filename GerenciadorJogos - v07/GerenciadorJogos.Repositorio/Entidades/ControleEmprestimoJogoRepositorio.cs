using GerenciadorJogos.AcessoDados.Contexto;
using GerenciadorJogos.Dominio.Entidades;
using GerenciadorJogos.Generico.Repositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GerenciadorJogos.Repositorio.Entidades
{
    public class ControleEmprestimoJogoRepositorio : RepositorioGenerico<ControleEmprestimoJogo, int>
    {
        protected GerenciadorJogosDbContext _contexto;
        public ControleEmprestimoJogoRepositorio(GerenciadorJogosDbContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public IEnumerable<ControleEmprestimoJogo> SelecionarComRelacionamento()
        {
            return _contexto.ControleEmprestimoJogo
                .Include(u => u.Pessoa).Include(u => u.Jogo)
                .ToList();
        }

        public IEnumerable<ControleEmprestimoJogo> SelecionarTodosJogosComUltimoControleEmprestimo()
        {
            var jogos = _contexto.Jogo;
            IEnumerable<ControleEmprestimoJogo> controleEmprestimoJogos = SelecionarComRelacionamento();
            var jogosEmprestados = new List<ControleEmprestimoJogo>();

            foreach (var jogo in jogos)
            {
                var controleEmprestimoJogo = controleEmprestimoJogos.OrderByDescending(c => c.Id)
                    .Where(c => c.JogoId == jogo.Id)
                    .Select(c => c)
                    .FirstOrDefault();

                if (controleEmprestimoJogo == null)
                {
                    controleEmprestimoJogo = new ControleEmprestimoJogo
                    {
                        Id = 0,
                        PessoaId = 0,
                        JogoId = jogo.Id,
                        Jogo = jogo
                    };
                }

                jogosEmprestados.Add(controleEmprestimoJogo);
            }
        
            return jogosEmprestados;
        }

        public ControleEmprestimoJogo SelecionarComRelacionamentoPorId(int? id)
        {
            _contexto.ChangeTracker.LazyLoadingEnabled = false;

            var controleEmprestimoJogo = _contexto.ControleEmprestimoJogo
                  .SingleOrDefault(b => b.Id == id);

            if (controleEmprestimoJogo == null)
            {
                return null;
            }

            _contexto.Entry(controleEmprestimoJogo)
                .Reference(b => b.Jogo)
                .Load();

            _contexto.Entry(controleEmprestimoJogo)
                .Reference(b => b.Pessoa)
                .Load();

            return controleEmprestimoJogo;
        }
    }
}
