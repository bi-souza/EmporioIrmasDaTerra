using EmporioIrmasDaTerra.Models;

namespace EmporioIrmasDaTerra.Repositories
{
    public interface IProdutoRepository
    {
        // Contrato que define um método para buscar um produto pelo seu Id.
        Task<Produto?> GetById(int id);

        // Contrato que define um método para buscar todos os produtos (simples).
        Task<IEnumerable<Produto>> GetAll();

        // Contrato para buscar todos os produtos incluindo suas categorias.
        Task<IEnumerable<Produto>> GetAllWithCategories();

        // Contrato para buscar os produtos em destaque
        IEnumerable<Produto> GetFeaturedProducts();

        IEnumerable<Produto> GetByCategory(string categoria);
        
        IEnumerable<Produto> Search(string termo);
    
    }
}