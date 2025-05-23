using FamilyTreeWebAPI.Core.Entities;

namespace FamilyTreeWebAPI.Application.DTOs
{
    public class FamilyTreeDto
    {
        public List<MemberDTO> Members { get; set; }
        public List<RelationshipDTO> Relationships { get; set; }
    }
}
