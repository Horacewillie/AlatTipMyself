﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlatTipMyself.Api.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserDetails",
                columns: table => new
                {
                    AcctNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    AcctName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AcctBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDetails", x => x.AcctNumber);
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    WalletId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AcctNumber = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    WalletBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TipStatus = table.Column<bool>(type: "bit", nullable: false),
                    TipPercent = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.WalletId);
                    table.ForeignKey(
                        name: "FK_Wallets_UserDetails_AcctNumber",
                        column: x => x.AcctNumber,
                        principalTable: "UserDetails",
                        principalColumn: "AcctNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WalletHistories",
                columns: table => new
                {
                    WalletHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AcctNumber = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    WalletId = table.Column<int>(type: "int", nullable: false),
                    TransactionAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TipPercent = table.Column<int>(type: "int", nullable: false),
                    TipAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletHistories", x => x.WalletHistoryId);
                    table.ForeignKey(
                        name: "FK_WalletHistories_UserDetails_AcctNumber",
                        column: x => x.AcctNumber,
                        principalTable: "UserDetails",
                        principalColumn: "AcctNumber",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WalletHistories_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "WalletId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WalletHistories_AcctNumber",
                table: "WalletHistories",
                column: "AcctNumber");

            migrationBuilder.CreateIndex(
                name: "IX_WalletHistories_WalletId",
                table: "WalletHistories",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_AcctNumber",
                table: "Wallets",
                column: "AcctNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WalletHistories");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropTable(
                name: "UserDetails");
        }
    }
}
