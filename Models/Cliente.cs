using System.Text.Json.Serialization;

namespace WebApiProjeto.Models;

public class Cliente
{
    public int Id { get; set; }

    public string Nome { get; set; }
    public string SobreNome { get; set; }

    public string Telefone { get; set; }
    [JsonIgnore]
    public ICollection<Orcamento> Orcamentos { get; set; }

    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
}