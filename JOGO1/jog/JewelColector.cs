namespace jogo;

using System;
using System.Collections.Generic;
/// <summary>
/// Parte principal do programa, onde todos os parâmetros são inicializados.
/// </summary>
public class JewelCollector
{
    private static bool running = true;
    private static Level currentLevel;

    /// <summary>
    /// Função principal que é chamada ao iniciar o programa.
    /// </summary>
    public static void Main()
    {
        Console.Clear(); // Limpa o console para que tenhamos uma visualização mais limpa do jogo
        currentLevel = new Level(); // Cria o objeto Level com o nível inicial 1

        PlayGame(); // Inicia o jogo com o nível atual
    }

    /// <summary>
    /// Função responsável por iniciar e executar o jogo.
    /// </summary>
    public static void PlayGame()
    {
        Map gameMap = new Map(currentLevel.GetMaxRow(), currentLevel.GetMaxColumn());
        gameMap.FillEmptyCells();
        Points gamePoints = new Points();
        Charger gameEnergy = new Charger();

        Robot robot = new Robot(gameMap, 0, 0, 5, currentLevel);

        List<(int, int, int)> jewelPositions = currentLevel.GetJewelPositions();

        // Coloca as joias no mapa com suas respectivas posições e pontos
        foreach ((int row, int column, int points) in jewelPositions)
        {
            if (points == 100)
            {
                gameMap.SetCell(row, column, new JewelRed(points));
            }
            else if (points == 50)
            {
                gameMap.SetCell(row, column, new JewelGreen(points));
            }
            else if (points == 10)
            {
                gameMap.SetCell(row, column, new JewelBlue(points));
            }
        }

        List<(int, int)> treePositions = currentLevel.GetTreePositions();

        // Coloca as árvores no mapa com suas respectivas posições
        foreach ((int row, int column) in treePositions)
        {
            gameMap.SetCell(row, column, new Tree(3));
        }

        List<(int, int)> waterPositions = currentLevel.GetWaterPositions();

        // Coloca a água no mapa com suas respectivas posições
        foreach ((int row, int column) in waterPositions)
        {
            gameMap.SetCell(row, column, new Water());
        }

        gameMap.DisplayMap();
        gameEnergy.DisplayEnergy(robot.currentEnergy);
        gamePoints.DisplayPoints(robot.currentPoints);
        currentLevel.DisplayLevel();

        Console.CancelKeyPress += (sender, e) =>
        {
            robot.SeeYou();
            e.Cancel = true;
            running = false;
        };

        Dictionary<ConsoleKey, Action> keyActions = new Dictionary<ConsoleKey, Action>()
        {
            { ConsoleKey.W, () => robot.MoveUp() },
            { ConsoleKey.A, () => robot.MoveLeft() },
            { ConsoleKey.S, () => robot.MoveDown() },
            { ConsoleKey.D, () => robot.MoveRight() },
            { ConsoleKey.G, () => robot.Getitens() },
            { ConsoleKey.Q, () => { robot.SeeYou(); running = false; } }
        };

        while (running)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true); // true para não exibir a tecla pressionada

            if (keyActions.ContainsKey(keyInfo.Key))
            {
                keyActions[keyInfo.Key].Invoke();
            }
            else
            {
                robot.InvalidKey();
            }
        }
    }
}

