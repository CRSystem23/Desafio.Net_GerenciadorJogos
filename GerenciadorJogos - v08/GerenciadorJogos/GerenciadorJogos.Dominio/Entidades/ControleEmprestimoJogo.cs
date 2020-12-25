using System;

namespace GerenciadorJogos.Dominio.Entidades
{
    public class ControleEmprestimoJogo
    {
        public int Id { get; set; }
        public int PessoaId { get; set; }
        public int JogoId { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime? DataDevolucao { get; set; }


        public virtual Pessoa Pessoa { get; set; }
        public virtual Jogo Jogo { get; set; }
    }
}
