using System;
using System.Collections.Generic;

namespace FamilyTreeWebAPI.Core.Entities;

public partial class Member
{
    public int Id { get; set; }

    public string? Fname { get; set; }

    public string? Role { get; set; }

    public string? Lname { get; set; }

    public DateOnly? BirthDate { get; set; }
    public DateOnly? DeathDate { get; set; }

    public string? BirthPlace { get; set; }
    public string? NName { get; set; }
}
