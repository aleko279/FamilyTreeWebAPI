using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FamilyTreeWebAPI.Models;
using FamilyTreeWebAPI.Services;
[Route("api/family")]
[ApiController]
public class FamilyTreeController : ControllerBase
{
    private readonly FamilyTreeContext _context;
    private readonly FamilyTreeService _familyTreeService;

    public FamilyTreeController(FamilyTreeContext context, FamilyTreeService familyTreeService)
    {
        _context = context;
        _familyTreeService = familyTreeService;
    }
    [HttpGet]
    public async Task<IActionResult> GetFamilyTree()
    {
        var members = await _context.Members.ToListAsync();
        var relationships = await _context.Relationships.ToListAsync();

        return Ok(new { members, relationships });
    }
    //[HttpGet]
    //public async Task<ActionResult<object>> GetFamilyTreeData()
    //{
    //    var nodes = await _familyTreeService.GetFamilyTreeNodes();
    //    var edges = await _familyTreeService.GetFamilyTreeEdges();

    //    var result = new
    //    {
    //        elements = nodes.Select(e => new
    //        {
    //            data = new
    //            {
    //                e.Id,
    //                e.Name,
    //                e.Role
    //            }
    //        }).ToList(),

    //        connections = edges.Select(c => new
    //        {
    //            data = new
    //            {
    //                c.Source,
    //                c.Target
    //            }
    //        }).ToList()
    //    };

    //    return Ok(result);
    //}
    //// GET: api/FamilyTree
    //[HttpGet]
    //public async Task<ActionResult<IEnumerable<FamilyTree>>> GetFamilyTree()
    //{
    //    var familyTree = await _context.FamilyTrees
    //        .Select(member => new FamilyTree
    //        {
    //            Id = member.Id,
    //            Name = member.Name,
    //            Role = member.Role,
    //            ParentId = member.ParentId
    //        })
    //        .ToListAsync();

    //    return Ok(familyTree);
    //}

}
