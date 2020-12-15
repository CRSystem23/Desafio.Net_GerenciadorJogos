using GerenciadorJogos.Dominio.Entidades;
using GerenciadorJogos.Generico.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;

namespace GerenciadorJogos.WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class JogoController : ControllerBase
    {
        private readonly IRepositorioGenerico<Jogo, int> _repositorio;

        public JogoController(IRepositorioGenerico<Jogo, int> jogoRepositorio)
        {
            _repositorio = jogoRepositorio;
        }


        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        public IActionResult Get()
        {
            try
            {
                List<Jogo> jogos = _repositorio.Selecionar();

                return Ok(jogos);
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
                Jogo jogo = _repositorio.SelecionarPorId(id);

                if (jogo == null)
                {
                    return NotFound();
                }

                return Ok(jogo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult Post([FromBody] Jogo jogo)
        {
            try
            {
                _repositorio.Inserir(jogo);

                return CreatedAtRoute(new { id = jogo.Id }, jogo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult Put(int id, [FromBody] Jogo jogo)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }

                _repositorio.Atualizar(jogo);

                return Ok(jogo);
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

                Jogo jogo = _repositorio.SelecionarPorId(id);

                if (jogo == null)
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