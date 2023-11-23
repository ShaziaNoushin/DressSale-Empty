using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mid1273286.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dresses",
                columns: table => new
                {
                    DressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DressName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OnSale = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dresses", x => x.DressId);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    SaleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    DressId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.SaleId);
                    table.ForeignKey(
                        name: "FK_Sales_Dresses_DressId",
                        column: x => x.DressId,
                        principalTable: "Dresses",
                        principalColumn: "DressId");
                });

            migrationBuilder.InsertData(
                table: "Dresses",
                columns: new[] { "DressId", "DressName", "OnSale", "Picture", "Price", "Size" },
                values: new object[,]
                {
                    { 1, "Dress 1", true, "1.jpg", 1186.00m, 3 },
                    { 2, "Dress 2", true, "2.jpg", 1818.00m, 3 },
                    { 3, "Dress 3", true, "3.jpg", 1348.00m, 4 },
                    { 4, "Dress 4", true, "4.jpg", 1986.00m, 4 },
                    { 5, "Dress 5", true, "5.jpg", 1502.00m, 1 }
                });

            migrationBuilder.InsertData(
                table: "Sales",
                columns: new[] { "SaleId", "Date", "DressId", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 5, 31, 18, 23, 16, 507, DateTimeKind.Local).AddTicks(1692), null, 1, 190 },
                    { 2, new DateTime(2022, 5, 26, 18, 23, 16, 507, DateTimeKind.Local).AddTicks(1725), null, 2, 183 },
                    { 3, new DateTime(2022, 9, 9, 18, 23, 16, 507, DateTimeKind.Local).AddTicks(1733), null, 3, 261 },
                    { 4, new DateTime(2023, 2, 1, 18, 23, 16, 507, DateTimeKind.Local).AddTicks(1742), null, 4, 202 },
                    { 5, new DateTime(2022, 10, 26, 18, 23, 16, 507, DateTimeKind.Local).AddTicks(1749), null, 5, 182 },
                    { 6, new DateTime(2022, 12, 27, 18, 23, 16, 507, DateTimeKind.Local).AddTicks(1758), null, 1, 226 },
                    { 7, new DateTime(2022, 12, 4, 18, 23, 16, 507, DateTimeKind.Local).AddTicks(1766), null, 2, 278 },
                    { 8, new DateTime(2023, 1, 23, 18, 23, 16, 507, DateTimeKind.Local).AddTicks(1773), null, 3, 240 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sales_DressId",
                table: "Sales",
                column: "DressId");
            string sqlI = @"CREATE PROC InsertDress @n VARCHAR(50), @p MONEY, @s INT, @pi VARCHAR(50), @o BIT
             AS
             INSERT INTO Dresses (DressName, Price, Size, Picture, OnSale)
             VALUES (@n, @p, @s, @pi, @o )
            
             GO";
            migrationBuilder.Sql(sqlI);
            string sqlU = @"CREATE PROC UpdateDress @i INT, @n VARCHAR(50), @p MONEY, @s INT, @pi VARCHAR(50), @o BIT
             AS
             UPDATE Dresses SET DressName=@n, Price=@p, Size=@s, Picture=@pi, OnSale=@o
             WHERE DressId=@i
             GO";
            migrationBuilder.Sql(sqlU);
            string sqlD = @"CREATE PROC DeleteDress @i INT
                 AS
                 DELETE Dresses 
                 WHERE DressId=@i
                 GO";
            migrationBuilder.Sql(sqlD);
            string sqlS = @"CREATE PROC InsertSale @d DATE, @q INT, @did INT
             AS
             INSERT INTO Sales ([Date], Quantity, DressId)
             VALUES (@d, @q, @did )
             GO";
            migrationBuilder.Sql(sqlS);
            string sqlSU = @"CREATE PROC UpdateSale @id INT, @d DATE, @q INT, @did INT
             AS
             UPDATE Sales SET [Date]=@d, Quantity=@q, DressId=@did
             WHERE SaleId = @id
             GO";
            migrationBuilder.Sql(sqlSU);
            string sqlDU = @"CREATE PROC DeleteSale @id INT
             AS
             DELETE Sales 
             WHERE SaleId = @id
             GO";
            migrationBuilder.Sql(sqlDU);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Dresses");
        }
    }
}
