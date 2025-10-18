using System.ComponentModel.DataAnnotations;


namespace EmporioIrmasDaTerra.Models
{
    public class Cliente
    {
        [Key]
        // Chave Primária da tabela Cliente.
        public int IdCliente { get; set; }
        public string NomeCliente { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public DateTime DataNasc { get; set; }


        // Relacionamento 1-para-N: Um Cliente pode ter N Endereços.
        // Propriedade de Navegação para a coleção de endereços do cliente.
        public ICollection<Endereco> Enderecos { get; set; } = new List<Endereco>();

        // Relacionamento 1-para-N: Um Cliente pode realizar N Pedidos.
        // Propriedade de Navegação para a coleção de pedidos do cliente.
        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

        // Relacionamento 1-para-N: Um Cliente pode fazer N Avaliações.
        // Propriedade de Navegação para a coleção de avaliações do cliente.
        public ICollection<Avaliacao> Avaliacoes { get; set; } = new List<Avaliacao>();
    }
}