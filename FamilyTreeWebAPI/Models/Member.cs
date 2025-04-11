using System;
using System.Collections.Generic;

namespace FamilyTreeWebAPI.Models;

public partial class Member
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Role { get; set; }
}
