using System;
using System.Collections.Generic;
using System.Text;

namespace Expert.Common.Library
{
    public class ReferenceImageDB
    {
        public int ReferenceImageID { get; set; }
        public int ModelID { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public int Score { get; set; }
        public int NumberOfOccurances { get; set; }
        public string ImageSize { get; set; }
        public string AOISize { get; set; }
    }
}
