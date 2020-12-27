using GerenciadorJogos.Dominio.Entidades;
using GerenciadorJogos.Generico.Interface;
using GerenciadorJogos.Repositorio.Entidades;
using GerenciadorJogos.WebApi.AutoMapper;
using GerenciadorJogos.WebApi.DTOs;
using GerenciadorJogos.WebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace GerenciadorJogos.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IRepositorioGenerico<Usuario, int> _repositorio;
        private readonly UsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(IRepositorioGenerico<Usuario, int> repositorio, UsuarioRepositorio usuarioRepositorio)
        {
            _repositorio = repositorio;
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult Get()
        {
            try
            {
                var usuarios = _usuarioRepositorio.SelecionarComRelacionamento();

                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("logar/{login}/{senha}")]
        public ActionResult<dynamic> Get(string login, string senha)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(senha))
                {
                    return BadRequest();
                }

                List<Usuario> usuarios = _repositorio.Selecionar(s =>
                                                                          s.Login.ToLower() == login.ToLower() &&
                                                                          s.Senha == new UsuarioDTO().Retornar_Senha_Hash(senha));


                if (usuarios == null || usuarios.Count == 0)
                    return NotFound(new { mensagem = "Usuário e/ou senha inválidos" });

                UsuarioDTO user = AutoMapperConfiguration.Instance.Mapper.Map<Usuario, UsuarioDTO>(usuarios[0]);
                var token = TokenService.GerarToken(user.IsAdmin);

                user.Senha = "";
                string mensagem = "Ok";

                return new
                {
                    user,
                    mensagem,
                    token
                };
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        public IActionResult GetById(int id)
        {
            Usuario usuario = _repositorio.SelecionarPorId(id);

            if (usuario == null)
            {
                return NotFound();
            }


            usuario.Senha = string.Empty;

            return Ok(usuario);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        public IActionResult Post([FromBody]UsuarioDTO dto)
        {
            try
            {
                List<Usuario> usuarios = _repositorio.Selecionar(s => s.Login.ToLower().Contains(dto.Login.ToLower()));

                if (usuarios.Count > 0)
                {
                    return BadRequest("Esse login já está em uso!");
                }

                if (dto.PessoaId == 0)
                {
                    return BadRequest("Dados Incompletos");
                }

                dto.Preencher_Propriedades_Para_Insercao();
                Usuario user = AutoMapperConfiguration.Instance.Mapper.Map<UsuarioDTO, Usuario>(dto);
                _repositorio.Inserir(user);
                user.Senha = string.Empty;

                return CreatedAtRoute(new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult Put(int id, [FromBody]Usuario usuario)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }

                _repositorio.Atualizar(usuario);

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }

                Usuario usuario = _repositorio.SelecionarPorId(id);

                if (usuario == null)
                {
                    return NotFound();
                }

                _repositorio.ExcluirPorId(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}