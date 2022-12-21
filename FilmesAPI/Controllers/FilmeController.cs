using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
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
    public IEnumerable<ReadFilmeDto> RecuperaFilmes(
        [FromQuery] int skip = 0,
        [FromQuery] int take = 2
        )
    {
        // CMT: a opção por retornar um IEnumerable em vez de uma List
        // é melhor, pois IEnumerable é mais abstrato, e assim previne
        // ter de refatorar o código caso o tipo de Filmes mude

        // CMT: [FromQuery] é o dado enviado na URL após um "?", cuja
        // função é definir critérios para busca.
        // As tecnologias do .NET relativas às queries pertecem ao LINQ

        // CMT: a Paginação é a técnica de filtrar os
        // dados obtidos da base de dados. Ela serve para reduzir
        // o consumo da memória ou o custo financeiro de uma consulta
        // à db hospedada em algum serviço. No .NET, o método
        // Skip() evita o nro de elementos para iniciar a consulta, e o Take() 
        // puxa um nro de elementos fixado

        // CMT: sobre mapeamento da entidade, ver HttpGet("{id}")

        var filmes = _context.Filmes.Skip(skip).Take(take);

        return _mapper.Map<List<ReadFilmeDto>>(filmes);
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaFilmePorId(
        int id
        )
    {
        // CMT: "{id}" é o dado enviado na URL após um "/",
        // isto é, é o parâmetro da requisição

        // CMT: validar se filme existe; se filme não for encontrado,
        // retornar 404

        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null ) return NotFound();

        // CMT: mapear a entidade para o DTO, porque interessa ao cliente
        // retornar o DTO

        var filmeDto = _mapper.Map<ReadFilmeDto>(filme);

        // CMT: retornar 200 com os dados da consulta

        return Ok(filmeDto);
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaFilme(
        int id,
        [FromBody] UpdateFilmeDto filmeDto
        )
    {
        var filme = _context.Filmes.FirstOrDefault(
            filme => filme.Id == id);

        if (filme == null) return NotFound();

        _mapper.Map(filmeDto, filme);
        _context.SaveChanges();

        // CMT: o retorno de um PUT deve ser 204 - No Content

        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult AtualizaFilmeParcial(
        int id,
        JsonPatchDocument<UpdateFilmeDto> patch
        )
    {
        // CMT: para processar operações Patch, é necessário
        // instalar o Microsoft.AspNetCore.MVC.NewtonsoftJson

        // CMT: o corpo da requisição deve seguir o exemplo:
        // [ { "op": "replace", "path": "/duracao", "value": "200" } ]

        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();

        // CMT: para realizar o patch, é necessário mapear uma versão
        // dto da entidade que será modificada pelo patch

        var filmeParaAtualizar = _mapper.Map<UpdateFilmeDto>(filme);

        patch.ApplyTo(filmeParaAtualizar, ModelState);

        // CMT: é necessário validar se os dados do patch podem
        // ser aplicados ao objeto a ser atualizado

        if (!TryValidateModel(filmeParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }

        // CMT: agora é possível alterar na base de dados

        _mapper.Map(filmeParaAtualizar, filme);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletaFilme(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();

        _context.Filmes.Remove(filme);
        _context.SaveChanges();

        return NoContent();
    }
}
