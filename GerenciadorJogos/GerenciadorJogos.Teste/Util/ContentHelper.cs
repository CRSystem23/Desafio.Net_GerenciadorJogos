using GerenciadorJogos.WebApi.Services;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace GerenciadorJogos.Teste
{
    public static class ContentHelper
    {
        public static StringContent GetStringContent(object obj)
            => new StringContent(JsonConvert.SerializeObject(obj), Encoding.Default, "application/json");

        public static AuthenticationHeaderValue GetToken(bool isAdmin = true) 
            => new AuthenticationHeaderValue(scheme: "bearer", parameter: TokenService.GerarToken(isAdmin ? true : false));
    }
}
