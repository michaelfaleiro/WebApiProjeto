using WebApiProjeto.Models;

namespace WebApiProjeto.Data.Interface;

public interface IProdutoInterface
{
    Task<Produto> CreateProduto(Produto produto);

    Task<IEnumerable<Produto>> GetProdutos();

    Task<Produto> GetProdutoById(int id);
}
