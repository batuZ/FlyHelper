using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 采集方案参数.功能区;

namespace 采集方案参数
{
    
    class input 
    {
        public static double jingdu;     //精度,单位厘米
        public static int chongdieX;      //航向重叠率
        public static int chongdieY;      //旁向重叠率
        public static double chang;       //画幅X像素数
        public static double kuan;        //画幅Y像素数
        public static double jiaoju;      //等效焦距
        public static double mianji;      //面积,单位平方公里
        public static double haiba;       //测区平均海拔高度.用于出图
        public static double sudu;        //飞机飞行速度.单位米/秒,用于计算触发时间
        public static int xuhang;         //飞机有效续航时长,用于计算架次
        public static int rongliang;      //相机可容纳的照片数量,用于计算架次,并提出警告

    }
 
    class JiSuan
    {
        /// <returns>飞行高度</returns>
        public static int gaodu()
        {
            double jiaoChang = Math.Atan(18.0 / input.jiaoju) / Math.PI * 180;//当前焦距下横向夹角
            // double jiaoKuan = Math.Atan(12.0 / input.jiaoju) / Math.PI * 180;//当前焦距下纵向夹角

            double gaodu = (input.jingdu / 100.0) * (input.chang / 2.0) / Math.Tan(jiaoChang * Math.PI / 180);
            return Convert.ToInt32(gaodu);
        }
        /// <returns>航线间距</returns>
        public static double jianju( )
        {
            double a = input.chang * (input.jingdu / 100) * (1 - (input.chongdieY / 100));
            return Convert.ToInt32(a);
        }
        /// <returns>触发距离</returns>
        public static int chufaJL()
        {
            double a = input.kuan * (input.jingdu / 100) * (1 - (input.chongdieX / 100));
            return Convert.ToInt32(a);
        }
        /// <returns>触发时间</returns>
        public static int chufaSJ()
        {
            try
            {
                double a = input.kuan * (input.jingdu / 100) * (1 - (input.chongdieX / 100));
                double b = a / input.sudu;
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
            //面积开平方得到边长 a, a *1000 后单位变成米.
            double a = Math.Sqrt(input.mianji)*1000;
            //边长除航线间距,得到航线数
            int c =Convert.ToInt32( Math.Ceiling(a / jianju()));
            //航线数*单航线长得到总里程
            double b = a * c;
            //单位变回KM
            return b/1000;
        }
        /// <returns>总照片量</returns>
        public static int zongzhaopian()
        {
            double a = zonglicheng()*1000 / chufaJL();
            return Convert.ToInt32(a) * 5;
        }
        /// <returns>架次</returns>
        public static int jiaci()
        {
            try
            {
                double a = chufaJL() * input.rongliang;
                double b = zonglicheng() / a;
                int c = Convert.ToInt32(Math.Truncate(b)) + 1;
                return c;
            }
            catch
            {
                return 0;
            }
        }
        /// <returns>通过巡航计算架次</returns>
        public static int jiaciXH()
        {
            double a = zonglicheng();
            double b = input.xuhang * input.sudu * 60;
            int c = Convert.ToInt32( a * 1000 / b);
            return c;
        }
    }
}
