using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vilani.MatrixVision.Common
{
    public class ComboBoxItem
    {
        public ComboBoxItem(string name, int id)
        {
            this.ID = id;
            this.Name = name;

        }
        public int ID { get; set; }
        public string Name { get; set; }

    }
}
