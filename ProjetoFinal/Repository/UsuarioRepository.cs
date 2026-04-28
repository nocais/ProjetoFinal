// Simula o banco de dados, guardando e buscando usuários

using System.Collections.Concurrent;
using ProjetoFinal.Models;
using ProjetoFinal.Interfaces;

namespace ProjetoFinal.Repository;

public class UsuarioRepository : IUsuarioRepository
{
    private static readonly ConcurrentDictionary<Guid, Usuario> _db = new();
    private static readonly ConcurrentDictionary<string, Guid> _emailIndex = new();

    public Task<Usuario?> ObterPorId(Guid id) =>
        Task.FromResult(_db.TryGetValue(id, out var u) ? u : null);

    public Task<Usuario?> ObterPorEmail(string email)
    {
        if (_emailIndex.TryGetValue(email.ToLower(), out var id))
            return Task.FromResult(_db.TryGetValue(id, out var u) ? u : null);
        return Task.FromResult<Usuario?>(null);
    }

    public Task<List<Usuario>> ObterTodos() =>
        Task.FromResult(_db.Values.Where(u => u.Ativo).ToList());

    public Task Adicionar(Usuario u)
    {
        _db[u.Id] = u;
        _emailIndex[u.Email.ToLower()] = u.Id;
        return Task.CompletedTask;
    }

    public Task Atualizar(Usuario u) => Task.FromResult(_db[u.Id] = u);

    public Task<bool> EmailExiste(string email, Guid? ignore = null)
    {
        var exists = _emailIndex.ContainsKey(email.ToLower());
        if (exists && ignore.HasValue)
        {
            var u = ObterPorId(_emailIndex[email.ToLower()]).Result;
            exists = u?.Id != ignore.Value;
        }
        return Task.FromResult(exists);
    }
}