using System;
using System.Collections.Generic;

namespace FamilyTreeWebAPI.Models;

public partial class Relationship
{
    public int Id { get; set; }

    public int? Source { get; set; }

    public int? Target { get; set; }
}
