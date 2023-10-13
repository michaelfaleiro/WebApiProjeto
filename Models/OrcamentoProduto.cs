using System.Text.Json.Serialization;

namespace WebApiProjeto.Models;

public class OrcamentoProduto
{
    public int Id { get; set; }
    public int OrcamentoId { get; set; }
    [JsonIgnore]
    public Orcamento Orcamento { get; set; }
    public int ProdutoId { get; set; }
    [JsonIgnore]
    public Produto Produto { get; set; }
}
