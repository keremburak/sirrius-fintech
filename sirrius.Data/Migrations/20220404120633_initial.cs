using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace sirrius.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Default = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperationCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BICCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Banks_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Companies_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Companies_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Companies_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BankStatements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankStatementDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatementNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpeningBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClosingBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClosingAvailableBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankStatements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankStatements_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyBankAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IBAN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    BankId = table.Column<int>(type: "int", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyBankAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyBankAccounts_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyBankAccounts_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyBankAccounts_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyUsers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FTPConnectionSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Host = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Port = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BankId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FTPConnectionSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FTPConnectionSettings_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FTPConnectionSettings_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MyClientAccountingCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MyClientFTPMatchingId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyClientAccountingCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyClientAccountingCodes_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MyClientAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MyClientFTPMatchingId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyClientAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyClientAccounts_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankStatementTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsCredit = table.Column<bool>(type: "bit", nullable: false),
                    CurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OperationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperationCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsTransferred = table.Column<bool>(type: "bit", nullable: false),
                    ClientReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankStatementId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankStatementTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankStatementTransactions_BankStatements_BankStatementId",
                        column: x => x.BankStatementId,
                        principalTable: "BankStatements",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MyClientFTPMatchings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchingWord = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperationTypeId = table.Column<byte>(type: "tinyint", nullable: false),
                    TransactionTypeId = table.Column<byte>(type: "tinyint", nullable: false),
                    MyClientAccountId = table.Column<int>(type: "int", nullable: false),
                    MyClientAccountingCodeId = table.Column<int>(type: "int", nullable: false),
                    OperationCodeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyClientFTPMatchings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyClientFTPMatchings_MyClientAccountingCodes_MyClientAccountingCodeId",
                        column: x => x.MyClientAccountingCodeId,
                        principalTable: "MyClientAccountingCodes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MyClientFTPMatchings_MyClientAccounts_MyClientAccountId",
                        column: x => x.MyClientAccountId,
                        principalTable: "MyClientAccounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MyClientFTPMatchings_OperationCodes_OperationCodeId",
                        column: x => x.OperationCodeId,
                        principalTable: "OperationCodes",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Active", "CreatedAt", "CreatedBy", "Deleted", "Name" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(1349), 0, false, "Altın Mücevherat" },
                    { 25, true, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(1385), 0, false, "Taşımacılık" },
                    { 24, true, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(1384), 0, false, "Catering" },
                    { 23, true, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(1383), 0, false, "Turizm" },
                    { 22, true, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(1381), 0, false, "Bilişim" },
                    { 21, true, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(1380), 0, false, "Mobilya" },
                    { 20, true, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(1379), 0, false, "İnsaat Malzemeleri" },
                    { 19, true, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(1378), 0, false, "Tarım Aletleri" },
                    { 18, true, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(1376), 0, false, "Madencilik" },
                    { 17, true, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(1375), 0, false, "İlaç Sanayi" },
                    { 16, true, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(1374), 0, false, "Temizlik Maddeleri" },
                    { 14, true, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(1371), 0, false, "Kimya" },
                    { 15, true, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(1373), 0, false, "Oto Ana/Yan Sanayi" },
                    { 12, true, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(1368), 0, false, "Gıda" },
                    { 2, true, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(1355), 0, false, "Ambalaj" },
                    { 3, true, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(1356), 0, false, "Ayakkabı" },
                    { 4, true, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(1358), 0, false, "Boya" },
                    { 13, true, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(1369), 0, false, "Hazır Giyim" },
                    { 6, true, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(1360), 0, false, "Deri ve Deri Mamülleri" },
                    { 5, true, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(1359), 0, false, "Demir Çelik" },
                    { 8, true, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(1363), 0, false, "Tekstil" },
                    { 9, true, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(1364), 0, false, "Gemi İnşa" },
                    { 10, true, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(1366), 0, false, "Denizcilik" },
                    { 11, true, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(1367), 0, false, "Halı İmalatı" },
                    { 7, true, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(1362), 0, false, "Elektrikli Aletler" }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Active", "CreatedAt", "CreatedBy", "Deleted", "Email", "FirstName", "LastName", "Name", "PhoneNumber", "UserId" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2022, 4, 4, 12, 6, 32, 3, DateTimeKind.Utc).AddTicks(7545), 0, false, "kerem@test.com", "Kerem", "Burak", null, "111223344", 2 },
                    { 2, true, new DateTime(2022, 4, 4, 12, 6, 32, 4, DateTimeKind.Utc).AddTicks(67), 0, false, "john@test.com", "John", "Doe", null, "111223344", 3 }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Active", "Code", "CreatedAt", "CreatedBy", "Default", "Deleted", "Name" },
                values: new object[,]
                {
                    { 2, true, "CT", new DateTime(2022, 4, 4, 12, 6, 32, 859, DateTimeKind.Utc).AddTicks(9359), 0, false, false, "KKTC" },
                    { 1, true, "TR", new DateTime(2022, 4, 4, 12, 6, 32, 859, DateTimeKind.Utc).AddTicks(8481), 0, true, false, "Türkiye" }
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Active", "CreatedAt", "CreatedBy", "Deleted", "Description", "Name" },
                values: new object[,]
                {
                    { 4, true, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(4449), 0, false, "Sterlin", "GBP" },
                    { 3, true, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(4447), 0, false, "Euro", "EUR" },
                    { 2, true, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(4443), 0, false, "Dolar", "USD" },
                    { 1, true, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(4108), 0, false, "Türk Lirası", "TRY" }
                });

            migrationBuilder.InsertData(
                table: "OperationCodes",
                columns: new[] { "Id", "Active", "Code", "CreatedAt", "CreatedBy", "Deleted", "Description", "Name" },
                values: new object[,]
                {
                    { 47, true, "EQA", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3489), 0, false, "", "KENDİ HESABINA HARİCİ TRANSFER" },
                    { 46, true, "DKK", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3486), 0, false, "", "EŞDEĞER MİKTAR" },
                    { 45, true, "DIV", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3484), 0, false, "", "TEMETTÜLER" },
                    { 44, true, "DIS", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3481), 0, false, "", "KAZANÇ ÖDEMESİ" },
                    { 43, true, "DDT", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3479), 0, false, "", "OTOMATİK ÖDEME ÖĞESİ" },
                    { 42, true, "DCR", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3476), 0, false, "", "BELGESEL KREDİ" },
                    { 41, true, "CPN", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3474), 0, false, "", "KUPON ÖDEMELERİ" },
                    { 40, true, "COM", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3472), 0, false, "", "KOMİSYON" },
                    { 39, true, "COL", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3469), 0, false, "", "TAHSİLATLAR" }
                });

            migrationBuilder.InsertData(
                table: "OperationCodes",
                columns: new[] { "Id", "Active", "Code", "CreatedAt", "CreatedBy", "Deleted", "Description", "Name" },
                values: new object[,]
                {
                    { 38, true, "CMP", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3467), 0, false, "", "TAZMİNAT TALEPLERİ" },
                    { 37, true, "CLR", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3464), 0, false, "", "NAKİT / ÇEK HAVALESİ" },
                    { 36, true, "CHK", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3462), 0, false, "", "ÇEKLER" },
                    { 35, true, "BRF", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3460), 0, false, "", "ARACILIK ÜCRETİ" },
                    { 48, true, "MAR", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3491), 0, false, "", "TEMİNAT ÖDEMELERİ/MAKBUZLAR" },
                    { 34, true, "BOE", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3457), 0, false, "", "SENET" },
                    { 49, true, "MAT", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3493), 0, false, "", "VADE" },
                    { 51, true, "NWI", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3498), 0, false, "", "YENİ İHRAÇLARIN DAĞITIMI" },
                    { 52, true, "ODC", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3501), 0, false, "", "FAZLA ÇEKİM ÜCRETİ" },
                    { 53, true, "PCH", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3503), 0, false, "", "SATIN ALMA (STIF VE VADELİ MEVDUAT DAHİL)" },
                    { 54, true, "PRN", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3505), 0, false, "", "ANAPARA ÖDEMESİ/ÖDEMESİ" },
                    { 55, true, "REC", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3508), 0, false, "", "VERGİ İADESİ" },
                    { 56, true, "RTI", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3510), 0, false, "", "İADE EDİLEN ÜRÜN" },
                    { 57, true, "STP", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3512), 0, false, "", "DAMGA VERGİSİ" },
                    { 58, true, "SWP", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3514), 0, false, "", "SWAP ÖDEMESİ" },
                    { 59, true, "TCK", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3517), 0, false, "", "SEYAHAT ÇEKLERİ" },
                    { 60, true, "TCM", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3519), 0, false, "", "ÜÇLÜ TEMİNAT YÖNETİMİ" },
                    { 61, true, "TRA", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3521), 0, false, "", "KENDİ HESABINA DAHİLİ TRANSFER" },
                    { 62, true, "TRN", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3523), 0, false, "", "İŞLEM ÜCRETİ" },
                    { 63, true, "UWC", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3525), 0, false, "", "SİGORTA KOMİSYONU" },
                    { 50, true, "MGT", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3496), 0, false, "", "YÖNETİM ÜCRETLERİ" },
                    { 33, true, "BNK", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3455), 0, false, "", "BANKA ÜCRETLERİ" },
                    { 17, true, "SEC", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3416), 0, false, "", "REPO GERİ DÖNÜŞ" },
                    { 31, true, "CCP", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3449), 0, false, "", "KREDİ KARTI  PEŞİNSATIŞ" },
                    { 1, true, "COC", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3372), 0, false, "", "KREDİ KARTI KOMİSYONU" },
                    { 2, true, "DOM", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3378), 0, false, "", "TEMİNAT MEKTUP KOMİSYONU" },
                    { 3, true, "DDB", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3381), 0, false, "", "MÜŞTERİ HESABINDAN" },
                    { 4, true, "EFT", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3383), 0, false, "", "GELEN EFT" },
                    { 5, true, "EXP", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3386), 0, false, "", "İHRACAT TAHSİLATI" },
                    { 6, true, "FEX", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3388), 0, false, "", "DÖVİZ SATIŞ" },
                    { 7, true, "FND", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3390), 0, false, "", "STOPAJ ÜZERİNDEN" },
                    { 8, true, "IFN", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3393), 0, false, "", "YATIRIM FONU SATIŞI" },
                    { 9, true, "IMC", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3395), 0, false, "", "İTHALAT MASRAFI" },
                    { 10, true, "IMP", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3398), 0, false, "", "İTHALAT ÖDEMESİ" },
                    { 11, true, "INS", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3400), 0, false, "", "REPO GELİRİ" },
                    { 12, true, "INT", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3402), 0, false, "", "FAİZ GELİRİ VADELİ" },
                    { 13, true, "LDP", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3405), 0, false, "", "KREDİ AÇILIŞI" },
                    { 14, true, "MSC", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3407), 0, false, "", "DİĞER İŞLEMLER" },
                    { 32, true, "CHG", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3451), 0, false, "", "DİĞER BANKA MASRAFLARI" },
                    { 15, true, "PRO", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3410), 0, false, "", "KARŞILIKSIZ SENET PROTESTOSU" },
                    { 25, true, "TRF", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3435), 0, false, "", "GELEN HAVALE" },
                    { 18, true, "SLR", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3418), 0, false, "", "TOPLU MAAŞ ÖDEME" }
                });

            migrationBuilder.InsertData(
                table: "OperationCodes",
                columns: new[] { "Id", "Active", "Code", "CreatedAt", "CreatedBy", "Deleted", "Description", "Name" },
                values: new object[,]
                {
                    { 19, true, "SSK", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3420), 0, false, "", "SSK TAHSİLATLARI" },
                    { 20, true, "STM", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3423), 0, false, "", "DAMGA VERGİSİ" },
                    { 21, true, "STX", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3426), 0, false, "", "ÖZEL İŞEM VERGİSİ" },
                    { 22, true, "SUF", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3428), 0, false, "", "KKDF ÖDEMESİ" },
                    { 23, true, "TAX", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3431), 0, false, "", "STOPAJ KESİNTİSİ" },
                    { 24, true, "TDP", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3433), 0, false, "", "VADELİ HESAP KAPANIŞI" },
                    { 16, true, "RDP", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3413), 0, false, "", "KREDİ AÇILIŞ" },
                    { 26, true, "TXP", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3437), 0, false, "", "VERGİ TAHSİLATLARI" },
                    { 27, true, "VRG", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3440), 0, false, "", "DİĞER VERGİ TAHSİLATLARI" },
                    { 28, true, "VRM", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3442), 0, false, "", "GELEN VİRMAN" },
                    { 29, true, "WHT", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3444), 0, false, "", "VADELİ HESAP VERGİ" },
                    { 30, true, "ZRH", new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(3447), 0, false, "", "NAKİT TOPLAMA - TAHSİLAT" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Active", "CreatedAt", "CreatedBy", "Deleted", "Name" },
                values: new object[,]
                {
                    { 2, true, new DateTime(2022, 4, 4, 12, 6, 32, 5, DateTimeKind.Utc).AddTicks(3658), 0, false, "Admin" },
                    { 1, true, new DateTime(2022, 4, 4, 12, 6, 32, 5, DateTimeKind.Utc).AddTicks(3227), 0, false, "SuperAdmin" },
                    { 3, true, new DateTime(2022, 4, 4, 12, 6, 32, 5, DateTimeKind.Utc).AddTicks(3661), 0, false, "User" }
                });

            migrationBuilder.InsertData(
                table: "Banks",
                columns: new[] { "Id", "Active", "BICCode", "Code", "CountryId", "CreatedAt", "CreatedBy", "Deleted", "Description", "Name" },
                values: new object[,]
                {
                    { 1, true, "", "0001", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2124), 0, false, "", "T.C. MERKEZ BANKASI" },
                    { 27, true, "TBNKTRIS", "00108", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2201), 0, false, "", "Turkland Bank A.Ş." },
                    { 28, true, "TEKBTRIS", "00109", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2204), 0, false, "", "Tekstil Bankası A.Ş." },
                    { 29, true, "FNNBTRIS", "00111", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2207), 0, false, "", "Finans Bank A.Ş." },
                    { 30, true, "YAPITRISFEX", "00115", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2209), 0, false, "", "Deutsche Bank A.Ş." },
                    { 31, true, "TAIBTRIS", "00116", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2212), 0, false, "", "Taib Yatırım Bank A.Ş." },
                    { 32, true, "BSUITRIS", "00121", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2214), 0, false, "", "Credit Agricole Yatırım Bankası Türk A.Ş." },
                    { 33, true, "SOGETRIS", "00122", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2217), 0, false, "", "Societe Generale" },
                    { 34, true, "HSBCTRIX", "00123", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2220), 0, false, "", "HSBC Bank A.Ş." },
                    { 35, true, "ALFBTRIS", "00124", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2222), 0, false, "", "Alternatif Bank A.Ş." },
                    { 36, true, "TEKFTRIS", "00125", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2224), 0, false, "", "Eurobank Tekfen A.Ş." },
                    { 37, true, "MLMBIE2XIST", "00129", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2227), 0, false, "", "Merrill Lynch Yatırım Bank A.Ş." },
                    { 38, true, "TVABTRIS", "00132", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2230), 0, false, "", "İMKB Takas ve Saklama Bankası A.Ş." },
                    { 39, true, "DENITRIS", "00134", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2232), 0, false, "", "Denizbank A.Ş." },
                    { 40, true, "ANDLTRIS", "00135", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2235), 0, false, "", "Anadolubank A.Ş." },
                    { 41, true, "DYAKTRIS", "00138", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2238), 0, false, "", "Diler Yatırım Bankası A.Ş." },
                    { 42, true, "GSDBTRIS", "00139", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2240), 0, false, "", "GSD Yatırım Bankası A.Ş." },
                    { 43, true, "NUROTRIS", "00141", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2243), 0, false, "", "Nurol Yatırım Bankası A.Ş." },
                    { 45, true, "CAYTTRIS", "00143", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2248), 0, false, "", "Aktif Yatırım Bankası A.Ş." },
                    { 46, true, "KTEFTRIS", "00205", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2251), 0, false, "", "Kuveyt Türk Katılım Bankası A.Ş." },
                    { 47, true, "AFKBTRIS", "00206", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2253), 0, false, "", "Türkiye Finans Katılım Bankası A.Ş." },
                    { 48, true, "ASYATRIS", "00208", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2256), 0, false, "", "Asya Katılım Bankası A.Ş." },
                    { 26, true, "WELATRIS", "00106", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2198), 0, false, "", "WestLB AG" },
                    { 25, true, "FBHLTRIS", "00103", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2196), 0, false, "", "Fibabanka A.Ş." },
                    { 44, true, "BPTRTRIS", "00142", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2246), 0, false, "", "BankPozitif Kredi ve Kalkınma Bankası A.Ş." },
                    { 23, true, "INGBTRIS", "0099", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2190), 0, false, "", "ING Bank A.Ş." },
                    { 24, true, "ADABTRIS", "00100", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2193), 0, false, "", "Adabank A.Ş." },
                    { 2, true, "", "0004", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2132), 0, false, "", "İLLER BANKASI A.Ş." },
                    { 3, true, "TCZBTR2A", "0010", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2135), 0, false, "", "T.C.ZİRAAT BANKASI A.Ş." },
                    { 5, true, "TSKBTRIS", "0014", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2141), 0, false, "", "T.SINAİ KALKINMA BANKASI A.Ş." },
                    { 6, true, "TVBATR2A", "0015", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2144), 0, false, "", "T.VAKIFLAR BANKASI T.A.O" },
                    { 7, true, "TIKBTR2A", "0016", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2146), 0, false, "", "T.EXİMBANK" },
                    { 8, true, "TKBNTR2A", "0017", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2149), 0, false, "", "T.KALKINMA BANKASI A.Ş." },
                    { 9, true, "BAYDTRIS", "0029", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2152), 0, false, "", "BİRLEŞİK FON BANKASI A.Ş." },
                    { 10, true, "TEBUTRIS", "0032", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2155), 0, false, "", "T.EKONOMİ BANKASI A.Ş." },
                    { 11, true, "AKBKTRIS", "0046", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2158), 0, false, "", "AKBANK T.A.Ş." },
                    { 4, true, "TRHBTR2A", "0012", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2138), 0, false, "", "T.HALK BANKASI A.Ş." },
                    { 13, true, "TGBATRIS", "0062", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2164), 0, false, "", "T.GARANTİ BANKASI A.Ş." },
                    { 12, true, "SEKETR2A", "0059", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2161), 0, false, "", "ŞEKERBANK T.A.Ş." },
                    { 21, true, "HABBTRIS", "0097", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2185), 0, false, "", "Habib Bank Limited" },
                    { 19, true, "BKMTTRIS", "0094", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2181), 0, false, "", "YBank Mellat" },
                    { 18, true, "CITITRIX", "0092", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2178), 0, false, "", "Citibank A.Ş." }
                });

            migrationBuilder.InsertData(
                table: "Banks",
                columns: new[] { "Id", "Active", "BICCode", "Code", "CountryId", "CreatedAt", "CreatedBy", "Deleted", "Description", "Name" },
                values: new object[,]
                {
                    { 20, true, "TUBATRIS", "0096", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2183), 0, false, "", "Turkish Bank A.Ş." },
                    { 16, true, "ABNATRIS", "0088", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2173), 0, false, "", "The Royal Bank of Scotland N.V." },
                    { 15, true, "YAPITRISFEX", "0067", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2169), 0, false, "", "YAPI VE KREDİ BANKASI A.Ş." },
                    { 14, true, "ISBKTRIS", "0064", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2166), 0, false, "", "T.İŞ BANKASI A.Ş." },
                    { 17, true, "ATUBTRIS", "0091", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2175), 0, false, "", "Arap Türk Bankası A.Ş." },
                    { 22, true, "CHASTRIS", "0098", 1, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(2188), 0, false, "", "JPMorgan Chase Bank N.A." }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Active", "CountryId", "CreatedAt", "CreatedBy", "Deleted", "Name" },
                values: new object[,]
                {
                    { 60, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(677), 0, false, "TOKAT" },
                    { 59, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(676), 0, false, "TEKİRDAĞ" },
                    { 53, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(576), 0, false, "RİZE" },
                    { 58, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(674), 0, false, "SİVAS" },
                    { 57, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(582), 0, false, "SİNOP" },
                    { 56, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(581), 0, false, "SİİRT" },
                    { 55, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(579), 0, false, "SAMSUN" },
                    { 54, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(577), 0, false, "SAKARYA" },
                    { 48, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(569), 0, false, "MUĞLA" },
                    { 51, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(573), 0, false, "NİĞDE" },
                    { 50, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(572), 0, false, "NEVŞEHİR" },
                    { 49, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(571), 0, false, "MUŞ" },
                    { 47, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(568), 0, false, "MARDİN" },
                    { 46, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(566), 0, false, "KAHRAMANMARAŞ" },
                    { 45, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(565), 0, false, "MANİSA" },
                    { 61, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(680), 0, false, "TRABZON" },
                    { 44, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(563), 0, false, "MALATYA" },
                    { 52, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(575), 0, false, "ORDU" },
                    { 62, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(681), 0, false, "TUNCELİ" },
                    { 73, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(697), 0, false, "ŞIRNAK" },
                    { 64, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(684), 0, false, "UŞAK" },
                    { 43, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(562), 0, false, "KÜTAHYA" },
                    { 81, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(708), 0, false, "DÜZCE" },
                    { 80, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(707), 0, false, "OSMANİYE" },
                    { 79, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(705), 0, false, "KİLİS" },
                    { 78, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(704), 0, false, "KARABÜK" },
                    { 77, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(702), 0, false, "YALOVA" },
                    { 76, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(701), 0, false, "IĞDIR" },
                    { 75, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(700), 0, false, "ARDAHAN" },
                    { 74, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(698), 0, false, "BARTIN" },
                    { 72, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(696), 0, false, "BATMAN" },
                    { 71, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(694), 0, false, "KIRIKKALE" },
                    { 70, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(693), 0, false, "KARAMAN" },
                    { 69, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(691), 0, false, "BAYBURT" },
                    { 68, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(689), 0, false, "AKSARAY" },
                    { 67, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(688), 0, false, "ZONGULDAK" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Active", "CountryId", "CreatedAt", "CreatedBy", "Deleted", "Name" },
                values: new object[,]
                {
                    { 66, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(687), 0, false, "YOZGAT" },
                    { 65, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(685), 0, false, "VAN" },
                    { 63, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(682), 0, false, "ŞANLIURFA" },
                    { 42, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(561), 0, false, "KONYA" },
                    { 19, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(504), 0, false, "ÇORUM" },
                    { 40, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(558), 0, false, "KIRŞEHİR" },
                    { 18, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(503), 0, false, "ÇANKIRI" },
                    { 17, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(501), 0, false, "ÇANAKKALE" },
                    { 16, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(500), 0, false, "BURSA" },
                    { 15, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(498), 0, false, "BURDUR" },
                    { 14, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(496), 0, false, "BOLU" },
                    { 13, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(495), 0, false, "BİTLİS" },
                    { 12, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(404), 0, false, "BİNGÖL" },
                    { 11, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(402), 0, false, "BİLECİK" },
                    { 10, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(401), 0, false, "BALIKESİR" },
                    { 9, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(399), 0, false, "AYDIN" },
                    { 8, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(396), 0, false, "ARTVİN" },
                    { 7, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(395), 0, false, "ANTALYA" },
                    { 6, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(393), 0, false, "ANKARA" },
                    { 5, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(392), 0, false, "AMASYA" },
                    { 4, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(387), 0, false, "AĞRI" },
                    { 3, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(386), 0, false, "AFYON" },
                    { 2, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(381), 0, false, "ADIYAMAN" },
                    { 41, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(559), 0, false, "KOCAELİ" },
                    { 1, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(23), 0, false, "ADANA" },
                    { 20, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(506), 0, false, "DENİZLİ" },
                    { 22, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(509), 0, false, "EDİRNE" },
                    { 39, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(556), 0, false, "KIRKLARELİ" },
                    { 38, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(555), 0, false, "KAYSERİ" },
                    { 37, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(553), 0, false, "KASTAMONU" },
                    { 36, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(552), 0, false, "KARS" },
                    { 35, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(550), 0, false, "İZMİR" },
                    { 34, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(549), 0, false, "İSTANBUL" },
                    { 33, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(548), 0, false, "İÇEL" },
                    { 32, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(546), 0, false, "ISPARTA" },
                    { 31, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(544), 0, false, "HATAY" },
                    { 30, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(543), 0, false, "HAKKARİ" },
                    { 29, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(542), 0, false, "GÜMÜŞHANE" },
                    { 28, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(540), 0, false, "GİRESUN" },
                    { 27, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(539), 0, false, "GAZİANTEP" },
                    { 26, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(538), 0, false, "ESKİŞEHİR" },
                    { 25, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(536), 0, false, "ERZURUM" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Active", "CountryId", "CreatedAt", "CreatedBy", "Deleted", "Name" },
                values: new object[,]
                {
                    { 24, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(524), 0, false, "ERZİNCAN" },
                    { 23, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(510), 0, false, "ELAZIĞ" },
                    { 21, true, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(508), 0, false, "DİYARBAKIR" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "CreatedAt", "CreatedBy", "Deleted", "Email", "FirstName", "LastName", "Name", "PasswordHash", "RefreshToken", "RefreshTokenExpiryTime", "RoleId", "UpdatedAt", "UpdatedBy", "UserName" },
                values: new object[,]
                {
                    { 2, true, new DateTime(2022, 4, 4, 12, 6, 32, 222, DateTimeKind.Utc).AddTicks(5518), 1, false, "kerem@test.com", "Kerem", "Burak", null, "$2a$11$AuJCHjecOQc6ArgUmM01.OEvpwKsfTeOI7Q8J7/lIg3Myf787cYEK", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, 0, "kerem" },
                    { 3, true, new DateTime(2022, 4, 4, 12, 6, 32, 437, DateTimeKind.Utc).AddTicks(5371), 0, false, "john@test.com", "John", "Doe", null, "$2a$11$SgU2a2NM41YIQ17Ku3xVS.hmyI7B5aYkEdZk8rCB0yXjL1JSQmcHO", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, 0, "john" },
                    { 1, true, new DateTime(2022, 4, 4, 12, 6, 32, 5, DateTimeKind.Utc).AddTicks(4304), 0, false, "admin@metixcrm.com", "Admin", "Admin", null, "$2a$11$MbuallqanQzWq960K8VFG.1plQFPVLdAQnRzcnhYW56R4ihTdmFJq", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, 0, "admin" },
                    { 4, true, new DateTime(2022, 4, 4, 12, 6, 32, 650, DateTimeKind.Utc).AddTicks(2175), 0, false, "jane@test.com", "Jane", "Doe", null, "$2a$11$c88v3TaZDhOclhKtER8yhel1PmmHOnz7cxAQ2S5D05FadqMmoSQU2", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, null, 0, "jane" }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Active", "Address", "CategoryId", "CityId", "ClientId", "CountryId", "CreatedAt", "CreatedBy", "Deleted", "Email", "FaxNumber", "Name", "PhoneNumber", "ShortName", "TaxNumber" },
                values: new object[] { 1, true, "Gebze", 22, 34, 1, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(1948), 1, false, "info@metix.com.tr", "", "Metix Bilişim A.Ş", "0262-5551055", "MBAS", "12345678" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Active", "Address", "CategoryId", "CityId", "ClientId", "CountryId", "CreatedAt", "CreatedBy", "Deleted", "Email", "FaxNumber", "Name", "PhoneNumber", "ShortName", "TaxNumber" },
                values: new object[] { 2, true, "Kocaeli", 22, 41, 2, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(6020), 1, false, "sambga@metix.com.tr", "", "Samba Bilişim A.Ş", "0262-456465", "SBA", "12345678" });

            migrationBuilder.InsertData(
                table: "CompanyUsers",
                columns: new[] { "Id", "CompanyId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 }
                });

            migrationBuilder.InsertData(
                table: "FTPConnectionSettings",
                columns: new[] { "Id", "Active", "Address", "BankId", "CompanyId", "CreatedAt", "CreatedBy", "Deleted", "Host", "Name", "Password", "Port", "UpdatedAt", "UpdatedBy", "UserName" },
                values: new object[] { 1, true, "192.168.56.1", 1, 1, new DateTime(2022, 4, 4, 12, 6, 32, 860, DateTimeKind.Utc).AddTicks(9913), 1, false, "Rebex Host", "Rebex Tiny FTP Server", "password", 22, null, null, "tester" });

            migrationBuilder.InsertData(
                table: "MyClientAccountingCodes",
                columns: new[] { "Id", "Active", "Code", "CompanyId", "CreatedAt", "CreatedBy", "Deleted", "Description", "MyClientFTPMatchingId", "Name" },
                values: new object[,]
                {
                    { 1, true, "300.100.101", 2, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(6479), 0, false, null, 0, "Aktifler" },
                    { 2, true, "400.100.102", 2, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(7174), 0, false, null, 0, "KDV" },
                    { 3, true, "500.100.103", 2, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(7178), 0, false, null, 0, "Cariler" },
                    { 4, true, "600.100.104", 2, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(7180), 0, false, null, 0, "Giderler" }
                });

            migrationBuilder.InsertData(
                table: "MyClientAccounts",
                columns: new[] { "Id", "Active", "Code", "CompanyId", "CreatedAt", "CreatedBy", "Deleted", "Description", "MyClientFTPMatchingId", "Name" },
                values: new object[,]
                {
                    { 1, true, "100.100.101", 2, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(5017), 0, false, null, 0, "XYZ Izolasyon LTD STI" },
                    { 2, true, "100.100.102", 2, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(5731), 0, false, null, 0, "MDF Sunta LTD STI" },
                    { 3, true, "200.100.103", 2, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(5736), 0, false, null, 0, "Bakiroglu Yapi LTD STI" },
                    { 4, true, "200.100.104", 2, new DateTime(2022, 4, 4, 12, 6, 32, 861, DateTimeKind.Utc).AddTicks(5737), 0, false, null, 0, "Adatepe Koll.Ltd" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Banks_CountryId",
                table: "Banks",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_BankStatements_CompanyId",
                table: "BankStatements",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BankStatementTransactions_BankStatementId",
                table: "BankStatementTransactions",
                column: "BankStatementId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CategoryId",
                table: "Companies",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CityId",
                table: "Companies",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ClientId",
                table: "Companies",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CountryId",
                table: "Companies",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyBankAccounts_BankId",
                table: "CompanyBankAccounts",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyBankAccounts_CompanyId",
                table: "CompanyBankAccounts",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyBankAccounts_CurrencyId",
                table: "CompanyBankAccounts",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUsers_CompanyId",
                table: "CompanyUsers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUsers_UserId",
                table: "CompanyUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FTPConnectionSettings_BankId",
                table: "FTPConnectionSettings",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_FTPConnectionSettings_CompanyId",
                table: "FTPConnectionSettings",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_MyClientAccountingCodes_CompanyId",
                table: "MyClientAccountingCodes",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_MyClientAccounts_CompanyId",
                table: "MyClientAccounts",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_MyClientFTPMatchings_MyClientAccountId",
                table: "MyClientFTPMatchings",
                column: "MyClientAccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MyClientFTPMatchings_MyClientAccountingCodeId",
                table: "MyClientFTPMatchings",
                column: "MyClientAccountingCodeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MyClientFTPMatchings_OperationCodeId",
                table: "MyClientFTPMatchings",
                column: "OperationCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankStatementTransactions");

            migrationBuilder.DropTable(
                name: "CompanyBankAccounts");

            migrationBuilder.DropTable(
                name: "CompanyUsers");

            migrationBuilder.DropTable(
                name: "DocumentTypes");

            migrationBuilder.DropTable(
                name: "FTPConnectionSettings");

            migrationBuilder.DropTable(
                name: "MyClientFTPMatchings");

            migrationBuilder.DropTable(
                name: "OperationTypes");

            migrationBuilder.DropTable(
                name: "BankStatements");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "MyClientAccountingCodes");

            migrationBuilder.DropTable(
                name: "MyClientAccounts");

            migrationBuilder.DropTable(
                name: "OperationCodes");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
