using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")] // CMT: a rota é o nome precedente ao sufixo Controller
public class FilmeController : ControllerBase // CMT: extende ControllerBase
{
    // Inicializar instância do contexto
    private FilmeContext _context;    
    // Inicializar instância do AutoMapper
    private IMapper _mapper;

    // Implementar contexto e mapper via DI
    public FilmeController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // CMT: inicializar lista para guardar dados in-memory
    //private static List<Filme> filmes = new List<Filme>();
    //private static int id = 0;

    [HttpPost]
    public IActionResult AdicionaFilme(
        [FromBody] CreateFilmeDto filmeDto
        )
    {
        //filme.Id = id++;
        //filmes.Add(filme);

        Filme filme = _mapper.Map<Filme>( filmeDto );

        _context.Filmes.Add(filme);
        _context.SaveChanges();

        // CMT: para um Post, a boa prática é retornar o
        // elemento criado e a localização de como recuperá-lo depois.
        // No .NET, usa-se o método CreatedAtAction(método?, params?, elemento?)
        // para isso. Ele retorna um 201 e o caminho para a requisição GET
        // em Headers > Location

        return CreatedAtAction(nameof(RecuperaFilmePorId),
            new { id = filme.Id },
            filme);
    }

    [HttpGet]
    public IEnumerable<Filme> RecuperaFilmes(
        [FromQuery] int skip = 0,
        [FromQuery] int take = 2
        )
    {
        // CMT: a opção por retornar um IEnumerable em vez de uma List
        // é melhor, pois IEnumerable é mais abstrato, e assim previne
        // ter de refatorar o código caso o tipo de Filmes mude

        // CMT: [FromQuery] é o dado enviado na URL após um "?", cuja
        // função é definir critérios para busca

        // CMT: a Paginação é a técnica de filtrar os
        // dados obtidos da base de dados. Ela serve para reduzir
        // o consumo da memória ou o custo financeiro de uma consulta
        // à db hospedada em algum serviço. No .NET, o método
        // Skip() evita o nro de elementos para iniciar a consulta, e o Take() 
        // puxa um nro de elementos fixado

        return _context.Filmes.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaFilmePorId(
        int id
        )
    {
        // CMT: "{id}" é o dado enviado na URL após um "/",
        // isto é, é o parâmetro/query da requisição, cuja finalidade
        // é filtrar os dados obtidos. As tecnologias do .NET relativas
        // às queries pertecem ao LINQ

        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

        // se filme não for encontrado, retornar 404; senão, retornar 200 e o filme
        if (filme == null ) return NotFound();
        return Ok(filme);
    }
}
