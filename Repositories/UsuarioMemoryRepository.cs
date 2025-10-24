using EmporioIrmasDaTerra.Models;
using System.Collections.Generic;
using System.Linq;

namespace EmporioIrmasDaTerra.Repositories
{
    public class UsuarioMemoryRepository : IUsuarioRepository
    {
        private static List<Usuario> listaUsuarios = new List<Usuario>();

        public void Create(Usuario usuario)
        {
            usuario.UsuarioId = listaUsuarios.Any() ? listaUsuarios.Max(u => u.UsuarioId) + 1 : 1;

            // Define o primeiro usuário como Admin, todos os outros como Cliente
            if (!listaUsuarios.Any())
            {
                usuario.Papel = "Admin";
            }
            else
            {
                usuario.Papel = "Cliente";
            }

            listaUsuarios.Add(usuario);
        }

        public Usuario? Read(int id)
        {
            return listaUsuarios.FirstOrDefault(u => u.UsuarioId == id);
        }

        public Usuario? ReadByEmailAndSenha(string email, string senha)
        {
            return listaUsuarios.FirstOrDefault(u => u.Email == email && u.Senha == senha);
        }

        public Usuario? ReadByEmail(string email)
        {
            return listaUsuarios.FirstOrDefault(u => u.Email == email);
        }
}
}