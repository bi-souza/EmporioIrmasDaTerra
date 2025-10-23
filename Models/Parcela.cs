using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmporioIrmasDaTerra.Models
{
    public class Parcela
    {
        [Key]
        public int IdParcela { get; set; }
        public int NumeroParcela { get; set; }
        public DateTime DataVencimento { get; set; }
        public string StatusParcela { get; set; } = string.Empty; // Ex: "Paga", "Pendente"
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorParcela { get; set; }
        
        
        
        // Relacionamento N-para-1: Uma Parcela pertence a UM Pagamento.
        public int IdPagamento { get; set; } // Chave Estrangeira
        public Pagamento Pagamento { get; set; } = null!; // Propriedade de Navegação
    }
}