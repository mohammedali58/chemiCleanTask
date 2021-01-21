using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChemiClean.Infrastructure.Migrations
{
    public partial class initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastModified = table.Column<DateTime>(nullable: true),
                    ProductName = table.Column<string>(maxLength: 250, nullable: false),
                    SupplierName = table.Column<string>(maxLength: 250, nullable: true),
                    Url = table.Column<string>(maxLength: 300, nullable: false),
                    FileContent = table.Column<byte[]>(type: "varbinary(MAX)", nullable: true),
                    Password = table.Column<string>(maxLength: 50, nullable: true),
                    UserName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
