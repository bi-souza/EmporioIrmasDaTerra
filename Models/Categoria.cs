using System.ComponentModel.DataAnnotations;

namespace EmporioIrmasDaTerra.Models
{
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }
        public string NomeCategoria { get; set; } = string.Empty;


        // Relacionamento 1-para-N: Uma Categoria pode possuir N Produtos.
        // Propriedade de Navegação para a coleção de produtos desta categoria.
        public ICollection<Produto> Produtos { get; set; } = new List<Produto>();
    }
}