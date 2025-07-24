using LocaGames_API.Infra;
using Microsoft.AspNetCore.Connections;
using Senac.GerenciamentoLocaGames.Domain.Repositories;
using Senac.GerenciamentoLocaGames.Domain.Servicos;
using Senac.GerenciamentoLocaGames.Infra.Data.DatabaseConfiguration;

// Add services to the container.
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IDbConnectionFactory>(x =>
{
    return new DbConnectionFactory("Server=(localdb)\\MSSQLLocalDB; Database=gerenciamento_locadora; Trusted_Connection=True;");
});
builder.Services.AddScoped<IJogoService, JogoService>();
builder.Services.AddScoped<IJogoRepository, JogoRepository>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
