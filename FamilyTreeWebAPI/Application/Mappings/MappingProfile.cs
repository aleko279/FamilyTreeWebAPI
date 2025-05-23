using AutoMapper;
using FamilyTreeWebAPI.Application.DTOs;
using FamilyTreeWebAPI.Core.Entities;

namespace FamilyTreeWebAPI.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Member, MemberDTO>(); 
            CreateMap<Relationship, RelationshipDTO>();
        }
    }
}
