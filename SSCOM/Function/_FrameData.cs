using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Function

{
    class _FrameData
    {
        //规定帧头和帧尾
        private static  byte[] Head = new byte[2] { 0xa5, 0x06 };
        private static  byte[] Tail = new byte[2] { 0xb6, 0x07 };

        //CRC校验程序
        private static ushort CRC16(byte[] ptr, int len)
        {
            ushort i, j, tmp, crc16;

            crc16 = 0xFFFF;
            for (i = 0; i < len; i++)
            {
                crc16 = (ushort)(ptr[i] ^ crc16);
                for (j = 0; j < 8; j++)
                {
                    tmp = (ushort)(crc16 & 0x0001);
                    crc16 = (ushort)(crc16 >> 1);
                    if (tmp != 0)
                        crc16 = (ushort)(crc16 ^ 0xa001);
                }
            }
            return crc16;
        }


        //将数据封装成为发送帧
        public static byte[] Send_Frame_Init(byte[] DataArray)
        {
            byte[] FrameArray = new byte[DataArray.Length + 8];//将来要返回的封装好帧的数组

            int index = 0;

            //封装帧头
            FrameArray[index++] = Head[0];
            FrameArray[index++] = Head[1];

            //封装数据长度
            FrameArray[index++] = (byte)(DataArray.Length >> 8);//高8位
            FrameArray[index++] = (byte)(DataArray.Length);//低8位

            //封装数据
            if (DataArray.Length > 0)
            {
                Array.Copy(DataArray, 0, FrameArray, index, DataArray.Length);
            }
            index += DataArray.Length;

            //CRC校验
            UInt16 temp16 = CRC16(DataArray, DataArray.Length);
            FrameArray[index++] = (byte)(temp16 >> 8);//高8位
            FrameArray[index++] = (byte)(temp16);//低8位

            //封装帧尾
            FrameArray[index++] = Tail[0];
            FrameArray[index++] = Tail[1];
            return FrameArray;
        }

        //将数据封装成为Flash帧
        public static byte[] Flash_Frame_Init(int sector,int offset,int n,byte[] DataArray,char state)
        {
            byte Flash_Sector = Convert.ToByte(sector);
            int Flash_Offset = offset;
            byte Flash_Higth_len = (byte)(n >> 8);
            byte Flash_Low_len = (byte)n;
            Console.WriteLine((Flash_Higth_len << 8) | Flash_Low_len);
            byte[] FlashArray = new byte[DataArray.Length + 6];

            try
            {
                FlashArray[0] = (byte)state;
                FlashArray[1] = Flash_Sector;
                FlashArray[2] = (byte)(Flash_Offset / 256 );
                FlashArray[3] = (byte)(Flash_Offset % 256);
                FlashArray[4] = Flash_Higth_len;
                FlashArray[5] = Flash_Low_len;

                for (int i = 6; i < 6 + DataArray.Length; i++)
                {
                    FlashArray[i] = DataArray[i - 6];
                }

                return FlashArray;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public static byte[] Flash_Frame_Init(int sector, int offset, int n, char state)
        {
            byte Flash_Sector = Convert.ToByte(sector);
            int Flash_Offset = offset;
            byte Flash_Higth_len = (byte)(n >> 8);
            byte Flash_Low_len = (byte)n;
            byte[] FlashArray = new byte[6];

            try
            {
                FlashArray[0] = (byte)state;
                FlashArray[1] = Flash_Sector;
                FlashArray[2] = (byte)(Flash_Offset / 256);
                FlashArray[3] = (byte)(Flash_Offset % 256);
                Console.WriteLine(FlashArray[2] << 8 | FlashArray[3]);
                FlashArray[4] = Flash_Higth_len;
                FlashArray[5] = Flash_Low_len;
                return FlashArray;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
