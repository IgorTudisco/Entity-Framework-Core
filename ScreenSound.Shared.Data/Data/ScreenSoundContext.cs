
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ScreenSound.Modelos;
using ScreenSound.Shared.Dados.Modelos;
using ScreenSound.Shared.Models.Models;

namespace ScreenSound.Data;

/*
 
 * Agora as minhas classes PessoaComAcesso e PerfilDeAcesso que estão herdando de IdentityUser<int> e IdentityRole<int> respectivamente.
 * São responsáveis por representar as entidades de usuário e perfil de acesso no banco de dados e são utilizadas para a autenticação, além
 * de ter a responsabilidade de gerenciar as permissões e acessos dos usuários no sistema para modelar as classes de Artistas, Músicas e Gêneros.
 
 */

public class ScreenSoundContext : IdentityDbContext<PessoaComAcesso, PerfilDeAcesso, int>
{

    public DbSet<Artista> Artistas { get; set; }
    public DbSet<Musica> Musicas { get; set; }
    public DbSet<Genero> Generos { get; set; }
    public DbSet<AvaliacaoArtista> AvaliacaoArtistas { get; set; }

    private string ConnectionString = "Data Source=IGOR-TUDISCO\\IGORTUDISCO;Initial Catalog=ScreenSoundV0;Integrated Security=True;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

    //private string ConnectionString = "Server=tcp:screensoundserver.database.windows.net,1433;Initial Catalog=ScreenSoundV0;Persist Security Info=False;User ID=RootAZ;Password=@Root123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
    public ScreenSoundContext() { }

    public ScreenSoundContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;

        optionsBuilder.UseSqlServer(ConnectionString).UseLazyLoadingProxies(false);
    }

    /*
     
     * Foi incluída a linha de código base.OnModelCreating(modelBuilder), com o objetivo de permitir que códigos escritos em classes
     * ancestrais continuem sendo executados. Essa alteração foi feita para permitir o uso de uma outra biblioteca da Microsoft.
     
     */

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Musica>()
            .HasMany(c => c.Generos)
            .WithMany(c => c.Musica!);

        // Definindo a chave primária composta para a tabela de avaliação de artistas
        modelBuilder.Entity<AvaliacaoArtista>()
            .HasKey(c => new { c.ArtistaId, c.PessoaId });
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


/*
 
 * No contexto de banco de dados, utilizamos as migrations para gerenciar alterações no banco de dados. E no contexto em que estamos trabalhando
 * com o Entity Framework, as migrations são uma maneira de controlar as alterações no banco de dados com base nas modificações do projeto de maneira
 * controlada e automatizada.
 
 * Através delas conseguimos fazer inclusão e exclusão de tabelas, alterações de colunas e mudanças de informações, tudo isso atrelado à evolução
 * e crescimento do projeto de forma estruturada.
 
 * Além disso, utilizando as migrations, é possível ter um histórico estruturado das alterações que ocorreram no banco de dados, facilitando o trabalho
 * em equipe e também as atualizações em diversos ambientes existentes.
 
 * Para saber mais sobre as migrations, você pode consultar a documentação oficial.
 * (https://learn.microsoft.com/pt-br/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli)
 
 */

/*
 
 * Utilizamos o pacote Proxies para utilizar o carregamento lento de informações na aplicação. Ele permite que os recursos sejam utilizados
 * realmente quando forem necessários, otimizando o processo e sendo bastante útil quando temos recursos mais custosos na aplicação.
 
 * Para conhecer mais sobre o funcionamento do Proxies você pode acessar
 * a documentação oficial. (https://learn.microsoft.com/en-us/ef/core/querying/related-data/lazy)
 
 */

/*
  
 * A documentação sobre tópicos de segurança relacionados ao ASP.NET Core (https://learn.microsoft.com/en-us/aspnet/core/security/?view=aspnetcore-8.0),
 * relacionados ao framework web da Microsoft. Um dos tópicos presentes na documentação é esse que estamos usando,Authentication vs. Authorization
 * (https://learn.microsoft.com/en-us/aspnet/core/security/?view=aspnetcore-8.0#authentication-vs-authorization autenticação e autorização).
 
 * Vamos usar a solução Identity que está dentro de "Authentication". Mas, tem outras soluções disponíveis.
 
 * Autenticação e Autorização, é somente a ponta de um iceberg bem mais profundo chamado Segurança. A própria Microsoft busca levantar o tema em sua
 * página sobre segurança no ASP.NET Core (https://learn.microsoft.com/pt-br/aspnet/core/security/?view=aspnetcore-9.0). Nela, cita também outros
 * assuntos que você, como pessoa desenvolvedora, precisará estudar em outros momentos de sua carreira, tais como principais vulnerabilidades,
 * proteção de dados críticos, HTTPS e CORS.
 
 * Autenticação e Autorização são processos complementares para identificar e permitir o acesso da pessoa a recursos e informações
 * do sistema sendoprotegido. Uma solução de Gestão de Identidade e Acesso (IAM - Identity and Access Management)
 * deve fornecer estratégias para implantar estes dois processos. Leia mais sobre gestão de identidade e acesso neste artigo da
 * Microsoft (https://learn.microsoft.com/pt-br/azure/active-directory/fundamentals/introduction-identity-access-management).
 
 * Uma das soluções mais simples é a biblioteca ASP.NET Core Identity, que estamos usando, isso segundo sua
 * documentação (https://learn.microsoft.com/pt-br/aspnet/core/security/authentication/identity?view=aspnetcore-9.0&tabs=visual-studio).
 
 * ASP.NET Core Identity:
 
 * É uma API que suporta a funcionalidade de logon da interface do usuário (UI);
 
 * Gerencia usuários, senhas, dados de perfil, funções, declarações, tokens, confirmação por e-mail e muito mais.
 
  
 */