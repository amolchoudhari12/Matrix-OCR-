using Expert.Common.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vilani.MatrixVision.Common;

namespace Vilani.MatrixVision.Collections
{
    public class ResultDisplayModelContainer
    {
        public List<ResultDisplayModel> ResultCollection = new List<ResultDisplayModel>();

        public ResultDisplayModelContainer()
        {
            for (int i = 0; i < GlobalSettings.TotalReferenceImagesSupported; i++)
            {
                ResultCollection.Add(new ResultDisplayModel());
            }
        }

        public ResultDisplayModel GetElementAt(int index)
        {
            return ResultCollection[index];
        }

        public List<ResultDisplayModel> Take(int counts)
        {
            return ResultCollection.Take(counts).ToList();
        }


    }
}
