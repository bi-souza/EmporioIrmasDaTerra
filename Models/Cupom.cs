using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmporioIrmasDaTerra.Models
{
    public class Cupom
    {   
        [Key]     
        public int IdCupom { get; set; }
        public string CodigoCupom { get; set; } = string.Empty;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorDesconto { get; set; }
        public string TipoDesconto { get; set; } = string.Empty; // Ex: "Percentual" ou "Fixo"

        

        // Relacionamento 1-para-N: Um Cupom pode ser usado em N Pedidos.
        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}