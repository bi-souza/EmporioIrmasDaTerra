using EmporioIrmasDaTerra.Data;
using EmporioIrmasDaTerra.Models;
using Microsoft.EntityFrameworkCore;

namespace EmporioIrmasDaTerra.Repositories
{
    // A classe implementa o contrato definido pela interface IProdutoRepository.
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;

        // O construtor recebe o AppDbContext via injeção de dependência.         
        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        // Implementação do método para buscar todos os produtos (simples).
        public async Task<IEnumerable<Produto>> GetAll()
        {
            // Usa o DbContext para acessar a "tabela" de Produtos e retorna todos como uma lista.
            return await _context.Produtos.ToListAsync();
        }

        // Implementação do método para buscar um produto pelo seu Id.
        public async Task<Produto?> GetById(int id)
        {
            // Busca na tabela de Produtos o primeiro que tiver o Id correspondente.
            return await _context.Produtos.FirstOrDefaultAsync(p => p.IdProduto == id);
        }

        // Implementação do método para buscar produtos COM suas categorias.
        public async Task<IEnumerable<Produto>> GetAllWithCategories()
        {
            // Esta é a consulta que o HomeController precisa.
            return await _context.Produtos
                                 .Include(p => p.Categoria)
                                 .ToListAsync();
        }
    }
}