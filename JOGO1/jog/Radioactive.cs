namespace jogo
{
    using System;

    //<summary>
    // Classe responsável pelas árvores e seus valores de energia.
    //</summary>
    public class Radioactive : Empty, Cell // Herda da classe Cell
    {
        public Radioactive()
        {

        }

        public void Display()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed; // Altera a cor para Vermelho
            Console.Write(" !! "); // Imprime o item radioativo
            Console.ResetColor(); // Restaura a cor   
        }
    }
}
