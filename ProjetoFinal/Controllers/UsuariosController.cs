// Recebe as requisições para o Service

using Microsoft.AspNetCore.Mvc;
using ProjetoFinal.Filters;
using ProjetoFinal.Models;
using ProjetoFinal.Models.Dtos;
using ProjetoFinal.Interfaces;

namespace ProjetoFinal.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[ServiceFilter(typeof(ValidacaoFilter))]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioService _service;
    private readonly ILogger<UsuariosController> _logger;

    public UsuariosController(IUsuarioService service, ILogger<UsuariosController> logger)
    {
        _service = service;
        _logger = logger;
    }

    private IActionResult Resposta(object dados)
    {
        var inicio = HttpContext.Items["StartTime"] as DateTime? ?? DateTime.Now;
        var tempo = (DateTime.Now - inicio).TotalMilliseconds;

        return Ok(new RespostaApi
        {
            DadosResposta = dados,
            TimestampResposta = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
            TempoDaResposta = $"{tempo:F0} ms"
        });
    }

    [HttpPost]
    public async Task<IActionResult> Cadastrar([FromBody] UsuarioCadastroDto request)
    {
        try
        {
            return Resposta(await _service.Cadastrar(request));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(Resposta(new { erro = ex.Message }));
        }
    }

    [HttpGet]
    public async Task<IActionResult> Listar() => Resposta(await _service.Listar());

    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarPorId(Guid id)
    {
        var u = await _service.BuscarPorId(id);
        return u == null ? NotFound(Resposta(new { erro = "Não encontrado" })) : Resposta(u);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] UsuarioAtualizacaoDto request)
    {
        var u = await _service.Atualizar(id, request);
        return u == null ? NotFound(Resposta(new { erro = "Não encontrado ou inativo" })) : Resposta(u);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Desativar(Guid id)
    {
        return await _service.Desativar(id)
            ? Resposta(new { mensagem = "Usuário desativado com sucesso" })
            : NotFound(Resposta(new { erro = "Usuário não encontrado ou já inativo" }));
    }

    [HttpPost("{id}/ativar")]
    public async Task<IActionResult> Ativar(Guid id)
    {
        return await _service.Ativar(id)
            ? Resposta(new { mensagem = "Usuário ativado com sucesso" })
            : NotFound(Resposta(new { erro = "Usuário não encontrado ou já ativo" }));
    }
}