namespace jogo
{
    using System;
    
    /// <summary>
    /// Classe que representa as jóias azuis e seus valores.
    /// </summary>
    public class JewelBlue : Jewel, Cell // Usa a classe Jewel como base
    {
        /// <summary>
        /// Construtor da classe JewelBlue.
        /// </summary>
        /// <param name="value">Valor da jóia azul a ser definido.</param>
        public JewelBlue(int value) : base(value) // Usamos para definir o valor da jóia
        {
           
        }
        
        /// <summary>
        /// Método para exibir a jóia azul.
        /// </summary>
        public void Display() // Imprime a jóia
        {
            Console.BackgroundColor = ConsoleColor.Cyan; // Define a cor da jóia
            Console.Write(" JB "); // Imprime a jóia
            Console.ResetColor(); // Restaura a cor padrão 
        }
    }
}
