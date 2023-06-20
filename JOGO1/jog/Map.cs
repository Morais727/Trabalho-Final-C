namespace jogo
{
    using System;
    
    //<summary>
    // Classe responsável por gerar a visualização do mapa.
    //</summary>
    public class Map
    {
        private Cell[,] cells;

        public int Rows { get; } //Linhas do mapa
        public int Columns { get; } //Colunas do mapa

        public Map(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            cells = new Cell[rows, columns];
        }

        public void SetCell(int row, int column, Cell cell) //Definir as células e colunas
        {
            cells[row, column] = cell;
        }

        public Cell GetCell(int row, int column) //Pegar o valor das células e colunas
        {
            return cells[row, column];
        }

        public void DisplayMap() //Imprime o mapa
        {
            for (int row = 0; row < cells.GetLength(0); row++)
            {
                for (int column = 0; column < cells.GetLength(1); column++)
                {
                    Cell cell = cells[row, column]; //Define as células
                    cell.Display(); //Imprime
                }
                Console.WriteLine(); //Pula uma linha a cada execução
            }
        }
        
        public void FillEmptyCells()
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    SetCell(row, column, new Empty());
                }
            }
        }
    }
}
