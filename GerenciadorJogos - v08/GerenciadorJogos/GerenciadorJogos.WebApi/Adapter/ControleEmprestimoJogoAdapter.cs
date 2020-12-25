using GerenciadorJogos.Dominio.Entidades;
using GerenciadorJogos.WebApi.DTOs;
using System;
using System.Collections.Generic;

namespace GerenciadorJogos.WebApi.Adapter
{
    public static class ControleEmprestimoJogoAdapter
    {
        public static ControleEmprestimoJogoDTO ControleEmprestimoJogoToDTO(ControleEmprestimoJogo controleEmprestimoJogo, bool isHistorico)
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



            if (isHistorico == false && (dto.DataEmprestimo != null && dto.DataEmprestimo != default(DateTime)) &&
                (dto.DataDevolucao != null && dto.DataDevolucao != default(DateTime)))
            {
                dto.DataEmprestimo = null;
                dto.DataDevolucao = null;
                dto.PessoaNome = string.Empty;
                dto.PessoaId = 0;
            }

            return dto;
        }


        public static List<ControleEmprestimoJogoDTO> ControleEmprestimoJogoToDTOs(IEnumerable<ControleEmprestimoJogo> controleEmprestimoJogos,
            bool isHistorico = true)
        {
            ControleEmprestimoJogoDTO dto = new ControleEmprestimoJogoDTO();
            List<ControleEmprestimoJogoDTO> dtos = new List<ControleEmprestimoJogoDTO>();

            foreach (var controleEmprestimoJogo in controleEmprestimoJogos)
            {
                dto = ControleEmprestimoJogoToDTO(controleEmprestimoJogo, isHistorico);

                dtos.Add(dto);
            }

            return dtos;
        }
    }
}
