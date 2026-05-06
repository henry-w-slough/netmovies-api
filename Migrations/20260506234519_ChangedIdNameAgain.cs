using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace netmovies_api.Migrations
{
    /// <inheritdoc />
    public partial class ChangedIdNameAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "Movies",
                newName: "StorageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StorageId",
                table: "Movies",
                newName: "MovieId");
        }
    }
}
