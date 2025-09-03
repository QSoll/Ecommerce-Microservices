using System.ComponentModel.DataAnnotations;

namespace VendasService.Models
{
    public class ItemPedido
    {
        public int Id { get; set; }

        [Required]
        public int ProdutoId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantidade { get; set; }

        public int PedidoId { get; set; }
        public Pedido? Pedido { get; set; } // ✅ Nullable

    }
}
