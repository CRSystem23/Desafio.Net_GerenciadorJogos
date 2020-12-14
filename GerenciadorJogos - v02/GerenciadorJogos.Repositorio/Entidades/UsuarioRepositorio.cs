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
    }
}
