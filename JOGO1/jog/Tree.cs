namespace jogo;
using System;
    //<summary>
    // Classe reponsável pelas árvores e os seu valores de energia. 
    //</summary>

public class Tree : Energy, Cell //Herda da classe de energia
{
    public Tree(int value) : base(value) //Definir o valor da energia
        {}
    public void Display()
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen; //Altera a cor para verde
        Console.Write(" $$ "); //Imprime a arvore
        Console.ResetColor(); //Restaura a cor   
    }
}