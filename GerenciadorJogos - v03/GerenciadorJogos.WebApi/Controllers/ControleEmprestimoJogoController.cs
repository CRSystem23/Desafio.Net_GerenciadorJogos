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
    public class ControleEmprestimoJogoController : ControllerBase
    {
        private readonly IRepositorioGenerico<ControleEmprestimoJogo, int> _repositorio;

        public ControleEmprestimoJogoController(IRepositorioGenerico<ControleEmprestimoJogo, int> jogoRepositorio)
        {
            _repositorio = jogoRepositorio;
        }


        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        public IActionResult Get()
        {
            try
            {
                ICollection<ControleEmprestimoJogo> controleEmprestimoJogos = _repositorio.Selecionar();

                return Ok(controleEmprestimoJogos);
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
                ControleEmprestimoJogo controleEmprestimoJogo = _repositorio.SelecionarPorId(id);

                if (controleEmprestimoJogo == null)
                {
                    return NotFound();
                }

                return Ok(controleEmprestimoJogo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult Post([FromBody] ControleEmprestimoJogo controleEmprestimoJogo)
        {
            try
            {
                _repositorio.Inserir(controleEmprestimoJogo);

                return CreatedAtRoute(new { id = controleEmprestimoJogo.Id }, controleEmprestimoJogo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult Put(int id, [FromBody] ControleEmprestimoJogo controleEmprestimoJogo)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }

                _repositorio.Atualizar(controleEmprestimoJogo);

                return Ok(controleEmprestimoJogo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}