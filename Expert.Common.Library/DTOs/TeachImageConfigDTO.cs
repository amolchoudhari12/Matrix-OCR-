using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Expert.Common.Library.DTOs
{
    public class TeachImageConfigDTO
    {
        public int TeachImageConfigID { get; set; }
        public int ImageFactor { get; set; }
        public int AreaGreaterThan { get; set; }
        public string CropArea { get; set; }
        public decimal CropAreaPercent { get; set; }
        public bool FillHoles { get; set; }
        public string validHoles { get; set; }
        public int MinRange { get; set; }
        public int MaxRange { get; set; }
        public int iErrCode { get; set; }
        public int TotalCounts { get; set; }
        public int VisionInspectionTypeID { get; set; }
    }
}
