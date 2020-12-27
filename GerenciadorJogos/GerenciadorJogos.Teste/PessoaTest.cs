using GerenciadorJogos.WebApi;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
namespace GerenciadorJogos.Teste
{
    public class PessoaTest : IClassFixture<TestFixture<Startup>>
    {
        private HttpClient _client;
        public PessoaTest(TestFixture<Startup> fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task RetornarPessoas()
        {
            // Arrange
            var request = "/api/pessoa";

            // Act
            _client.DefaultRequestHeaders.Authorization = ContentHelper.GetToken();
            var response = await _client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
