using Application.Advisors;
using AutoMapper;

namespace Api.Config
{
    public class AutoMapping : Profile
    {
        public AutoMapping() { 
            CreateMap<CreateAdvisorCommand, Domain.Models.Advisor>().ReverseMap();
            CreateMap<UpdateAdvisorCommand, Domain.Models.Advisor>().ReverseMap();
            CreateMap<UpdateAdvisorCommand, CreateAdvisorCommand>().ReverseMap();
        }
    }
}
