using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmporioIrmasDaTerra.Models
{
    public class Produto
    {        
        [Key] 
        public int IdProduto { get; set; }
        public string NomeProduto { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        
        [Column(TypeName = "decimal(18,2)")] 
        public decimal Preco { get; set; }
        public int Estoque { get; set; }        
        
        public bool EmDestaque { get; set; } // Propriedade para "Produtos em Destaque"

        public string ImagemUrl { get; set; } = string.Empty; // Armazena o caminho (ex: /images/produtos/queijo.png)
        
        // PROPRIEDADE PARA PESO
        [Required(ErrorMessage = "O peso é obrigatório")]
        [Column(TypeName = "decimal(18,2)")] // Tipo de dado para precisão no banco
        [Display(Name = "Peso (kg)")] // Rótulo para Views
        public decimal Peso { get; set; }

        

        // Relacionamento com Categoria
        public int IdCategoria { get; set; } 
        [ForeignKey("IdCategoria")]
        public Categoria Categoria { get; set; } = null!;

        // Relacionamento com Avaliacao
        public ICollection<Avaliacao> Avaliacoes { get; set; } = new List<Avaliacao>();
        
        // Relacionamento com ItemPedido
        public ICollection<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>();
    }
}