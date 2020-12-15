using GerenciadorJogos.Dominio.Entidades;
using GerenciadorJogos.Generico.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace GerenciadorJogos.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly IRepositorioGenerico<Pessoa, int> _repositorio;

        public PessoaController(IRepositorioGenerico<Pessoa, int> pessoaRepositorio)
        {
            _repositorio = pessoaRepositorio;
        }


        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        public IActionResult Get()
        {
            try
            {
                List<Pessoa> pessoas = _repositorio.Selecionar();

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
                Pessoa pessoa = _repositorio.SelecionarPorId(id);

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
                _repositorio.Inserir(pessoa);

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

                _repositorio.Atualizar(pessoa);

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

                Pessoa pessoa = _repositorio.SelecionarPorId(id);

                if (pessoa == null)
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