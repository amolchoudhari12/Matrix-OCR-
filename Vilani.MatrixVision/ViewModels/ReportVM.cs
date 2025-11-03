using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vilani.MatrixVision.Core;
using Vilani.Systems.Common.Core;

namespace Vilani.MatrixVision.ViewModels
{
    [ProtoContract]
    public class ReportVM : BaseFileDB
    {
        [ProtoMember(1)]
        public int SrNo { get; set; }
        [ProtoMember(2)]
        public String PartNo { get; set; }
        [ProtoMember(3)]
        public Double Score { get; set; }
        [ProtoMember(4)]
        public Double ActualScore { get; set; }
        [ProtoMember(5)]
        public String Status { get; set; }
        [ProtoMember(6)]
        public bool IsChecked { get; set; }
        [ProtoMember(7)]
        public string IsCheckedString { get; set; }

        public string ProductName { get; set; }
        public string ReportName { get; set; }
        public DateTime ReportDate { get; set; }
    }
}
