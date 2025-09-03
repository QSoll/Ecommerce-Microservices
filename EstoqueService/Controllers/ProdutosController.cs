using Microsoft.AspNetCore.Mvc;
using EstoqueService.Data;
using EstoqueService.Models;
using Microsoft.EntityFrameworkCore;

namespace EstoqueService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly EstoqueDbContext _context;

        public ProdutosController(EstoqueDbContext context)
        {
            _context = context;
        }


        // POST: api/produtos
        [HttpPost]
        public async Task<IActionResult> CriarProduto([FromBody] Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ObterProduto), new { id = produto.Id }, produto);
        }

        // GET: api/produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> ListarProdutos()
        {
            return await _context.Produtos.ToListAsync();
        }

        // GET: api/produtos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> ObterProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null) return NotFound();
            return produto;
        }

        // PUT: api/produtos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarProduto(int id, [FromBody] Produto produtoAtualizado)
        {
            if (id != produtoAtualizado.Id) return BadRequest();

            var existe = await _context.Produtos.AnyAsync(p => p.Id == id);
            if (!existe) return NotFound(); // ✅ Verifica se o produto existe

            _context.Entry(produtoAtualizado).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }


        // DELETE: api/produtos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null) return NotFound();

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}/reduzir-estoque")]
public async Task<IActionResult> ReduzirEstoque(int id, [FromBody] int quantidade)
{
    var produto = await _context.Produtos.FindAsync(id);
    if (produto == null) return NotFound();

    if (quantidade <= 0) return BadRequest("Quantidade deve ser maior que zero.");
    if (produto.QuantidadeEstoque < quantidade) return BadRequest("Estoque insuficiente.");

    produto.QuantidadeEstoque -= quantidade;
    await _context.SaveChangesAsync();

    return Ok(produto);
}

    }
}
