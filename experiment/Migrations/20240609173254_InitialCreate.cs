using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExperimentalGoal.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerTB",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    Phoneno = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerTB", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    GameID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Race = table.Column<string>(type: "TEXT", nullable: false),
                    Age = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false),
                    Gender = table.Column<string>(type: "TEXT", nullable: false),
                    GameSelectionText = table.Column<string>(type: "TEXT", nullable: true),
                    PlayersOrder = table.Column<string>(type: "TEXT", nullable: true),
                    Team1 = table.Column<string>(type: "TEXT", nullable: true),
                    Team2 = table.Column<string>(type: "TEXT", nullable: true),
                    PursuingCollege = table.Column<string>(type: "TEXT", nullable: false),
                    Rating = table.Column<string>(type: "TEXT", nullable: true),
                    NoGame = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.GameID);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Race = table.Column<string>(type: "TEXT", nullable: false),
                    Age = table.Column<string>(type: "TEXT", nullable: false),
                    Gender = table.Column<string>(type: "TEXT", nullable: false),
                    Picture = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerTB");

            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
