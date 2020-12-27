using GerenciadorJogos.WebApi;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace GerenciadorJogos.Teste
{
    public class ControleEmprestimoJogoTest : IClassFixture<TestFixture<Startup>>
    {
        private HttpClient _client;
        public ControleEmprestimoJogoTest(TestFixture<Startup> fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task RetornarControleEmprestimoJogos()
        {
            // Arrange
            var request = "/api/controleemprestimojogo";

            // Act
            _client.DefaultRequestHeaders.Authorization = ContentHelper.GetToken();
            var response = await _client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
