using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjetoFinal.Models.Dtos;

public class UsuarioCadastroDto
{
    [JsonPropertyName("nome")]
    [Required(ErrorMessage = "Nome é obrigatório")]
    [MinLength(3, ErrorMessage = "Nome deve ter pelo menos 3 caracteres")]
    [MaxLength(100, ErrorMessage = "Nome deve ter no máximo 100 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Digite um email válido")]
    [MaxLength(150, ErrorMessage = "Email deve ter no máximo 150 caracteres")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("senha")]
    [Required(ErrorMessage = "Senha é obrigatória")]
    [MinLength(6, ErrorMessage = "Senha deve ter pelo menos 6 caracteres")]
    [MaxLength(50, ErrorMessage = "Senha deve ter no máximo 50 caracteres")]
    public string Senha { get; set; } = string.Empty;

    [JsonPropertyName("cargo")]
    [MaxLength(50, ErrorMessage = "Cargo deve ter no máximo 50 caracteres")]
    public string? Cargo { get; set; }
}