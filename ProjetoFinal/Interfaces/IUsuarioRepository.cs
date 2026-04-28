// Contrato do repositório

using ProjetoFinal.Models;

namespace ProjetoFinal.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario?> ObterPorId(Guid id);
    Task<Usuario?> ObterPorEmail(string email);
    Task<List<Usuario>> ObterTodos();
    Task Adicionar(Usuario usuario);
    Task Atualizar(Usuario usuario);
    Task<bool> EmailExiste(string email, Guid? idIgnorar = null);
}