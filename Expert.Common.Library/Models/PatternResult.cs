using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Expert.Common.Library
{
    public class PatternResult
    {
        public int ReferenceImageID { get; set; }

        public int ModelID { get; set; }

        public string ModelName { get; set; }

        public string CapImageName
        {
            get;
            set;
        }

        public string RefImageName
        {
            get;
            set;
        }

        public int ActualOccurances
        {
            get;
            set;
        }

        public bool IsValid
        {
            get;
            set;
        }

        public int SetScore
        {
            get;
            set;
        }

        public double ActualScore
        {
            get;
            set;
        }

        public Rectangle AOIRectangle
        {
            get;
            set;
        }
    }
}
