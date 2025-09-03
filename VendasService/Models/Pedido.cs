using System.ComponentModel.DataAnnotations;

namespace VendasService.Models
{
    public enum StatusPedido
    {
        Pendente,
        Confirmado,
        Cancelado
    }

    public class Pedido
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Cliente { get; set; } = string.Empty;

        public DateTime DataPedido { get; set; } = DateTime.UtcNow;

        [Required]
        public List<ItemPedido> Itens { get; set; } = new();

        public StatusPedido Status { get; set; } = StatusPedido.Pendente;
    }
}
