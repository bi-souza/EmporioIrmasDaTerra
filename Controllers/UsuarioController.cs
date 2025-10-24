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

        [HttpGet] // Exibe a página de cadastro
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost] // Processa a submissão, faz o Auto-Login e redireciona para a Home
        public IActionResult Cadastro(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var usuarioExistente = repository.ReadByEmail(usuario.Email);
                if (usuarioExistente != null)
                {
                    ModelState.AddModelError("Email", "Este e-mail já está cadastrado.");
                    return View(usuario);
                }

                repository.Create(usuario);

                // FAZ O AUTO-LOGIN APÓS O CADASTRO
                HttpContext.Session.SetInt32("UsuarioId", usuario.UsuarioId);
                HttpContext.Session.SetString("UsuarioNome", usuario.Nome);
                HttpContext.Session.SetString("UsuarioPapel", usuario.Papel);

                // Redireciona para a tela inicial (Home)
                return RedirectToAction("Index", "Home");
            }

            return View(usuario);
        }

        // =========================================================
        // AÇÕES DE LOGIN
        // =========================================================
        
        [HttpGet] // Exibe a página de login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost] // Lógica de autenticação com redirecionamento SIMPLES para a Home
        public IActionResult Login(string email, string senha)
        {
            var usuario = repository.ReadByEmailAndSenha(email, senha);

            if (usuario == null)
            {
                ModelState.AddModelError("", "E-mail ou senha inválidos.");
                return View(); 
            }

            // Autenticação (Usando Session)
            HttpContext.Session.SetInt32("UsuarioId", usuario.UsuarioId);
            HttpContext.Session.SetString("UsuarioNome", usuario.Nome);
            HttpContext.Session.SetString("UsuarioPapel", usuario.Papel);

            // REDIRECIONAMENTO SIMPLIFICADO: Sempre volta para a Home
            return RedirectToAction("Index", "Home");
        }

        // =========================================================
        // AÇÃO DE LOGOUT
        // =========================================================

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Limpa todas as chaves de sessão
            return RedirectToAction("Index", "Home");
        }
    }
}