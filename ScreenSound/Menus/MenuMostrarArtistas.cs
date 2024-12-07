﻿using ScreenSound.Data;
using ScreenSound.Modelos;

namespace ScreenSound.Menus;

internal class MenuMostrarArtistas : Menu
{
    public override void Executar(ArtistaDAL artistaDal)
    {
        base.Executar(artistaDal);
        ExibirTituloDaOpcao("Exibindo todos os artistas registradas na nossa aplicação");

        foreach (var artista in artistaDal.ListarArtistas())
        {
            Console.WriteLine($"Artista: {artista}");
        }

        Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
        Console.ReadKey();
        Console.Clear();
    }
}
