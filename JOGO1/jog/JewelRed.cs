namespace jogo
{
    using System;
    
    /// <summary>
    /// Jóias vermelhas e seus valores.
    /// </summary>
    public class JewelRed : Jewel, Cell // Herda da classe Jewel
    {
        public JewelRed(int value) : base(value) // Define o valor da jóia
        {
            
        }
        
        /// <summary>
        /// Responsável por imprimir a jóia.
        /// </summary>
        public void Display()
        {
            Console.BackgroundColor = ConsoleColor.DarkRed; // Muda a cor da jóia para vermelho
            Console.Write(" JR "); // Imprime a jóia
            Console.ResetColor(); // Volta à cor padrão
        }
    }
}
