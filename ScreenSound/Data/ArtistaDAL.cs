using Microsoft.Data.SqlClient;
using ScreenSound.Modelos;

namespace ScreenSound.Data;

internal class ArtistaDAL
{

    public IEnumerable<Artista> ListarArtistas()
    {
        var lista = new List<Artista>();
        using var connection = new Connection().ObterConexao();
        connection.Open();

        string sql = "SELECT * FROM Artistas;";
        SqlCommand cmd = new SqlCommand(sql, connection);
        using SqlDataReader dataReader = cmd.ExecuteReader();

        while (dataReader.Read())
        {

            string? nomeArtista = Convert.ToString(dataReader["Nome"]);
            string? bioArtista = Convert.ToString(dataReader["Bio"]);
            int idArtista = Convert.ToInt32(dataReader["Id"]);

            Artista artista = new(nomeArtista, bioArtista) { Id = idArtista };

            lista.Add(artista);
        }

        return lista;

    }

    public void AdicionarArtista(Artista artista)
    {
        using var connection = new Connection().ObterConexao();
        connection.Open();

        string sql = "INSERT INTO Artistas (Nome, FotoPerfil, Bio) VALUES (@nome, @perfilPadrão, @bio)";
        SqlCommand command = new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@nome", artista.Nome);
        command.Parameters.AddWithValue("@perfilPadrão", artista.FotoPerfil);
        command.Parameters.AddWithValue("@bio", artista.Bio);

        int retorno = command.ExecuteNonQuery();
        Console.WriteLine($"Linhas afetadas: {retorno}");

    }

    public void AtulaizaArtista(int id, Artista artista)
    {

        using var connection = new Connection().ObterConexao();
        connection.Open();

        string sql = "UPDATE Artistas SET Nome = @nome, Bio = @bio WHERE Id = @id";
        SqlCommand cmd = new SqlCommand(sql, connection);

        cmd.Parameters.AddWithValue("@nome", artista.Nome);
        cmd.Parameters.AddWithValue("@bio", artista.Bio);
        cmd.Parameters.AddWithValue("@id", id);

        int retorno = cmd.ExecuteNonQuery();
        Console.WriteLine($"Linhas afetadas: {retorno}");
        ListarArtistas();

    }


    public void ExcluirArtista(int id)
    {

        using var connection = new Connection().ObterConexao();
        connection.Open();

        string sql = "DELETE FROM Artistas WHERE Id = @id";
        SqlCommand command = new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@id", id);

        int retorno = command.ExecuteNonQuery();
        Console.WriteLine($"Linhas afetadas: {retorno}");
        ListarArtistas();

    }


}
