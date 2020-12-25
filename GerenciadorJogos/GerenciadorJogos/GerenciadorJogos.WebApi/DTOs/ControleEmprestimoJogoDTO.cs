using System;

namespace GerenciadorJogos.WebApi.DTOs
{
    public class ControleEmprestimoJogoDTO
    {
        public int Id { get; set; }
        public int PessoaId { get; set; }
        public string PessoaNome { get; set; }
        public int JogoId { get; set; }
        public string JogoNome { get; set; }
        public DateTime? DataEmprestimo { get; set; }
        public DateTime? DataDevolucao { get; set; }
    }
}
