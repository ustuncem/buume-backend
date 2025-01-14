using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BUUME.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserSwitchBusinessAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "switched_to_business",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "switched_to_business",
                table: "users");
        }
    }
}
