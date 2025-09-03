using Microsoft.EntityFrameworkCore;
using VendasService.Data;
using VendasService.Services;




var builder = WebApplication.CreateBuilder(args);

// 🔧 Configurações de serviços
builder.Services.AddDbContext<VendasDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("VendasConnection")));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<EstoqueClient>();

// 🔧 OpenAPI (se estiver usando pacote compatível)
builder.Services.AddOpenApi(); // Se estiver usando NSwag ou outro pacote

var app = builder.Build();

// 🔧 Pipeline de requisição
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi(); // Se estiver usando NSwag
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
