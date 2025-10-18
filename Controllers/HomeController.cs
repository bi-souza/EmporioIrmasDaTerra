using EmporioIrmasDaTerra.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EmporioIrmasDaTerra.Models;

namespace EmporioIrmasDaTerra.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context; 

    // Construtor atualizado para receber o AppDbContext
    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context; 
    }

    // Método Index atualizado (sem o debug log)
    public IActionResult Index()
    {
        // Busca os produtos e suas categorias no banco
        var produtos = _context.Produtos
                               .Include(p => p.Categoria) 
                               .ToList();
        
        // Envia a lista de produtos para a View
        return View(produtos);
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