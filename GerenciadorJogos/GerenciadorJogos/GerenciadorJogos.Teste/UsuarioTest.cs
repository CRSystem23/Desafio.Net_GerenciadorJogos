using GerenciadorJogos.WebApi;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace GerenciadorJogos.Teste
{
    public class UsuarioTest : IClassFixture<TestFixture<Startup>>
    {
        private HttpClient _client;

        public UsuarioTest(TestFixture<Startup> fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task RetornarErroLoginSenha()
        {
            string login = "errado";
            string senha = "senha";

            // Arrange
            var request = string.Concat("/api/usuario/logar/{0}/{1}", login, senha);

            // Act
            var response = await _client.GetAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }


        [Fact]
        public async Task RetornarOkLoginSenha()
        {
            string login = "admin";
            string senha = "123456";

            // Arrange
            var request = string.Concat("/api/usuario/logar/{0}/{1}", login, senha);

            // Act
            var response = await _client.GetAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task RetornarUsuarios()
        {
            // Arrange
            var request = "/api/usuario";

            // Act
            _client.DefaultRequestHeaders.Authorization = ContentHelper.GetToken();
            var response = await _client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
