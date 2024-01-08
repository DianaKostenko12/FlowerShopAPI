using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowerShop.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bouquets",
                columns: table => new
                {
                    BouquetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BouquetName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BouquetCost = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bouquets", x => x.BouquetId);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientSurname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "Flowers",
                columns: table => new
                {
                    FlowerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlowerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlowerCount = table.Column<int>(type: "int", nullable: false),
                    FlowerCost = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flowers", x => x.FlowerId);
                });

            migrationBuilder.CreateTable(
                name: "Gifts",
                columns: table => new
                {
                    GiftId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GiftName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GiftCost = table.Column<float>(type: "real", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gifts", x => x.GiftId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderSum = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId");
                });

            migrationBuilder.CreateTable(
                name: "BouquetFlowers",
                columns: table => new
                {
                    FlowerId = table.Column<int>(type: "int", nullable: false),
                    BouquetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BouquetFlowers", x => new { x.BouquetId, x.FlowerId });
                    table.ForeignKey(
                        name: "FK_BouquetFlowers_Bouquets_BouquetId",
                        column: x => x.BouquetId,
                        principalTable: "Bouquets",
                        principalColumn: "BouquetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BouquetFlowers_Flowers_FlowerId",
                        column: x => x.FlowerId,
                        principalTable: "Flowers",
                        principalColumn: "FlowerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderBouquets",
                columns: table => new
                {
                    BouquetId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    BouquetCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderBouquets", x => new { x.OrderId, x.BouquetId });
                    table.ForeignKey(
                        name: "FK_OrderBouquets_Bouquets_BouquetId",
                        column: x => x.BouquetId,
                        principalTable: "Bouquets",
                        principalColumn: "BouquetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderBouquets_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderGifts",
                columns: table => new
                {
                    GiftId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    GiftCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderGifts", x => new { x.OrderId, x.GiftId });
                    table.ForeignKey(
                        name: "FK_OrderGifts_Gifts_GiftId",
                        column: x => x.GiftId,
                        principalTable: "Gifts",
                        principalColumn: "GiftId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderGifts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BouquetFlowers_FlowerId",
                table: "BouquetFlowers",
                column: "FlowerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderBouquets_BouquetId",
                table: "OrderBouquets",
                column: "BouquetId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderGifts_GiftId",
                table: "OrderGifts",
                column: "GiftId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ClientId",
                table: "Orders",
                column: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BouquetFlowers");

            migrationBuilder.DropTable(
                name: "OrderBouquets");

            migrationBuilder.DropTable(
                name: "OrderGifts");

            migrationBuilder.DropTable(
                name: "Flowers");

            migrationBuilder.DropTable(
                name: "Bouquets");

            migrationBuilder.DropTable(
                name: "Gifts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
