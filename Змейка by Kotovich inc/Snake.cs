using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Змейка_by_Kotovich_inc
{
    static class Snake
    {
        static private int? direction = null;
        static public int snakeLength = 5;

        public static int? Direction()
        {
            return direction;
        }
         public static void goUp()
        {
            if (Snake.direction == 2)
                return;
            Snake.direction = 0;
        }

         public static void goRight()
        {
            if (Snake.direction == 3)
                return;
            Snake.direction = 1;
        }

        public static void goDown()
        {
            if (Snake.direction == 0)
                return;
            Snake.direction = 2;
        }

        public static void goLeft()
        {
            if (Snake.direction == 1)
                return;
            Snake.direction = 3;
        }
    }
}
