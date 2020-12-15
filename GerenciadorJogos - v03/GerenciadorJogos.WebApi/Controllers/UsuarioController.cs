using GerenciadorJogos.Dominio.Entidades;
using GerenciadorJogos.Generico.Interface;
using GerenciadorJogos.WebApi.AutoMapper;
using GerenciadorJogos.WebApi.DTOs;
using GerenciadorJogos.WebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace GerenciadorJogos.WebApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("*")]
    public class UsuarioController : ControllerBase
    {
        private readonly IRepositorioGenerico<Usuario, int> _usuarioRepositorio;

        public UsuarioController(IRepositorioGenerico<Usuario, int> usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult Get()
        {
            try
            {
                var usuarios = _usuarioRepositorio.Selecionar();

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

                List<Usuario> usuarios = _usuarioRepositorio.Selecionar(s =>
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
            Usuario usuario = _usuarioRepositorio.SelecionarPorId(id);

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
                List<Usuario> usuarios = _usuarioRepositorio.Selecionar(s => s.Login.ToLower().Contains(dto.Login.ToLower()));

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
                _usuarioRepositorio.Inserir(user);
                user.Senha = string.Empty;

                return CreatedAtRoute(new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult Put([FromBody]UsuarioDTO dto)
        {
            try
            {
                if (dto.Id.HasValue == false)
                {
                    return BadRequest();
                }

                Usuario usuario = AutoMapperConfiguration.Instance.Mapper.Map<UsuarioDTO, Usuario>(dto);
                usuario.Senha = dto.Retornar_Senha_Hash(usuario.Senha);
                _usuarioRepositorio.Atualizar(usuario);

                return Ok(dto);
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

                Usuario usuario = _usuarioRepositorio.SelecionarPorId(id);

                if (usuario == null)
                {
                    return NotFound();
                }

                _usuarioRepositorio.ExcluirPorId(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}