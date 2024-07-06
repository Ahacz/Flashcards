using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Flashcards.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Flashcards",
                keyColumn: "Id",
                keyValue: new Guid("b3ffefb3-cb1b-40d1-a1bb-375ed53597d6"));

            migrationBuilder.DeleteData(
                table: "Flashcards",
                keyColumn: "Id",
                keyValue: new Guid("b7c44ab7-b9db-4048-bd9f-c9c8244815fc"));

            migrationBuilder.DeleteData(
                table: "Flashcards",
                keyColumn: "Id",
                keyValue: new Guid("fb20407f-e351-45b1-8eb4-34e666598147"));

            migrationBuilder.InsertData(
                table: "Flashcards",
                columns: new[] { "Id", "Description", "QueuePriority", "SnapshotPath", "Title" },
                values: new object[,]
                {
                    { new Guid("40bf5e5b-b1c5-4566-bdb1-1b9263c54822"), "Make an algorithm that finds a value in an (un)sorted array in fewest steps possible", 0, "f0a5f63d-6a35-4b05-abfa-84f6905c79b0", "Binary search" },
                    { new Guid("a09a8cce-4978-4a88-b03d-a389e1935705"), "Make an algorithm that accepts an integer and returns the nth number of fibonacci series", 0, "f0a5f63d-6a35-4b05-abfa-84f6905c79b0", "Fibonacci" },
                    { new Guid("a85b12d2-61eb-4def-b319-d55a70653df2"), "Implement a simple mail sending piece of code", 0, "f0a5f63d-6a35-4b05-abfa-84f6905c79b0", "Mail sender" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Flashcards",
                keyColumn: "Id",
                keyValue: new Guid("40bf5e5b-b1c5-4566-bdb1-1b9263c54822"));

            migrationBuilder.DeleteData(
                table: "Flashcards",
                keyColumn: "Id",
                keyValue: new Guid("a09a8cce-4978-4a88-b03d-a389e1935705"));

            migrationBuilder.DeleteData(
                table: "Flashcards",
                keyColumn: "Id",
                keyValue: new Guid("a85b12d2-61eb-4def-b319-d55a70653df2"));

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
    }
}
