using System;
using System.Collections.Generic;
using FamilyTreeWebAPI.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FamilyTreeWebAPI.Infrastructure.Data;

public partial class FamilyTreeContext : DbContext
{
    public FamilyTreeContext(DbContextOptions<FamilyTreeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Relationship> Relationships { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Fname).HasMaxLength(500);
            entity.Property(e => e.Lname).HasMaxLength(500);
            entity.Property(e => e.Role).HasMaxLength(100);
            entity.Property(e => e.BirthPlace).HasMaxLength(500);
        });

        modelBuilder.Entity<Relationship>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.RelationshipType).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(1000);
        });
    }
}
