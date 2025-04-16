using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotorcycleRentalSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initalCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "DeliveryMan",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LegalEntity = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", maxLength: 10, nullable: false),
                    DriveLicenseNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DriveLicenseType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DriveLicensePhoto = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryMan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Motorcycle",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Year = table.Column<int>(type: "integer", maxLength: 4, nullable: false),
                    Plate = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    Model = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsAvailable = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motorcycle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MotorcycleEvent",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    MessageBody = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotorcycleEvent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rent",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    DeliveryManId = table.Column<string>(type: "text", nullable: false),
                    MotorcycleId = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", maxLength: 10, nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", maxLength: 10, nullable: false),
                    ExpectedEndDate = table.Column<DateTime>(type: "timestamp with time zone", maxLength: 10, nullable: false),
                    Plan = table.Column<int>(type: "integer", nullable: false),
                    DailyRate = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rent_DeliveryMan_DeliveryManId",
                        column: x => x.DeliveryManId,
                        principalSchema: "public",
                        principalTable: "DeliveryMan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rent_Motorcycle_MotorcycleId",
                        column: x => x.MotorcycleId,
                        principalSchema: "public",
                        principalTable: "Motorcycle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryMan_DriveLicenseNumber",
                schema: "public",
                table: "DeliveryMan",
                column: "DriveLicenseNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryMan_LegalEntity",
                schema: "public",
                table: "DeliveryMan",
                column: "LegalEntity",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Motorcycle_Plate",
                schema: "public",
                table: "Motorcycle",
                column: "Plate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rent_DeliveryManId",
                schema: "public",
                table: "Rent",
                column: "DeliveryManId");

            migrationBuilder.CreateIndex(
                name: "IX_Rent_MotorcycleId",
                schema: "public",
                table: "Rent",
                column: "MotorcycleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MotorcycleEvent",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Rent",
                schema: "public");

            migrationBuilder.DropTable(
                name: "DeliveryMan",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Motorcycle",
                schema: "public");
        }
    }
}
