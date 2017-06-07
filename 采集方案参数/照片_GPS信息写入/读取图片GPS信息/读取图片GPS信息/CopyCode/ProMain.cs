using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 读取图片GPS信息.CopyCode
{
    class ProMain
    {
        public void MyPro()
        {
            DialogUtil openFolder = new DialogUtil();
            if (openFolder.ShowFolderDialog() == DialogResult.OK)
            {
                DirectoryInfo source = new DirectoryInfo(openFolder.Path);
                foreach (FileInfo fi in source.GetFiles("*.jpg"))
                {
                    ExifManager exif = new ExifManager(fi.FullName);
                    exif.DateTimeOriginal = exif.DateTimeOriginal.AddHours(12);
                    exif.DateTimeDigitized = exif.DateTimeDigitized.AddHours(12);
                    exif.Save(fi.DirectoryName + "\\new\\" + fi.Name);
                    
                    exif.Dispose();
                }
            }
        }
    }
}
