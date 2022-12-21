using FilmesAPI.Data;
using Microsoft.EntityFrameworkCore;

// CMT: o Program.cs é o arquivo inicializador da aplicação

var builder = WebApplication.CreateBuilder(args);

// # Adicionar conexão ao banco de dados

var connectionString = builder.Configuration.GetConnectionString("FilmeConnection");

builder.Services.AddDbContext<FilmeContext>(options =>
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)
        ));
// CMT: o método ServerVersion é necessário por causa do conflito de versões
// do .NET e o MySql da Pomelo

// # Adicionar AutoMapper na aplicação
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.

// CMT: Eu adicionei o AddNewtonSoftJson para viabilizar requisições Patch
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
