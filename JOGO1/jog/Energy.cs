namespace jogo
{
    using System;
    
    /// <summary>
    /// Interface que representa a energia.
    /// </summary>
    public class Energy
    {
        /// <summary>
        /// Obtém ou define o valor da energia.
        /// </summary>
        public int Value { get; set; } // Pega o valor definido da energia e também define um valor.
        
        /// <summary>
        /// Construtor da classe Energy.
        /// </summary>
        /// <param name="value">Valor da energia a ser definido.</param>
        public Energy(int value)
        {
            Value = value; // Serve para atribuirmos um valor à energia na classe JewelColector.
        }
    }
}
