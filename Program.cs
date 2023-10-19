using Data;
using System.Text.Json.Serialization;
using WebApiProjeto.Data.Interface;
using WebApiProjeto.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

ServicesMvc(builder);


var app = builder.Build();

app.MapControllers();

app.Run();

static void ServicesMvc(WebApplicationBuilder builder)
{
 
    
    builder.Services
    .AddControllers()
    .ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        })
    .AddJsonOptions(x =>
        {
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });
    builder.Services.AddDbContext<AppDbContext>();
    builder.Services.AddScoped<IProdutoInterface, ProdutoService>();
}