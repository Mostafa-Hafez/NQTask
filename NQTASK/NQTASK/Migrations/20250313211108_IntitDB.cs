using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NQTASK.Migrations
{
    /// <inheritdoc />
    public partial class IntitDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Acc_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Acc_ID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Pr_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Pr_ID);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Cl_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatetimeofRegistration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Account_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Cl_ID);
                    table.ForeignKey(
                        name: "FK_Clients_Accounts_Account_Id",
                        column: x => x.Account_Id,
                        principalTable: "Accounts",
                        principalColumn: "Acc_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    In_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OutDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    cl_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.In_Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Clients_cl_Id",
                        column: x => x.cl_Id,
                        principalTable: "Clients",
                        principalColumn: "Cl_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoice_Product",
                columns: table => new
                {
                    In_ID = table.Column<int>(type: "int", nullable: false),
                    Pr_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice_Product", x => new { x.Pr_ID, x.In_ID });
                    table.ForeignKey(
                        name: "FK_Invoice_Product_Invoices_In_ID",
                        column: x => x.In_ID,
                        principalTable: "Invoices",
                        principalColumn: "In_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoice_Product_Products_Pr_ID",
                        column: x => x.Pr_ID,
                        principalTable: "Products",
                        principalColumn: "Pr_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Account_Id",
                table: "Clients",
                column: "Account_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_Product_In_ID",
                table: "Invoice_Product",
                column: "In_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_cl_Id",
                table: "Invoices",
                column: "cl_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoice_Product");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
