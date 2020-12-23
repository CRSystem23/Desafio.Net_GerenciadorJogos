using GerenciadorJogos.Generico.Utilitario;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorJogos.WebApi.DTOs
{
    public class UsuarioDTO
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "O Login é obrigatório")]
        [StringLength(maximumLength: 20, MinimumLength = 5,
            ErrorMessage = "O login deve conter entre 5 e 20 caracteres")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Dados imcompletos")]
        public int PessoaId { get; set; }

        public string PessoaNome { get; set; }

        [Required(ErrorMessage = "A Senha é obrigatória")]
        [StringLength(maximumLength: 20, MinimumLength = 5,
            ErrorMessage = "A Senha deve conter entre 5 e 20 caracteres")]
        public string Senha { get; set; }
        public bool? IsAdmin { get; set; }


        public void Preencher_Propriedades_Para_Insercao()
        {
            Senha = this.Retornar_Senha_Hash(Senha);
            Login = Login.ToLower();
            PessoaId = PessoaId;
            IsAdmin = IsAdmin == null ? false : IsAdmin;
        }

        public void Preencher_Propriedades_Para_Alteracao()
        {
            Senha = CriptografiaHash.GerarHash(Senha);
            Login = Login.ToLower();
        }

        public string Retornar_Senha_Hash(string senha)
        {
            if (string.IsNullOrEmpty(senha))
            {
                return senha;
            }

            return CriptografiaHash.GerarHash(senha);
        }
    }
}
