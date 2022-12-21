using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos;

public class UpdateFilmeDto
{
    [Required(ErrorMessage = "Título do filme é obrigatório")]
    public string Titulo { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "Gênero deve conter até 50 caracteres")]
    public string Genero { get; set; }

    [Required(ErrorMessage = "Duração do filme é obrigatório")]
    [Range(70, 300, ErrorMessage = "Duração deve ser de 70 a 300 minutos")]
    public int Duracao { get; set; }
}
