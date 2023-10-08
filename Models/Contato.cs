using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApiProjeto.Models;

public class Contato
{
    public int Id { get; set; }
    public string Telefone { get; set; }

    [ForeignKey("ClienteId")]
    public int ClienteId { get; set; }
    [JsonIgnore]
    public Cliente Clientes { get; set; }
    [JsonIgnore]
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;


}