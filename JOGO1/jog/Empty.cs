namespace jogo
{
    using System;
    
    /// <summary>
    /// Classe responsável pelas células vazias do mapa.
    /// </summary>
    public class Empty : Cell
    {
        /// <summary>
        /// Exibe a representação visual da célula vazia.
        /// </summary>
        public void Display()
        {
            Console.Write(" -- "); // Imprime o símbolo definido para a célula vazia.
        }
    }
}
