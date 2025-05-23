using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyTreeWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddNnameToMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NName",
                table: "Members",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NName",
                table: "Members");
        }
    }
}
