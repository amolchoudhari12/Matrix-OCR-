using System;
using System.Collections.Generic;
using System.Text;

namespace Expert.Common.Library
{
    public class PatternDetails
    {
        public List<Item> RefImageFiles
        {
            get;
            set;
        }

        public string SrcImageFile
        {
            get;
            set;
        }

        public string ModelName
        {
            get;
            set;
        }

        public int ModelID
        {
            get;
            set;
        }

        public int Score
        {
            get;
            set;
        }
    }
}
