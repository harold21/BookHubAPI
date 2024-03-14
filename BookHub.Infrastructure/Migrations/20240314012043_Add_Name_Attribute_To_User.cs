using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Name_Attribute_To_User : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "Email");
        }
    }
}
