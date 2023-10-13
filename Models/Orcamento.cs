namespace WebApiProjeto.Models;

public class Orcamento
{
    public int Id { get; set; }

    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; }

    public ICollection<OrcamentoProduto> OrcamentoProdutos { get; set; }

    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
}
