namespace GerenciadorJogos.Dominio.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public int PessoaId { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public bool IsAdmin { get; set; }

        public virtual Pessoa Pessoa { get; set; }
    }
}
