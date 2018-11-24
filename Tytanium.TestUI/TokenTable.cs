using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Tytanium.Scanner;

namespace Tytanium
{
    public partial class TokenTable : Form
    {
        List<Token> TokenSet;
        public TokenTable(List<Token> T)
        {
            TokenSet = T;
            InitializeComponent();
        }

        private void TokenTable_Load(object sender, EventArgs e)
        {
            this.Location = new Point(700, 100);
            int index = 1;
            foreach (Token T in TokenSet)
            {
                TokenView.Rows.Add(index.ToString(), T.Literal, T.UpperType.ToString(), T.Type.ToString());
                index++;
            }
            TokenView.Refresh();
        }
    }
}
