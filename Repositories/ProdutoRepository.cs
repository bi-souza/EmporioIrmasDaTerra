using System.Linq;
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
            return await _context.Produtos.Include(p => p.Categoria).ToListAsync();
        }

        public IEnumerable<Produto> GetFeaturedProducts()
        {

            return _context.Produtos
                                 .Where(p => p.EmDestaque)
                                 .Include(p => p.Categoria)
                                 .ToList(); // Esta é a mudança principal
         }

     
        public IEnumerable<Produto> GetByCategory(string categoria)
        {
            
            string termoBusca = categoria switch
            {
                "chas" => "Chás e Infusões",
                "temperos" => "Temperos e Especiarias",
                "suplementos" => "Suplementos Naturais",
                "queijos" => "Queijos e Laticínios",
                "vinhos" => "Vinhos e Bebidas",
                "graos" => "Grãos e Cereais",
                _ => "" // Se não achar, retorna lista vazia
            };

            if (string.IsNullOrEmpty(termoBusca))
            {
                return new List<Produto>();
            }

            // Busca produtos onde o nome da categoria seja igual ao termo            
            return _context.Produtos 
                        .Include(p => p.Categoria) 
                        .Where(p => p.Categoria.NomeCategoria == termoBusca)
                        .ToList();
        }

        public IEnumerable<Produto> Search(string termo)
        {
            
            if (string.IsNullOrWhiteSpace(termo))
            {
                return new List<Produto>(); 
            }

            var termoBusca = termo.ToLower();

            // Busca produtos onde o Nome ou a Descrição contenham o termo
           
            return _context.Produtos 
                        .Include(p => p.Categoria) 
                        .Where(p => 
                                p.NomeProduto.ToLower().Contains(termoBusca) ||
                                p.Descricao.ToLower().Contains(termoBusca)
                        )
                        .ToList(); 
        }
    }
}
