using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Expert.Common.Library.DTOs
{
    public class ReportMenuDTO
    {
        public string ProductName { get; set; }
        public string ReportName { get; set; }
        public DateTime ReportDate { get; set; }
        public string Action { get; set; }
    }
}
