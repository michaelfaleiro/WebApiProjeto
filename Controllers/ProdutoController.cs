using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiProjeto.Data.Interface;
using WebApiProjeto.Extensions;
using WebApiProjeto.Models;
using WebApiProjeto.ViewModels;
using WebApiProjeto.ViewModels.Produtos;

namespace WebApiProjeto.Controllers;

[ApiController]
[Route("v1/api")]
public class ProdutoController : ControllerBase
{

   
    private readonly IProdutoInterface _produtoInterface;

    public ProdutoController( IProdutoInterface produtoInterface)
    {
        
        _produtoInterface = produtoInterface;
    }

    [HttpPost("produtos")]
    public async Task<IActionResult> NovoProdutoAsync(
        [FromBody] EditorProdutoViewModel model
    )
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResultViewModel<Produto>(ModelState.GetErrors()));
            }

            Produto produto = new(
                model.Sku,
                model.Nome,
                model.Marca,
                model.PrecoVenda
                );

          
            await _produtoInterface.CreateProduto(produto);

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

    [HttpGet("produtos")]

    public async Task<IActionResult> GetProdutosAsync()
    {
        try
        {
            var produtos = await _produtoInterface.GetProdutos();

            return Ok(new ResultViewModel<dynamic>(produtos));
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

    [HttpGet("produtos/{id:int}")]

    public async Task<IActionResult> GetProdutoByIdAsync(
         [FromRoute] int id
        )
    {
        try
        {
            var produtos = await _produtoInterface.GetProdutoById(id);

            if(produtos != null)
                return Ok(new ResultViewModel<dynamic>(produtos));

            return NotFound(new ResultViewModel<Produto>("Não Encontrado!"));
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
