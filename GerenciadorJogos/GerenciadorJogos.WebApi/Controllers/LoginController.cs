using GerenciadorJogos.Dominio.Entidades;
using GerenciadorJogos.Generico.Interface;
using GerenciadorJogos.WebApi.DTOs;
using GerenciadorJogos.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GerenciadorJogos.WebApi.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IRepositorioGenerico<Usuario, int> _respositorioUsuarios;

        public LoginController(IRepositorioGenerico<Usuario, int> respositorioUsuarios)
        {
            _respositorioUsuarios = respositorioUsuarios;
        }

        [HttpGet]
        [Route("logar/{login}/{senha}")]
        public ActionResult<dynamic> Logar(string login, string senha)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(senha))
            {
                return BadRequest();
            }

            List<Usuario> usuarios = _respositorioUsuarios.Selecionar(s =>
                                                                      s.Login.ToLower() == login.ToLower() &&
                                                                      s.Senha == new UsuarioDTO().Retornar_Senha_Hash(senha));


            if (usuarios == null || usuarios.Count == 0)
                return NotFound(new { mensagem = "Usuário e/ou senha inválidos" });

            Usuario usuario = usuarios[0];
            var token = TokenService.GerarToken(usuario);

            usuario.Senha = "";
            string mensagem = "Ok";

            return new
            {
                usuario,
                mensagem,
                token
            };
        }
    }
}