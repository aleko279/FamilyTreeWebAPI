using System;
using System.Collections.Generic;

namespace FamilyTreeWebAPI.Core.Entities;

public partial class Relationship
{
    public int Id { get; set; }

    public int? Source { get; set; }

    public int? Target { get; set; }

    public string? RelationshipType { get; set; }  // მაგ: "parent", "sibling", "spouse"

    public string? Description { get; set; }       // დამატებითი აღწერა

    public DateOnly? Since { get; set; }
}
