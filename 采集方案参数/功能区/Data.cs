using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 采集方案参数.功能区
{
    struct Point
    {
        public double X;
        public double Y;
        public double Z;
    }
    struct LineD
    {
        public Point Start;
        public Point End;
    }
    class Data
    {
        /// <summary>
        /// 来自窗体的参数
        /// </summary>
        public static double jingdu;      //精度,单位厘米
        public static int chongdieX;      //航向重叠率
        public static int chongdieY;      //旁向重叠率
        public static double chang;       //画幅X像素数
        public static double kuan;        //画幅Y像素数
        public static double jiaoju;      //等效焦距
        public static double mianji;      //面积,单位平方公里
        public static double haiba;       //测区平均海拔高度.用于出图
        public static double sudu;        //飞机飞行速度.单位米/秒,用于计算触发时间
        public static int xuhang;         //飞机有效续航时长,用于计算架次
        ///计算后得到的飞行参数
        /// 0飞行高度
        /// 1航线间距
        /// 2触发距离
        /// 3平均海拔
        /// 4多边形面积
        /// 5航线总长
        /// 6照片量
        /// 7
        public static double[] fxcs = new double[7];

        /// <summary>
        /// 计算最小外接矩形时获得的旋转中心和旋转角度
        /// </summary>
        /// 0 X坐标
        /// 1 Y坐标
        /// 2 旋转角度
        public static double[] xuanZhuanCenter = new double[3];

        /// <summary>
        /// 
        /// </summary>
        public static OSGeo.OSR.SpatialReference oriSpat;
        public static string savePath;
        public static Point[] Buffer范围;
        public static Point[] 相机点;
        public static LineD[] 航线;

        public static Point[] tempSub;
        public static List<Point[]> subCenter;
        public static List<Point[]> subLeft;
        public static List<Point[]> subRight;
        public static List<Point[]> subFront;
        public static List<Point[]> subBack;
    }
}
