namespace WebApiProjeto.Models;

public class Orcamento
{
    public int Id { get; set; }

    public Cliente Cliente { get; set; }

    public List<Produto> Produtos { get; set; }

    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

}
