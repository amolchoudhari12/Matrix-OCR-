using System;
using System.Collections.Generic;
using System.Text;

namespace Expert.Common.Library
{
    public class Model
    {
        public string ModelName
        {
            get;
            set;
        }

       
        public List<Item> ReferenceImageOccurancePairList
        {
            get;
            set;
        }

        public int ModelID { get; set; }

        public string PLCInputValue { get; set; }

        public string Score { get; set; }

        public string PLCOutoutValue { get; set; }

        public int ErrorCode { get; set; }


        public bool InvertResult { get; set; }
        public List<ReferenceImageDB> ReferenceImages { get; set; }
    }

   
}
