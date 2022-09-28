using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFProjectDigitalDesign
{
    enum Orientation
    {
        Horizontal,
        Vertical
    }

    internal class Game
    {
        int size;
        Orientation[,] orientation;
        Random random = new Random();

        public Game(int size)
        {
            this.size = size;
            orientation = new Orientation[size, size];
        }

        public void Start()
        { 
            bool startOrientation = (random.Next(0, 2) == 0) ? true : false;
            if (size < 2)
            {
                size = 2;
            }
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    this.orientation[i,j] = startOrientation ? Orientation.Horizontal : Orientation.Vertical;
                }
            }  
        }

        public void TurnOrientation(int x, int y)
        {
            orientation[x, y] = orientation[x, y] == Orientation.Horizontal ? Orientation.Vertical : Orientation.Horizontal;
        }

        public bool CheckOrientation()
        {
            var first = orientation[0, 0];
            
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (first != orientation[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public string GetOrientation(int x, int y)
        {
            return $"{orientation[x, y]}";
        }
        public int GetSize() => size;
    }
}
