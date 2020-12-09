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

namespace SSCOM
{
    public partial class u5 : UserControl
    {
        public u5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] send = new byte[1];
            send[0] = 1;
            _Uart.SendData(send, false);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] send = new byte[1];
            send[0] = 2;
            _Uart.SendData(send, false);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            byte[] send = new byte[1];
            send[0] = 3;
            _Uart.SendData(send, false);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            byte[] send = new byte[1];
            send[0] = 4;
            _Uart.SendData(send, false);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            byte[] send = new byte[1];
            send[0] = 5;
            _Uart.SendData(send, false);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            byte[] send = new byte[1];
            send[0] = 6;
            _Uart.SendData(send, false);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            byte[] send = new byte[1];
            send[0] = 7;
            _Uart.SendData(send, false);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            byte[] send = new byte[1];
            send[0] = 8;
            _Uart.SendData(send, false);
        }
    }
}
