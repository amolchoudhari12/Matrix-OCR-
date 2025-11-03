using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Expert.Common.Library
{
   

    public class ResultDisplayModel
    {

        public ResultDisplayModel()
        {

        }

     
        [DisplayName("Ref.Image Name")]
        public string RefFileName
        {
            get;
            set;
        }

        [DisplayName("Set Score(%)")]
        public string SetScore
        {
            get;
            set;
        }

        [DisplayName("Actual Score(%)")]
        public double ActualScore
        {
            get;
            set;
        }


        [DisplayName("Result")]
        public string Result
        {
            get;
            set;
        }
    }
}
