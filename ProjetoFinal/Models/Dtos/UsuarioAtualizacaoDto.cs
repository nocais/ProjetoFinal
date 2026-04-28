// Formulário que o frontend preenche e a API responde

using System.Text.Json.Serialization;

namespace ProjetoFinal.Models.Dtos;

public class UsuarioAtualizacaoDto
{
    [JsonPropertyName("nome")]
    public string? Nome { get; set; }

    [JsonPropertyName("cargo")]
    public string? Cargo { get; set; }
}