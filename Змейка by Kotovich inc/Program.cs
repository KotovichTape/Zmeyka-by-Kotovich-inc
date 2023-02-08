using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Змейка_by_Kotovich_inc;
namespace ConsoleApp1
{
    class Program
    {
         public enum GridLimits
        {
          gridW = 90,
          gridH = 25

        }
          


        static Cell[,] grid = new Cell[(int)GridLimits.gridH, (int)GridLimits.gridW];
        static Cell currentCell;
        static Cell food;
        static int FoodCount;
  
        static readonly int speed = 1;
        static bool Populated = false;
        static bool Lost = false;
       

        static void Main(string[] args)
        {
            if (!Populated)
            {
                
                FoodCount = 0;
                
                
                populateGrid();



                currentCell = grid[(int)Math.Ceiling((double)(int)GridLimits.gridH / 2), (int)Math.Ceiling((double)(int)GridLimits.gridW / 2)];
                updatePos();
                addFood();
                Populated = true;
            }

            while (!Lost)
            {
                Restart();
            }
        }

        static void Restart()
        {
            Console.SetCursorPosition(0, 0);
            printGrid();
            Console.WriteLine("Length: {0}", Snake.snakeLength);
            getInput();
        }

        static void updateScreen()
        {
            Console.SetCursorPosition(0, 0);
            printGrid();
            Console.WriteLine("Length: {0}", Snake.snakeLength);
        }

        static void getInput()
        {

            //Console.Write("Where to move? [WASD] ");
            ConsoleKeyInfo input;
            while (!Console.KeyAvailable)
            {
                Move();
                updateScreen();
            }
            input = Console.ReadKey();
            doInput(input.KeyChar);
        }

        static void checkCell(Cell cell)
        {
            if (cell.val == "%")
            {
                eatFood();
            }
            if (cell.visited)
            {
                Lose();
            }
        }

        static void Lose()
        {
            Console.WriteLine("\n You lose!");
            Thread.Sleep(1000);
            Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
            Environment.Exit(-1);
        }

        static void doInput(char inp)
        {
            switch (inp)
            {
                case 'w':
                    Snake.goUp();
                    break;
                case 's':
                    Snake.goDown();
                    break;
                case 'a':
                    Snake.goRight();
                    break;
                case 'd':
                    Snake.goLeft();
                    break;
            }
        }

        static void addFood()
        {
            Random r = new Random();
            Cell cell;
            while (true)
            {
                cell = grid[r.Next(grid.GetLength(0)-2)+1, r.Next(grid.GetLength(1)-2)+1];
                if (cell.val == " ")
                    cell.val = "%";
                break;
            }
        }

        static void eatFood()
        {
            Snake.snakeLength += 1;
            addFood();
        }

       

        static void Move()
        {
            if (Snake.Direction() == 0)
            {
                //up
                if (grid[currentCell.y - 1, currentCell.x].val == "-")
                {
                    Lose();
                    return;
                }
                visitCell(grid[currentCell.y - 1, currentCell.x]);
            }
            else if (Snake.Direction() == 1)
            {
                //right
                if (grid[currentCell.y, currentCell.x - 1].val == "|")
                {
                    Lose();
                    return;
                }
                visitCell(grid[currentCell.y, currentCell.x - 1]);
            }
            else if (Snake.Direction() == 2)
            {
                //down
                if (grid[currentCell.y + 1, currentCell.x].val == "-")
                {
                    Lose();
                    return;
                }
                visitCell(grid[currentCell.y + 1, currentCell.x]);
            }
            else if (Snake.Direction() == 3)
            {
                //left
                if (grid[currentCell.y, currentCell.x + 1].val == "|")
                {
                    Lose();
                    return;
                }
                visitCell(grid[currentCell.y, currentCell.x + 1]);
            }
            Thread.Sleep(speed * 100);
        }

        static void visitCell(Cell cell)
        {
            currentCell.val = "#";
            currentCell.visited = true;
            currentCell.decay = Snake.snakeLength;
            checkCell(cell);
            currentCell = cell;
            updatePos();

            //checkCell(currentCell);
        }

        static void updatePos()
        {

            currentCell.Set("@");
            
            if (Snake.Direction() == 0)
            {
                currentCell.val = "^";
            }
            else if (Snake.Direction() == 1)
            {
                currentCell.val = "<";
            }
            else if (Snake.Direction() == 2)
            {
                currentCell.val = "v";
            }
            else if (Snake.Direction() == 3)
            {
                currentCell.val = ">";
            }
            else if(Snake.Direction() == null)
            {
                Console.ReadKey();
            }
            currentCell.visited = false;
            return;
        }

        static void populateGrid()
        {
            Random random = new Random();
            for (int col = 0; col < (int)GridLimits.gridH; col++)
            {
                for (int row = 0; row < (int)GridLimits.gridW; row++)
                {
                    Cell cell = new Cell();
                    cell.x = row;
                    cell.y = col;
                    cell.visited = false;
                    if (cell.x == 0)
                        cell.Set("|");
                    else if (cell.x > (int)GridLimits.gridW - 2)
                        cell.Set("|");
                    else if (cell.y == 0)
                        cell.Set("-");
                    else if (cell.y > (int)GridLimits.gridH - 2)
                        cell.Set("-");

                    //if (cell.x == 0 || cell.x > (int)GridLimits.gridW - 2 || cell.y == 0 || cell.y > (int)GridLimits.gridH - 2)
                    //    cell.Set("|");
                    else
                        cell.Clear();
                    grid[col, row] = cell;
                }
            }
        }

        static void printGrid()
        {
            string toPrint = "";
            for (int col = 0; col < (int)GridLimits.gridH; col++)
            {
                for (int row = 0; row < (int)GridLimits.gridW; row++)
                {
                    grid[col, row].decaySnake();
                    toPrint += grid[col, row].val;

                }
                toPrint += "\n";
            }
            Console.WriteLine(toPrint);
        }

    }

}