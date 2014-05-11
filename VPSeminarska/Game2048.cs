using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VPSeminarska
{
    public class Game2048 : Game
    {

        public Game2048() : base()
        {
            //napolni ja sekvencata
            Sequence = new int[] {2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048};

            // generiraj pocetni broevi
            int[] number = generateNumber();
            board[number[1], number[2]] = new Number(number[0], SEQUENCE[number[0]]);
            number = generateNumber();
            board[number[1], number[2]] = new Number(number[0], SEQUENCE[number[0]]);
        }


        //
        // METODI
        //        

        public override bool CanShift(string direction)
        {
            // proveri dali e vozmozno pomestuvanje vo desno
            if (direction == "right")
            {
                for (int j = 2; j >= 0; j--)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (board[i, j] != null)
                        {
                            if (board[i, j + 1] == null)
                                return true;
                            else if (board[i, j].Value == board[i, j + 1].Value && !board[i, j + 1].Fresh && !board[i, j].Fresh)
                                return true;
                        }
                    }
                }
            }

            // proveri dali e vozmozno pomestuvanje vo levo
            else if (direction == "left")
            {
                for (int j = 1; j < 4; j++)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (board[i, j] != null)
                        {
                            if (board[i, j - 1] == null)
                                return true;
                            else if (board[i, j].Value == board[i, j - 1].Value && !board[i, j - 1].Fresh && !board[i, j].Fresh)
                                return true;
                        }
                    }
                }
            }

            // proveri dali e vozmozno pomestuvanje nagore
            else if (direction == "up")
            {
                for (int i = 1; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (board[i, j] != null)
                        {
                            if (board[i - 1, j] == null)
                                return true;
                            else if (board[i, j].Value == board[i - 1, j].Value && !board[i - 1, j].Fresh && !board[i, j].Fresh)
                                return true;
                        }
                    }
                }
            }

            // proveri dali e vozmozno pomestuvanje nadole
            else if (direction == "down")
            {
                for (int i = 2; i >= 0; i--)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (board[i, j] != null)
                        {
                            if (board[i + 1, j] == null)
                                return true;
                            else if (board[i, j].Value == board[i + 1, j].Value && !board[i + 1, j].Fresh && !board[i, j].Fresh)
                                return true;
                        }
                    }
                }
            }

            return false;
        }

        public override void Shift(string direction)
        {
            // pomesti gi site brojki koi mozat da se pomestat ednas na desno
            if (direction == "right")
            {
                for (int j = 2; j >= 0; j--)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (board[i, j] != null)
                        {
                            if (board[i, j + 1] == null)
                            {
                                board[i, j + 1] = board[i, j];
                                board[i, j] = null;
                            }
                            else if (board[i, j].Value == board[i, j + 1].Value && !board[i, j + 1].Fresh && !board[i, j].Fresh)
                            {
                                score += board[i, j].Value * 2;
                                board[i, j + 1] = new Number(board[i,j].Index + 1, board[i, j].Value * 2);
                                board[i, j] = null;
                               
                            }
                        }
                    }
                }
            }

            // pomesti gi site brojki koi mozat da se pomestat ednas na levo
            else if (direction == "left")
            {
                for (int j = 1; j < 4; j++)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (board[i, j] != null)
                        {
                            if (board[i, j - 1] == null)
                            {
                                board[i, j - 1] = board[i, j];
                                board[i, j] = null;
                            }
                            else if (board[i, j].Value == board[i, j - 1].Value && !board[i, j - 1].Fresh && !board[i, j].Fresh)
                            {
                                score += board[i, j].Value * 2;
                                board[i, j - 1] = new Number(board[i, j].Index + 1, board[i, j].Value * 2);
                                board[i, j] = null;
                                
                            }
                        }
                    }
                }
            }

            // pomesti gi site brojki koi mozat da se pomestat ednas nagore
            else if (direction == "up")
            {
                for (int i = 1; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (board[i, j] != null)
                        {
                            if (board[i - 1, j] == null)
                            {
                                board[i - 1, j] = board[i, j];
                                board[i, j] = null;
                            }
                            else if (board[i, j].Value == board[i - 1, j].Value && !board[i - 1, j].Fresh && !board[i, j].Fresh)
                            {
                                score += board[i, j].Value * 2;
                                board[i - 1, j] = new Number(board[i, j].Index + 1, board[i, j].Value * 2);
                                board[i, j] = null;
                                
                            }
                        }
                    }
                }
            }

            // pomesti gi site brojki koi mozat da se pomestat ednas nadole
            else if (direction == "down")
            {
                for (int i = 2; i >= 0; i--)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (board[i, j] != null)
                        {
                            if (board[i + 1, j] == null)
                            {
                                board[i + 1, j] = board[i, j];
                                board[i, j] = null;
                            }
                            else if (board[i, j].Value == board[i + 1, j].Value && !board[i + 1, j].Fresh && !board[i, j].Fresh)
                            {
                                score += board[i, j].Value * 2;
                                board[i + 1, j] = new Number(board[i, j].Index + 1, board[i, j].Value * 2);
                                board[i, j] = null;
                            }
                        }
                    }
                }
            }
        }
    }
}
