using FamilyTreeWebAPI.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FamilyTreeWebAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamilyTreeController : ControllerBase
    {
        private readonly IFamilyTreeService _familyTreeService;

        public FamilyTreeController(IFamilyTreeService familyTreeService)
        {
            _familyTreeService = familyTreeService;
        }

        [HttpGet("GetFamilyTree")]
        public async Task<IActionResult> GetFamilyTree()
        {
            var familyTree = await _familyTreeService.GetFamilyTreeAsync();
            return Ok(familyTree);
        }
        [HttpGet("GetOnlyFamilyTree")]
        public async Task<IActionResult> GetOnlyFamilyTree(int spouseId1, int spouseId2)
        {
            var familyTree = await _familyTreeService.GetOnlyFamilyTreeMembersAsync(spouseId1, spouseId2);
            return Ok(familyTree);
        }
    }
}
