using EmporioIrmasDaTerra.Models;
using EmporioIrmasDaTerra.Repositories; 
using Microsoft.AspNetCore.Mvc;


namespace EmporioIrmasDaTerra.Controllers
{
    public class HomeController : Controller
    {   
        // readonly: valor do campo é definido só uma vez no construtor e nao muda mais     
        private readonly IProdutoRepository _produtoRepository;

        // O construtor "pede" ao sistema de Injeção de Dependência do ASP.NET Core que forneça
        // um objeto (um serviço) que implemente o contrato `IProdutoRepository`
        public HomeController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }
        
        //retorna produtos em destaque
        public IActionResult Index() 
        {            
            var produtosDestaque = _produtoRepository.GetFeaturedProducts(); // <-- Ver aviso abaixo!
            return View(produtosDestaque);
        }
     
       
    }
}