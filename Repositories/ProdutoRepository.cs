using EmporioIrmasDaTerra.Data;
using EmporioIrmasDaTerra.Models;
using Microsoft.EntityFrameworkCore;

namespace EmporioIrmasDaTerra.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;
       
        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task<Produto?> GetById(int id)
        {
            return await _context.Produtos.FirstOrDefaultAsync(p => p.IdProduto == id);
        }

        public async Task<IEnumerable<Produto>> GetAllWithCategories()
        {
            return await _context.Produtos
                                 .Include(p => p.Categoria)
                                 .ToListAsync();
        }


        public async Task<IEnumerable<Produto>> GetFeaturedProducts()
        {
            // Busca apenas produtos marcados como "EmDestaque"            
            return await _context.Produtos
                                 .Where(p => p.EmDestaque)
                                 .Include(p => p.Categoria)
                                 .ToListAsync();
        }
        
        
    }
}