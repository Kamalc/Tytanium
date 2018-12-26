using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tytanium.Continuum
{
    public partial class Splashscreen : Form
    {
        public Splashscreen()
        {
            InitializeComponent();
        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            List<Bitmap> Animation = new List<Bitmap>();
            Animation.Add(Properties.Resources.SEQ0001);
            Animation.Add(Properties.Resources.SEQ0002);
            Animation.Add(Properties.Resources.SEQ0003);
            Animation.Add(Properties.Resources.SEQ0004);
            Animation.Add(Properties.Resources.SEQ0005);
            Animation.Add(Properties.Resources.SEQ0006);
            Animation.Add(Properties.Resources.SEQ0007);
            Animation.Add(Properties.Resources.SEQ0008);
            Animation.Add(Properties.Resources.SEQ0009);
            Animation.Add(Properties.Resources.SEQ0010);
            Animation.Add(Properties.Resources.SEQ0011);
            Animation.Add(Properties.Resources.SEQ0012);
            Animation.Add(Properties.Resources.SEQ0013);
            Animation.Add(Properties.Resources.SEQ0014);
            Animation.Add(Properties.Resources.SEQ0015);
            Animation.Add(Properties.Resources.SEQ0016);
            Animation.Add(Properties.Resources.SEQ0017);
            Animation.Add(Properties.Resources.SEQ0018);
            Animation.Add(Properties.Resources.SEQ0019);
            Animation.Add(Properties.Resources.SEQ0020);
            Animation.Add(Properties.Resources.SEQ0021);
            Animation.Add(Properties.Resources.SEQ0022);
            Animation.Add(Properties.Resources.SEQ0023);
            Animation.Add(Properties.Resources.SEQ0024);
            Animation.Add(Properties.Resources.SEQ0025);
            Animation.Add(Properties.Resources.SEQ0026);
            Animation.Add(Properties.Resources.SEQ0027);
            Animation.Add(Properties.Resources.SEQ0028);
            Animation.Add(Properties.Resources.SEQ0029);
            Animation.Add(Properties.Resources.SEQ0030);
            Animation.Add(Properties.Resources.SEQ0031);
            Animation.Add(Properties.Resources.SEQ0032);
            Animation.Add(Properties.Resources.SEQ0033);
            Animation.Add(Properties.Resources.SEQ0034);
            Animation.Add(Properties.Resources.SEQ0035);
            Timer T = new Timer();
            T.Interval = 8;
            int i = 0;
            int j = 0;
            int z = 0;
            T.Tick += (object senderII, EventArgs EA) =>
            {
                T.Interval += z / 20;
                z++;
                AnimationBox.Image = Animation[i++];
                j++;
                if (j == 63)
                {
                    T.Stop();
                    Close();
                }
                if (i > 30)
                {
                    i = 30;
                }
            };
            T.Start();
        }
    }
}
