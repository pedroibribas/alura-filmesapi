using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models;

// CMT: modelo é a representação do mundo real

// CMT: Data Annotations são atributos de validação usados
// no model para criar restrições à requisição enviada pelo usuário.
// Caso ocorra erro na validação, uma resposta JSON
// é retornada com status 400 (Bad Request) e a mensagem de erro.

public class Filme
{
    // # Id
    public int Id { get; set; }

    // # Título
    // Valor não pode ser nulo ou vazio
    [Required(ErrorMessage = "Título do filme é obrigatório")]
    public string Titulo { get; set; }
    
    // # Gênero
    // Valor não pode ser nulo ou vazio
    [Required()]
    // Valor deve possuir até 50 caracteres
    [MaxLength(50, ErrorMessage = "Gênero deve conter até 50 caracteres")]
    public string Genero { get; set; }
    
    // # Duração
    // Valor não pode ser nulo ou vazio
    [Required(ErrorMessage = "Duração do filme é obrigatório")]
    // Valor deve estar entre 70 e 300
    [Range(70, 300, ErrorMessage = "Duração deve ser de 70 a 300 minutos")]
    public int Duracao { get; set; }
}
