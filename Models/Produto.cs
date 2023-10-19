namespace WebApiProjeto.Models;

public class Produto
{
    public Produto()
    {
    }

    public Produto( string sku, string nome, string marca, double precoVenda)
    {
        Sku = sku;
        Nome = nome;
        Marca = marca;
        PrecoVenda = precoVenda;
    }

    public int Id { get; private set; }
    public string Sku { get; private set; } = string.Empty;
    public string Nome { get; private set; } = string.Empty;
    public string Marca { get; private set; } = string.Empty;
    public double PrecoVenda { get; private set; }

    public List<Orcamento> Orcamentos { get; set; } = new();

    public DateTime DataCriacao { get;  private set; } = DateTime.UtcNow;
}
