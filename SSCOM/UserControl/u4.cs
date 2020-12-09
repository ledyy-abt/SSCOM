using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Function;

namespace Flash
{
    public partial class u4 : UserControl
    {
        private DateTime X_minValue;
        private Random rand = new Random();
        public u4()
        {
            InitializeComponent();
        }

        //初始化
        private void u4_Load(object sender, EventArgs e)
        {
            init();
        }

        private void init() 
        {
            X_minValue = DateTime.Now;
            chart1.ChartAreas.Clear();
            ChartArea chartArea = new ChartArea("C1");
            chart1.ChartAreas.Add(chartArea);

            //定义存储和显示的容器
            chart1.Series.Clear();

            Series series1 = new Series("环境温度");
            series1.ChartArea = "C1";
            chart1.Series.Add(series1);

            Series series2 = new Series("MCU温度");
            series1.ChartArea = "C1";
            chart1.Series.Add(series2);

            /*********************************对于坐标轴的一些设置****************************************************/

            //允许X轴进行放大操作
            chartArea.CursorX.IsUserEnabled = true;
            chartArea.CursorX.IsUserSelectionEnabled = true;
            chartArea.CursorX.Interval = 0;
            chartArea.CursorX.IntervalOffset = 0;
            chartArea.CursorX.IntervalType = DateTimeIntervalType.Minutes;
            chartArea.AxisX.ScaleView.Zoomable = true;
            chartArea.AxisX.ScrollBar.IsPositionedInside = false;
            /*********************************************************************************************************/

            //设置图标样式
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss";
            chart1.ChartAreas[0].AxisX.LabelStyle.IntervalType = DateTimeIntervalType.Seconds;
            chart1.ChartAreas[0].AxisX.LabelStyle.Interval = 1;
            chart1.ChartAreas[0].AxisX.LabelStyle.IsEndLabelVisible = false;
            chart1.ChartAreas[0].AxisX.MajorGrid.IntervalType = DateTimeIntervalType.Seconds;
            chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 1;
            chart1.ChartAreas[0].AxisX.Minimum = DateTime.Now.ToOADate();
            chart1.ChartAreas[0].AxisX.Maximum = DateTime.Now.ToOADate();
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Transparent;
            chart1.ChartAreas[0].AxisX.LabelStyle.Angle = 90;

            //设置标题
            chart1.Titles.Clear();
            chart1.Titles.Add("温度折线图");
            chart1.Titles[0].ForeColor = Color.RoyalBlue;
            //chart1.Titles[0].Font = new Font("Microsoft Sans Serif", 12F);


            //设置图表图例显示样式
            chart1.Series[0].Color = Color.Red;
            chart1.Series[1].Color = Color.FromArgb(0, 122, 204);
            chart1.Series[0].ChartType = SeriesChartType.Spline;
            chart1.Series[1].ChartType = SeriesChartType.Spline;
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            init();
            timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] sendData = new byte[2];
            sendData[0] = (byte)0;
            _Uart.SendData(sendData, false);
            timer1.Stop();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].AxisX.Minimum = X_minValue.ToOADate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            byte[] sendData = new byte[2];
            sendData[0] = (byte)1;
            byte[] TempArray = _Uart.SendData(sendData, true);
            int eTemp = (int)TempArray[0];
            int mcuTemp = (int)TempArray[1];
            chart1.Series[0].Points.AddXY(DateTime.Now.ToOADate(), eTemp);
            chart1.Series[1].Points.AddXY(DateTime.Now.ToOADate(), mcuTemp);

            chart1.ChartAreas[0].AxisX.Maximum = DateTime.Now.AddSeconds(1).ToOADate();//X坐标后移动1秒

            if (checkBox1.Checked == true)
            {
                chart1.ChartAreas[0].AxisX.Minimum = DateTime.Now.AddSeconds(-10).ToOADate();
            }
        }
    }
}
