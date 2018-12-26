using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Handler;

namespace Tytanium.Continuum
{
    public partial class Output : Form
    {
        private List<Error> ErrorsList;

        public Output()
        {
            InitializeComponent();
        }

        public Output(List<Error> errorList)
        {
            this.ErrorsList = errorList;
        }

        private void close_MouseEnter(object sender, EventArgs e)
        {
            close.Image = Tytanium.Continuum.Properties.Resources.CloseHighlight;
        }

        private void close_MouseLeave(object sender, EventArgs e)
        {
            close.Image = Tytanium.Continuum.Properties.Resources.Close;
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void Output_Shown(object sender, EventArgs e)
        {
            close.MouseEnter += close_MouseEnter;
            close.MouseLeave += close_MouseLeave;
            close.MouseClick += close_Click;
            this.Location = new Point(40, 400);
            foreach (Error E in ErrorsList)
            {
                ErrorListBox.Items.Add(E.ErrorMessage);
            }
        }
    }
}
