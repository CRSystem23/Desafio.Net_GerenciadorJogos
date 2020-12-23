using System.Collections.Generic;

namespace GerenciadorJogos.Dominio.Entidades
{
    public class Jogo
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<ControleEmprestimoJogo> ControleEmprestimoJogos { get; set; }
    }
}
