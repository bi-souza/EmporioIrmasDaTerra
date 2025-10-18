using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmporioIrmasDaTerra.Models
{
    public class Produto
    {   
        [Key]     
        public int IdProduto { get; set; }
        public string NomeProduto { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        
        [Column(TypeName = "decimal(18,2)")] // Garante que o preço seja armazenado corretamente no banco.
        public decimal Preco { get; set; }
        public int Estoque { get; set; }

      
        // Relacionamento N-para-1: Um Produto pertence a UMA Categoria.
        public int IdCategoria { get; set; } // Chave Estrangeira.

        [ForeignKey("IdCategoria")]
        public Categoria Categoria { get; set; } = null!; // Propriedade de Navegação.

        // Relacionamento 1-para-N: Um Produto pode receber N Avaliações.
        public ICollection<Avaliacao> Avaliacoes { get; set; } = new List<Avaliacao>();
        
        // Relacionamento N-para-N com Pedido, através da tabela de junção PedidoProduto.
        public ICollection<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>();
    }
}