using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmporioIrmasDaTerra.Models
{
    public class Pedido
    {   
        [Key]     
        public int IdPedido { get; set; }
        public DateTime DataPedido { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorTotal { get; set; }
        

        // Relacionamento N-para-1: Um Pedido é realizado por UM Cliente.
        public int IdCliente { get; set; } // Chave Estrangeira.
        public Cliente Cliente { get; set; } = null!; // Propriedade de Navegação.

        // Relacionamento N-para-1: Um Pedido pode usar UM Cupom.
        // A '?' torna o relacionamento opcional (um pedido pode não ter cupom).
        public int? IdCupom { get; set; } // Chave Estrangeira (opcional).
        public Cupom? Cupom { get; set; } // Propriedade de Navegação (opcional).
        
        // Relacionamento 1-para-1: Um Pedido tem UM Pagamento.
        public int IdPagamento { get; set; } // Chave Estrangeira
        public Pagamento Pagamento { get; set; } = null!; // Propriedade de Navegação

        // Relacionamento N-para-N com Produto, através da tabela de junção PedidoProduto.
        public ICollection<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>();
    }
}