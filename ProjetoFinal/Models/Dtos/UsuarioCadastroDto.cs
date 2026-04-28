// Formulário que o frontend preenche e a API responde

using System.Text.Json.Serialization;

namespace ProjetoFinal.Models.Dtos;

public class UsuarioCadastroDto
{
    [JsonPropertyName("nome")]
    public string Nome { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("senha")]
    public string Senha { get; set; } = string.Empty;

    [JsonPropertyName("cargo")]
    public string? Cargo { get; set; }
}