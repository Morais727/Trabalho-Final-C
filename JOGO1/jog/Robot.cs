namespace jogo;
using System;
using System.Collections.Generic;
    //<summary>
    // Classe reponsável por todas as funcionalidades do Robô.
    //</summary>

public class Robot : Cell
{   
    public bool great; // Mensagem de bom sinal
    public bool error; // Mensagem de erro
    public int currentLevel;
    public int currentRow; // Posição atual do robô (linha)
    public int currentColumn; // Posição atual do robô (coluna)
    public int currentEnergy; // Energia atual do robô
    public int currentPoints; //Pontuação atual
    private int totalJewel; //Rastrear o total de joias em jogo
    private Charger gameEnergy; //Referência a energia
    private Points gamePoints; //Referência aos pontos
    private Map gameMap; // Referência ao mapa
    private Level level;
    public List<Jewel> bagJewels; // Bag armazena as jóias coletadas
    public List<Energy> EnergyAmount; // Bag armazena a energia coletada
    
    public Robot(Map map, int initialRow, int initialColumn, int initialEnergy, Level currentLevel) //Define o robô recebendo o  mapa, sua posição inicial e sua vida inicial
    {
        level = currentLevel;
        totalJewel = level.GetTotalJewels();
        gamePoints = new Points(); 
        gameEnergy = new Charger();
        gameMap = map;
        currentRow = initialRow;
        currentColumn = initialColumn;
        map.SetCell(currentRow, currentColumn, this);
        bagJewels = new List<Jewel>(); //Sacola de jóias
        EnergyAmount = new List<Energy>(); //Sacola de energia
        Energy energy = new Energy(1); //Inicializa a energia
        for (int i = 0; i < initialEnergy; i++)
            {
                EnergyAmount.Add(energy); //Adiciona a energia inicial a sacola
            }
        currentEnergy = EnergyAmount.Count; //Contabiliza a energia
    }
    
    public void Display() //Imprime mensagens
    {   
        if (error) //Mensagem de erro
        {
            Console.ForegroundColor = ConsoleColor.Red; //Altera a cor para vermelho
            Console.Write(" ME ");
            Console.ResetColor(); //Restaura a cor
        }
        else
        {
            if (great) //Mensagem positiva de incentivo
            {
            Console.ForegroundColor = ConsoleColor.Green; //Altera a cor para verde
            Console.Write(" ME ");
            Console.ResetColor(); //Restaura a cor
            }
            else
            {
                Console.Write(" ME "); //Imprime o robô
            }
        }
    }
    public void really() //Mensagem para alerta da energia excessiva
    {
        if (currentEnergy > 10) //Acima de 10 "energias" o jogador será alertado e questionado
        {
           Console.WriteLine("Tem certeza de que precisa de tanta energia?");
        }
        if (currentEnergy >= 15) //Acima de 15 pontos de energia o jogador será alertado de que já tem energia suficiente
        {
            Console.BackgroundColor = ConsoleColor.DarkRed; //Altera a cor para vermelho
            Console.WriteLine("Já tem muita energia!"); //Alerta o jogador
            Console.ResetColor(); //Restaura a cor
        }
    }
    public void SeeYou() //Mensagem de até mais
    {
        Console.Clear(); //Limpa console
        Console.ForegroundColor = ConsoleColor.Green; //Altera a cor
        Console.WriteLine("\n\n\n\n\n\n\n\n               ATÉ MAIS! ;)\n\n\n\n\n\n\n\n "); //Mensagem 
        Console.ResetColor(); 
    }

    public void InvalidKey() //Tecla ou comando inválido
    {
        error = true; //Serve para ligar a mensagem 
        Console.Clear(); //Limpa console
        upDate(); //Atualiza as visualizações
        error = false; //Desliga erro

        Console.ForegroundColor = ConsoleColor.Red; //Altera cor
        Console.WriteLine("\nMovimento inválido"); //Alerta 
        Console.ResetColor(); //Restaura cor
    }
    public bool HasEnergy() //Verifica se o robô tem energia
    {
        return EnergyAmount.Count > 0; //Retorna positivo caso o robõ tenha energia
    }
    private void ConsumeEnergy() //Retira pontos de energia do robô a cada movimento
    {
        if (HasEnergy())
        {
            EnergyAmount.RemoveAt(0); // Remove a primeira energia da lista
            currentEnergy = EnergyAmount.Count; // Atualiza o valor da currentEnergy
        }

    }
    public void MoveUp() //Responsável por mover robô para cima 
    {
        int newRow = currentRow - 1; //Nova linha recebe linha atual -1, o robo sobe para a linha de cima 
        if (IsValidMove(newRow, currentColumn) && HasEnergy()) //Se o movimento for válido (veremos mais abaixo) e se o robô tiver energia 
        {
            ConsumeEnergy(); //Consome energia para realizar o seu movimento
            MoveToCell(newRow, currentColumn); //Leva o robô para sua nova posição no mapa
        }
        else
        {
            InvalidKey(); //Se o movimento for inválido ou não tiver energia. será alertado de que não é possível 
        }
    }

    public void MoveDown() //Responsável por mover robô para baixo 
    {
        int newRow = currentRow + 1; //Nova linha recebe linha atual +1, o robo sobe para a linha de baixo 
        if (IsValidMove(newRow, currentColumn) && HasEnergy()) //Se o movimento for válido (veremos mais abaixo) e se o robô tiver energia 
        {
            ConsumeEnergy(); //Consome energia para realizar o seu movimento
            MoveToCell(newRow, currentColumn); //Leva o robô para sua nova posição no mapa
        }
        else
        {
            InvalidKey(); //Se o movimento for inválido ou não tiver energia. será alertado de que não é possível 
        }
    }

