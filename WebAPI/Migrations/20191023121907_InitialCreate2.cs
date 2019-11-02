using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class InitialCreate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    Birthday = table.Column<string>(nullable: true),
                    Biography = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SerieMovies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsSerie = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Episode = table.Column<int>(nullable: false),
                    Season = table.Column<int>(nullable: false),
                    Synopsis = table.Column<string>(nullable: true),
                    Background = table.Column<string>(nullable: true),
                    Producer = table.Column<string>(nullable: true),
                    Director = table.Column<string>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    Aired = table.Column<string>(nullable: false),
                    Duration = table.Column<string>(nullable: false),
                    Score = table.Column<float>(nullable: false),
                    Ranking = table.Column<int>(nullable: false),
                    Member = table.Column<int>(nullable: false),
                    Image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SerieMovies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Username = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    Birthday = table.Column<string>(nullable: true),
                    About = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Joined = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SerieMovieActors",
                columns: table => new
                {
                    SerieMovieId = table.Column<int>(nullable: false),
                    ActorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SerieMovieActors", x => new { x.SerieMovieId, x.ActorId });
                    table.ForeignKey(
                        name: "FK_SerieMovieActors_Actors_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SerieMovieActors_SerieMovies_SerieMovieId",
                        column: x => x.SerieMovieId,
                        principalTable: "SerieMovies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SerieMovieGenres",
                columns: table => new
                {
                    SerieMovieId = table.Column<int>(nullable: false),
                    GenreId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SerieMovieGenres", x => new { x.SerieMovieId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_SerieMovieGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SerieMovieGenres_SerieMovies_SerieMovieId",
                        column: x => x.SerieMovieId,
                        principalTable: "SerieMovies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserFriends",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    FriendId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFriends", x => new { x.UserId, x.FriendId });
                    table.ForeignKey(
                        name: "FK_UserFriends_Users_FriendId",
                        column: x => x.FriendId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserFriends_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WatchLists",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    SerieMovieId = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Score = table.Column<int>(nullable: false),
                    Episode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchLists", x => new { x.UserId, x.SerieMovieId });
                    table.ForeignKey(
                        name: "FK_WatchLists_SerieMovies_SerieMovieId",
                        column: x => x.SerieMovieId,
                        principalTable: "SerieMovies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WatchLists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SerieMovieActors_ActorId",
                table: "SerieMovieActors",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_SerieMovieGenres_GenreId",
                table: "SerieMovieGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFriends_FriendId",
                table: "UserFriends",
                column: "FriendId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchLists_SerieMovieId",
                table: "WatchLists",
                column: "SerieMovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SerieMovieActors");

            migrationBuilder.DropTable(
                name: "SerieMovieGenres");

            migrationBuilder.DropTable(
                name: "UserFriends");

            migrationBuilder.DropTable(
                name: "WatchLists");

            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "SerieMovies");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
