using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Runtime.Serialization;

namespace VPSeminarska
{
    [Serializable()]
    public class HighScores
    {
        public string Name { get; set; }
        public int Score { get; set; } // najdobriot rezultat od igrata
        public string Game { get; set; }

        public HighScores(string name, int score, string game)
        {
            this.Name = name;
            this.Score = score;
            this.Game = game;
        }

        public override string ToString()
        {
            string str = "";
            str=String.Format("Name: {0,-10} \n\t Score: {1,-5} Game: {2,-7} \n",Name,Score,Game);
            return str;
        }
    }
}
