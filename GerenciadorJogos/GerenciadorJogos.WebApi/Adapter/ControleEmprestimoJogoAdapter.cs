using GerenciadorJogos.Dominio.Entidades;
using GerenciadorJogos.WebApi.DTOs;
using System;
using System.Collections.Generic;

namespace GerenciadorJogos.WebApi.Adapter
{
    public static class ControleEmprestimoJogoAdapter
    {
        public static ControleEmprestimoJogoDTO ControleEmprestimoJogoToDTO(ControleEmprestimoJogo controleEmprestimoJogo)
        {
            ControleEmprestimoJogoDTO dto = new ControleEmprestimoJogoDTO();

            dto.Id = controleEmprestimoJogo.Id;
            dto.PessoaId = controleEmprestimoJogo.PessoaId;
            dto.PessoaNome = controleEmprestimoJogo.Pessoa == null ? string.Empty : controleEmprestimoJogo.Pessoa.Nome;
            dto.JogoId = controleEmprestimoJogo.JogoId;
            dto.JogoNome = controleEmprestimoJogo.Jogo.Nome;

            if (controleEmprestimoJogo.DataEmprestimo != default(DateTime))
            {
                dto.DataEmprestimo = controleEmprestimoJogo.DataEmprestimo;
            }

            dto.DataDevolucao = controleEmprestimoJogo.DataDevolucao;

            return dto;
        }


        public static List<ControleEmprestimoJogoDTO> ControleEmprestimoJogoToDTOs(IEnumerable<ControleEmprestimoJogo> controleEmprestimoJogos)
        {
            ControleEmprestimoJogoDTO dto = new ControleEmprestimoJogoDTO();
            List<ControleEmprestimoJogoDTO> dtos = new List<ControleEmprestimoJogoDTO>();

            foreach (var controleEmprestimoJogo in controleEmprestimoJogos)
            {
                dto = ControleEmprestimoJogoToDTO(controleEmprestimoJogo);

                dtos.Add(dto);
            }

            return dtos;
        }
    }
}
