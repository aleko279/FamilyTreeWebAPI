using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilyTreeWebAPI.Core.Entities;
using FamilyTreeWebAPI.Infrastructure.Data;
using FamilyTreeWebAPI.Application.DTOs;
using FamilyTreeWebAPI.Infrastructure.Data.Repositories;
using FamilyTreeWebAPI.Core.Interfaces;

namespace FamilyTreeWebAPI.Application.Services
{
    public class FamilyTreeService:IFamilyTreeService
    {
        private readonly IFamilyTreeRepository _repository;

        public FamilyTreeService(IFamilyTreeRepository repository)
        {
            _repository = repository;
        }

        public async Task<FamilyTreeDto> GetFamilyTreeAsync()
        {
            var members = await _repository.GetAllMembersAsync();
            var relationships = await _repository.GetAllRelationshipsAsync();

            return new FamilyTreeDto
            {
                Members = members,
                Relationships = relationships
            };
        }

        public async Task<FamilyTreeDto> GetOnlyFamilyTreeMembersAsync(int spouseId1, int spouseId2)
        {
            var members = await _repository.GetAllDescendantsWithSpousesAsync(spouseId1, spouseId2);

            return members;
        }
    }
}