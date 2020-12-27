using System.Collections.Generic;

namespace GerenciadorJogos.Dominio.Entidades
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public string Endereco { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }

        public virtual ICollection<ControleEmprestimoJogo> ControleEmprestimoJogos { get; set; }
    }
}
