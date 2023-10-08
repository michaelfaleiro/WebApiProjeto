using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiProjeto.Extensions;
using WebApiProjeto.Models;
using WebApiProjeto.ViewModels;
using WebApiProjeto.ViewModels.Contato;

namespace WebApiProjeto.Controllers;

[ApiController]
[Route("v1")]
public class ContatoController : ControllerBase
{
    [HttpPost("contatos")]
    public async Task<IActionResult> NovoContatoAsync(
        [FromServices] AppDbContext context,
        [FromBody] EditorContatoViewModel model
        )
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Contato>(ModelState.GetErrors()));

            var contato = new Contato
            {
                Telefone = model.Telefone,
                ClienteId = model.ClienteId,
            };

            await context.Contatos.AddAsync(contato);
            await context.SaveChangesAsync();

            return Created($"v1/contatos/{contato.Id}", new ResultViewModel<Contato>(contato));
                
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, new ResultViewModel<Contato>("Erro ao criar"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Contato>("Erro interno no servidor"));
        }
    }

    [HttpGet("contatos")]
    public async Task<IActionResult> ListContatoAsync(
        [FromServices] AppDbContext context,
        [FromQuery] int page = 0,
        [FromQuery] int pageSize = 25
        )
    {
        try
        {
            var count = await context.Contatos.CountAsync();
            var contatos = await context.Contatos
                .AsNoTracking()
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new ResultViewModel<dynamic>(new
            {
                total = count,
                page,
                pageSize,
                contatos
            }));
        }
        catch (DbUpdateException)
        {

            return StatusCode(500, new ResultViewModel<List<Cliente>>("Não foi buscar os dados"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<List<Cliente>>("Falha interna no Servidor"));
        }
    }
}
