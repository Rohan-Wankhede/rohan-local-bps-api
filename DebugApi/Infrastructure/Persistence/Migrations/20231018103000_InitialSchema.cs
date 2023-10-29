using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DebugApi.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class InitialSchema : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Employees",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "TEXT", nullable: false),
                Grade = table.Column<int>(type: "INTEGER", nullable: false),
                Title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                IsManager = table.Column<bool>(type: "INTEGER", nullable: false),
                RowVersion = table.Column<byte[]>(type: "BLOB", rowVersion: true, nullable: true),
                CreatedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: false),
                LastModifiedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Employees", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Employees");
    }
}
