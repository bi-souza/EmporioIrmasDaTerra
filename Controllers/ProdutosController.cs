using EmporioIrmasDaTerra.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EmporioIrmasDaTerra.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutosController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        // Este é o método que o formulário chama (asp-action="Buscar")
        // O 'string termo' recebe o valor do input (name="termo")
        public async Task<IActionResult> Buscar(string termo)
        {
            // 1. Chama o método do repositório
            var produtos = await _produtoRepository.Buscar(termo);

            // 2. Envia o termo de busca para a View (para sabermos o que foi buscado)
            ViewData["TermoBuscado"] = termo;

            // 3. Envia a lista de produtos (cheia ou vazia) para a View "Buscar"
            return View("Buscar", produtos); 
        }

        [HttpGet]
        public async Task<IActionResult> PorCategoria(string categoria)
        {
            // 1. Busca os produtos no repositório
            var produtos = await _produtoRepository.GetByCategoria(categoria);

            // 2. Mapeia o slug para um nome bonito para o título da página
            string tituloPagina = categoria switch
            {
                "chas" => "Chás e Infusões",
                "temperos" => "Temperos e Especiarias",
                "suplementos" => "Suplementos Naturais",
                "queijos" => "Queijos e Laticínios",
                "vinhos" => "Vinhos e Bebidas",
                "graos" => "Grãos e Cereais",
                _ => "Produtos"
            };
            
            // 3. Passa o título para a View
            ViewData["NomeCategoria"] = tituloPagina;

            // 4. Retorna a View (que vamos criar agora)
            return View(produtos);
        }
    }
}