using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Vilani.MatrixVision.Core
{
    public class SafeDataGridView : DataGridView
    {
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            try
            {              
                base.OnPaint(e);
            }
            catch (Exception)
            {
                this.Invalidate();
            }
        }
    }
}
