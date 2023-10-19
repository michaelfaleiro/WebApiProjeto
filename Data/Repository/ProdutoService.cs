using Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebApiProjeto.Data.Interface;
using WebApiProjeto.Models;

namespace WebApiProjeto.Data.Repository;

public class ProdutoService : IProdutoInterface
{
    private readonly AppDbContext _context;

    public ProdutoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Produto> CreateProduto(Produto produto)
    {
        await _context.Produtos.AddAsync(produto);
        await _context.SaveChangesAsync();

        return produto;
    }

    public async Task<IEnumerable<Produto>> GetProdutos()
    {
        return await _context.Produtos.AsNoTracking().ToListAsync();
    }

    async Task<Produto> IProdutoInterface.GetProdutoById(int id)
    {
         var produto = await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        return produto;
    }
}
