using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Expert.Common.Library
{
    public class Item
    {
        public int ReferenceImageID { get; set; }

        public int SequenceNumber { get; set; }

        public string Name
        {
            get;
            set;
        }

        public int Value
        {
            get;
            set;
        }

        public int Score
        {
            get;
            set;
        }

        public Rectangle ROI
        {
            get;
            set;
        }

      
    }
}
