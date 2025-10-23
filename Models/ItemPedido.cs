using System.ComponentModel.DataAnnotations.Schema;

namespace EmporioIrmasDaTerra.Models
{
    public class ItemPedido
    {
        // Atributos específicos da relação
        public int Quantidade { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecoUnitario { get; set; }

        // --- Relacionamentos e Chave Primária Composta ---

        // Chave Estrangeira para Pedido.
        public int IdPedido { get; set; }
        public Pedido Pedido { get; set; } = null!;

        // Chave Estrangeira para Produto.
        public int IdProduto { get; set; }
        public Produto Produto { get; set; } = null!;
    }
}