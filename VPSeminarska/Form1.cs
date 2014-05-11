using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VPSeminarska
{
    public partial class Form1 : Form
    {
        public Game Game { get; set; }
        public System.Windows.Forms.Button[,] buttons = new Button[4, 4];
        public string shiftDirection;
        int PopOut;
        public int bestScore;
        public string Type;
        bool Troll;
        string[] Directions;
        Random random;

        //
        // KONSTRUKTOR
        //

        public Form1(string type)
        {
            InitializeComponent();

            // postavi ja matricata so kopcinja
            buttons[0, 0] = cell00;
            buttons[0, 1] = cell01;
            buttons[0, 2] = cell02;
            buttons[0, 3] = cell03;
            buttons[1, 0] = cell10;
            buttons[1, 1] = cell11;
            buttons[1, 2] = cell12;
            buttons[1, 3] = cell13;
            buttons[2, 0] = cell20;
            buttons[2, 1] = cell21;
            buttons[2, 2] = cell22;
            buttons[2, 3] = cell23;
            buttons[3, 0] = cell30;
            buttons[3, 1] = cell31;
            buttons[3, 2] = cell32;
            buttons[3, 3] = cell33;

            // napravi nova igra i sinhroniziraj ja tablata
            Type = type;
            if (type == "2048")
            {
                Game = new Game2048();
                lblHeader.Text = "2048";
                lblText.Text = "Join the numbers and get to the 2048 tile.";
                this.Text = "2048";
                Troll = false;
            }
            else if (type == "Fibonacci")
            {
                Game = new GameFibonacci();
                lblHeader.Text = "Fibonacci";
                lblHeader.Font = new System.Drawing.Font("Ravie", 28F);
                lblText.Text = "Join the numbers and get to the 6765 tile.";
                this.Text = "Fibonacci";
                Troll = false;
            }
            else
            {
                Game = new Game2048();
                lblHeader.Text = "Troll\n2048";
                lblText.Text = "Join the numbers and get to the 2048 tile.";
                lblHeader.Font = new System.Drawing.Font("Ravie", 36F);
                this.Text = "Troll 2048";
                Troll = true;
                Directions = new string[] { "up", "down", "left", "right" };
                random = new Random();
            }

            Game.makeStale();
            Synchronize();
            shiftDirection = null;
            PopOut = 0;
            lblScore.Text = "Score:" + "\n" + "0";
            lblBestScore.Text = "Best Score:" + "\n" + "0";
            bestScore = 0;
        }


        //
        // METODI
        //

        public void Synchronize()
        {
            // sinhroniziraj ja tablata so formata
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (Game.board[i, j] != null)
                    {
                        
                        buttons[i, j].Text = Game.board[i, j].Value.ToString();
                        buttons[i, j].BackColor = Game.board[i, j].Color;

                        // postavi ja goleminata na fontot vo zavisnost od cifrite na brojot
                        if (buttons[i, j].Text.Length == 3)
                        {
                            buttons[i, j].Font = new System.Drawing.Font("Arial Black", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        }
                        else if (buttons[i, j].Text.Length == 4)
                        {
                            buttons[i, j].Font = new System.Drawing.Font("Arial Black", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        }
                        else
                        {
                            buttons[i, j].Font = new System.Drawing.Font("Arial Black", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        }

                        //proveri dali igrata zavrshila so stiganje do 2048
                        if (buttons[i, j].Text == "2048")
                        {
                            lblYouWon.Font = new System.Drawing.Font("Ravie", 48F);
                            lblYouWon.Text = "You Won!";
                            lblYouWon.Visible = true;
                            //lblHighScore.Visible = true;
                            DialogResult = MessageBox.Show("Congratulations! You won!", "You won !", MessageBoxButtons.OK);
                        }

                        
                        //prikazi score
                        lblScore.Text = "Score:\n" + Game.score;
                        if (Game.score > bestScore)
                        {
                            lblBestScore.Text = "Best Score:\n" + Game.score;
                            bestScore = Game.score;
                        }
                    }
                    else
                    {
                        buttons[i, j].Text = "";
                        buttons[i, j].BackColor = System.Drawing.Color.LightGray;
                    }
                }
            }
        }


        //
        // NASTANI
        //

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (PopOut == 0)
            {
                if (Troll && (e.KeyCode == Keys.Right || e.KeyCode == Keys.Left || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down))
                {
                    string dir = Directions[random.Next(4)];
                    if (Game.CanShift(dir))
                    {
                        shiftDirection = dir;
                        timer.Start();
                    }
                }
                else
                {
                    if (e.KeyCode == Keys.Right)
                    {
                        if (Game.CanShift("right"))
                        {
                            shiftDirection = "right";
                            timer.Start();
                        }
                    }
                    else if (e.KeyCode == Keys.Left)
                    {
                        if (Game.CanShift("left"))
                        {
                            shiftDirection = "left";
                            timer.Start();
                        }
                    }
                    else if (e.KeyCode == Keys.Up)
                    {
                        if (Game.CanShift("up"))
                        {
                            shiftDirection = "up";
                            timer.Start();
                        }
                    }
                    else if (e.KeyCode == Keys.Down)
                    {
                        if (Game.CanShift("down"))
                        {
                            shiftDirection = "down";
                            timer.Start();
                        }
                    }
                }

                // proveri dali izgubil
                if (Game.isFull() && !Game.CanShift("right") && !Game.CanShift("left") && !Game.CanShift("up") && !Game.CanShift("down"))
                {
                    lblYouWon.Font = new System.Drawing.Font("Ravie", 42F);
                    lblYouWon.Text = "Game Over!";
                    lblYouWon.Visible = true;

                    DialogResult result = MessageBox.Show("Go back to the start menu !", "Game over", MessageBoxButtons.OKCancel);
                    if (result == DialogResult.OK)
                        DialogResult = DialogResult.OK;
                }
            }

            
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Game.Shift(shiftDirection);
            if (!Game.CanShift(shiftDirection))
            {
                // dodadi nov broj
                int[] number = Game.generateNumber();
                Game.board[number[1], number[2]] = new Number(number[0], Game.SEQUENCE[number[0]]);

                // zavrsi so dvizenjeto
                shiftDirection = null;
                PopOut = 1;
                popOutTimer.Start();
                timer.Stop();

                
            }
            Synchronize();
        }

        private void popOutTimer_Tick(object sender, EventArgs e)
        {
            PopOut++;
            if (PopOut == 8)
            {
                PopOut = 0;
                popOutTimer.Stop();
                Game.makeStale();
            }
            else if (PopOut < 5)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (Game.board[i, j] != null && Game.board[i, j].Fresh)
                        {
                            buttons[i, j].Size = new Size(buttons[i, j].Size.Width + 6, buttons[i, j].Size.Height + 6);
                            buttons[i, j].Location = new Point(buttons[i, j].Location.X - 3, buttons[i, j].Location.Y - 3);
                            buttons[i, j].BringToFront();
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (Game.board[i, j] != null && Game.board[i, j].Fresh)
                        {
                            buttons[i, j].Size = new Size(buttons[i, j].Size.Width - 6, buttons[i, j].Size.Height - 6);
                            buttons[i, j].Location = new Point(buttons[i, j].Location.X + 3, buttons[i, j].Location.Y + 3);
                            buttons[i, j].BringToFront();
                        }
                    }
                }
            }
        }


        private void btnNewGame_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
           
            switch (e.KeyCode)
            {
                case Keys.Down:
                case Keys.Up:
                case Keys.Left:
                case Keys.Right:
                    e.IsInputKey = true;
                    break;
            }
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            if (Type == "2048")
                Game = new Game2048();
            else if (Type == "Fibonacci")
                Game = new GameFibonacci();
            else
                Game = new Game2048();

            lblScore.Text = "Score:\n" + Game.score;

            lblYouWon.Visible = false;
            Synchronize();
            
        } 
    }
}
