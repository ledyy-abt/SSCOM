using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Function;
using System.Threading;

namespace Flash
{
    public partial class u6 : UserControl
    {
        public u6()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {

            byte[] temp = System.Text.Encoding.Default.GetBytes(textBox1.Text);
            byte[] senddata = new byte[temp.Length + 3];
            senddata[0] = 0xEF;
            senddata[1] = (byte)temp.Length;
            for (int i = 2; i < temp.Length + 2; i++)
            {
                senddata[i] = temp[i - 2];
            }
            senddata[senddata.Length - 1] = 0xFE;
            _Uart.SendData(senddata, false);
        }


        private void u3_Load(object sender, EventArgs e)
        {
            //timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            byte[] receive = _Uart.DataReceive();
            if (receive != null)
            {
                string text = System.Text.Encoding.Default.GetString(receive);
                richTextBox1.Text = text;
            }
            else
            {
                richTextBox1.Text = "暂无数据";
            }


        }
    }
}
