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

        public async Task<IEnumerable<Produto>> GetByCategoria(string categoriaSlug)
        {
            // Mapeia o 'slug' da URL para o nome da categoria no banco
            string termoBusca = categoriaSlug switch
            {
                "chas" => "Chás e Infusões",
                "temperos" => "Temperos e Especiarias",
                "suplementos" => "Suplementos Naturais",
                "queijos" => "Queijos e Laticínios",
                "vinhos" => "Vinhos e Bebidas",
                "graos" => "Grãos e Cereais",
                _ => "" // Se não achar, não retorna nada
            };

            if (string.IsNullOrEmpty(termoBusca))
            {
                return new List<Produto>();
            }

            // Busca produtos onde o nome da categoria seja igual ao termo
            return await _context.Produtos //
                                 .Include(p => p.Categoria) //
                                 .Where(p => p.Categoria.NomeCategoria == termoBusca)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Produto>> Buscar(string termo)
        {
            // Se o termo de busca for nulo ou vazio, retorna uma lista vazia
            if (string.IsNullOrWhiteSpace(termo))
            {
                return new List<Produto>(); // Retorna lista vazia
            }

            var termoBusca = termo.ToLower();

            // Busca produtos onde o Nome ou a Descrição contenham o termo
            return await _context.Produtos //
                                .Include(p => p.Categoria) //
                                .Where(p =>
                                    p.NomeProduto.ToLower().Contains(termoBusca) ||
                                    p.Descricao.ToLower().Contains(termoBusca)
                                )
                                .ToListAsync();
        }

        public async Task Add(Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync(); // Salva as mudanças no banco
        }
        
    }
}