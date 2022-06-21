using Microsoft.EntityFrameworkCore.Migrations;

namespace AppNt.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Semesters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semesters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    IdentificationNumber = table.Column<int>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Lastname = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    GenderId = table.Column<int>(nullable: true),
                    RoleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Gender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Gender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Asignatures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    SemesterId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asignatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Asignatures_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profesors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Lastname = table.Column<string>(nullable: false),
                    Age = table.Column<string>(nullable: false),
                    AsignatureId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profesors_Asignatures_AsignatureId",
                        column: x => x.AsignatureId,
                        principalTable: "Asignatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: true),
                    ProfesorId = table.Column<int>(nullable: true),
                    valueVote = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Votes_Profesors_ProfesorId",
                        column: x => x.ProfesorId,
                        principalTable: "Profesors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Votes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Asignatures_SemesterId",
                table: "Asignatures",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Profesors_AsignatureId",
                table: "Profesors",
                column: "AsignatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_GenderId",
                table: "Users",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_ProfesorId",
                table: "Votes",
                column: "ProfesorId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_UserId",
                table: "Votes",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "Profesors");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Asignatures");

            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Semesters");
        }
    }
}
