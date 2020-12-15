using GerenciadorJogos.AcessoDados.Contexto;
using GerenciadorJogos.Dominio.Entidades;
using GerenciadorJogos.Generico.Interface;
using GerenciadorJogos.Generico.Utilitario;
using GerenciadorJogos.Repositorio.Entidades;
using GerenciadorJogos.WebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using System;
using System.Text;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Swagger;
using static GerenciadorJogos.Generico.Enumerados.Enumerado;

namespace GerenciadorJogos.WebApi.Register
{
    public static class RegistroServicos
    {
        public static void RegistrarStringConexao(IServiceCollection services, IConfiguration configuration)
        {
            if (configuration["Provider"].DefaultString() == Provider.MYSQL.DefaultString())
            {
                services.AddDbContext<GerenciadorJogosDbContext>(options => options.UseMySql(configuration.GetConnectionString("ConexaoMySql")));
                return;
            }

            services.AddDbContext<GerenciadorJogosDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ConexaoSqlServer")));
        }

        public static void RegistarInterfaceRepositorio(IServiceCollection services)
        {
            services.AddTransient<IRepositorioGenerico<Usuario, int>, UsuarioRepositorio>();
            services.AddTransient<IRepositorioGenerico<Pessoa, int>, PessoaRepositorio>();
            services.AddTransient<IRepositorioGenerico<Jogo, int>, JogoRepositorio>();
            services.AddTransient<IRepositorioGenerico<ControleEmprestimoJogo, int>, ControleEmprestimoJogoRepositorio>();
        }

        public static void RegistarFormatoJson(IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
        }

        public static void RegistarSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(sw => 
            {
                sw.SwaggerDoc("v1", new Info { Title = "GerenciadorJogos", Version = "v1" });
            });
        }


        public static void RegistrarAutenticacao(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidIssuer = "desafioinvillia.net",
                   ValidAudience = "desafioinvillia.net",
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Settings.ChaveSecreta))
               };

               options.Events = new JwtBearerEvents
               {
                   OnAuthenticationFailed = context =>
                   {
                       Console.WriteLine("Token inválido...." + context.Exception.Message);
                       return Task.CompletedTask;
                   },

                   OnTokenValidated = context =>
                   {
                       Console.WriteLine("Token válido...." + context.SecurityToken);
                       return Task.CompletedTask;
                   }
               };
           });
        }
    }
}
