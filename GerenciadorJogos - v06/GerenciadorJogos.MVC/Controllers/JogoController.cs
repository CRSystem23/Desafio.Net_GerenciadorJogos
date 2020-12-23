using GerenciadorJogos.Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace GerenciadorJogos.MVC.Controllers
{
    public class JogoController : Controller
    {
        private Uri padraoUri = new Uri("http://192.168.15.18:9010/api/");

        [Route("index")]
        public IActionResult Index(string token)
        {
            IEnumerable<Jogo> jogos = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = this.padraoUri;
                client.DefaultRequestHeaders.Add("www-authenticate", token);
                var responseTask = client.GetAsync("Jogo");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var resultRead = result.Content.ReadAsAsync<IList<Jogo>>();
                    resultRead.Wait();
                    jogos = resultRead.Result;
                }
                else
                {
                    jogos = Enumerable.Empty<Jogo>();
                    ModelState.AddModelError(string.Empty, "Erro no servidor!");
                }
            }

            return View(jogos);
        }
    }
    }