using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Estacionamento.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Estabelecimentos") ?? "Data Source=Estabelecimentos.db";


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSqlite<EstabelecimentoDb>(connectionString);
builder.Services.AddSqlite<VeiculoDb>(connectionString);
builder.Services.AddSwaggerGen(c => 
{
    c.SwaggerDoc("v1", new OpenApiInfo {
        Title = "API Estacionamento",
        Description = "Apenas um humilde estacionamento :^]",
        Version = "v1" });
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Estacionamento V1");
    });
}

app.MapGet("/", () => "Hello World!");
app.MapGet("/estabelecimentos", async (EstabelecimentoDb db) => await db.Estabelecimentos.ToListAsync());
app.MapGet("/veiculos", async (VeiculoDb db) => await db.Veiculos.ToListAsync());
app.MapPost("/estabelecimento", async (EstabelecimentoDb db, Estabelecimento estabelecimento) =>
{
    await db.Estabelecimentos.AddAsync(estabelecimento);
    await db.SaveChangesAsync();
    return Results.Created($"/estabelecimento/{estabelecimento.Id}", estabelecimento);
});
app.MapPost("/veiculo", async (VeiculoDb db, Veiculo veiculo) =>
{
    await db.Veiculos.AddAsync(veiculo);
    await db.SaveChangesAsync();
    return Results.Created($"/veiculo/{veiculo.Id}", veiculo);
});
app.MapGet("/veiculo/{id}", async (VeiculoDb db, int id) => await db.Veiculos.FindAsync(id));
app.MapGet("/estabelecimento/{id}", async (EstabelecimentoDb db, int id) => await db.Estabelecimentos.FindAsync(id));
app.MapPut("/veiculo/{id}", async (VeiculoDb db, Veiculo updateveiculo, int id) =>
{
    var veiculo = await db.Veiculos.FindAsync(id);
    if (veiculo is null) return Results.NotFound();
    veiculo.Marca = updateveiculo.Marca;
    veiculo.Modelo = updateveiculo.Modelo;
    veiculo.Cor = updateveiculo.Cor;
    veiculo.Placa = updateveiculo.Placa;
    veiculo.Tipo = updateveiculo.Tipo;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapPut("/estabelecimento/{id}", async (EstabelecimentoDb db, Estabelecimento updateestabelecimento, int id) =>
{
    var estabelecimento = await db.Estabelecimentos.FindAsync(id);
    if (estabelecimento is null) return Results.NotFound();
    estabelecimento.Nome = updateestabelecimento.Nome;
    estabelecimento.CNPJ = updateestabelecimento.CNPJ;
    estabelecimento.Endereço = updateestabelecimento.Endereço;
    estabelecimento.Telefone = updateestabelecimento.Telefone;
    estabelecimento.quantidadeVagaMoto = updateestabelecimento.quantidadeVagaMoto;
    estabelecimento.quantidadeVagaCarro = updateestabelecimento.quantidadeVagaCarro;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("/veiculo/{id}", async (VeiculoDb db, int id) =>
{
   var veiculo = await db.Veiculos.FindAsync(id);
   if (veiculo is null)
   {
      return Results.NotFound();
   }
   db.Veiculos.Remove(veiculo);
   await db.SaveChangesAsync();
   return Results.Ok();
});
app.MapDelete("/estabelecimento/{id}", async (EstabelecimentoDb db, int id) =>
{
   var veiculo = await db.Estabelecimentos.FindAsync(id);
   if (veiculo is null)
   {
      return Results.NotFound();
   }
   db.Estabelecimentos.Remove(veiculo);
   await db.SaveChangesAsync();
   return Results.Ok();
});



app.Run();
