using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RateAmData.Entities;
using RateAmData.Repositories;
using System.Xml.Linq;

#nullable disable

namespace RateAmData.Migrations
{
    public partial class InitialCreate : Migration
    {

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "banks",
                columns: table => new
                {
                    bank_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    icon_url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_banks", x => x.bank_id);
                });

            migrationBuilder.CreateTable(
                name: "currencies",
                columns: table => new
                {
                    currency_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    symbol = table.Column<string>(type: "text", nullable: false),
                    icon_url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_currencies", x => x.currency_id);
                });

            migrationBuilder.CreateTable(
                name: "last_updated",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    last_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_last_updated", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "rates",
                columns: table => new
                {
                    rate_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    publish_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    sell_rate = table.Column<decimal>(type: "numeric", nullable: false),
                    buy_rate = table.Column<decimal>(type: "numeric", nullable: false),
                    bank_id = table.Column<int>(type: "integer", nullable: false),
                    currency_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rates", x => x.rate_id);
                });




            migrationBuilder.InsertData("banks",
           columns: new[] { nameof(BankEntity.Id), nameof(BankEntity.Name), nameof(BankEntity.IconURL) },
           values: new object[,] {
                   { 1 ,"Fast Bank", "https://rate.am/images/organization/logo/767eaf3e45ae41bca8d7e4e481da6501.jpg"},
                   { 2 ,"Unibank", "https://rate.am/images/organization/logo/9cf13d95c8214c7e989a242cc0772311.jpg"},
                   { 3 ,"Acba bank","https://rate.am/images/organization/logo/8203181c42c441a68f3a4cd769ab09c6.jpg"},
                   { 4 ,"Artsakhbank", "https://rate.am/images/organization/logo/8.gif"},
                   { 5 ,"VTB Bank (Armenia)", "https://rate.am/images/organization/logo/2ae3fc783f014420a23da55d69552d90.gif"},
                   { 6 ,"Evocabank", "https://rate.am/images/organization/logo/7d9ba737c27842de87bc5abe9e901525.png"},
                   { 7 ,"Inecobank", "https://rate.am/images/organization/logo/13.gif"},
                   { 8 ,"IDBank", "https://rate.am/images/organization/logo/170e82d5663b4d20ab76303baddfdec4.jpg"},
                   { 9 ,"Byblos Bank Armenia", "https://rate.am/images/organization/logo/9.gif"},
                   { 10,  "ArmSwissBank", "https://rate.am/images/organization/logo/aa6589e16c574e9981e3bc75719c6e3e.gif"},
                   { 11,  "Ardshinbank",  "https://rate.am/images/organization/logo/6.gif"},
                   { 12,  "AraratBank",  "https://rate.am/images/organization/logo/b5700b0d821c412a8a69a22f5ce350c0.jpg"},
                   { 13,  "HSBC Bank Armenia", "https://rate.am/images/organization/logo/12.gif"},
                   { 14,  "AMIO BANK",  "https://rate.am/images/organization/logo/b07ab5399fda4fea9c511ac0fa040288.png"},
                   { 15,  "Converse Bank", "https://rate.am/images/organization/logo/6988d8a8552c45eaaff4d5b779294d01.png"},
                   { 16,  "Ameriabank", "https://rate.am/images/organization/logo/da4585f3df0345778afb0a01e81203ea.png"},
                   { 17,  "Mellat Bank","https://rate.am/images/organization/logo/17.gif"},
                   { 18,  "ARMECONOMBANK",  "https://rate.am/images/organization/logo/e5ef9988870b4896be0399d803cedf57.jpg"}
           });
            migrationBuilder.InsertData("currencies",
           columns: new[] { nameof(CurrencyEntity.Id), nameof(CurrencyEntity.Name), nameof(CurrencyEntity.IconUrl), nameof(CurrencyEntity.Symbol) },
           values: new object[,] {

               {1, "USD", "https://rate.am/images/currency/icon/USD.gif", "$" },
               {2, "EUR",  "https://rate.am/images/currency/icon/EUR.gif", "€" },
               {3, "RUR", "https://rate.am/images/currency/icon/RUR.gif", "₽" },
               {4, "GBP", "https://rate.am/images/currency/icon/GBP.gif", "£" },

               {5, "GEL",  "https://rate.am/images/currency/icon/GEL.gif", "₾" },
               {6, "CHF",  "https://rate.am/images/currency/icon/CHF.gif", "₣" },
               {7, "CAD",  "https://rate.am/images/currency/icon/CAD.gif", "$" },
               {8, "AED",  "https://rate.am/images/currency/icon/AED.gif", "د.إ" },

               {9, "CNY",  "https://rate.am/images/currency/icon/CNY.gif", "¥" },
               {10, "AUD", "https://rate.am/images/currency/icon/AUD.gif", "$" },
               {11, "JPY", "https://rate.am/images/currency/icon/JPY.gif", "¥" },
               {12, "SEK", "https://rate.am/images/currency/icon/SEK.gif", "kr" },

               {13, "HKD", "https://rate.am/images/currency/icon/HKD.gif", "$" },
               {14, "KZT", "https://rate.am/images/currency/icon/KZT.gif", "₸" },
               {15, "XAU", "https://rate.am/images/currency/icon/XAU.gif", "" },

           });


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "banks");

            migrationBuilder.DropTable(
                name: "currencies");

            migrationBuilder.DropTable(
                name: "last_updated");

            migrationBuilder.DropTable(
                name: "rates");
        }

    }
}
