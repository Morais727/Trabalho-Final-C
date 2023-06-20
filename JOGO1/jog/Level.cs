
namespace jogo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Position
    {
        public int Row { get; set; }
        public int Column { get; set; }
    }

    public class Level
    {
        private int currentLevel;
        private int totalJewels = 6;
        private int totalTrees = 5;
        private int maxRow = 30;
        private int maxColumn = 30;
        private List<Position> radioactivePositions;
        private List<(int, int)> waterPositions;
        private List<(int, int)> treePositions;
        private List<(int, int, int)> jewelPositions;

        public Level()
        {
            currentLevel = 1;
            WaterPosition();
            TreePosition();
            JewelPosition();
            MapSize();
            RadioactivePosition();
        }

        public bool IsOccupiedByOtherItems(int row, int column)
        {
            bool isWater = waterPositions.Contains((row, column));
            bool isTree = treePositions.Contains((row, column));
            bool isJewel = jewelPositions.Any(jewel => jewel.Item1 == row && jewel.Item2 == column);

            return isWater || isTree || isJewel;
        }

        public void RadioactivePosition()
        {
            radioactivePositions = new List<Position>();
            Random random = new Random();

            int numRadioactivePositions = currentLevel - 1; // Define o número de posições radioativas

            for (int i = 0; i < numRadioactivePositions; i++)
            {
                int row, column;
                do
                {
                    row = random.Next(maxRow);
                    column = random.Next(maxColumn);
                } while (!IsCellEmpty(row, column));

                radioactivePositions.Add(new Position { Row = row, Column = column });
            }
        }

        public void WaterPosition()
        {
            waterPositions = new List<(int, int)>
            {
                (5, 0), (5, 1), (5, 2), (5, 3), (5, 4), (5, 5), (5, 6)
            };
        }

        public List<(int, int)> GetWaterPositions()
        {
            return waterPositions;
        }

        public void TreePosition()
        {
            treePositions = new List<(int, int)>
            {
                (5, 9), (3, 9), (8, 3), (2, 5), (1, 4)
            };
            totalTrees = treePositions.Count;

            if (currentLevel > 1)
            {
                Random random = new Random();
                int row, column;
                bool validPosition = false;

                while (!validPosition)
                {
                    // Gera coordenadas aleatórias dentro dos limites do mapa
                    row = random.Next(maxRow);
                    column = random.Next(maxColumn);

                    // Verifica se a célula está vazia, não está ocupada por outras entidades, não é a célula (0, 0)
                    // e não está ocupada por outros itens
                    if (IsCellEmpty(row, column) && !IsOccupiedByOtherItems(row, column) && row != 0 && column != 0)
                    {
                        validPosition = true;
                    }
                     // Adiciona a nova posição aleatória à lista
                treePositions.Add((row, column));
                }
                totalTrees = treePositions.Count;
            }
        }

        public List<(int, int)> GetTreePositions()
        {
            return treePositions;
        }

        public void JewelPosition()
        {
            jewelPositions = new List<(int, int, int)>
            {
                (1, 9, 100), (8, 8, 100), (9, 1, 50), (7, 6, 50), (2, 1, 10), (9, 4, 10)
            };
            totalJewels = jewelPositions.Count;
            if (currentLevel > 1)
            {
                totalJewels++;
                Random random = new Random();
                for (int i = 0; i < totalJewels; i++)
                {
                    int row, column;
                    bool validPosition = false;

                    while (!validPosition)
                    {
                        // Gera coordenadas aleatórias dentro dos limites do mapa
                        row = random.Next(maxRow);
                        column = random.Next(maxColumn);

                        // Verifica se a célula está vazia, não está ocupada por outras entidades, não é a célula (0, 0)
                        // e não está ocupada por outros itens
                        if (IsCellEmpty(row, column) && !IsOccupiedByOtherItems(row, column) && row != 0 && column != 0)
                        {
                            validPosition = true;
                        }
                        // Adiciona a nova posição aleatória à lista
                    jewelPositions.Add((row, column, jewelPositions[i].Item3));
                    }
                }
            }
            totalJewels = jewelPositions.Count;
        }

        public List<(int, int, int)> GetJewelPositions()
        {
            return jewelPositions;
        }

        public bool IsCellEmpty(int row, int column)
        {
            bool isWater = waterPositions.Contains((row, column));
            bool isTree = treePositions.Contains((row, column));
            bool isJewel = jewelPositions.Any(jewel => jewel.Item1 == row && jewel.Item2 == column);

            return !isWater && !isTree && !isJewel;
        }

        public void ShufflePositions<T>(List<T> positions) where T : Position
        {
            Random random = new Random();
            int n = positions.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = positions[k];
                positions[k] = positions[n];
                positions[n] = value;
            }

            for (int i = 0; i < positions.Count; i++)
            {
                if (positions[i] is Position position)
                {
                    int row = position.Row;
                    int column = position.Column;

                    if (row >= maxRow || column >= maxColumn || !IsCellEmpty(row, column))
                    {
                        do
                        {
                            row = random.Next(maxRow);
                            column = random.Next(maxColumn);
                        } while (!IsCellEmpty(row, column));

                        positions[i] = new Position { Row = row, Column = column } as T;
                    }
                }
            }
        }

        public void ShuffleMap()
        {
            ShufflePositions(waterPositions.Select(pos => new Position { Row = pos.Item1, Column = pos.Item2 }).ToList());
            ShufflePositions(treePositions.Select(pos => new Position { Row = pos.Item1, Column = pos.Item2 }).ToList());
            ShufflePositions(jewelPositions.Select(pos => new Position { Row = pos.Item1, Column = pos.Item2 }).ToList());
            if (currentLevel > 1)
            {
                ShufflePositions(radioactivePositions);
            }
        }

        private int CalculateMapSize()
        {
            return 10 + currentLevel - 1;
        }

        public void MapSize()
        {
            maxRow = CalculateMapSize();
            maxColumn = CalculateMapSize();

            if (maxRow > 30)
            {
                maxRow = 30;
            }

            if (maxColumn > 30)
            {
                maxColumn = 30;
            }
        }

        public int GetMaxRow()
        {
            return CalculateMapSize();
        }

        public int GetMaxColumn()
        {
            return CalculateMapSize();
        }

        public int GetTotalJewels()
        {
            return totalJewels;
        }

        public void DisplayLevel()
        {
            Console.Write(currentLevel);
        }

        public void UpdateLevel()
        {
            Console.Clear();
            currentLevel++;
            JewelPosition();
            MapSize();
            ShuffleMap();
            JewelCollector.PlayGame();
        }
    }
}