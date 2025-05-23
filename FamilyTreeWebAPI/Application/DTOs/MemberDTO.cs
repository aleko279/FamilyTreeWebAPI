namespace FamilyTreeWebAPI.Application.DTOs
{
    public class MemberDTO
    {
        public int Id { get; set; }

        public string? Fname { get; set; }

        public string? Role { get; set; }

        public string? Lname { get; set; }

        public DateOnly? BirthDate { get; set; }

        public string? BirthPlace { get; set; }

    }
}
