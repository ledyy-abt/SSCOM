using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using Function;

namespace Flash
{
    public partial class u2 : UserControl
    {
        public u2()
        {
            InitializeComponent();

        }

        private void u2_Load(object sender, EventArgs e)
        {
            string[] PortName = _Uart.get_portname();
            if (PortName.Length == 0)
            {
                comboBox6.Items.Add("None");
            }
            else
            {
                foreach (string i in PortName)
                {
                    comboBox6.Items.Add(i);
                }
            }

            comboBox1.SelectedIndex = 12;
            comboBox2.SelectedIndex = 3;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            comboBox5.SelectedIndex = 2;
            comboBox6.SelectedIndex = 0;


            _Uart.BaudRate = int.Parse(comboBox1.Text);
            _Uart.DataBits = int.Parse(comboBox2.Text);
            _Uart.StopBits = (StopBits)int.Parse(comboBox3.Text);
            switch (comboBox4.SelectedIndex)
            {
                case 0: _Uart.Parity = Parity.None; break;
                case 1: _Uart.Parity = Parity.Odd; break;
                case 2: _Uart.Parity = Parity.Even; break;
                case 3: _Uart.Parity = Parity.Mark; break;
                case 4: _Uart.Parity = Parity.Space; break;
            }
            _Uart.PortName = comboBox6.Text;
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            _Uart.BaudRate = int.Parse(comboBox1.Text);
            _Uart.DataBits = int.Parse(comboBox2.Text);
            _Uart.StopBits = (StopBits)int.Parse(comboBox3.Text);
            switch (comboBox4.SelectedIndex)
            {
                case 0: _Uart.Parity = Parity.None;break;
                case 1: _Uart.Parity = Parity.Odd; break;
                case 2: _Uart.Parity = Parity.Even; break;
                case 3: _Uart.Parity = Parity.Mark; break;
                case 4: _Uart.Parity = Parity.Space; break;
            }
            _Uart.PortName = comboBox6.Text;
            MessageBox.Show("设置成功");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            _Uart.BaudRate = int.Parse(comboBox1.Text);
            _Uart.DataBits = int.Parse(comboBox2.Text);
            _Uart.StopBits = (StopBits)int.Parse(comboBox3.Text);
            switch (comboBox4.SelectedIndex)
            {
                case 0: _Uart.Parity = Parity.None; break;
                case 1: _Uart.Parity = Parity.Odd; break;
                case 2: _Uart.Parity = Parity.Even; break;
                case 3: _Uart.Parity = Parity.Mark; break;
                case 4: _Uart.Parity = Parity.Space; break;
            }
            _Uart.PortName = comboBox6.Text;
            Boolean Is_Connect = _Uart.uart_init();
            if (Is_Connect)
            {
                Thread.Sleep(500);
                MessageBox.Show("连接成功");
                label7.Text = "已连接";
                label7.ForeColor = Color.Green;
            }
        }
    }
}
