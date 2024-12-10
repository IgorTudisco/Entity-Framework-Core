using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreenSound.Migrations
{
    /// <inheritdoc />
    public partial class PapularTabela : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Artistas", new string[] { "Nome", "Bio", "FotoPerfil" }, new object[] { "Djavan", "Djavan nasceu em Maceió, no estado de Alagoas, no dia 27 de janeiro de 1949. Filho de uma família de poucos recursos, na adolescência, aprendeu sozinho a tocar violão. Nessa época, ganhava a vida como meio-de-campo do CSA. Com 18 anos formou o conjunto Luz e já animava as festinhas em sua cidade.", "https://i.scdn.co/image/ab67616d0000b273ff6dbdb76c0cce850eb706e3" });

            migrationBuilder.InsertData("Artistas", new string[] { "Nome", "Bio", "FotoPerfil" }, new object[] { "Bryan Adams", "Bryan Guy Adams CC OBC (nascido em 5 de novembro de 1959) é um cantor e compositor canadense, músico, produtor musical e fotógrafo. Estima-se que ele tenha vendido entre 75 milhões e mais de 100 milhões de discos e singles em todo o mundo, colocando-o nalista dos artistas musicais mais vendidos.", "https://i.scdn.co/image/ab67616d0000b27305a41288f037a08fb45db5e2" });

            migrationBuilder.InsertData("Artistas", new string[] { "Nome", "Bio", "FotoPerfil" }, new object[] { "Foo Fighters", "Foo Fighters é uma banda de rock alternativo americana formada por Dave Grohl em 1995.", "https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_1280.png" });

            migrationBuilder.InsertData("Artistas", new string[] { "Nome", "Bio", "FotoPerfil" }, new object[] { "Gilberto Gil", "Gilberto Passos Gil Moreira é um cantor, compositor, multi-instrumentista, produtor musical, político e escritor brasileiro.", "https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_1280.png" });


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Artistas");
        }
    }
}
