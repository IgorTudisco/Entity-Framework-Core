using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreenSound.Migrations
{
    /// <inheritdoc />
    public partial class PapularRelacionamentoMusicaGenero : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO GeneroMusica (GenerosId, MusicaId) VALUES (1, 1)");
            migrationBuilder.Sql("INSERT INTO GeneroMusica (GenerosId, MusicaId) VALUES (1, 2)");
            migrationBuilder.Sql("INSERT INTO GeneroMusica (GenerosId, MusicaId) VALUES (3, 3)");
            migrationBuilder.Sql("INSERT INTO GeneroMusica (GenerosId, MusicaId) VALUES (3, 4)");
            migrationBuilder.Sql("INSERT INTO GeneroMusica (GenerosId, MusicaId) VALUES (4, 4)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM GeneroMusica");
        }
    }
}
