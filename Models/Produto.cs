namespace WebApiProjeto.Models;

public class Produto
{
    public int Id { get; set; }
    public string Sku { get; set; }
    public string Nome { get; set; }
    public string Marca { get; set; }
    public double PrecoVenda { get; set; }
    public ICollection<OrcamentoProduto> OrcamentoProdutos { get; set; }

    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
}
