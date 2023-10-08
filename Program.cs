using Data;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>();

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
       
}