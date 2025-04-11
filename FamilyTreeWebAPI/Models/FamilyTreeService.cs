using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilyTreeWebAPI.Models;

namespace FamilyTreeWebAPI.Services
{
    public class FamilyTreeService
    {
        private readonly FamilyTreeContext _dbContext;

        public FamilyTreeService(FamilyTreeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Member>> GetFamilyTreeNodes()
        {
            return await _dbContext.Members.ToListAsync();
        }

        public async Task<IEnumerable<Relationship>> GetFamilyTreeEdges()
        {
            return await _dbContext.Relationships.ToListAsync();
        }
    }
}
