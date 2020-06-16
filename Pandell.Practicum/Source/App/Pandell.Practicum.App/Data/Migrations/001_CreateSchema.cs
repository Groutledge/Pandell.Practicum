using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pandell.Practicum.App.Data.Migrations
{
    public partial class CreateSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable("RandomSequence", table => new
            {
                Id = table.Column<byte[]>(nullable: false),
                GeneratedSequence = table.Column<JsonObject<string[]>>(nullable: false),
                DateInserted = table.Column<DateTime>(nullable: false),
                DateUpdated = table.Column<DateTime>(nullable: true),
                LastModifiedBy = table.Column<string>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_RandomSequence", x => x.Id);
            });
        }
    }
}