using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Expert.Common.Library.DTOs
{
  
    public class ReportDTO
    {
      
        public int SrNo { get; set; }     
        public String PartNo { get; set; }
        public Double Score { get; set; }
        public Double ActualScore { get; set; }
        public String Status {get; set;}
        public bool IsChecked { get; set; }
    }
}
