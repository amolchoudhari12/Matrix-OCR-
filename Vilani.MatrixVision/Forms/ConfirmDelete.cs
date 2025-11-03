using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;


namespace Vilani.WindowsApp.Forms.Common
{
    public partial class ConfirmDelete : Form
    {

        public delegate void YesButton(int id);
        public event YesButton YesButtonClicked;

        public delegate void NoButton();
        public event NoButton NoButtonClicked;

        public string DeleteInfo { get; set; }

        private static ConfirmDelete inst;
        public static ConfirmDelete GetForm
        {
            get
            {
                if (inst == null || inst.IsDisposed)
                    inst = new ConfirmDelete();
                return inst;
            }


        }

        public int DeleteID { get; set; }
       

        public ConfirmDelete()
        {
            InitializeComponent();
         
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            YesButtonClicked(DeleteID);
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            NoButtonClicked();
        }

        private void ConfirmDelete_Load(object sender, EventArgs e)
        {
            lblDeleteInfo.Text = DeleteInfo;
        }
    }
}
