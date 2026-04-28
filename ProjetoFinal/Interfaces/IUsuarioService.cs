// Contrato das regras de negócio

using ProjetoFinal.Models.Dtos;

namespace ProjetoFinal.Interfaces;

public interface IUsuarioService
{
    Task<UsuarioRespostaDto?> Cadastrar(UsuarioCadastroDto dto);
    Task<List<UsuarioRespostaDto>> Listar();
    Task<UsuarioRespostaDto?> BuscarPorId(Guid id);
    Task<UsuarioRespostaDto?> Atualizar(Guid id, UsuarioAtualizacaoDto dto);
    Task<bool> Desativar(Guid id);
    Task<bool> Ativar(Guid id);
}