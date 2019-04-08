using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Server.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    GroupNum = table.Column<int>(nullable: true),
                    GroupID = table.Column<int>(nullable: true),
                    SpecialtyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specialties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    MidName = table.Column<string>(nullable: true),
                    Birthday = table.Column<DateTime>(nullable: true),
                    Education = table.Column<string>(nullable: true),
                    CertNum = table.Column<string>(nullable: true),
                    AttNum = table.Column<string>(nullable: true),
                    Language = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    PassportType = table.Column<int>(nullable: true),
                    PassportNumber = table.Column<string>(nullable: true),
                    PassportPlace = table.Column<string>(nullable: true),
                    PassportDate = table.Column<DateTime>(nullable: true),
                    Region = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    House = table.Column<string>(nullable: true),
                    FlatNum = table.Column<string>(nullable: true),
                    FacticalRegion = table.Column<string>(nullable: true),
                    FacticalCity = table.Column<string>(nullable: true),
                    FacticalStreet = table.Column<string>(nullable: true),
                    FacticalHouse = table.Column<string>(nullable: true),
                    FacticalFlatNum = table.Column<string>(nullable: true),
                    AddressesIdentical = table.Column<bool>(nullable: false),
                    CaseNum = table.Column<int>(nullable: true),
                    StudyForm = table.Column<int>(nullable: true),
                    Degree = table.Column<int>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    AverageGrade = table.Column<float>(nullable: true),
                    GroupIndex = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Specialties");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
