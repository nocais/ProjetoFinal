// Formulário que o frontend preenche e a API responde

using System.Text.Json.Serialization;

namespace ProjetoFinal.Models.Dtos;

public class UsuarioRespostaDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("nome")]
    public string Nome { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("ativo")]
    public bool Ativo { get; set; }

    [JsonPropertyName("criado_em")]
    public string CriadoEm { get; set; } = string.Empty;

    [JsonPropertyName("atualizado_em")]
    public string? AtualizadoEm { get; set; }

    [JsonPropertyName("cargo")]
    public string? Cargo { get; set; }
}