using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreenSound.Migrations
{
    /// <inheritdoc />
    public partial class PopularTabelaGenero : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Generos", new string[] { "Nome", "Descricao" }, new object[] { "MPB", "A canção 'Oceano', do renomado artista brasileiro Djavan, é uma obra que mergulha nas profundezas do amor e da paixão, explorando a intensidade e a complexidade dos sentimentos que acompanham um relacionamento amoroso." });

            migrationBuilder.InsertData("Generos", new string[] { "Nome", "Descricao" }, new object[] { "Samba", "O samba é um gênero musical que surgiu no Brasil, no começo do século XX, e é reconhecido nacional e internacionalmente como um dos símbolos do país. Essa expressão cultural é considerada patrimônio cultural imaterial brasileiro e surgiu nas comunidades de afro-brasileiros em alguns bairros do Rio de Janeiro" });

            migrationBuilder.InsertData("Generos", new string[] { "Nome", "Descricao" }, new object[] { "Rock", "O rock surgiu nos Estados Unidos na década de 1950 e teve seu auge nas décadas de 1970 e 1980. Ao longo do tempo, o rock se diversificou em vários subgêneros, como o rock clássico, o rock alternativo, o punk rock, o heavy metal, o grunge e o indie rock" });

            migrationBuilder.InsertData("Generos", new string[] { "Nome", "Descricao" }, new object[] { "Rap", "O rap é um discurso rítmico, geralmente sem acompanhamento de instrumentos musicais tradicionais, mas acompanhado por um DJ. As letras do rap são carregadas de simbolismo e expressões, e os rappers podem externar os seus sentimentos e defender as suas ideias" });

            migrationBuilder.InsertData("Generos", new string[] { "Nome", "Descricao" }, new object[] { "Eletrônica", "A música eletrônica é um gênero musical que se caracteriza pelo uso de instrumentos eletrônicos e tecnologia de produção musical para criar sons e ritmos inovadores" });

            migrationBuilder.InsertData("Generos", new string[] { "Nome", "Descricao" }, new object[] { "Jazz", "O jazz surgiu entre 1890 e 1910 em Nova Orleans. É relativamente difícil estabelecer uma definição para esse estilo musical, porém podemos dizer que ele é marcado pela improvisação, o swing e os ritmos não lineares. O jazz tem suas raízes na música negra americana pouco antes de 1850" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Generos");
        }
    }
}
