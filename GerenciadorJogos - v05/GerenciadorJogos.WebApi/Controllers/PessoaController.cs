using GerenciadorJogos.Dominio.Entidades;
using GerenciadorJogos.Generico.Interface;
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
    public class PessoaController : ControllerBase
    {
        private readonly IRepositorioGenerico<Pessoa, int> _pessoaRepositorio;
        private readonly IRepositorioGenerico<Usuario, int> _usuarioRepositorio;

        public PessoaController(IRepositorioGenerico<Pessoa, int> pessoaRepositorio, IRepositorioGenerico<Usuario, int> usuarioRepositorio)
        {
            _pessoaRepositorio = pessoaRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
        }


        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        public IActionResult Get()
        {
            try
            {
                List<Pessoa> pessoas = _pessoaRepositorio.Selecionar();

                return Ok(pessoas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("not-user")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        public IActionResult GetNotUser()
        {
            try
            {
                List<Usuario> usuarios = _usuarioRepositorio.Selecionar();
                List<Pessoa> pessoas = _pessoaRepositorio.Selecionar();

                if(usuarios != null && usuarios.Count > 0 && pessoas != null && pessoas.Count > 0)
                {
                    foreach (var usuario in usuarios)
                    {
                        pessoas.Remove(usuario.Pessoa);
                    }
                }

                return Ok(pessoas);
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
            try
            {
                Pessoa pessoa = _pessoaRepositorio.SelecionarPorId(id);

                if (pessoa == null)
                {
                    return NotFound();
                }

                return Ok(pessoa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        public IActionResult Post([FromBody] Pessoa pessoa)
        {
            try
            {
                _pessoaRepositorio.Inserir(pessoa);

                return CreatedAtRoute(new { id = pessoa.Id }, pessoa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        public IActionResult Put(int id, [FromBody] Pessoa pessoa)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }

                _pessoaRepositorio.Atualizar(pessoa);

                return Ok(pessoa);
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

                Pessoa pessoa = _pessoaRepositorio.SelecionarPorId(id);

                if (pessoa == null)
                {
                    return NotFound();
                }

                _pessoaRepositorio.ExcluirPorId(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}