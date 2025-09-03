using Microsoft.EntityFrameworkCore;
using EstoqueService.Data;
using Microsoft.Extensions.Configuration;
using EstoqueService.Models;


var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true)
    .AddJsonFile("appsettings.Test.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

var builder = WebApplication.CreateBuilder(args);

// Usa a configuração carregada manualmente
var useInMemory = configuration.GetValue<bool>("UseInMemory");

if (useInMemory)
{
    builder.Services.AddDbContext<EstoqueDbContext>(options =>
        options.UseInMemoryDatabase("TestDb")); // ✅ Banco em memória para testes
}
else
{
    builder.Services.AddDbContext<EstoqueDbContext>(options =>
        options.UseSqlite(configuration.GetConnectionString("EstoqueConnection"))); // ✅ SQLite para produção
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<EstoqueDbContext>();

    // Verifica se já existe algum usuário
    if (!db.Usuarios.Any())
    {
        // Cria um usuário padrão para testes
        db.Usuarios.Add(new Usuario
        {
            Email = "admin@teste.com",
            Senha = "admin123" // Use hash se o sistema exigir criptografia
        });

        db.SaveChanges();
    }
}

app.Run();

public partial class Program { }
