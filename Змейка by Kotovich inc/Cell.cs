using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Змейка_by_Kotovich_inc
{
    public class Cell
    {
        public string val
        {
            get;
            set;
        }
        public int x
        {
            get;
            set;
        }
        public int y
        {
            get;
            set;
        }
        public bool visited
        {
            get;
            set;
        }
        public int decay
        {
            get;
            set;
        }

        public void decaySnake()
        {
            decay -= 1;
            if (decay == 0)
            {
                visited = false;
                val = " ";
            }
        }

        public void Clear()
        {
            val = " ";
        }

        public void Set(string newVal)
        {
            val = newVal;
        }
    }
}
