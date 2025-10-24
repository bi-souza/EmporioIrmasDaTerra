using EmporioIrmasDaTerra.Models;
using System.Collections.Generic;

namespace EmporioIrmasDaTerra.Repositories 
{
    public interface IUsuarioRepository
    {
        // Métodos de Repositório (que o Controller usa)
        void Create(Usuario usuario);
        Usuario Read(int id);
        Usuario ReadByEmailAndSenha(string email, string senha);
        Usuario ReadByEmail(string email);
    }
}   