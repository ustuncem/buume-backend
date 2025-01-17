using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace BUUME.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PostGISAndBusinessChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_businesses_tax_office_tax_office_id",
                table: "businesses");

            migrationBuilder.DropIndex(
                name: "ix_businesses_tax_office_id",
                table: "businesses");

            migrationBuilder.DropColumn(
                name: "address_info_latitude",
                table: "businesses");

            migrationBuilder.DropColumn(
                name: "address_info_longitude",
                table: "businesses");

            migrationBuilder.DropColumn(
                name: "tax_info_trade_name",
                table: "businesses");

            migrationBuilder.DropColumn(
                name: "tax_info_vkn",
                table: "businesses");

            migrationBuilder.DropColumn(
                name: "tax_office_id",
                table: "businesses");

            migrationBuilder.RenameColumn(
                name: "address_info_address",
                table: "businesses",
                newName: "address");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.AddColumn<Point>(
                name: "location",
                table: "businesses",
                type: "geometry(Point, 4326)",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "ix_businesses_location",
                table: "businesses",
                column: "location")
                .Annotation("Npgsql:IndexMethod", "gist");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_businesses_location",
                table: "businesses");

            migrationBuilder.DropColumn(
                name: "location",
                table: "businesses");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "businesses",
                newName: "address_info_address");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.AddColumn<decimal>(
                name: "address_info_latitude",
                table: "businesses",
                type: "numeric",
                maxLength: 100,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "address_info_longitude",
                table: "businesses",
                type: "numeric",
                maxLength: 100,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "tax_info_trade_name",
                table: "businesses",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "tax_info_vkn",
                table: "businesses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "tax_office_id",
                table: "businesses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "ix_businesses_tax_office_id",
                table: "businesses",
                column: "tax_office_id");

            migrationBuilder.AddForeignKey(
                name: "fk_businesses_tax_office_tax_office_id",
                table: "businesses",
                column: "tax_office_id",
                principalTable: "tax_offices",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
