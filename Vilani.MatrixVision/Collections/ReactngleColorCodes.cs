using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Vilani.MatrixVision.Collections
{
    public class ReactngleColorCodesContainer
    {
        private List<Color> ReactngleColorCodes = new List<Color>();

        static ReactngleColorCodesContainer _Instace;

        public static ReactngleColorCodesContainer GetInstance
        {
            get
            {
                if (_Instace == null)
                    _Instace = new ReactngleColorCodesContainer();

                return _Instace;
            }
        }

        public ReactngleColorCodesContainer()
        {
            ReactngleColorCodes.Add(Color.LightSalmon);
            ReactngleColorCodes.Add(Color.Aqua);
            ReactngleColorCodes.Add(Color.Maroon);
            ReactngleColorCodes.Add(Color.LightSkyBlue);

            ReactngleColorCodes.Add(Color.MediumPurple);
            ReactngleColorCodes.Add(Color.Magenta);

            ReactngleColorCodes.Add(Color.LightGreen);
            ReactngleColorCodes.Add(Color.LightPink);
            ReactngleColorCodes.Add(Color.AliceBlue);

            ReactngleColorCodes.Add(Color.Azure);
            ReactngleColorCodes.Add(Color.Chocolate);
            ReactngleColorCodes.Add(Color.Cornsilk);
            ReactngleColorCodes.Add(Color.LightCoral);
            ReactngleColorCodes.Add(Color.LightBlue);
            ReactngleColorCodes.Add(Color.LightGoldenrodYellow);
            ReactngleColorCodes.Add(Color.LightYellow);

            //  ----

            ReactngleColorCodes.Add(Color.LightSalmon);
            ReactngleColorCodes.Add(Color.Aqua);
            ReactngleColorCodes.Add(Color.Maroon);
            ReactngleColorCodes.Add(Color.LightSkyBlue);

            ReactngleColorCodes.Add(Color.MediumPurple);
            ReactngleColorCodes.Add(Color.Magenta);

            ReactngleColorCodes.Add(Color.LightGreen);
            ReactngleColorCodes.Add(Color.LightPink);
            ReactngleColorCodes.Add(Color.AliceBlue);

            ReactngleColorCodes.Add(Color.Azure);
            ReactngleColorCodes.Add(Color.Chocolate);
            ReactngleColorCodes.Add(Color.Cornsilk);
            ReactngleColorCodes.Add(Color.LightCoral);
            ReactngleColorCodes.Add(Color.LightBlue);
            ReactngleColorCodes.Add(Color.LightGoldenrodYellow);
            ReactngleColorCodes.Add(Color.LightYellow);
        }

        public Color GetElementAt(int index)
        {
            return ReactngleColorCodes[index];
        }


    }
}
