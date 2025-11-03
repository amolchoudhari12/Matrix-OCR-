using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Expert.Common.Library.Models
{
    public class ReactngleColorCodes
    {
        static List<Color> ReactngleColorCodesList = new List<Color>();

        public ReactngleColorCodes()
        {

            ReactngleColorCodesList.Add(Color.LightSalmon);
            ReactngleColorCodesList.Add(Color.Aqua);
            ReactngleColorCodesList.Add(Color.Maroon);
            ReactngleColorCodesList.Add(Color.LightSkyBlue);
            ReactngleColorCodesList.Add(Color.MediumPurple);
            ReactngleColorCodesList.Add(Color.Magenta);
            ReactngleColorCodesList.Add(Color.LightGreen);
            ReactngleColorCodesList.Add(Color.LightPink);
            ReactngleColorCodesList.Add(Color.AliceBlue);
            ReactngleColorCodesList.Add(Color.Azure);
            ReactngleColorCodesList.Add(Color.Chocolate);
            ReactngleColorCodesList.Add(Color.Cornsilk);
            ReactngleColorCodesList.Add(Color.LightCoral);
            ReactngleColorCodesList.Add(Color.LightBlue);
            ReactngleColorCodesList.Add(Color.LightGoldenrodYellow);
            ReactngleColorCodesList.Add(Color.LightYellow);

        }

        public static Color GetColor(int index)
        {
            return ReactngleColorCodesList[index];
        }

    }
}
