using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Expert.Common.Library.Models
{
    public class PtternMatchingResult
    {
        public string ModelName { get; set; }
        public string PLCInputValue { get; set; }

        public int OKCount { get; set; }
        public int NotOKCount { get; set; }

        private static PtternMatchingResult _VisionCounters = null;

        private PtternMatchingResult()
        {

        }

        public static PtternMatchingResult GetInstace()
        {
            if (_VisionCounters == null)
            {
                _VisionCounters = new PtternMatchingResult();
                return _VisionCounters;
            }
            return _VisionCounters;
        }


        public int TotalCounts
        {
            get
            {
                return OKCount + NotOKCount;
            }
        }

        public int TotalTriggerRecieved { get; set; }

        public void SetCounters(bool isValid)
        {
            if (isValid)
                this.OKCount++;
            else
                this.NotOKCount++;
        }

        public void IncrementCounters(int validCounts, int invalidCounts)
        {
            this.OKCount += validCounts;
            this.NotOKCount += invalidCounts;
        }

        public void SetCounters(int okCounts, int notOKCounts)
        {
            this.OKCount = okCounts;
            this.NotOKCount = notOKCounts;
        }

        public void AddTriggerCount()
        {
            TotalTriggerRecieved += 1;
        }

        public void Reset()
        {
            this.OKCount = 0;
            this.NotOKCount = 0;
            this.TotalTriggerRecieved = 0;
        }
    }
}
