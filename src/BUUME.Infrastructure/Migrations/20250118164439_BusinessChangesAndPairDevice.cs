using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace BUUME.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BusinessChangesAndPairDevice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_businesses_tax_office_tax_office_id",
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

            migrationBuilder.RenameColumn(
                name: "address_info_address",
                table: "businesses",
                newName: "address");

            migrationBuilder.RenameColumn(
                name: "tax_office_id",
                table: "businesses",
                newName: "tax_document_id");

            migrationBuilder.RenameIndex(
                name: "ix_businesses_tax_office_id",
                table: "businesses",
                newName: "ix_businesses_tax_document_id");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.AddColumn<bool>(
                name: "has_allowed_notifications",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_enabled",
                table: "businesses",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Point>(
                name: "location",
                table: "businesses",
                type: "geometry(Point, 4326)",
                nullable: false);

            migrationBuilder.AddColumn<Guid>(
                name: "validator_id",
                table: "businesses",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "pair_device",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    device_name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    fcm_token = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    operating_system = table.Column<int>(type: "integer", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pair_device", x => x.id);
                    table.ForeignKey(
                        name: "fk_pair_device_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_businesses_location",
                table: "businesses",
                column: "location")
                .Annotation("Npgsql:IndexMethod", "gist");

            migrationBuilder.CreateIndex(
                name: "ix_businesses_validator_id",
                table: "businesses",
                column: "validator_id");

            migrationBuilder.CreateIndex(
                name: "ix_pair_device_user_id",
                table: "pair_device",
                column: "user_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_businesses_file_tax_document_id",
                table: "businesses",
                column: "tax_document_id",
                principalTable: "files",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_businesses_users_validator_id",
                table: "businesses",
                column: "validator_id",
                principalTable: "users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_businesses_file_tax_document_id",
                table: "businesses");

            migrationBuilder.DropForeignKey(
                name: "fk_businesses_users_validator_id",
                table: "businesses");

            migrationBuilder.DropTable(
                name: "pair_device");

            migrationBuilder.DropIndex(
                name: "ix_businesses_location",
                table: "businesses");

            migrationBuilder.DropIndex(
                name: "ix_businesses_validator_id",
                table: "businesses");

            migrationBuilder.DropColumn(
                name: "has_allowed_notifications",
                table: "users");

            migrationBuilder.DropColumn(
                name: "is_enabled",
                table: "businesses");

            migrationBuilder.DropColumn(
                name: "location",
                table: "businesses");

            migrationBuilder.DropColumn(
                name: "validator_id",
                table: "businesses");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "businesses",
                newName: "address_info_address");

            migrationBuilder.RenameColumn(
                name: "tax_document_id",
                table: "businesses",
                newName: "tax_office_id");

            migrationBuilder.RenameIndex(
                name: "ix_businesses_tax_document_id",
                table: "businesses",
                newName: "ix_businesses_tax_office_id");

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
