// Repositories/IUsuarioRepository.cs

using EmporioIrmasDaTerra.Models;
using System.Collections.Generic;

namespace EmporioIrmasDaTerra.Repositories 
{
    public interface IUsuarioRepository
    {
        void Create(Usuario usuario);
        // ATENÇÃO: Adicionar '?'
        Usuario? Read(int id); 
        // ATENÇÃO: Adicionar '?'
        Usuario? ReadByEmailAndSenha(string email, string senha);
        // ATENÇÃO: Adicionar '?'
        Usuario? ReadByEmail(string email); 
    }
}