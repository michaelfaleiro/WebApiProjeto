using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiProjeto.Extensions;
using WebApiProjeto.Models;
using WebApiProjeto.ViewModels;
using WebApiProjeto.ViewModels.Clientes;

namespace WebApiProjeto.Controllers;

[ApiController]
[Route("v1/api")]
public class ClienteController : ControllerBase
{


    [HttpPost("clientes")]
    public async Task<IActionResult> NovoClienteAsync(
        [FromServices] AppDbContext context,
        [FromBody] EditorClienteViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<Cliente>(ModelState.GetErrors()));

        try
        {
            Cliente cliente = new()
            {
                Nome = model.Nome,
                SobreNome = model.SobreNome,
                Telefone = model.Telefone
            };
            await context.Clientes.AddAsync(cliente);
            await context.SaveChangesAsync();


            return Created($"v1/{cliente.Id}", new ResultViewModel<Cliente>(cliente));
        }
        catch (DbUpdateException)
        {

            return StatusCode(500, new ResultViewModel<List<Cliente>>("Não foi possível adicionar novo cliente"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<List<Cliente>>("Falha interna no Servidor"));
        }
    }

    [HttpGet("clientes")]
    public async Task<IActionResult> GetClientesAsync(
        [FromServices] AppDbContext context,
        [FromQuery] int page = 0,
        [FromQuery] int pageSize = 25
        )
    {
        try
        {
            var count = await context.Clientes.AsNoTracking().CountAsync();
            var clientes = await context.Clientes
                .AsNoTracking()
                .Include(c => c.Orcamentos)
                .Select(x => new 
                {
                    x.Id,
                    x.Nome,
                    x.SobreNome,
                    x.Telefone,
                    Orcamentos = x.Orcamentos.Select(o => new
                    {
                        o.Id,
                    })

                })
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (clientes == null)
                return NotFound();

            return Ok(new ResultViewModel<dynamic>(new
            {
                total = count,
                page,
                pageSize,
                clientes,
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

    [HttpGet("clientes/{id:int}")]
    public async Task<IActionResult> GetByIdClienteAsync([FromServices] AppDbContext context, int id)
    {
        try
        {
            var cliente = await context.Clientes
                .AsNoTracking()
                .Include(o => o.Orcamentos)
                .Select(x => new 
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    SobreNome = x.SobreNome,
                    Telefone = x.Telefone,
                    Orcamentos = x.Orcamentos.Select(o => new
                    {
                        o.Id,
                    })
                })
                .FirstOrDefaultAsync(x => x.Id == id);


            if (cliente == null)
                return NotFound(new ResultViewModel<Cliente>("Não foi encontrado"));

            return Ok(new ResultViewModel<dynamic>(new
            {
                cliente
            }));
        }
        catch (DbUpdateException)
        {

            return StatusCode(500, new ResultViewModel<Cliente>("Não foi buscar os dados"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Cliente>("Falha interna no Servidor"));
        }
    }

    [HttpPut("clientes/{id:int}")]
    public async Task<IActionResult> PutAsync(
        [FromRoute] int id,
        [FromBody] EditorClienteViewModel model,
        [FromServices] AppDbContext context)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Cliente>(ModelState.GetErrors()));

            var cliente = await context.Clientes.FirstOrDefaultAsync(x => x.Id == id);

            if (cliente == null)
                return NotFound(new ResultViewModel<Cliente>("Não Encontrado"));

            cliente.Nome = model.Nome;
            cliente.SobreNome = model.SobreNome;
            cliente.Telefone = model.Telefone;

            context.Clientes.Update(cliente);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Cliente>(cliente));

        }
        catch (DbUpdateException)
        {

            return StatusCode(500, new ResultViewModel<Cliente>("Não foi atualizar os dados"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Cliente>("Falha interna no Servidor"));
        }
    }

    [HttpDelete("clientes/{id:int}")]
    public async Task<IActionResult> DeleteClienteAsync(int id, [FromServices] AppDbContext context)
    {
        try
        {
            var cliente = await context.Clientes.FirstOrDefaultAsync(x => x.Id == id);

            if (cliente == null) return BadRequest(new ResultViewModel<Cliente>("Não encontrado"));

            context.Clientes.Remove(cliente);
            await context.SaveChangesAsync();

            return Ok();
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, new ResultViewModel<Cliente>("Não foi possível excluir"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Cliente>("Falha interna no Servidor"));
        }
    }

}