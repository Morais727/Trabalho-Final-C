namespace jogo
{
    using System;
    
    /// <summary>
    /// Jóias verdes e seus valores.
    /// </summary>
    public class JewelGreen : Jewel, Cell // Herda da classe Jewel
    {
        public JewelGreen(int value) : base(value) // Define o valor da jóia
        {
            
        }
        
        /// <summary>
        /// Responsável por imprimir a jóia.
        /// </summary>
        public void Display()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen; // Muda a cor da jóia para verde
            Console.Write(" JG "); // Imprime a jóia
            Console.ResetColor(); // Volta à cor padrão
        }
    }
}
