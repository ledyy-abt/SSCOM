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
    public partial class u3 : UserControl
    {
        public u3()
        {
            InitializeComponent();
          
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
             
            byte[] temp = System.Text.Encoding.Default.GetBytes(textBox1.Text);
            byte[] data = new byte[temp.Length + 2];
            data[0] = (byte)temp.Length;
            for (int i = 1; i <= temp.Length; i++)
            {
                data[i] = temp[i - 1];
            }
            data[temp.Length + 1] = 0xFF;
            _Uart.SpiSendData(data);     
        }

        private void Is_Message()
        {
            while (true)
            {
                byte[] temp = _Uart.SpiDataReceive();
                if (temp == null) {
                    Console.WriteLine("无数据");
                }
                else
                {
                   
                    byte[] text = new byte[255];
                    for (int i = 1; i <= temp[0]; i++)
                    {
                        text[i - 1] = temp[i];
                    }
                    string str = System.Text.Encoding.Default.GetString(text);
                    richTextBox1.Text = str;
                    Console.WriteLine(str);
                }
            }
            
        }

        private void u3_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            Thread message = new Thread(Is_Message);
            message.Start();
        }
    }
}
