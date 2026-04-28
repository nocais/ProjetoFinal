// Envelope de padronização das respostas

using System.Text.Json.Serialization;

namespace ProjetoFinal.Models;

public class RespostaApi
{
    [JsonPropertyName("dados_resposta")]
    public object DadosResposta { get; set; } = new();

    [JsonPropertyName("timestamp_resposta")]
    public string TimestampResposta { get; set; } = string.Empty;

    [JsonPropertyName("tempo_da_resposta")]
    public string TempoDaResposta { get; set; } = string.Empty;
}