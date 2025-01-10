﻿
using ScreenSound.Modelos;

namespace ScreenSound.Shared.Models.Models;

public class Genero
{
    public int Id { get; set; }
    public string? Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; } = string.Empty;
    public virtual ICollection<Musica?>? Musica { get; set; }

    public Genero()
    {
    }

    public override string ToString()
    {
        return $"Nome: {Nome} - Descrição: {Descricao}";
    }
}
