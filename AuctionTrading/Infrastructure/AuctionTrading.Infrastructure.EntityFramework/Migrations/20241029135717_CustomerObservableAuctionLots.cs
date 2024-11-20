using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionTrading.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class CustomerObservableAuctionLots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuctionLotCustomer",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    _observableAuctionLotsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionLotCustomer", x => new { x.CustomerId, x._observableAuctionLotsId });
                    table.ForeignKey(
                        name: "FK_AuctionLotCustomer_AuctionLots__observableAuctionLotsId",
                        column: x => x._observableAuctionLotsId,
                        principalTable: "AuctionLots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuctionLotCustomer_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuctionLotCustomer__observableAuctionLotsId",
                table: "AuctionLotCustomer",
                column: "_observableAuctionLotsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuctionLotCustomer");
        }
    }
}
