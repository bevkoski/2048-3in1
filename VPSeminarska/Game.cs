using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VPSeminarska
{
    public abstract class Game
    {
        // nizata potrebna za igrata
        protected int[] Sequence;
        // promenliva za tablata
        public Number[,] board = new Number[4, 4];
        // random generator
        public Random random = new Random();

        // score
        public static int score;

        public Number[,] Board
        {
            get { return board; }
        }

        public int[] SEQUENCE
        {
            get { return Sequence; }
        }

        public Game()
        {
            // inicijaliziraj ja nizata
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    board[i, j] = null;
                }
            }
            score = 0;
        }


        //
        // METODI
        //

        public int[] generateNumber()
        {
            int[] data = new int[3];

            // generiraj vrednost
            double probability = random.NextDouble();
            if (probability < 0.9)
                data[0] = 0;
            else
                data[0] = 1;

            // generiraj pozicija
            int index = random.Next(16);
            while (board[index / 4, index % 4] != null)
                index = random.Next(16);

            // vrati rezultat
            data[1] = index / 4;
            data[2] = index % 4;
            return data;
        }

        public abstract bool CanShift(string direction);

        public abstract void Shift(string direction);

        public void makeStale()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (board[i, j] != null)
                    {
                        board[i, j].Fresh = false;
                    }
                }
            }
        }

        public bool isFull()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (board[i, j] == null)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
