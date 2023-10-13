using Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiProjeto.Extensions;
using WebApiProjeto.Models;
using WebApiProjeto.ViewModels;
using WebApiProjeto.ViewModels.Produtos;

namespace WebApiProjeto.Controllers;

[ApiController]
[Route("v1/api")]
public class ProdutoController : ControllerBase
{
    [HttpPost("produtos")]
    public async Task<IActionResult> NovoProdutoAsync(
        [FromServices] AppDbContext context,
        [FromBody] EditorProdutoViewModel model
    )
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResultViewModel<Produto>(ModelState.GetErrors()));
            }

            Produto produto = new()
            {
                Nome = model.Nome,
                Marca = model.Marca,
                Sku = model.Sku,
                PrecoVenda = model.PrecoVenda,
            };

            await context.Produtos.AddAsync(produto);
            await context.SaveChangesAsync();

            return Created($"v1/api/produtos/{produto.Id}", new ResultViewModel<Produto>(produto));

        }
        catch (DbUpdateException)
        {
            return StatusCode(500, new ResultViewModel<Produto>("Não foi possível salvar os dados"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Produto>("Erro interno no servidor"));
        }
    }
}
