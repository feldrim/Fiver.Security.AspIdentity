using Microsoft.EntityFrameworkCore.Migrations;

namespace Security.AspIdentity.Migrations
{
    public partial class Reset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Business");

            migrationBuilder.DropTable(
                "Personnel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Business",
                table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsRoot = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ParentBusinessUnitId = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Business", x => x.Id);
                    table.ForeignKey(
                        "FK_Business_Business_ParentBusinessUnitId",
                        x => x.ParentBusinessUnitId,
                        "Business",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "Personnel",
                table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ReportsToId = table.Column<string>(nullable: true),
                    UserDataId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personnel", x => x.Id);
                    table.ForeignKey(
                        "FK_Personnel_Personnel_ReportsToId",
                        x => x.ReportsToId,
                        "Personnel",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Personnel_AspNetUsers_UserDataId",
                        x => x.UserDataId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_Business_ParentBusinessUnitId",
                "Business",
                "ParentBusinessUnitId");

            migrationBuilder.CreateIndex(
                "IX_Personnel_ReportsToId",
                "Personnel",
                "ReportsToId");

            migrationBuilder.CreateIndex(
                "IX_Personnel_UserDataId",
                "Personnel",
                "UserDataId");
        }
    }
}