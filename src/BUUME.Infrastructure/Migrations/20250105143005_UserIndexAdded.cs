using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BUUME.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserIndexAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_users_phone_number",
                table: "users",
                column: "phone_number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_users_phone_number",
                table: "users");
        }
    }
}
