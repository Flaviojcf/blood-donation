using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodDonationSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donation_Donor_DonorId",
                table: "Donation");

            migrationBuilder.CreateTable(
                name: "BloodStock",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BloodType = table.Column<int>(type: "int", nullable: false),
                    RhFactorType = table.Column<int>(type: "int", nullable: false),
                    QuantityML = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodStock", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Donation_Donor_DonorId",
                table: "Donation",
                column: "DonorId",
                principalTable: "Donor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donation_Donor_DonorId",
                table: "Donation");

            migrationBuilder.DropTable(
                name: "BloodStock");

            migrationBuilder.AddForeignKey(
                name: "FK_Donation_Donor_DonorId",
                table: "Donation",
                column: "DonorId",
                principalTable: "Donor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
