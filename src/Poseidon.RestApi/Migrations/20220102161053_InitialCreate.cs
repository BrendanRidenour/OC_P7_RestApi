using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poseidon.RestApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BidList",
                columns: table => new
                {
                    BidListId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "100, 1"),
                    bidQuantity = table.Column<double>(type: "float", nullable: true),
                    askQuantity = table.Column<double>(type: "float", nullable: true),
                    bid = table.Column<double>(type: "float", nullable: true),
                    ask = table.Column<double>(type: "float", nullable: true),
                    bidListDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    commentary = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: true),
                    account = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    type = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    benchmark = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: true),
                    security = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: true),
                    status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    trader = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: true),
                    book = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: true),
                    creationName = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: true),
                    creationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    revisionName = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: true),
                    revisionDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    dealName = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: true),
                    dealType = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: true),
                    sourceListId = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: true),
                    side = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BidList", x => x.BidListId);
                });

            migrationBuilder.CreateTable(
                name: "CurvePoint",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "100, 1"),
                    CurveId = table.Column<int>(type: "int", nullable: true),
                    asOfDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    term = table.Column<double>(type: "float", nullable: true),
                    value = table.Column<double>(type: "float", nullable: true),
                    creationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurvePoint", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "100, 1"),
                    moodysRating = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: true),
                    sandPRating = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: true),
                    fitchRating = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: true),
                    orderNumber = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RuleName",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "100, 1"),
                    name = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: false),
                    description = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: true),
                    json = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: true),
                    template = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    sqlStr = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: true),
                    sqlPart = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RuleName", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trade",
                columns: table => new
                {
                    TradeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "100, 1"),
                    buyQuantity = table.Column<double>(type: "float", nullable: true),
                    sellQuantity = table.Column<double>(type: "float", nullable: true),
                    buyPrice = table.Column<double>(type: "float", nullable: true),
                    sellPrice = table.Column<double>(type: "float", nullable: true),
                    TradeDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    account = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    type = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    benchmark = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: true),
                    security = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: true),
                    status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    trader = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: true),
                    book = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: true),
                    creationName = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: true),
                    creationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    revisionName = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: true),
                    revisionDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    dealName = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: true),
                    dealType = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: true),
                    sourceListId = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: true),
                    side = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trade", x => x.TradeId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "100, 1"),
                    username = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: false),
                    password = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: false),
                    fullname = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: false),
                    role = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_username",
                table: "Users",
                column: "username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BidList");

            migrationBuilder.DropTable(
                name: "CurvePoint");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "RuleName");

            migrationBuilder.DropTable(
                name: "Trade");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
