using Microsoft.AspNetCore.Mvc;
using EmporioIrmasDaTerra.Repositories;
using EmporioIrmasDaTerra.Models;
using System.Threading.Tasks;

namespace EmporioIrmasDaTerra.Controllers 
{
    // Este controller NÃO usa autenticação, mas idealmente usaria [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;

        public AdminController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        // Ação 1: Exibir o formulário de Cadastro de Novo Produto
        public IActionResult CriarProduto()
        {
            // Retorna a View CriarProduto.cshtml com um Produto vazio
            return View();
        }

        // Ação 2: Receber os dados do formulário e salvar
        [HttpPost]
        [ValidateAntiForgeryToken] // Boa prática de segurança para formulários
        public async Task<IActionResult> CriarProduto(Produto produto)
        {
            if (ModelState.IsValid)
            {
                // Nota: Você precisará de um método Add/Create no seu IProdutoRepository
                await _produtoRepository.Add(produto);

                // Redireciona para uma lista de produtos ou para o próprio produto criado
                return RedirectToAction("Index", "Home");
            }

            // Se houver erro de validação, retorna o formulário com os dados preenchidos
            return View(produto);
        }
    }
}