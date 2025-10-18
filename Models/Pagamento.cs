using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmporioIrmasDaTerra.Models
{
    public class Pagamento
    {
        [Key]
        public int IdPagamento { get; set; }
        public string TipoPagamento { get; set; } = string.Empty; 
        public DateTime DataPagamento { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorPago { get; set; }
        
        
        
        // Relacionamento 1-para-N: Um Pagamento pode ter N Parcelas.
        public ICollection<Parcela> Parcelas { get; set; } = new List<Parcela>();
    }
}