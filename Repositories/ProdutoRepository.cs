using EmporioIrmasDaTerra.Data;
using EmporioIrmasDaTerra.Models;
using Microsoft.EntityFrameworkCore;

namespace EmporioIrmasDaTerra.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;

        // cria a conexão com o BD por meio da injeção de dependência
        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Produto> ObterProdutosEmDestaque()
        {

            return _context.Produtos
                                 .Where(p => p.EmDestaque)
                                 .Include(p => p.Categoria) //trazer os dados da Categoria de cada produto
                                 .ToList();  //envia a consulta para o BD e materializa os resultados em uma List<Produtos>
         }

     
        public IEnumerable<Produto> PorCategoria(string categoria)
        {
            // traduz o slug da URL para o nome da categoria no BD
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

            // se a categoria nao foi encontrada, retorna uma lista vazia
            if (string.IsNullOrEmpty(termoBusca))
            {
                return new List<Produto>();
            }

            // Busca produtos onde o nome da categoria seja igual ao termo            
            return _context.Produtos 
                        .Include(p => p.Categoria) //trazer os dados da Categoria de cada produto
                        .Where(p => p.Categoria.NomeCategoria == termoBusca)
                        .ToList();
        }


        public IEnumerable<Produto> Buscar(string termo)
        {
            // verifica se o termo é null, empty ou só contém espaços em branco
            if (string.IsNullOrWhiteSpace(termo))
            {
                return new List<Produto>();
            }

            var termoBusca = termo.ToLower();

            return _context.Produtos
                        .Include(p => p.Categoria)
                        .Where(p =>
                                p.NomeProduto.ToLower().Contains(termoBusca) ||
                                p.Descricao.ToLower().Contains(termoBusca)
                        ) // procura o termo no nome e na descrição
                        .ToList();
        }
        
        public async Task<Produto?> GetById(int id)
        {
            // Busca no banco de dados, na tabela de Produtos,
            // o primeiro produto onde o IdProduto seja igual ao 'id' recebido.
            // O FirstOrDefault retorna o produto ou 'null' se não encontrar.
            return await _context.Produtos
                           .FirstOrDefaultAsync(p => p.IdProduto == id);
        }     
        
    }
}