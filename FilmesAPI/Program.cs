using FilmesAPI.Data;
using Microsoft.EntityFrameworkCore;

// CMT: o Program.cs � o arquivo inicializador da aplica��o

var builder = WebApplication.CreateBuilder(args);

// # Adicionar conex�o ao banco de dados

var connectionString = builder.Configuration.GetConnectionString("FilmeConnection");

builder.Services.AddDbContext<FilmeContext>(options =>
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)
        ));
// CMT: o m�todo ServerVersion � necess�rio por causa do conflito de vers�es
// do .NET e o MySql da Pomelo

// # Adicionar AutoMapper na aplica��o
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.

// CMT: Eu adicionei o AddNewtonSoftJson para viabilizar requisi��es Patch
builder.Services.AddControllers().AddNewtonsoftJson();
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
