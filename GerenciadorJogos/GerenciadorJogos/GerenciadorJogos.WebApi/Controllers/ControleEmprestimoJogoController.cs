using GerenciadorJogos.Dominio.Entidades;
using GerenciadorJogos.Generico.Interface;
using GerenciadorJogos.Repositorio.Entidades;
using GerenciadorJogos.WebApi.Adapter;
using GerenciadorJogos.WebApi.AutoMapper;
using GerenciadorJogos.WebApi.DTOs;
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
        private readonly ControleEmprestimoJogoRepositorio _controleEmprestimoJogoRepositorio;

        public ControleEmprestimoJogoController(IRepositorioGenerico<ControleEmprestimoJogo, int> repositorio,
            ControleEmprestimoJogoRepositorio controleEmprestimoJogoRepositorio)
        {
            _repositorio = repositorio;
            _controleEmprestimoJogoRepositorio = controleEmprestimoJogoRepositorio;
        }


        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<ControleEmprestimoJogoDTO> controleEmprestimoJogos = 
                    ControleEmprestimoJogoAdapter.ControleEmprestimoJogoToDTOs(_controleEmprestimoJogoRepositorio.SelecionarTodosJogosComUltimoControleEmprestimo(), false);

                return Ok(controleEmprestimoJogos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        [Route("SelecionarTodos")]
        public IActionResult GetPadrao()
        {
            try
            {
                IEnumerable<ControleEmprestimoJogoDTO> controleEmprestimoJogos =
                    ControleEmprestimoJogoAdapter.ControleEmprestimoJogoToDTOs(_controleEmprestimoJogoRepositorio.SelecionarComRelacionamento());

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
                ControleEmprestimoJogo controleEmprestimoJogo = _controleEmprestimoJogoRepositorio.SelecionarComRelacionamentoPorId(id);

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
        public IActionResult Post([FromBody] ControleEmprestimoJogoDTO controleEmprestimoJogoDTO)
        {
            try
            {
                var controleEmprestimoJogo = AutoMapperConfiguration.Instance.Mapper.Map<ControleEmprestimoJogoDTO, ControleEmprestimoJogo>(controleEmprestimoJogoDTO);

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
        public IActionResult Put(int id, [FromBody] ControleEmprestimoJogoDTO controleEmprestimoJogoDTO)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }

                var controleEmprestimoJogo = AutoMapperConfiguration.Instance.Mapper.Map<ControleEmprestimoJogoDTO, ControleEmprestimoJogo>(controleEmprestimoJogoDTO);

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