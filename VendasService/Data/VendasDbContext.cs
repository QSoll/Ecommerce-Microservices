
using Microsoft.EntityFrameworkCore;
using VendasService.Models; // ✅ Corrigido


namespace VendasService.Data

{
    public class VendasDbContext : DbContext
    {
        public VendasDbContext(DbContextOptions<VendasDbContext> options) : base(options) { }

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItensPedido { get; set; }
    }
}
