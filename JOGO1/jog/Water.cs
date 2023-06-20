namespace jogo;
using System;
    //<summary>
    // Classe reponsável pela barreira "água".
    //</summary>

public class Water : Cell //Herda da classe Cell
{
    public void Display()
    {
        Console.ForegroundColor = ConsoleColor.DarkBlue; //Atera a cor para azul
        Console.Write(" ## "); //Imprime o símbolo da água 
        Console.ResetColor(); //Restaura a cor 
    }
}    