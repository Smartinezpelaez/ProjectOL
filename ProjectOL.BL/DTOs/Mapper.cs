using AutoMapper;
using ProjectOL.BL.Models;


namespace ProjectOL.BL.DTOs
{
    public class Mapper : Profile
    {
        public Mapper()
        {

            CreateMap<Customer, CustomerDTOs>();
            CreateMap<CustomerDTOs, Customer>();

            CreateMap<Project, ProjectDTOs>();
            CreateMap<ProjectDTOs, Project>();

            CreateMap<Language , LanguageDTOs>();
            CreateMap<LanguageDTOs, Language>();

            CreateMap<ProjectLanguage, ProjectLanguageDTOs>();
            CreateMap<ProjectLanguageDTOs, ProjectLanguage>();

            CreateMap<ProjectState, ProjectStateDTOs>();
            CreateMap<ProjectStateDTOs, ProjectState>();

        }
    }
}
