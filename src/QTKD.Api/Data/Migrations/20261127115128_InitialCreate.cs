using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QTKD.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    TaxCode = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    RepresentativeName = table.Column<string>(type: "text", nullable: true),
                    ContactInfo = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.CheckConstraint("CK_Customers_Status", "\"Status\" IN ('Active', 'Inactive')");
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContractNo = table.Column<string>(type: "text", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    PaymentTerms = table.Column<string>(type: "text", nullable: true),
                    ServiceTerms = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.CheckConstraint("CK_Contracts_Status", "\"Status\" IN ('Draft', 'PendingApproval', 'RevisionRequested', 'Rejected', 'Approved', 'Active', 'Cancelled', 'Expired')");
                    table.ForeignKey(
                        name: "FK_Contracts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContractAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContractId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    StorageKey = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractAttachments_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PriceLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ContractId = table.Column<Guid>(type: "uuid", nullable: false),
                    EffectiveDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceLists", x => x.Id);
                    table.CheckConstraint("CK_PriceLists_Status", "\"Status\" IN ('Draft', 'Submitted', 'Approved', 'Effective', 'Superseded', 'Rejected', 'Expired')");
                    table.ForeignKey(
                        name: "FK_PriceLists_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PriceItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PriceListId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceCode = table.Column<string>(type: "text", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceItems_PriceLists_PriceListId",
                        column: x => x.PriceListId,
                        principalTable: "PriceLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractAttachments_ContractId",
                table: "ContractAttachments",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ContractNo",
                table: "Contracts",
                column: "ContractNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_CustomerId",
                table: "Contracts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Code",
                table: "Customers",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PriceItems_PriceListId",
                table: "PriceItems",
                column: "PriceListId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceLists_ContractId",
                table: "PriceLists",
                column: "ContractId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractAttachments");

            migrationBuilder.DropTable(
                name: "PriceItems");

            migrationBuilder.DropTable(
                name: "PriceLists");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
