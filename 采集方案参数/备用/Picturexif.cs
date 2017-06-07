using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 采集方案参数
{
    class Picturexif
    {
        public DataSet ExcelToDS2(string Path)
        {
            string fileType = System.IO.Path.GetExtension(Path);
            //string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + Path + ";" + "Extended Properties=Excel 8.0;";
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data source=" + Path + ";" + "Extended Properties='Excel 12.0;HDR=NO;IMEX=1'";

           // string strCom = " SELECT * FROM [Sheet1$]";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            DataTable dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            string strExcel = dt.Rows[0][2].ToString().Trim();
            OleDbDataAdapter myCommand = null;
            DataSet ds = null;
            strExcel = "select * from [" + strExcel + "]";
            myCommand = new OleDbDataAdapter(strExcel, strConn);
            ds = new DataSet();
            myCommand.Fill(ds, "table1");
            return ds;
        }
        public string FindFilepathUnderDir(DirectoryInfo dir, string fileName)
        {
            foreach (FileInfo file in dir.GetFiles())
            {
                if (Path.GetFileName(file.Name) == fileName)
                    return file.Name;
            }
            return null;
        }
        public void AddInfo2Image(string filePath, double latitude, double longitude, double altitude)
        {
            Image bmp = Image.FromFile(filePath);
            List<PropertyItem> ProItems = bmp.PropertyItems.ToList();
            bool needWrite = false;
            bmp.Dispose();
            this.fnGPS坐标2(filePath, ref ProItems, ref needWrite, latitude, longitude, altitude);
            if (needWrite)
            {
                WriteNewDescriptionInImage(filePath, ProItems);
            }
        }
        #region 读取图片中GPS点
        /// <summary>
        /// 获取图片中的GPS坐标点
        /// </summary>
        /// <param name="p_图片路径">图片路径</param>
        /// <returns>返回坐标【纬度+经度】用"+"分割 取数组中第0和1个位置的值</returns>
        public string fnGPS坐标2(string p_图片路径, ref List<PropertyItem> ProItems, ref bool NeedWrite, double latitude, double longitude, double altitude)
        {
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(p_图片路径);
            string s_GPS坐标 = "";


            //取得所有的属性(以PropertyId做排序)   
            List<PropertyItem> propertyItems = bmp.PropertyItems.OrderBy(x => x.Id).ToList();

            byte[] val;
            //if (!ListItemIDEqual(propertyItems,0))//给重新赋GPS信息
            //{
            val = new byte[4];
            propertyItems[0].Id = 0;
            propertyItems[0].Len = 4;
            propertyItems[0].Type = 1;
            val[0] = 2;
            val[1] = 3;
            val[2] = 0;
            val[3] = 0;
            propertyItems[0].Value = val;
            propertyItems.Add(propertyItems[0]);
            NeedWrite = true;
            //}
            //if (!ListItemIDEqual(propertyItems, 1))//给重新赋GPS信息
            //{
            propertyItems[1].Id = 1;
            propertyItems[1].Len = 2;
            propertyItems[1].Type = 2;
            val = new byte[2];
            val[0] = 78;
            val[1] = 0;
            propertyItems[1].Value = val;
            propertyItems.Add(propertyItems[1]);
            NeedWrite = true;
            //}
            //if (!ListItemIDEqual(propertyItems,2))//给重新赋GPS信息
            //{
            propertyItems[2].Id = 2;
            propertyItems[2].Len = 24;
            propertyItems[2].Type = 5;
            val = new byte[24];
            propertyItems[2].Value = this.ReverseCood(longitude);
            propertyItems.Add(propertyItems[2]);
            NeedWrite = true;
            //}
            //if (!ListItemIDEqual(propertyItems, 3))//给重新赋GPS信息
            //{
            propertyItems[3].Id = 3;
            propertyItems[3].Len = 2;
            propertyItems[3].Type = 2;
            val = new byte[2];
            val[0] = 69;
            val[1] = 0;
            propertyItems[3].Value = val;
            propertyItems.Add(propertyItems[3]);
            NeedWrite = true;
            //}
            //if (!ListItemIDEqual(propertyItems, 4))//给重新赋GPS信息
            //{
            propertyItems[4].Id = 4;
            propertyItems[4].Len = 24;
            propertyItems[4].Type = 5;
            val = new byte[24];
            propertyItems[4].Value = this.ReverseCood(latitude);
            propertyItems.Add(propertyItems[4]);
            NeedWrite = true;
            //}
            //if (!ListItemIDEqual(propertyItems, 6))//给重新赋GPS信息
            //{
            propertyItems[6].Id = 6;
            propertyItems[6].Len = 24;
            propertyItems[6].Type = 5;
            val = new byte[8];
            propertyItems[6].Value = this.ReverseAltitude(altitude);
            propertyItems.Add(propertyItems[6]);
            NeedWrite = true;
            //}
            ProItems = propertyItems;





            //暂定纬度N(北纬) 
            char chrGPSLatitudeRef = 'N';
            //暂定经度为E(东经)   
            char chrGPSLongitudeRef = 'E';
            foreach (PropertyItem objItem in propertyItems)
            {
                //只取Id范围为0x0000到0x001e
                if (objItem.Id >= 0x0000 && objItem.Id <= 0x001e)
                {
                    switch (objItem.Id)
                    {
                        case 0x0000:
                            var query = from tmpb in objItem.Value select tmpb.ToString();
                            string sreVersion = string.Join(".", query.ToArray());
                            break;
                        case 0x0001:
                            chrGPSLatitudeRef = BitConverter.ToChar(objItem.Value, 0);
                            break;
                        case 0x0002:
                            if (objItem.Value.Length == 24 && objItem.Type == 5)
                            {
                                //degrees(将byte[0]~byte[3]转成uint, 除以byte[4]~byte[7]转成的uint) 
                                double d = BitConverter.ToUInt32(objItem.Value, 0) * 1.0d / BitConverter.ToUInt32(objItem.Value, 4);
                                //minutes(將byte[8]~byte[11]转成uint, 除以byte[12]~byte[15]转成的uint)   
                                double m = BitConverter.ToUInt32(objItem.Value, 8) * 1.0d / BitConverter.ToUInt32(objItem.Value, 12);
                                //seconds(將byte[16]~byte[19]转成uint, 除以byte[20]~byte[23]转成的uint)   
                                double s = BitConverter.ToUInt32(objItem.Value, 16) * 1.0d / BitConverter.ToUInt32(objItem.Value, 20);
                                //计算经纬度数值, 如果是南纬, 要乘上(-1)   
                                double dblGPSLatitude = (((s / 60 + m) / 60) + d) * (chrGPSLatitudeRef.Equals('N') ? 1 : -1);
                                string strLatitude = string.Format("{0:#} deg {1:#}' {2:#.00}\" {3}", d, m, s, chrGPSLatitudeRef);
                                //纬度+经度
                                s_GPS坐标 += dblGPSLatitude + "+";
                            }
                            break;
                        case 0x0003:
                            //透过BitConverter, 将Value转成Char('E' / 'W')   
                            //此值在后续的Longitude计算上会用到   
                            chrGPSLongitudeRef = BitConverter.ToChar(objItem.Value, 0);
                            break;
                        case 0x0004:
                            if (objItem.Value.Length == 24)
                            {
                                //degrees(将byte[0]~byte[3]转成uint, 除以byte[4]~byte[7]转成的uint)   
                                double d = BitConverter.ToUInt32(objItem.Value, 0) * 1.0d / BitConverter.ToUInt32(objItem.Value, 4);
                                //minutes(将byte[8]~byte[11]转成uint, 除以byte[12]~byte[15]转成的uint)   
                                double m = BitConverter.ToUInt32(objItem.Value, 8) * 1.0d / BitConverter.ToUInt32(objItem.Value, 12);
                                //seconds(将byte[16]~byte[19]转成uint, 除以byte[20]~byte[23]转成的uint)   
                                double s = BitConverter.ToUInt32(objItem.Value, 16) * 1.0d / BitConverter.ToUInt32(objItem.Value, 20);
                                //计算精度的数值, 如果是西经, 要乘上(-1)   
                                double dblGPSLongitude = (((s / 60 + m) / 60) + d) * (chrGPSLongitudeRef.Equals('E') ? 1 : -1);
                                s_GPS坐标 += dblGPSLongitude + "+";
                            }
                            break;
                        case 0x0005:
                            string strAltitude = BitConverter.ToBoolean(objItem.Value, 0) ? "0" : "1";
                            break;
                        case 0x0006:
                            if (objItem.Value.Length == 8)
                            {
                                //将byte[0]~byte[3]转成uint, 除以byte[4]~byte[7]转成的uint   
                                double dblAltitude = BitConverter.ToUInt32(objItem.Value, 0) * 1.0d / BitConverter.ToUInt32(objItem.Value, 4);
                                s_GPS坐标 += "*" + dblAltitude;
                            }
                            break;
                    }
                }
            }
            bmp.Dispose();
            return s_GPS坐标;
        }
        #endregion
        private static void WriteNewDescriptionInImage(string Filename, List<PropertyItem> myProItems)
        {
            Image Pic;
            //int i;
            string FilenameTemp;
            System.Drawing.Imaging.Encoder Enc = System.Drawing.Imaging.Encoder.Transformation;//编码器
            EncoderParameters EncParms = new EncoderParameters(1);
            EncoderParameter EncParm;
            //ImageCodecInfo CodecInfo = GetEncoderInfo("image/jpeg");
            ImageCodecInfo CodecInfo = GetEncoderInfoByExtension(Path.GetExtension(Filename));
            // load the image to change加载图像变化
            Pic = Image.FromFile(Filename);
            foreach (PropertyItem item in myProItems)
            {
                Pic.SetPropertyItem(item);
            }
            // we cannot store in the same image, so use a temporary image instead
            //我们不能存储在相同的图像，所以使用一个临时的图像代替
            FilenameTemp = Filename + ".temp";
            // for lossless rewriting must rotate the image by 90 degrees!无损重写必须图像旋转90度！
            EncParm = new EncoderParameter(Enc, (long)EncoderValue.TransformRotate90);
            EncParms.Param[0] = EncParm;
            // now write the rotated image with new description现在写的旋转图像的新描述
            Pic.Save(FilenameTemp, CodecInfo, EncParms);
            // for computers with low memory and large pictures: release memory now电脑内存和释放内存现在：大图片
            Pic.Dispose();
            Pic = null;
            GC.Collect();
            // delete the original file, will be replaced later删除原始文件，将被替换后
            System.IO.File.Delete(Filename);
            // now must rotate back the written picture现在必须轮流回写图像
            Pic = Image.FromFile(FilenameTemp);
            EncParm = new EncoderParameter(Enc, (long)EncoderValue.TransformRotate270);
            EncParms.Param[0] = EncParm;

            Pic.Save(Filename, CodecInfo, EncParms);
            // release memory now释放内存
            Pic.Dispose();
            Pic = null;
            GC.Collect();
            // delete the temporary picture删除临时图片
            System.IO.File.Delete(FilenameTemp);
        }
        private static ImageCodecInfo GetEncoderInfoByExtension(String extensionName)
        {
            int j;
            ImageCodecInfo[] encoders;
            extensionName = extensionName.ToUpper();
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].FilenameExtension.IndexOf(extensionName) >= 0)
                    return encoders[j];
            }
            return null;
        }
        private byte[] ReverseCood(double dushu)
        {
            double leftfen = 0;
            double leftmiao = 0;
            int du = (int)dushu;
            leftfen = (dushu - du) * 60;
            int fen = (int)leftfen;
            leftmiao = (leftfen - fen) * 60;

            return ReverseCood(du, fen, leftmiao);
        }

        private byte[] ReverseCood(int du, int fen, double miao)
        {
            List<byte> _byteList = new List<byte>();
            int _fenzi = 0;
            int _fenmu = 0;
            XXtoBL(miao, ref _fenzi, ref _fenmu);

            _byteList.AddRange(this.ReverseVal(du));
            _byteList.AddRange(this.ReverseVal(1));
            _byteList.AddRange(this.ReverseVal(fen));
            _byteList.AddRange(this.ReverseVal(1));
            _byteList.AddRange(this.ReverseVal(_fenzi));
            _byteList.AddRange(this.ReverseVal(_fenmu));

            return _byteList.ToArray();
        }
        private byte[] ReverseAltitude(double altitude)
        {
            List<byte> _byteList = new List<byte>();
            int _fenzi = 0;
            int _fenmu = 0;
            GetFenshu(altitude, ref _fenzi, ref _fenmu);

            _byteList.AddRange(this.ReverseVal(_fenzi));
            _byteList.AddRange(this.ReverseVal(_fenmu));

            return _byteList.ToArray();
        }
        private byte[] ReverseVal(int val)
        {
            byte[] _byte = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                int a = val >> (i * 8) & 255;
                _byte[i] = (byte)a;
            }
            return _byte;
        }
        private static void GetFenshu(double d, ref int fenzi, ref int fenmu)
        {
            string dn = System.Text.RegularExpressions.Regex.Match(d.ToString(), @"(?<=\.)\d+").Value;
            int tn = 1;
            for (int i = dn.Length; i > 0; i--)
            {
                tn *= 10;
            }
            fenzi = (int)(d * tn);
            fenmu = tn;
            //return (d * tn).ToString() + "/" + tn.ToString();
        }
        private static void XXtoBL(double number, ref int fenzi, ref int fenmu)
        {

            decimal XX = Convert.ToDecimal(number);
            XX = Math.Round(XX, 7);
            int x1 = 1;
            //判断传入的数小数点后有几位小数
            int XXWS = XX.ToString().IndexOf(".");
            string XXBF = XX.ToString().Substring(XXWS + 1, XX.ToString().Length - XXWS - 1);
            for (int i = 0; i < XXBF.Length; i++)
            {
                x1 = x1 * 10;
            }
            int x2 = (int)(XX * x1);
            //寻找公约数
            int GYS = MaxY(x1, x2);
            x1 = x1 / GYS;
            x2 = x2 / GYS;

            fenzi = x2;
            fenmu = x1;
        }
        private static int MaxY(int firstNumber, int secondNumber)
        {
            int max = 0;
            int min = 0;
            if (firstNumber > secondNumber)
            {
                max = firstNumber;
                min = secondNumber;
            }
            else
            {
                max = secondNumber;
                min = firstNumber;

            }
            int r = max % min; if (r == 0)
            { return min; }
            else
            {
                while (r != 0)
                {
                    max = min;
                    min = r;
                    r = max % min;
                }
                return min;
            }
        }
    }
}
