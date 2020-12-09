using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flash
{
    class _File
    {
        public static string OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "图片文件|*.bmp;*.pcx;*.png;*.jpg;*.gif";
            openFileDialog.Title = "打开图像文件";
            if (openFileDialog.ShowDialog() == DialogResult.OK) 
            {
                string file = openFileDialog.FileName;
                return file;
            }
            return null;
        }

        public static byte[] SaveFile(string FileName)
        {
            FileStream fileStream = new FileStream(FileName,FileMode.Open,FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            byte[] img = binaryReader.ReadBytes((int)fileStream.Length);
            return img;
        }
    }
}
