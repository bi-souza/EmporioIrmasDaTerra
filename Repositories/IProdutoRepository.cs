using EmporioIrmasDaTerra.Models;

namespace EmporioIrmasDaTerra.Repositories
{
    public interface IProdutoRepository
    {
        // Contrato que define um método para buscar um produto pelo seu Id.
        Task<Produto?> GetById(int id);

        // Contrato que define um método para buscar todos os produtos (simples, sem includes).
        Task<IEnumerable<Produto>> GetAll();

        // NOVO: Contrato para buscar todos os produtos incluindo suas categorias.
        // É ESTE que o HomeController vai usar!
        Task<IEnumerable<Produto>> GetAllWithCategories();
    }
}