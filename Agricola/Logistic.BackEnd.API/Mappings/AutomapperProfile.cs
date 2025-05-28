using AutoMapper;

namespace Logistic.BackEnd.API.Mappings;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        //CreateMap<Empresa, EmpresaDto>().ReverseMap();
        //CreateMap<Usuarios, UsuarioDto>().ReverseMap();

        //CreateMap<Usuarios, UsuarioDto>()
        //    .ForAllMembers(opt => opt.Condition(src => src != null));
    }
}