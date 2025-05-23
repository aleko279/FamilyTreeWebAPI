using FamilyTreeWebAPI.Application.DTOs;

namespace FamilyTreeWebAPI.Core.Interfaces
{
    public interface IFamilyTreeService
    {
        Task<FamilyTreeDto> GetFamilyTreeAsync();
        Task<FamilyTreeDto> GetOnlyFamilyTreeMembersAsync(int spouseId1, int spouseId2);
    }
}
