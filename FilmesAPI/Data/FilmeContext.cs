using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

// CMT: O sufixo Context representa o contexto em que o domínio
// e o banco de dados se relacionam

namespace FilmesAPI.Data;

public class FilmeContext : DbContext // extende DbContext
{
	// # Configuração do acesso
	// CMT: O construtor recebe configurações de acesso ao banco,
	// isto é, as opções de contexto relativas ao FilmeContext,
	// e passa essas opções para o construtor da classe extendida DbContext,
	// através do base(options)
	public FilmeContext(DbContextOptions<FilmeContext> options)
		: base(options)
	{
	}

	// # Propriedades para acesso

	// CMT: DbSet represeta um conjunto de dados oriundos da Db

	public DbSet<Filme> Filmes { get; set; }
}