    public void MoveLeft() //Movimentar para esquerda
    {
        int newColumn = currentColumn - 1;
        if (IsValidMove(currentRow, newColumn) && HasEnergy()) //Verifica a possibilidade
        {
            ConsumeEnergy(); //Consome energia
            MoveToCell(currentRow, newColumn); //Nova posição
        }
        else
        {
            InvalidKey(); //Alerta
        }
    }

    public void MoveRight() //Movimentar para direita
    {
        int newColumn = currentColumn + 1;
        if (IsValidMove(currentRow, newColumn) && HasEnergy()) //Verifica a possibilidade
        {
            ConsumeEnergy(); //Consome energia
            MoveToCell(currentRow, newColumn); //Nova posição
        }
        else
        {
            InvalidKey(); //Alerta
        }
    }
    
    public void Getitens() //Coleta itens do mapa
    {    
        int[] directions = { 1, -1 }; // Direções possíveis: 1 para direita e -1 para esquerda
        foreach (int direction in directions) //Verifica cada direção possível 
        {
            int newRow = currentRow + direction;
            if (Radar(newRow, currentColumn))
            {
                var cell = gameMap.GetCell(newRow, currentColumn); //Verifica se as celulas próximas são Jóias
                if (cell is Energy energy) //Verifica se as celulas próximas são de energia
                {
                    collectEnergy(energy); //Coleta o item energia e seu valor
                    return; // Sai do método após encontrar e coletar energia
                }
                else  if (cell is Jewel jewel)
                {
                    if (jewel is JewelBlue jewelBlue)
                    {
                        collectEnergy(new Energy(5));
                    }
                    collectJewel(jewel); //Coleta o item jóia e seu valor
                    MoveToCell(newRow, currentColumn);                   
                    return; // Sai do método após encontrar e mover-se para uma jóia
                } 
            }
        }
        foreach (int direction in directions) //Verifica cada direção possível 
        {
            int newColumn = currentColumn + direction;
            if (Radar(currentRow, newColumn))
            {
                
                var cell = gameMap.GetCell(currentRow, newColumn); //Verifica se as celulas próximas são Jóias
                if (cell is Energy energy) //Verifica se as celulas próximas são de energia
                {
                    collectEnergy(energy); //Coleta o item energia e seu valor
                    return; // Sai do método após encontrar e coletar energia
                }
                else if (cell is Jewel jewel)
                {
                    collectJewel(jewel); //Coleta o item jóia e seu valor
                    MoveToCell(currentRow, newColumn);
                    return; // Sai do método após encontrar e mover-se para uma jóia
                }
            }
        }
        InvalidKey(); //Alerta de movimento inválido
    }

    public void collectJewel(Jewel jewel) //Metodo para coletar jóia 
    {
        int jewelValue = jewel.Value;
        currentPoints += jewelValue;
        bagJewels.Add(jewel);
        totalJewel -= 1; 
        checkJewels();       
    }

    public void collectEnergy(Energy energy) //Metodo para coletar energia
    {
        if (currentEnergy < 15)  //Acima de 15 pontos de energia o jogador não poderá coletar mais energia 
        {       
            int energyValue = energy.Value;// Obtém o valor atual da energia
            for (int i = 0; i < energyValue; i++)
            {
                EnergyAmount.Add(energy); //Adiciona o objeto energia a sacola
            }
            currentEnergy = EnergyAmount.Count; //Atualiza o quanto de energia o robô tem atualmente
            upDate(); //Atualiza as visualizações 
            really();
        }
    }
    
    private bool Radar(int row, int column) //Verfica as celulas ao redor 
    { 
        return row >= 0 && row < gameMap.Rows && column >= 0 && column < gameMap.Columns; //Verifica se o robõ está dentro dos limites do mapa
    }
    private bool IsValidMove(int row, int column)  //Verfica as celulas ao redor para saber se o robô não irá se deslocar para uma celula já preenchida
    {
        return Radar(row, column) && gameMap.GetCell(row, column) is Empty; //verifica se a celula de destina está vazia 
    }
    private void MoveToCell(int newRow, int newColumn) //Movimenta o robõ para sua nova posição
    {   
        gameMap.SetCell(currentRow, currentColumn, new Empty()); //Define a celula atual do robõ como uma nova celula vazia    
        currentRow = newRow;
        currentColumn = newColumn;
        gameMap.SetCell(currentRow, currentColumn, this);
        upDate(); //Atualiza as visualizações
    }
    private void upDate() //Atualiza as visualizações de mapa, pontos e energia
    {   
        Console.Clear(); //Limpa console 
        gameMap.DisplayMap(); //Atualiza mapa
        gameEnergy.DisplayEnergy(currentEnergy); //Atualiza a energia 
        gamePoints.DisplayPoints(currentPoints); //Atualiza os pontos 
        Console.Write(totalJewel);    
        if (currentEnergy == 0) //Verifica se a energia do robõ  chegou ao fim
        {
            Console.Clear(); 
            Console.BackgroundColor = ConsoleColor.DarkRed; //Altera a cor 
            Console.WriteLine("O jogo foi reiniciado!"); //Alerta de que o jogoo recomeçou 
            Console.ResetColor(); //Restaura a cor  
            Level currentLevel = new Level();
            JewelCollector.PlayGame(); //Reinicia o jogo 
        }
    }
    private void checkJewels()
    {
         if (totalJewel == 0) //Verifica se as jóias chegaram ao fim
        {
            upDate();
            Console.BackgroundColor = ConsoleColor.DarkRed; //Altera a cor 
            Console.WriteLine("Parabéns, você avançou de level!"); //Alerta de que o jogoo recomeçou 
            Console.ResetColor(); //Restaura a cor 
            level.UpdateLevel();
        } 
    }
}
