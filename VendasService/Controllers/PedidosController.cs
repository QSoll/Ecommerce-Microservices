using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VendasService.Data;
using VendasService.Models;
using VendasService.Services; // ✅ Importa o EstoqueClient

namespace VendasService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly VendasDbContext _context;
        private readonly EstoqueClient _estoqueClient; // ✅ Injeta o cliente

        public PedidosController(VendasDbContext context, EstoqueClient estoqueClient)
        {
            _context = context;
            _estoqueClient = estoqueClient;
        }

        // POST: api/pedidos
        [HttpPost]
        public async Task<IActionResult> CriarPedido([FromBody] Pedido pedido)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (pedido.Itens == null || !pedido.Itens.Any())
                return BadRequest("O pedido deve conter ao menos um item.");

            // ✅ Verifica e reduz estoque antes de salvar
            foreach (var item in pedido.Itens)
            {
                var estoqueOk = await _estoqueClient.VerificarEstoque(item.ProdutoId, item.Quantidade);
                if (!estoqueOk)
                    return BadRequest($"Produto {item.ProdutoId} com estoque insuficiente.");

                var reduziu = await _estoqueClient.ReduzirEstoque(item.ProdutoId, item.Quantidade);
                if (!reduziu)
                    return StatusCode(500, $"Erro ao reduzir estoque do produto {item.ProdutoId}.");
            }

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObterPedido), new { id = pedido.Id }, pedido);
        }

        // GET: api/pedidos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> ObterPedido(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Itens)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
                return NotFound();

            return pedido;
        }
    }
}
