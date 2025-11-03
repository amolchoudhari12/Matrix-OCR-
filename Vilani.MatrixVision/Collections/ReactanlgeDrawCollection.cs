using Expert.Common.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Vilani.MatrixVision.Common;

namespace Vilani.MatrixVision.Collections
{
    public class ReactanlgeDrawnContainer
    {
        public List<SourceImageReactangle> ReactianglesToDraw = new List<SourceImageReactangle>();

        public ReactanlgeDrawnContainer()
        {
            var counter = Convert.ToInt32(ConfigurationManager.AppSettings["SupportedReferenceImages"]);

            for (int i = 0; i < counter; i++)
            {
                ReactianglesToDraw.Add(new SourceImageReactangle());
            }
        }

        public SourceImageReactangle GetElementAt(int index)
        {
            try
            {
                return ReactianglesToDraw[index];
            }
            catch (IndexOutOfRangeException ex)
            {
                throw ex;
            }

        }

        public List<SourceImageReactangle> Take(int counts)
        {
            return ReactianglesToDraw.Take(counts).ToList();
        }

    }
}
