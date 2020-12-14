using System;

namespace GerenciadorJogos.WebApi.Services
{
    public static class Settings
    {
        public static string ChaveSecreta = "wwww%cccc%2020#f7d8863b48e197b9287d492b708e";

        public static DateTime TempoExpiracao = DateTime.Now.AddMinutes(30);
    }
}
