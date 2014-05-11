using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace VPSeminarska
{
    public class Number
    {
        // staticki promenlivi za razlicnite boi
        public static System.Drawing.Color clr2 = Color.FromArgb(255, 238, 228, 218);
        public static System.Drawing.Color clr4 = Color.FromArgb(255, 239, 224, 201);
        public static System.Drawing.Color clr8 = Color.FromArgb(255, 242, 177, 121);
        public static System.Drawing.Color clr16 = Color.FromArgb(255, 246, 148, 99);
        public static System.Drawing.Color clr32 = Color.FromArgb(255, 245, 124, 95);
        public static System.Drawing.Color clr64 = Color.FromArgb(255, 247, 94, 60);
        public static System.Drawing.Color clr128 = Color.FromArgb(255, 238, 207, 114);
        public static System.Drawing.Color clr256 = Color.FromArgb(255, 237, 204, 97);
        public static System.Drawing.Color clr512 = Color.FromArgb(255, 237, 201, 81);
        public static System.Drawing.Color clr1024 = Color.FromArgb(255, 239, 197, 63);
        public static System.Drawing.Color clr2048 = Color.FromArgb(255, 232, 191, 37);

        public int Index { get; set; }
        public int Value { get; set; }
        public System.Drawing.Color Color { get; set; }
        public bool Fresh { get; set; }

        public Number(int index, int value)
        {
            // zapisi go brojot
            Index = index;
            Value = value;
            Fresh = true;

            // dodadi mu ja soodvetnata boja
            if (index == 0)
                Color = clr2;
            else if (index == 1)
                Color = clr4;
            else if (index == 2)
                Color = clr8;
            else if (index == 3)
                Color = clr16;
            else if (index == 4)
                Color = clr32;
            else if (index == 5)
                Color = clr64;
            else if (index == 6)
                Color = clr128;
            else if (index == 7)
                Color = clr256;
            else if (index == 8)
                Color = clr512;
            else if (index == 9)
                Color = clr1024;
            else 
                Color = clr2048;            
        }
    }
}
