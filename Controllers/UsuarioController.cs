// Controllers/UsuarioController.cs
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

        // ... (Actions de Cadastro, Login e Logout permanecem as mesmas) ...
        // ... (A lógica usa Session e Repository, como definido anteriormente) ...

        // Exemplo da lógica de LOGIN com redirecionamento:
        [HttpPost]
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

            // Redirecionamento baseado no Papel
            if (usuario.Papel == "Admin")
            {
                return RedirectToAction("Index", "Admin");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}