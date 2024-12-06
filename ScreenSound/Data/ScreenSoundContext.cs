
using Microsoft.Data.SqlClient;
using ScreenSound.Modelos;

namespace ScreenSound.Data;

internal class ScreenSoundContext
{

    private string ConnectionString = "Data Source=IGOR-TUDISCO\\IGORTUDISCO;Initial Catalog=ScreenSound;Integrated Security=True;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

    public SqlConnection ObterConexao()
    {
        return new SqlConnection(ConnectionString);
    }

   
}