using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                ErrorListBox.Items.Add(E.ErrorMassege);
            }
        }
    }
}
