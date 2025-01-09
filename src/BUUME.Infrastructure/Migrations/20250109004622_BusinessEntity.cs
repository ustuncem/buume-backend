using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BUUME.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BusinessEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "businesses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    logo_id = table.Column<Guid>(type: "uuid", nullable: false),
                    owner_id = table.Column<Guid>(type: "uuid", nullable: false),
                    country_id = table.Column<Guid>(type: "uuid", nullable: false),
                    city_id = table.Column<Guid>(type: "uuid", nullable: false),
                    district_id = table.Column<Guid>(type: "uuid", nullable: false),
                    tax_office_id = table.Column<Guid>(type: "uuid", nullable: false),
                    base_info_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    base_info_email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    base_info_phone_number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    base_info_description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    base_info_online_order_link = table.Column<string>(type: "text", nullable: true),
                    base_info_menu_link = table.Column<string>(type: "text", nullable: true),
                    base_info_website_link = table.Column<string>(type: "text", nullable: true),
                    address_info_address = table.Column<string>(type: "text", nullable: false),
                    address_info_latitude = table.Column<decimal>(type: "numeric", maxLength: 100, nullable: false),
                    address_info_longitude = table.Column<decimal>(type: "numeric", maxLength: 100, nullable: false),
                    tax_info_trade_name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    tax_info_vkn = table.Column<int>(type: "integer", nullable: false),
                    is_kvkk_approved = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    working_hours_start_time = table.Column<TimeSpan>(type: "interval", nullable: true),
                    working_hours_end_time = table.Column<TimeSpan>(type: "interval", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_businesses", x => x.id);
                    table.ForeignKey(
                        name: "fk_businesses_city_city_id",
                        column: x => x.city_id,
                        principalTable: "cities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_businesses_country_country_id",
                        column: x => x.country_id,
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_businesses_district_district_id",
                        column: x => x.district_id,
                        principalTable: "districts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_businesses_file_logo_id",
                        column: x => x.logo_id,
                        principalTable: "files",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_businesses_tax_office_tax_office_id",
                        column: x => x.tax_office_id,
                        principalTable: "tax_offices",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_businesses_users_owner_id",
                        column: x => x.owner_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "business_business_category",
                columns: table => new
                {
                    business_category_id = table.Column<Guid>(type: "uuid", nullable: false),
                    business_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_business_business_category", x => new { x.business_category_id, x.business_id });
                    table.ForeignKey(
                        name: "fk_business_business_category_business_categories_business_cat",
                        column: x => x.business_category_id,
                        principalTable: "business_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_business_business_category_businesses_business_id",
                        column: x => x.business_id,
                        principalTable: "businesses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "business_file",
                columns: table => new
                {
                    business_id = table.Column<Guid>(type: "uuid", nullable: false),
                    file_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_business_file", x => new { x.business_id, x.file_id });
                    table.ForeignKey(
                        name: "fk_business_file_businesses_business_id",
                        column: x => x.business_id,
                        principalTable: "businesses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_business_file_file_file_id",
                        column: x => x.file_id,
                        principalTable: "files",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_business_business_category_business_id",
                table: "business_business_category",
                column: "business_id");

            migrationBuilder.CreateIndex(
                name: "ix_business_file_file_id",
                table: "business_file",
                column: "file_id");

            migrationBuilder.CreateIndex(
                name: "ix_businesses_city_id",
                table: "businesses",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "ix_businesses_country_id",
                table: "businesses",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "ix_businesses_district_id",
                table: "businesses",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "ix_businesses_logo_id",
                table: "businesses",
                column: "logo_id");

            migrationBuilder.CreateIndex(
                name: "ix_businesses_owner_id",
                table: "businesses",
                column: "owner_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_businesses_tax_office_id",
                table: "businesses",
                column: "tax_office_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "business_business_category");

            migrationBuilder.DropTable(
                name: "business_file");

            migrationBuilder.DropTable(
                name: "businesses");
        }
    }
}
