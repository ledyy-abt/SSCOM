using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO.Ports;
using System.IO;
using Function;

namespace Flash
{
    public partial class u1 : UserControl
    {

        private byte[] emuartFrameHead = new byte[2] { 0xa5, 0x06 };
        private byte[] emuartFrameTail = new byte[2] { 0xb6, 0x07 };
        public u1()
        {
            InitializeComponent();
        }

        //打开图片
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string FileName = _File.OpenFile();
                Console.WriteLine(FileName);
                pictureBox1.Load(FileName);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //写入
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                long currentTicks = DateTime.Now.Ticks;
                DateTime dtFrom = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                long currentMillis = (currentTicks - dtFrom.Ticks) / 10000;
                string name = Convert.ToString(currentMillis);

                string FileName = name + ".jpg";
                pictureBox1.Image.Save(FileName);
                byte[] PictureArray = _File.SaveFile(FileName);
                Console.WriteLine(PictureArray.Length);
                byte[] FlashArray = _FrameData.Flash_Frame_Init(63, 0, PictureArray.Length, PictureArray, 'w');
                byte[] SendArray = _FrameData.Send_Frame_Init(FlashArray);
               
                bool falg = _Uart.SendFrameData(SendArray);
                if (falg)
                {
                    Thread.Sleep(500);
                    MessageBox.Show("图片写入成功！");
                }
                else
                {
                    MessageBox.Show("图片写入失败，请重试！");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally 
            {
                pictureBox1.Image.Dispose();
            }
        }

        //读取
        private void button4_Click(object sender, EventArgs e)
        {
            byte[] FlashArray = _FrameData.Flash_Frame_Init(63, 0, 1024, 'r');
            byte[] SendArrray = _FrameData.Send_Frame_Init(FlashArray);
            byte[] RecieveData = _Uart.SendFrameData(SendArrray,'R');
            if (RecieveData!=null)
            {
                int len = (RecieveData[0] << 8) | RecieveData[1];
                byte[] photo = new byte[len];
                Array.Copy(RecieveData, 2, photo, 0, len);
                pictureBox2.Image = Image.FromStream(new MemoryStream(photo));
            }
            else
            {
                MessageBox.Show("读取出错");
            }

        }
    }
}
