namespace EmporioIrmasDaTerra.Models
{
    public class Avaliacao
    {
        public DateTime DataAvaliacao { get; set; }
        public string? Comentario { get; set; } // O '?' indica que o comentário pode ser opcional (nulo)
        public int Nota { get; set; } // Ex: 1 a 5 estrelas
                

        // Relacionamento com Cliente (uma avaliação é feita por UM cliente)
        public int IdCliente { get; set; } // Chave Estrangeira
        public Cliente Cliente { get; set; } = null!; // Propriedade de Navegação

        // Relacionamento com Produto (uma avaliação pertence a UM produto)
        public int IdProduto { get; set; } // Chave Estrangeira
        public Produto Produto { get; set; } = null!; // Propriedade de Navegação
    }
}