using System;
using System.Windows.Forms;

namespace Flash
{
    public partial class Form1 : Form
    {
        public u1 f1;
        public u2 f2;
        public u3 f3;
        public Form1()
        {
            InitializeComponent();
            f1 = new u1();
            f2 = new u2();
            f3 = new u3();

            f1.Show();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Focus();
        }
        private void tabCrlDecoder_SelectedIndexChanged(object sender, EventArgs e)
        {
            //System.Drawing.Color bgcolor = Color.;

            tabControl1.TabPages[tabControl1.SelectedIndex].Focus();

            //tabCrlDecoder.TabPages[tabCrlDecoder.SelectedIndex].BackColor = bgcolor;
        }
    }
}
