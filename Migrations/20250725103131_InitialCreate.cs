using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace core8_angular_mssql.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    lastname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    firstname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    roles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isactivated = table.Column<int>(type: "int", nullable: false),
                    isblocked = table.Column<int>(type: "int", nullable: false),
                    mailtoken = table.Column<int>(type: "int", nullable: false),
                    qrcodeurl = table.Column<string>(type: "ntext", nullable: true),
                    profilepic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    secretkey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
