using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace VPSeminarska
{
    public partial class StartGame : Form
    {
        public string name;
        Stream str;
        List<HighScores> highS;
        BinaryFormatter bf;
        string tmp;

        public StartGame()
        {
            InitializeComponent();
            name = "";
            tmp = "";
            highS=new List<HighScores>();
            
            //datoteka vo koja se cuvaat najdobrite rezultati
            if (File.Exists("high.bin"))
            {
                str = File.OpenRead("high.bin");
                if (new FileInfo("high.bin").Length != 0)
                {
                    bf = new BinaryFormatter();
                    highS = (List<HighScores>)bf.Deserialize(str);
                }
                str.Close();
            }
            else
            {
                str = File.Create("high.bin");
                str.Close();
            }
        }

        private void btnGame_Click(object sender, EventArgs e)
        {
            Form1 game = null;
            Button btn = (Button)sender;
            if (btn == btnGame2048)
                game = new Form1("2048");
            else if (btn == btnGameFibonacci)
                game = new Form1("Fibonacci");
            else
                game = new Form1("Troll-2048");

            String ime;
            if (textBox1.Text.Length == 0)
                ime = "User";
            else
                ime = textBox1.Text;

            if(game.ShowDialog() == DialogResult.OK)
                highS.Add(new HighScores(ime, game.bestScore, game.Type));

            str = File.Create("high.bin");
            bf = new BinaryFormatter();
            bf.Context = new StreamingContext(StreamingContextStates.CrossAppDomain);
            bf.Serialize(str, highS);
            str.Close();
        }

        private void btnHighScore_Click(object sender, EventArgs e)
        {
            str = File.OpenRead("high.bin");
            if (new FileInfo("high.bin").Length != 0)
            {
                bf = new BinaryFormatter();
                highS = (List<HighScores>)bf.Deserialize(str);
            }
            str.Close();

            highS = highS.OrderByDescending(x => x.Score).ToList();

            int i = 1;
            tmp = "";

            foreach (HighScores s in highS)
            {
                if (i <= 10)
                {
                    tmp = tmp + i + ". " + s.ToString() + "\n";
                    i++;
                }
                else
                    break;
            }

            Top10 top = new Top10(tmp);
            top.ShowDialog();
        }
    }
}
