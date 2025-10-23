// Localização: Data/AppDbContext.cs

using EmporioIrmasDaTerra.Models;
using Microsoft.EntityFrameworkCore;

namespace EmporioIrmasDaTerra.Data
{
    
    public class AppDbContext : DbContext 
    {
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // --- DECLARAÇÃO DAS TABELAS (DbSets) ---
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }
        public DbSet<Cupom> Cupons { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<Parcela> Parcelas { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<CEP> CEPs { get; set; }
        public DbSet<ItemPedido> ItensPedido { get; set; }


        // --- CONFIGURAÇÃO DAS RELAÇÕES COMPLEXAS ---
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Avaliacao>()
                .HasKey(a => new { a.IdCliente, a.IdProduto });

            modelBuilder.Entity<ItemPedido>()
                .HasKey(ip => new { ip.IdPedido, ip.IdProduto });

            /* Só será usado com um BD real
            modelBuilder.Entity<Produto>()
                .Property(p => p.Preco)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Pedido>()
                .Property(p => p.ValorTotal)
                .HasColumnType("decimal(18,2)");
            
            modelBuilder.Entity<ItemPedido>()
                .Property(ip => ip.PrecoUnitario)
                .HasColumnType("decimal(18,2)");
                
            modelBuilder.Entity<Cupom>()
                .Property(c => c.ValorDesconto)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Pagamento>()
                .Property(p => p.ValorPago)
                .HasColumnType("decimal(18,2)");
            
            modelBuilder.Entity<Parcela>()
                .Property(p => p.ValorParcela)
                .HasColumnType("decimal(18,2)"); */
        }
    }
}