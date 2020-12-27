using GerenciadorJogos.WebApi;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace GerenciadorJogos.Teste
{
    public class JogoTest : IClassFixture<TestFixture<Startup>>
    {
        private HttpClient _client;
        public JogoTest(TestFixture<Startup> fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task RetornarJogos()
        {
            // Arrange
            var request = "/api/jogo";

            // Act
            _client.DefaultRequestHeaders.Authorization = ContentHelper.GetToken();
            var response = await _client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
