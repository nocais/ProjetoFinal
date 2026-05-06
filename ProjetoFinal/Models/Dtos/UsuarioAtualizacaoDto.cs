using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjetoFinal.Models.Dtos;

public class UsuarioAtualizacaoDto
{
    [JsonPropertyName("nome")]
    [MinLength(3, ErrorMessage = "Nome deve ter pelo menos 3 caracteres")]
    [MaxLength(100, ErrorMessage = "Nome deve ter no máximo 100 caracteres")]
    public string? Nome { get; set; }

    [JsonPropertyName("cargo")]
    [MaxLength(50, ErrorMessage = "Cargo deve ter no máximo 50 caracteres")]
    public string? Cargo { get; set; }
}