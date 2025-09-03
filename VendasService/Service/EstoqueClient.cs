using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace VendasService.Services
{
    public class EstoqueClient
    {
        private readonly HttpClient _httpClient;

        public EstoqueClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> VerificarEstoque(int produtoId, int quantidade)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5179/api/produtos/{produtoId}");
            if (!response.IsSuccessStatusCode)
                return false;

            var produto = await response.Content.ReadFromJsonAsync<ProdutoDto>();
            return produto != null && produto.Quantidade >= quantidade;
        }

        public async Task<bool> ReduzirEstoque(int produtoId, int quantidade)
        {
            var content = JsonContent.Create(new { quantidade });
            var response = await _httpClient.PatchAsync($"http://localhost:5179/api/produtos/{produtoId}/reduzir-estoque", content);
            return response.IsSuccessStatusCode;
        }
    }

    public class ProdutoDto
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
    }
}
