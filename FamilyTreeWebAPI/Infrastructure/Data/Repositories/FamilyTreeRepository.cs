using AutoMapper;
using AutoMapper.QueryableExtensions;
using FamilyTreeWebAPI.Application.DTOs;
using FamilyTreeWebAPI.Core.Entities;
using FamilyTreeWebAPI.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FamilyTreeWebAPI.Infrastructure.Data.Repositories
{
    public class FamilyTreeRepository : IFamilyTreeRepository
    {
        private readonly FamilyTreeContext _context;
        private readonly IMapper _mapper;

        public FamilyTreeRepository(FamilyTreeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<MemberDTO>> GetAllMembersAsync()
        {
            return await _context.Members
    .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
    .ToListAsync();
        }

        public async Task<List<RelationshipDTO>> GetAllRelationshipsAsync()
        {

            return await _context.Relationships
                .ProjectTo<RelationshipDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
        public async Task<FamilyTreeDto> GetAllDescendantsWithSpousesAsync(int spouseId1, int spouseId2)
        {
            var collected = new HashSet<int?>();

            // 1. მოძებნე საერთო MergePoint
            int? mergePointId = await _context.Relationships
                .Where(r => (r.Source == spouseId1 || r.Source == spouseId2))
                .GroupBy(r => r.Target)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .FirstOrDefaultAsync();

            if (mergePointId == null || mergePointId == 0)
            {
                return new FamilyTreeDto
                {
                    Members = new List<MemberDTO>(),
                    Relationships = new List<RelationshipDTO>()
                };
            }

            // 2. მოძებნე ყველა შთამომავალი
            await CollectDescendantsAsync(mergePointId, collected);

            // 3. მოძებნე მათი spouse-ები
            var descendantMergePoints = await _context.Relationships
                .Where(r => collected.Contains(r.Source))
                .Select(r => r.Target)
                .Distinct()
                .ToListAsync();

            var spouseIds = await _context.Relationships
                .Where(r => descendantMergePoints.Contains(r.Target) && !collected.Contains(r.Source))
                .Select(r => r.Source)
                .Distinct()
                .ToListAsync();

            // 4. MergePoint-ები შთამომავლებისა და spouse-ებისთვის
            var allMergePoints = await _context.Relationships
                .Where(r => collected.Contains(r.Source) || collected.Contains(r.Target) ||
                            spouseIds.Contains(r.Source) || spouseIds.Contains(r.Target))
                .Select(r => r.Source)
                .Concat(_context.Relationships
                    .Where(r => collected.Contains(r.Source) || collected.Contains(r.Target) ||
                                spouseIds.Contains(r.Source) || spouseIds.Contains(r.Target))
                    .Select(r => r.Target))
                .Distinct()
                .ToListAsync();

            // 5. საბოლოო member-ები: შთამომავლები + spouse-ები + mergepoint-ები + ორი spouse
            var resultIds = collected
                .Union(spouseIds)
                .Union(allMergePoints)
                .Union(new[] { (int?)spouseId1, (int?)spouseId2 })
                .Where(id => id.HasValue)
                .Select(id => id.Value)
                .Distinct()
                .ToList();

            // 6. წამოიღე ყველა წევრი
            var members = await _context.Members
                .Where(m => resultIds.Contains(m.Id))
                .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            // 7. relationships — მხოლოდ მათ შორის ვისაც ჩავამატეთ members-ში
            var relationships = await _context.Relationships
                .Where(r => resultIds.Contains(r.Source.Value) && resultIds.Contains(r.Target.Value))
                .Select(r => new RelationshipDTO
                {
                    Source = r.Source.Value,
                    Target = r.Target.Value,
                    //RelationshipType = r.RelationshipType
                })
                .ToListAsync();

            return new FamilyTreeDto
            {
                Members = members,
                Relationships = relationships
            };
        }
        private async Task CollectDescendantsAsync(int? mergePointId, HashSet<int?> collected)
        {
            // მოძებნე ამ mergePoint-ის შვილები
            var children = await _context.Relationships
                .Where(r => r.Source == mergePointId)
                .Select(r => r.Target)
                .ToListAsync();

            foreach (var childId in children)
            {
                if (collected.Add(childId))
                {
                    // ყველა MergePoint-ი, სადაც ეს შვილი ფიგურირებს როგორც მშობელი (source)
                    var childMergePoints = await _context.Relationships
                        .Where(r => r.Source == childId)
                        .Select(r => r.Target)
                        .Distinct()
                        .ToListAsync();

                    foreach (var childMergePoint in childMergePoints)
                    {
                        // გამოიძახე ეს ფუნქცია შვილიშვილებზე
                        await CollectDescendantsAsync(childMergePoint, collected);
                    }
                }
            }
        }


    }
}
