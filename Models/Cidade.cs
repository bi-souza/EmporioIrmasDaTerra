using System.ComponentModel.DataAnnotations;

namespace EmporioIrmasDaTerra.Models
{
    public class Cidade
    {
        [Key]
        public int IdCidade { get; set; }
        public string NomeCidade { get; set; } = string.Empty;



        // Relacionamento N-para-1: Uma Cidade pertence a UM Estado.
        public int IdEstado { get; set; } // Chave Estrangeira.
        public Estado Estado { get; set; } = null!; // Propriedade de Navegação.

        // Relacionamento 1-para-N: Uma Cidade pode ter N CEPs.
        public ICollection<CEP> CEPs { get; set; } = new List<CEP>();
    }
}