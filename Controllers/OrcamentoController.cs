using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiProjeto.Extensions;
using WebApiProjeto.Models;
using WebApiProjeto.ViewModels;
using WebApiProjeto.ViewModels.Orcamentos;

namespace WebApiProjeto.Controllers;

[Route("v1/api/")]
[ApiController]
public class OrcamentoController : ControllerBase
{
    [HttpPost("orcamentos/")]
    public async Task<IActionResult> NovoOrcamentoAsync(
        [FromServices] AppDbContext context,
        [FromBody] EditorOrcamentoViewModel model
        )
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResultViewModel<Orcamento>(ModelState.GetErrors()));
            }

            var cliente = await context.Clientes.FirstOrDefaultAsync(x => x.Id == model.ClienteId);

            if (cliente == null) { return BadRequest(new ResultViewModel<Orcamento>("Cliente não encontrado")); }

            Orcamento orcamento = new()
            {
                Cliente = cliente
            };

            await context.Orcamentos.AddAsync(orcamento);
            await context.SaveChangesAsync();

            return Created("v1/api/orcamentos", new ResultViewModel<Orcamento>(orcamento));

        }
        catch (DbUpdateException)
        {
            return StatusCode(500, new ResultViewModel<Orcamento>("Não foi possível salvar"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Orcamento>("Erro interno no servidor"));

        }
    }

    [HttpPost("orcamentos/{orcamentoid:int}/produtos/{produtoid:int}")]
    public async Task<IActionResult> AddProdutoOrcamentoAsync(
        [FromServices] AppDbContext context,
        int orcamentoid,
        int produtoid
    )
    {
        try
        {
            var orcamento = await context.Orcamentos
                .Include(x => x.Produtos)
                .FirstOrDefaultAsync(x => x.Id == orcamentoid);
            if (orcamento == null)
                return BadRequest(new ResultViewModel<Orcamento>("Orçamento não encontrado"));

            var produto = await context.Produtos.FirstOrDefaultAsync(x => x.Id == produtoid);
            if (produto == null)
                return BadRequest(new ResultViewModel<Produto>("Produto não encontrado"));

            if (orcamento.Produtos.Any(x=> x.Id == produto.Id))
                return BadRequest(new ResultViewModel<Produto>("Produto já adicionado"));

            orcamento.Produtos = new List<Produto>()
            {
                produto,
            };

            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<dynamic>(new
            {
                orcamento
            }));

        }
        catch (DbUpdateException)
        {

            return StatusCode(500, new ResultViewModel<Orcamento>("Não foi possível salvar os dados"));
        }
        catch
        {

            return StatusCode(500, new ResultViewModel<Orcamento>("Erro interno no servidor"));
        }
    }

    [HttpGet("orcamentos")]
    public async Task<IActionResult> ListOrcamentosAsync(
        [FromServices] AppDbContext context
    )
    {
        try
        {
            var orcamentos = await context.Orcamentos
            .AsNoTracking()
            .Include(x => x.Cliente)
            .Include(p => p.Produtos)
            .Select(o => new
            {
                Id = o.Id,
                Cliente = o.Cliente,
                Produtos = o.Produtos.Select(p => new
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Sku = p.Sku,
                    PrecoVenda = p.PrecoVenda
                })

            })
            .ToListAsync();

            return Ok(new ResultViewModel<dynamic>(orcamentos));
        }
        catch (DbUpdateException)
        {

            return StatusCode(500, new ResultViewModel<Orcamento>("Não foi carregar os dados"));
        }
        catch
        {

            return StatusCode(500, new ResultViewModel<Orcamento>("Erro interno no servidor"));
        }
    }

    [HttpGet("orcamentos/{id:int}")]
    public async Task<IActionResult> ListOrcamentosByIdAsync(
        [FromServices] AppDbContext context,
        int id
    )
    {
        try
        {
            var orcamento = await context.Orcamentos
            .AsNoTracking()
            .Include(o => o.Cliente)
            .Include(c => c.Produtos)
            .Select(o => new
            {
                Id = o.Id,
                Cliente = o.Cliente,
                Produtos = o.Produtos

            })
            .FirstOrDefaultAsync(x => x.Id == id);

            if (orcamento == null) return BadRequest(new ResultViewModel<Orcamento>("Orçamento não encontrado"));


            return Ok(new ResultViewModel<dynamic>(new
            {
                orcamento


            }));
        }
        catch (DbUpdateException)
        {

            return StatusCode(500, new ResultViewModel<Orcamento>("Erro ao carregar os dados"));
        }
        catch
        {

            return StatusCode(500, new ResultViewModel<Orcamento>("Erro interno no servidor"));
        }
    }

    [HttpGet("orcamentos/{orcamentoid:int}/produtos")]
    public async Task<IActionResult> ListProdutosOrcamentoAsync(
        [FromServices] AppDbContext context,
        int orcamentoid
    )
    {
        try
        {

            return Ok();
        }
        catch (DbUpdateException)
        {

            return StatusCode(500, new ResultViewModel<Orcamento>("Erro ao carregar os dados"));
        }
        catch
        {

            return StatusCode(500, new ResultViewModel<Orcamento>("Erro interno no servidor"));
        }
    }


}

