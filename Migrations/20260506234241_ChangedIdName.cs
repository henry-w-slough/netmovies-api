using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace netmovies_api.Migrations
{
    /// <inheritdoc />
    public partial class ChangedIdName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MovieUUID",
                table: "Movies",
                newName: "MovieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "Movies",
                newName: "MovieUUID");
        }
    }
}
