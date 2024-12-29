using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BUUME.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RegionRelationshipWithMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_regions_country_id",
                table: "regions");

            migrationBuilder.CreateIndex(
                name: "ix_regions_country_id",
                table: "regions",
                column: "country_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_regions_country_id",
                table: "regions");

            migrationBuilder.CreateIndex(
                name: "ix_regions_country_id",
                table: "regions",
                column: "country_id",
                unique: true);
        }
    }
}
