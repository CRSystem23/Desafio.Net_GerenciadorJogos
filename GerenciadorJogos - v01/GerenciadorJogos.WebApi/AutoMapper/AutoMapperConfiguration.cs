using AutoMapper;
using GerenciadorJogos.Dominio.Entidades;
using GerenciadorJogos.WebApi.DTOs;

namespace GerenciadorJogos.WebApi.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Usuario, UsuarioDTO>().ReverseMap();
            });
        }
    }
}
