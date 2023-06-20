namespace jogo
{
    using System;
    
    /// <summary>
    /// Classe que representa as jóias.
    /// </summary>
    public class Jewel  
    {
        /// <summary>
        /// Obtém ou define o valor da jóia.
        /// </summary>
        public int Value { get; set; } // Pega o valor definido da jóia e também define um valor.
        
        /// <summary>
        /// Construtor da classe Jewel.
        /// </summary>
        /// <param name="value">Valor da jóia a ser definido.</param>
        public Jewel(int value)
        {
            Value = value; // Serve para atribuirmos um valor à jóia na classe JewelColector.
        }
    }
}
