using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Handler;

namespace Tytanium
{
    public partial class ErrorList : Form
    {
        List<Error> ErrorsList;
        public ErrorList(List<Error> Errors)
        {
            ErrorsList = Errors;
            InitializeComponent();
        }

        private void ErrorList_Load(object sender, EventArgs e)
        {
            this.Location = new Point(40, 400);
            foreach (Error E in ErrorsList)
            {
                ErrorListBox.Items.Add(E.ErrorMessage);
            }
        }
    }
}
