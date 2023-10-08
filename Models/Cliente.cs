using System.Text.Json.Serialization;

namespace WebApiProjeto.Models;

public class Cliente
{
    public int Id { get; set; }

    public string Nome { get; set; }
    public string SobreNome { get; set; }
    public List<Contato> Contatos { get; set; }

    [JsonIgnore]
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
}