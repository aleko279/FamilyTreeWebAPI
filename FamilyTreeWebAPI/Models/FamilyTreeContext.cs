using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FamilyTreeWebAPI.Models;

public partial class FamilyTreeContext : DbContext
{
    public FamilyTreeContext()
    {
    }

    public FamilyTreeContext(DbContextOptions<FamilyTreeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Relationship> Relationships { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ALEX\\SQLEXPRESS;Database=FamilyTree;User Id=sa;Password=123;Trusted_Connection=False;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Member>(entity =>
        {
            entity.ToTable("Member");

            entity.Property(e => e.Name).HasMaxLength(500);
            entity.Property(e => e.Role).HasMaxLength(100);
        });

        modelBuilder.Entity<Relationship>(entity =>
        {
            entity.ToTable("Relationship");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
