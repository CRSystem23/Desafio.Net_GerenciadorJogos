using GerenciadorJogos.AcessoDados.Contexto;
using GerenciadorJogos.Dominio.Entidades;
using GerenciadorJogos.Generico.Repositorio;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GerenciadorJogos.Repositorio.Entidades
{
    public class UsuarioRepositorio : RepositorioGenerico<Usuario, int>
    {
        protected GerenciadorJogosDbContext _contexto;
        public UsuarioRepositorio(GerenciadorJogosDbContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public IEnumerable<Usuario> SelecionarComRelacionamento()
        {
            return _contexto.Usuario
                .Include(u => u.Pessoa)
                .ToList();
        }

        public Usuario SelecionarComRelacionamentoPorId(int? id)
        {
            _contexto.ChangeTracker.LazyLoadingEnabled = false;

            Usuario usuario = _contexto.Usuario.SingleOrDefault(b => b.Id == id);

            if (usuario == null)
            {
                return null;
            }

            _contexto.Entry(usuario)
                .Reference(b => b.Pessoa)
                .Load();

             return usuario;
        }
    }
}
