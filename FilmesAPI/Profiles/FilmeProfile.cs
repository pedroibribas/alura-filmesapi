using AutoMapper;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;

// CMT: A classe profile serve para configurar o mapeamento
// de uma classe Model para uma DTO dentro da aplicação com AutoMapper.
// Nota: O AutoMapper está configurado em Program.cs.

namespace FilmesAPI.Profiles;

public class FilmeProfile : Profile
{
	public FilmeProfile()
	{
        // Mapear o filmeDto de POST para filme
        CreateMap<CreateFilmeDto, Filme>();

        // Mapear o filmeDto de PUT para filme
        CreateMap<UpdateFilmeDto, Filme>();

        // Mapear filme para dto no PATCH
        CreateMap<Filme, UpdateFilmeDto>();        
        
        // Mapear filme para dto no GET
        CreateMap<Filme, ReadFilmeDto>();
	}
}
