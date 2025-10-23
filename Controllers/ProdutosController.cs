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

        // Barra de pesquisa      
        public IActionResult Search(string termo)        
        {            
            var produtos = _produtoRepository.Search(termo); 
            
            ViewData["TermoBuscado"] = termo;
            
            return View("Buscar", produtos); 
        }

        [HttpGet]
        public IActionResult ByCategory(string categoria)
        {
            
            var produtos = _produtoRepository.GetByCategory(categoria);

            
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
            
            
            ViewData["NomeCategoria"] = tituloPagina;

            
            return View(produtos);
        }
    }
}