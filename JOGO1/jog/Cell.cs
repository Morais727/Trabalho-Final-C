namespace jogo;
using System;

/// <summary>
/// Interface de células que gera a visualização, base para mapa e itens do jogo.
/// </summary>
public interface Cell
{
    /// <summary>
    /// Gera a visualização da célula.
    /// </summary>
    void Display();
}
