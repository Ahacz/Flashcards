using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Flashcards.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flashcards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    SnapshotPath = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    QueuePriority = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flashcards", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Flashcards",
                columns: new[] { "Id", "Description", "QueuePriority", "SnapshotPath", "Title" },
                values: new object[,]
                {
                    { new Guid("b3ffefb3-cb1b-40d1-a1bb-375ed53597d6"), "Make an algorithm that accepts an integer and returns the nth number of fibonacci series", 0, "19dfb113-4b44-4d85-885a-2d552ed30ad0", "Fibonacci" },
                    { new Guid("b7c44ab7-b9db-4048-bd9f-c9c8244815fc"), "Make an algorithm that finds a value in an (un)sorted array in fewest steps possible", 0, "19dfb113-4b44-4d85-885a-2d552ed30ad0", "Binary search" },
                    { new Guid("fb20407f-e351-45b1-8eb4-34e666598147"), "Implement a simple mail sending piece of code", 0, "19dfb113-4b44-4d85-885a-2d552ed30ad0", "Mail sender" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flashcards");
        }
    }
}
