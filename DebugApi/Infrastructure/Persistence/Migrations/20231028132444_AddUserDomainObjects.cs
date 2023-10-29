using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DebugApi.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUserDomainObjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    UserEmail = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    UserRole = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    UserStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    SurName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "BLOB", rowVersion: true, nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastModifiedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
