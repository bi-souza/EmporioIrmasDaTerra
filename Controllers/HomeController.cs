using EmporioIrmasDaTerra.Models;
using EmporioIrmasDaTerra.Repositories; 
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmporioIrmasDaTerra.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProdutoRepository _produtoRepository;

        
        public HomeController(ILogger<HomeController> logger, IProdutoRepository produtoRepository)
        {
            _logger = logger;
            _produtoRepository = produtoRepository;
        }

        
        public async Task<IActionResult> Index()
        {
            // Usando esse método para os "Mais Vendidos" 
            var produtosDestaque = await _produtoRepository.GetFeaturedProducts(); 
            
            // Envia a lista de produtos para a View
            return View(produtosDestaque);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}