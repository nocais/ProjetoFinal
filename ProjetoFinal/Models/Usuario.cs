// Estrutura do usuário, como ele é guardado

using System.Text.Json.Serialization;

namespace ProjetoFinal.Models;

public class Usuario
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    [JsonIgnore]
    public string SenhaHash { get; set; } = string.Empty;

    public bool Ativo { get; set; } = true;
    public DateTime CriadoEm { get; set; } = DateTime.Now;
    public DateTime? AtualizadoEm { get; set; }
    public string? Cargo { get; set; }
}