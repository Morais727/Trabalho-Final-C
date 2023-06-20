namespace jogo
{
    using System;

    //<summary>
    // Classe responsável por gerenciar e exibir os pontos.
    //</summary>
    public class Points
    {
        public Points()
        {

        }

        public void DisplayPoints(int currentPoints) //Imprimir os pontos 
        {
            Console.WriteLine("+---------+"); // Cabeçalho decorativo
            Console.WriteLine("|  Score  |");
            Console.WriteLine("+---------+");
            Console.BackgroundColor = ConsoleColor.DarkMagenta; // Muda a cor
            Console.Write(currentPoints); // Imprime os pontos 
            Console.ResetColor(); // Volta cor ao padrão
            Console.WriteLine(); // Pula linha
        }
    }
}
