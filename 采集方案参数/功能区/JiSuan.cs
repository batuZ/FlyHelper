using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 采集方案参数.功能区
{
    class JiSuan
    {
        /// <returns>飞行高度</returns>
        public static int gaodu()
        {
            double jiaoChang = Math.Atan(18.0 / Data.jiaoju) / Math.PI * 180;//当前焦距下横向夹角
            // double jiaoKuan = Math.Atan(12.0 / DataInFromWindows.jiaoju) / Math.PI * 180;//当前焦距下纵向夹角

            double gaodu = (Data.jingdu / 100.0) * (Data.chang / 2.0) / Math.Tan(jiaoChang * Math.PI / 180);
            return Convert.ToInt32(gaodu);
        }
        /// <returns>航线间距</returns>
        public static int jianju()
        {
            double a = Data.chang * (Data.jingdu *0.01) * (1 - (Data.chongdieY *0.01));
            return Convert.ToInt32(a);
        }
        /// <returns>触发距离</returns>
        public static int chufaJL()
        {
            double a = Data.kuan * (Data.jingdu *0.01) * (1 - (Data.chongdieX*0.01));
            return Convert.ToInt32(a);
        }
        /// <returns>触发时间</returns>
        public static int chufaSJ()
        {
            try
            {
                double a = Data.kuan * (Data.jingdu / 100) * (1 - (Data.chongdieX / 100));
                double b = a / Data.sudu;
                return Convert.ToInt32(b);
            }
            catch
            {
                return 0;
            }
        }
        /// <returns>飞行总里程</returns>
        public static double zonglicheng()
        {
            double a = Math.Sqrt(Data.mianji)*1000;
            int c = Convert.ToInt32(Math.Ceiling(a / jianju()));
            double d = a * c;
            return d / 1000;
        }
        /// <returns>总照片量</returns>
        public static int zongzhaopian()
        {
            double a = zonglicheng()*1000 / chufaJL();
            return Convert.ToInt32(a) * 5;
        }
        /// <returns>通过巡航计算架次</returns>
        public static int jiaciXH()
        {
            double a = zonglicheng();
            double b = Data.xuhang * Data.sudu * 60;
            int c = Convert.ToInt32(a * 1000 / b);
            return c;
        }
        /// <summary>
        /// 求一条线段的长度
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static double lineLength(LineD line)
        {
            return Math.Sqrt((line.Start.X - line.End.X) * (line.Start.X - line.End.X) + (line.Start.Y - line.End.Y) * (line.Start.Y - line.End.Y));
        }
        /// <summary>
        /// 求多边形面积
        /// </summary>
        /// <param name="poly"></param>
        /// <returns></returns>
        public static double polyArea(Point[] poly)
        {
            double arce = 0;
            for (int i = 2; i < poly.Length; i++)
            {
                Point A = poly[0];
                Point B = poly[i - 1];
                Point C = poly[i];
                //三边长为
                double a = Math.Sqrt((A.X - B.X) * (A.X - B.X) + (A.Y - B.Y) * (A.Y - B.Y));
                double b = Math.Sqrt((C.X - B.X) * (C.X - B.X) + (C.Y - B.Y) * (C.Y - B.Y));
                double c = Math.Sqrt((A.X - C.X) * (A.X - C.X) + (A.Y - C.Y) * (A.Y - C.Y));
                //公式 设s=(a+b+c)/2则面积=√s(s-a)(s-b)(s-c)
                double s = (a + b + c) / 2;
                arce += Math.Sqrt( s*(s - a)*(s - b)*(s - c));
            }
            return arce;
        }

        /// <summary>
        /// 求一个多边形外接矩形的面积
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static double outRecArea(Point[] s)
        {
            double[] X = new double[s.Length];
            double[] Y = new double[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                X[i] = s[i].X;
                Y[i] = s[i].Y;
            }
            return (Xuanzhuan.myMax(X) - Xuanzhuan.myMin(X)) + (Xuanzhuan.myMax(Y) - Xuanzhuan.myMin(Y));
        }
        /// <summary>
        /// 求最大值
        /// </summary>
        /// <param name="suzu"></param>
        /// <returns>最大值</returns>
        public static double myMax(double[] suzu)
        {
            double temp = suzu[0];
            for (int i = 0; i < suzu.Length; i++)
            {
                if (suzu[i] > temp) { temp = suzu[i]; }
            }
            return temp;
        }
        /// <summary>
        /// 求最小值
        /// </summary>
        /// <param name="suzu"></param>
        /// <returns>最小值</returns>
        public static double myMin(double[] suzu)
        {
            double temp = suzu[0];
            for (int i = 0; i < suzu.Length; i++)
                temp = suzu[i] < temp ? suzu[i] : temp;
            return temp;
        }
        /// <summary>
        /// 求平均数
        /// </summary>
        /// <param name="suzu"></param>
        /// <returns>平均数</returns>
        public static double myAue(double[] suzu)
        {
            double sum = 0;
            int count = 0;
            for (; count < suzu.Length; count++)
                sum += suzu[count];
            return sum / count;
        }
    }
}
