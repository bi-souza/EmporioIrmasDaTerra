using System.ComponentModel.DataAnnotations;


namespace EmporioIrmasDaTerra.Models
{
    public class Estado
    {
        [Key]
        public int IdEstado { get; set; }
        public string NomeEstado { get; set; } = string.Empty;
        public string Sigla { get; set; } = string.Empty;



        // Relacionamento 1-para-N: Um Estado pode ter N Cidades.
        public ICollection<Cidade> Cidades { get; set; } = new List<Cidade>();
    }
}