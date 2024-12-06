
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ScreenSound.Modelos;

namespace ScreenSound.Data;

internal class ScreenSoundContext : DbContext
{

    public DbSet<Artista> Artistas { get; set; }
    public DbSet<Musica> Musicas { get; set; }

    private string ConnectionString = "Data Source=IGOR-TUDISCO\\IGORTUDISCO;Initial Catalog=ScreenSound;Integrated Security=True;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionString);
    }
       
}


/*
 
 * ORM significa Object-Relational Mapping e é uma técnica que permite a interação de um sistema orientado a objetos com um banco de dados relacional de maneira mais
 * intuitiva. O objetivo principal do ORM é mapear os objetos da aplicação diretamente para as tabelas do banco de dados utilizado, abstraindo esse acesso
 * e se tornando mais próximo do que utilizamos em orientação a objetos
 
 * Para aplicar essa técnica de maneira mais facilitada, temos as bibliotecas ORM que implementam e trazem um conjunto de ferramentas que fornecem
 * funcionalidades para mapeamento objeto-relacional, gerenciamento de entidades, geração de consultas SQL, entre outros.
 
 * Existem diversas bibliotecas ORM para as diversas linguagens de programação, e para o C# a mais utilizada no mercado de trabalho é o Entity Framework que vamos
 * trabalhar ao longo deste curso. Para conhecer mais sobre essa relação entre as bibliotecas ORM e o gerenciamento de dados, confira a página Módulos de conexão
 * para bancos de dados do Microsoft (https://learn.microsoft.com/pt-br/sql/connect/sql-connection-libraries?view=sql-server-ver16) SQL da documentação oficial.
 
 */


/*
 
 * O Entity Framework segue alguns padrões chamados de convenção para conseguir identificar corretamente os objetos, tabelas e informações que estamos
 * passando através dele. No vídeo anterior conhecemos algumas dessas convenções:
 
 * Chave primária identificada como Id;
 * Mapear o nome da classe como o nome da tabela através do DbSet.
 * 
 * Existem outras convenções que vão ser utilizadas de acordo com a necessidade e complexidade da aplicação que está sendo desenvolvida. Para saber mais
 * sobre este tema, você pode acessar a documentação da Microsoft (https://learn.microsoft.com/pt-br/ef/ef6/modeling/code-first/fluent/types-and-properties)
 * com mais informações sobre as convenções do Entity e exemplos de aplicação.
 
 */