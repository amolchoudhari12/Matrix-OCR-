using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Vilani.MatrixVision.Core
{
    public class SafeFlowLayoutPanel : FlowLayoutPanel
    {
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            try
            {
                base.OnPaint(e);
            }
            catch (OutOfMemoryException ex)
            {
                Thread.Sleep(10000);
            }
            catch (Exception)
            {
                this.Invalidate();
            }
        }
    }
}
