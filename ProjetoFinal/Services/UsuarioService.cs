// Regras de negócio

using System.Security.Cryptography;
using System.Text;
using ProjetoFinal.Models;
using ProjetoFinal.Models.Dtos;
using ProjetoFinal.Interfaces;

namespace ProjetoFinal.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _repository;

    public UsuarioService(IUsuarioRepository repository)
    {
        _repository = repository;
    }

    private static string Hash(string senha) =>
        Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(senha)));

    private static UsuarioRespostaDto Map(Usuario u) => new()
    {
        Id = u.Id,
        Nome = u.Nome,
        Email = u.Email,
        Ativo = u.Ativo,
        CriadoEm = u.CriadoEm.ToString("dd/MM/yyyy HH:mm:ss"),
        AtualizadoEm = u.AtualizadoEm?.ToString("dd/MM/yyyy HH:mm:ss"),
        Cargo = u.Cargo
    };

    public async Task<UsuarioRespostaDto?> Cadastrar(UsuarioCadastroDto dto)
    {
        if (await _repository.EmailExiste(dto.Email))
            throw new InvalidOperationException("Email já existe");

        var user = new Usuario
        {
            Nome = dto.Nome,
            Email = dto.Email.ToLower(),
            SenhaHash = Hash(dto.Senha),
            Cargo = dto.Cargo
        };
        await _repository.Adicionar(user);
        return Map(user);
    }

    public async Task<List<UsuarioRespostaDto>> Listar() =>
        (await _repository.ObterTodos()).Select(Map).ToList();

    public async Task<UsuarioRespostaDto?> BuscarPorId(Guid id)
    {
        var u = await _repository.ObterPorId(id);
        return u?.Ativo == true ? Map(u) : null;
    }

    public async Task<UsuarioRespostaDto?> Atualizar(Guid id, UsuarioAtualizacaoDto dto)
    {
        var u = await _repository.ObterPorId(id);
        if (u == null || !u.Ativo) return null;

        if (!string.IsNullOrWhiteSpace(dto.Nome)) u.Nome = dto.Nome;
        if (dto.Cargo != null) u.Cargo = dto.Cargo;
        u.AtualizadoEm = DateTime.Now;
        await _repository.Atualizar(u);
        return Map(u);
    }

    public async Task<bool> Desativar(Guid id)
    {
        var u = await _repository.ObterPorId(id);
        if (u == null || !u.Ativo) return false;
        u.Ativo = false;
        u.AtualizadoEm = DateTime.Now;
        await _repository.Atualizar(u);
        return true;
    }

    public async Task<bool> Ativar(Guid id)
    {
        var u = await _repository.ObterPorId(id);
        if (u == null || u.Ativo) return false;
        u.Ativo = true;
        u.AtualizadoEm = DateTime.Now;
        await _repository.Atualizar(u);
        return true;
    }
}