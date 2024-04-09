using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoyaltyPointsExchangeSystem.Migrations
{
    /// <inheritdoc />
    public partial class updatedb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pointsTransferHistories_AspNetUsers_ApplicationUserId",
                table: "pointsTransferHistories");

            migrationBuilder.DropTable(
                name: "carts");

            migrationBuilder.RenameColumn(
                name: "lastNane",
                table: "AspNetUsers",
                newName: "lastName");

            migrationBuilder.RenameColumn(
                name: "firstNane",
                table: "AspNetUsers",
                newName: "firstName");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "pointsTransferHistories",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_pointsTransferHistories_AspNetUsers_ApplicationUserId",
                table: "pointsTransferHistories",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pointsTransferHistories_AspNetUsers_ApplicationUserId",
                table: "pointsTransferHistories");

            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "AspNetUsers",
                newName: "lastNane");

            migrationBuilder.RenameColumn(
                name: "firstName",
                table: "AspNetUsers",
                newName: "firstNane");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "pointsTransferHistories",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "carts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EachPrice = table.Column<double>(type: "float", nullable: false),
                    ProdName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_carts_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_carts_ApplicationUserId",
                table: "carts",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_pointsTransferHistories_AspNetUsers_ApplicationUserId",
                table: "pointsTransferHistories",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
