using AutoMapper;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;

// CMT: A classe profile serve para configurar o mapeamento
// de uma classe Model para uma DTO dentro da aplicação com AutoMapper.

namespace FilmesAPI.Profiles;

public class FilmeProfile : Profile
{
	public FilmeProfile()
	{
        // Mapear uma classe CreateFilmeDto para uma Filme
        CreateMap<CreateFilmeDto, Filme>();
	}
}
