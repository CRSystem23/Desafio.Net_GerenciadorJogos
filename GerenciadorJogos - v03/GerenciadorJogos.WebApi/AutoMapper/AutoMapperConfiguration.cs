using AutoMapper;
using GerenciadorJogos.Dominio.Entidades;
using GerenciadorJogos.WebApi.DTOs;
using System;

namespace GerenciadorJogos.WebApi.AutoMapper
{
    public class AutoMapperConfiguration
    {
        private static readonly Lazy<AutoMapperConfiguration> _instance =
    new Lazy<AutoMapperConfiguration>(() => { return new AutoMapperConfiguration(); });

        public static AutoMapperConfiguration Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private MapperConfiguration _config;

        public IMapper Mapper
        {
            get
            {
                return _config.CreateMapper();
            }
        }

        private AutoMapperConfiguration()
        {
            _config = new MapperConfiguration((cfg) =>
            {
                cfg.CreateMap<Usuario, UsuarioDTO>();
                cfg.CreateMap<UsuarioDTO, Usuario>();
            });
        }
    }
}
