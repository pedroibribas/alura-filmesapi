using System.ComponentModel.DataAnnotations;

// A classe DTO apresenta somente informações que interessam ao cliente.
// Dessa forma, os controladores parametrizam os DTOS para proibir que
// o cliente envie dados desnecessários que podem comprometer os dados
// da base de dados.

namespace FilmesAPI.Data.Dtos;

public class CreateFilmeDto
{
    // # Título
    // Valor não pode ser nulo ou vazio
    [Required(ErrorMessage = "Título do filme é obrigatório")]
    public string Titulo { get; set; }

    // # Gênero
    // Valor não pode ser nulo ou vazio
    [Required]
    // Valor deve possuir até 50 caracteres
    [StringLength(50, ErrorMessage = "Gênero deve conter até 50 caracteres")]
    // CMT: StringLength é melhor que MaxLength, sem alocar memória no banco
    public string Genero { get; set; }

    // # Duração
    // Valor não pode ser nulo ou vazio
    [Required(ErrorMessage = "Duração do filme é obrigatório")]
    // Valor deve estar entre 70 e 300
    [Range(70, 300, ErrorMessage = "Duração deve ser de 70 a 300 minutos")]
    public int Duracao { get; set; }
}
