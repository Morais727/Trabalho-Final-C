namespace jogo
{
    using System;

    /// <summary>
    /// Classe responsável por gerenciar e exibir a energia do robô no placar.
    /// </summary>
    public class Charger
    {
        private int currentEnergy; // Variável para armazenar a energia atual do robô.

        /// <summary>
        /// Construtor da classe Charger.
        /// </summary>
        public Charger()
        {
            DetermineColor(currentEnergy); // Determina a cor da visualização no placar, com base na energia atual.
        }

        /// <summary>
        /// Determina a cor da visualização no placar com base no valor da energia atual.
        /// </summary>
        /// <param name="currentEnergy">Valor da energia atual do robô.</param>
        public void DetermineColor(int currentEnergy)
        {
            // Determina a cor com base no valor da energia atual.
            switch (currentEnergy)
            {
                case >= 5: // Se a energia for maior ou igual a 5.
                    color = ConsoleColor.DarkGreen; // Define a cor como verde escuro.
                    break;
                case >= 3: // Se a energia for maior ou igual a 3.
                    color = ConsoleColor.DarkYellow; // Define a cor como amarelo escuro.
                    break;
                case < 3: // Se a energia for menor que 3.
                    color = ConsoleColor.DarkRed; // Define a cor como vermelho escuro.
                    break;
            }
        }

        private ConsoleColor color; // Variável para armazenar a cor a ser utilizada no display de energia.

        /// <summary>
        /// Exibe a energia atual do robô no placar.
        /// </summary>
        /// <param name="currentEnergy">Valor da energia atual do robô.</param>
        public void DisplayEnergy(int currentEnergy)
        {
            DetermineColor(currentEnergy); // Atualiza a cor com base no valor da energia atual.

            Console.WriteLine("+---------+"); // Decoração
            Console.WriteLine("|  Life   |");
            Console.WriteLine("+---------+");
            Console.BackgroundColor = color; // Define a cor de fundo do console.
            Console.Write(currentEnergy); // Exibe o valor da energia atual.
            Console.ResetColor(); // Reseta a cor de fundo do console para a cor padrão.
            Console.WriteLine(); // Pula uma linha
        }
    }
}
