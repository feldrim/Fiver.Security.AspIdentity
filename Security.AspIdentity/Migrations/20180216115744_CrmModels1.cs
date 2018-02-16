using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Security.AspIdentity.Migrations
{
    public partial class CrmModels1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CrmPersonnel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    UserDataId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrmPersonnel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrmPersonnel_AspNetUsers_UserDataId",
                        column: x => x.UserDataId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CrmTitles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Description = table.Column<string>(maxLength: 50, nullable: false),
                    ParentId = table.Column<string>(nullable: true),
                    Subtitle = table.Column<string>(maxLength: 50, nullable: true),
                    Title = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrmTitles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrmTitles_CrmTitles_ParentId",
                        column: x => x.ParentId,
                        principalTable: "CrmTitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CrmUnits",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Description = table.Column<string>(maxLength: 150, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    ParentId = table.Column<string>(nullable: true),
                    Type = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrmUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrmUnits_CrmUnits_ParentId",
                        column: x => x.ParentId,
                        principalTable: "CrmUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CrmTitlePersonnels",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    PersonnelId = table.Column<int>(nullable: false),
                    TitleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrmTitlePersonnels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrmTitlePersonnels_CrmPersonnel_PersonnelId",
                        column: x => x.PersonnelId,
                        principalTable: "CrmPersonnel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CrmTitlePersonnels_CrmTitles_TitleId",
                        column: x => x.TitleId,
                        principalTable: "CrmTitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CrmTitleRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false),
                    TitleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrmTitleRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrmTitleRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CrmTitleRoles_CrmTitles_TitleId",
                        column: x => x.TitleId,
                        principalTable: "CrmTitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CrmUnitTitles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    TitleId = table.Column<string>(nullable: false),
                    UnitId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrmUnitTitles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrmUnitTitles_CrmTitles_TitleId",
                        column: x => x.TitleId,
                        principalTable: "CrmTitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CrmUnitTitles_CrmUnits_UnitId",
                        column: x => x.UnitId,
                        principalTable: "CrmUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CrmPersonnel_UserDataId",
                table: "CrmPersonnel",
                column: "UserDataId");

            migrationBuilder.CreateIndex(
                name: "IX_CrmTitlePersonnels_PersonnelId",
                table: "CrmTitlePersonnels",
                column: "PersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_CrmTitlePersonnels_TitleId",
                table: "CrmTitlePersonnels",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_CrmTitleRoles_RoleId",
                table: "CrmTitleRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_CrmTitleRoles_TitleId",
                table: "CrmTitleRoles",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_CrmTitles_ParentId",
                table: "CrmTitles",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_CrmUnits_ParentId",
                table: "CrmUnits",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_CrmUnitTitles_TitleId",
                table: "CrmUnitTitles",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_CrmUnitTitles_UnitId",
                table: "CrmUnitTitles",
                column: "UnitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CrmTitlePersonnels");

            migrationBuilder.DropTable(
                name: "CrmTitleRoles");

            migrationBuilder.DropTable(
                name: "CrmUnitTitles");

            migrationBuilder.DropTable(
                name: "CrmPersonnel");

            migrationBuilder.DropTable(
                name: "CrmTitles");

            migrationBuilder.DropTable(
                name: "CrmUnits");
        }
    }
}
