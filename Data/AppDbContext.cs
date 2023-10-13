using Microsoft.EntityFrameworkCore;
using WebApiProjeto.Models;

namespace Data;

public class AppDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<OrcamentoProduto> OrcamentoProdutos { get; set; }
    public DbSet<Orcamento> Orcamentos { get; set; }
    public DbSet<Produto> Produtos { get; set; }

    public AppDbContext(IConfiguration configuration, DbContextOptions options) : base(options)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration.GetConnectionString("Connection");
        optionsBuilder.UseNpgsql(connectionString);
    }

}

