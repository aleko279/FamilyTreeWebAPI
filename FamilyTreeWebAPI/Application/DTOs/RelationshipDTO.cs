namespace FamilyTreeWebAPI.Application.DTOs
{
    public class RelationshipDTO
    {
        public int Id { get; set; }

        public int? Source { get; set; }

        public int? Target { get; set; }
    }
}
