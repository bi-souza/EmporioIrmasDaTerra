using Microsoft.AspNetCore.Mvc;
using EmporioIrmasDaTerra.Models;
using EmporioIrmasDaTerra.Repositories;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace EmporioIrmasDaTerra.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository repository;

        public UsuarioController(IUsuarioRepository repository)
        {
            this.repository = repository;
        }

        // =========================================================
        // AÇÕES DE CADASTRO
        // =========================================================

        // [1] HTTP GET: Exibe a página de cadastro (Resolve o erro 404)
        public IActionResult Cadastro()
        {
            return View();
        }

        // [2] HTTP POST: Processa a submissão do formulário de Cadastro
        [HttpPost]
        public IActionResult Cadastro(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                // 1. Verifica se o e-mail já está cadastrado antes de criar
                var usuarioExistente = repository.ReadByEmail(usuario.Email);
                if (usuarioExistente != null)
                {
                    ModelState.AddModelError("Email", "Este e-mail já está cadastrado.");
                    return View(usuario);
                }

                // 2. Cria o novo usuário
                repository.Create(usuario);

                // 3. Redireciona para o Login após o sucesso
                return RedirectToAction("Login");
            }

            // Se a validação falhar, retorna a View com os dados preenchidos
            return View(usuario);
        }

        // =========================================================
        // AÇÕES DE LOGIN
        // =========================================================
        
        // [3] HTTP GET: Exibe a página de login (Necessário para a rota funcionar)
        public IActionResult Login()
        {
            return View();
        }

        // [4] HTTP POST: Lógica de autenticação (Seu código original)
        [HttpPost]
        public IActionResult Login(string email, string senha)
        {
            var usuario = repository.ReadByEmailAndSenha(email, senha);

            if (usuario == null)
            {
                ModelState.AddModelError("", "E-mail ou senha inválidos.");
                // Retorna a View Login.cshtml, caso haja erros
                return View(); 
            }

            // Autenticação (Usando Session)
            HttpContext.Session.SetInt32("UsuarioId", usuario.UsuarioId);
            HttpContext.Session.SetString("UsuarioNome", usuario.Nome);
            HttpContext.Session.SetString("UsuarioPapel", usuario.Papel);

            // Redirecionamento baseado no Papel
            if (usuario.Papel == "Admin")
            {
                return RedirectToAction("Index", "Admin");
            }

            return RedirectToAction("Index", "Home");
        }

        // =========================================================
        // AÇÃO DE LOGOUT
        // =========================================================

        // [5] HTTP GET: Encerra a sessão
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Limpa todas as chaves de sessão
            return RedirectToAction("Index", "Home");
        }
    }
}   