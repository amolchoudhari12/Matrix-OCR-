using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Configuration;

namespace Expert.Common.Library
{
   

    public class SourceImageReactangle
    {
      
        public string SourceImagePath { get; set; }
        public Rectangle Rectangle { get; set; }

        public SourceImageReactangle()
        {

        }

        public SourceImageReactangle(string path, Rectangle rect)
        {
            this.SourceImagePath = path;
            this.Rectangle = rect;
        }

    }
}
