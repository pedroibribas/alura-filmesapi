using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")] // CMT: a rota é o nome precedente ao sufixo Controller
public class FilmeController : ControllerBase // CMT: extende ControllerBase
{
    // CMT: inicializar lista para guardar dados in-memory
    private static List<Filme> filmes = new List<Filme>();
    private static int id = 0;

    // # Adicionar recurso na DB
    [HttpPost]
    public void AdicionaFilme([FromBody] Filme filme)
    {
        filme.Id = id++;
        filmes.Add(filme);
    }

    // # Recuperar/ler recursos da DB
    [HttpGet]
    public IEnumerable<Filme> RecuperaFilmes()
    {
        return filmes;

        // CMT: a opção por retornar um IEnumerable em vez de uma List
        // é melhor, pois IEnumerable é mais abstrato, e assim previne
        // ter de refatorar o código caso a tipagem de Filmes mude
    }

    [HttpGet("{id}")] // CMT: recebe o valor id como parâmetro da requisição
    public Filme? RecuperaFilmePorId(int id)
    {
        return filmes.FirstOrDefault(filme => filme.Id == id);
    }
}
