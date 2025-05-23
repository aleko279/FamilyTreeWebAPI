using FamilyTreeWebAPI.Application.DTOs;
using FamilyTreeWebAPI.Core.Entities;

namespace FamilyTreeWebAPI.Core.Interfaces
{
    public interface IFamilyTreeRepository
    {
        Task<List<MemberDTO>> GetAllMembersAsync();
        Task<List<RelationshipDTO>> GetAllRelationshipsAsync();
        Task<FamilyTreeDto> GetAllDescendantsWithSpousesAsync(int spouseId1, int spouseId2);

    }
}
