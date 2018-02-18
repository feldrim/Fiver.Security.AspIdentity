using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Security.AspIdentity.Migrations
{
    public partial class Guid_on_Personnel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PersonnelId2",
                table: "CrmTitlePersonnels");

            migrationBuilder.Sql("UPDATE CrmTitlePersonnels SET PersonnelId2 = PersonnelId");

            migrationBuilder.DropColumn(
                name: "PersonnelId",
                table: "CrmTitlePersonnels");

            migrationBuilder.RenameColumn(
                name: "PersonnelId2",
                table: "CrmTitlePersonnels",
                newName: "PersonnelId");

            migrationBuilder.AddColumn<Guid>(
                name: "Id2",
                table: "CrmPersonnel")
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                .Annotation("Key", true);

            migrationBuilder.Sql("UPDATE CrmPersonnel SET Id2 = Id");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CrmTitlePersonnels");

            migrationBuilder.RenameColumn(
                name: "Id2",
                table: "CrmTitlePersonnels",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PersonnelId",
                table: "CrmTitlePersonnels",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "CrmPersonnel",
                nullable: false,
                oldClrType: typeof(Guid))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
        }
    }
}
