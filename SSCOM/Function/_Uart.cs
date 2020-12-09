using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Function

{
    class _Uart
    {
        private static SerialPort serialPort;
        private static string portName;//串口号
        private static  int baudRate;//波特率
        private static  Parity parity;//校验位
        private static  int dataBits;//数据位
        private static StopBits stopBits;//停止位

        public static string PortName { get => portName; set => portName = value; }
        public static int BaudRate { get => baudRate; set => baudRate = value; }
        public static Parity Parity { get => parity; set => parity = value; }
        public static int DataBits { get => dataBits; set => dataBits = value; }
        public static StopBits StopBits { get => stopBits; set => stopBits = value; }

        public static Boolean uart_init()
        {
            try
            {
                serialPort = new SerialPort(PortName, BaudRate, Parity, DataBits, StopBits);
                serialPort.ReadTimeout = 1000;
                serialPort.Open();
                Console.WriteLine("打开端口成功");
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            return false;
        }

        public static string[]  get_portname()
        {

            return SerialPort.GetPortNames();
        }

        public static bool SendFrameData(byte[] datastr)
        {
            try
            {
                serialPort.Write(datastr, 0, datastr.Length);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static byte[] SendFrameData(byte[] datastr,char state)
        {
            try
            {
                serialPort.Write(datastr, 0, datastr.Length);
                
                return DataReceive();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static byte[] SendData(byte[] datastr,bool receive)
        {
            try
            {
                serialPort.Write(datastr, 0, datastr.Length);
                if (receive) 
                {
                    return DataReceive();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static byte[] DataReceive()
        {
            while (true)
            {
                if (serialPort != null && serialPort.IsOpen)
                {
                    try
                    {
                        byte[] buffer = new byte[1024];
                        Thread.Sleep(1000);
                        serialPort.Read(buffer, 0, buffer.Length);
                        return buffer;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally 
                    {
                        serialPort.DiscardInBuffer();
                    }
                }
                return null;
            }
        }

        public static byte[] SpiDataReceive()
        {
            while (true)
            {
                if (serialPort != null && serialPort.IsOpen)
                {
                    try
                    {
                        byte[] buffer = new byte[1024];
                        Thread.Sleep(2000);
                        serialPort.Read(buffer, 0, buffer.Length);
                        
                        return buffer;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        serialPort.DiscardInBuffer();
                    }
                }
                return null;
            }
        }

        public static void SpiSendData(byte[] datastr)
        {
            byte[] temp = new byte[1];
            try
            {
                int len = datastr.Length;
                for (int i = 0; i < len; i++)
                {
                    temp[0] = datastr[i];
                    Thread.Sleep(50);
                    serialPort.Write(temp, 0, temp.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
