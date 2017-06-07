using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 采集方案参数.功能区
{
    class Xuanzhuan
    {
        #region 第一版旋转算法
        /*
        public static Point[] mixJuxing(Point[] rineLine)
        {
            #region  转换数组
            int pointCount = rineLine.Length;
            double[] X = new double[pointCount];
            double[] Y = new double[pointCount];
            for (int i = 0; i < pointCount; i++)
            {
                X[i] = rineLine[i].X;
                Y[i] = rineLine[i].Y;
            }
            #endregion
            #region 定义两个旋转中心
            //把旋转中心放在X的最小值,和Y最小值减数组X的差,得到一个可顺时针旋转45度且不跨象限的中心点
            double centerXto45 = myMin(X);
            double centerYto45 = myMin(Y) - (myMax(X) - myMin(X));
            //把旋转中心放在X最小值减数组Y的差,和Y的最小值,得到一个可逆时针旋转45度且不跨象限的中心点
            double centerXto90 = myMin(X) - (myMax(Y) - myMin(Y));
            double centerYto90 = myMin(Y);
            //拿到所有变化后图形的面积
            double[] allMianji = new double[90];
            #endregion
            #region 计算所有旋转结果的面积并得到面积数组
            //循环顺时针旋转45度时,每次变化时得到的面积(allMianji[0to49])
            for (int j = 0; j < 45; j++)
            {
                //变化密度为每次1度
                double jiao = j;
                //循环某一个角度变化,得到的新坐标集(数组)
                double[] newX = new double[X.Length];
                double[] newY = new double[Y.Length];
                for (int i = 0; i < X.Length; i++)
                {
                    //求斜边长
                    double xiebian = Math.Sqrt((X[i] - centerXto45) * (X[i] - centerXto45) + (Y[i] - centerYto45) * (Y[i] - centerYto45));
                    //求原夹角
                    double yuanJiajiao = Math.Atan2(Y[i] - centerYto45, X[i] - centerXto45) * 180 / Math.PI;
                    //角度变化为递减
                    double newJiajiao = yuanJiajiao - jiao;
                    //求新坐标并赋给新数组
                    newX[i] = xiebian * Math.Cos(newJiajiao * Math.PI / 180);///////角度为0时有问题
                    newY[i] = xiebian * Math.Sin(newJiajiao * Math.PI / 180);
                }
                //拿到顺时针旋转45度时,每次变化时得到的面积,变化过程为0到44.9度的450个值
                allMianji[j] = (myMax(newX) - myMin(newX)) * (myMax(newY) - myMin(newY));
            }//完成顺时针旋转

            //循环逆时针旋转45度时,每次变化时得到的面积(allMianji[899to450])
            for (int j = 0; j < 45; j++)
            {
                //变化密度为每次1度
                double jiao = j;
                //循环某一个角度变化,得到的新坐标集(数组)
                double[] newX = new double[X.Length];
                double[] newY = new double[Y.Length];
                for (int i = 0; i < X.Length; i++)
                {
                    //求斜边长
                    double xiebian = Math.Sqrt((X[i] - centerXto90) * (X[i] - centerXto90) + (Y[i] - centerYto90) * (Y[i] - centerYto90));
                    //求原夹角
                    double yuanJiajiao = Math.Atan2(Y[i] - centerYto90, X[i] - centerXto90) * 180 / Math.PI;
                    //角度变化为递增
                    double newJiajiao = yuanJiajiao + jiao;
                    //求新坐标并赋给新数组
                    newX[i] = xiebian * Math.Cos(newJiajiao * Math.PI / 180);
                    newY[i] = xiebian * Math.Sin(newJiajiao * Math.PI / 180);
                }
                //重要!!! 倒序赋值
                int k = 89 - j;
                //拿到逆时针旋转45度时,每次变化时得到的面积,变化过程为89.9到45度的450个值
                allMianji[k] = (myMax(newX) - myMin(newX)) * (myMax(newY) - myMin(newY));
            }
            //完成逆时针旋转
            #endregion
            //获得数组中最小值的下标
            int MinMianjiID = Array.IndexOf(allMianji, myMin(allMianji));

            //以上为进行90次旋转,获得每次旋转后图形的外接矩形面积的数组
            //提取最小值,此值的下标既是其旋转角度
            //以下为重复旋转此角度,获得最小外接矩形中图形各点坐标及旋转中心坐标
            //将结果返回

            if (MinMianjiID < 45)
            {
                Point[] minRing = new Point[X.Length];
                for (int i = 0; i < X.Length; i++)
                {
                    //求斜边长
                    double xiebian = Math.Sqrt((X[i] - centerXto45) * (X[i] - centerXto45) + (Y[i] - centerYto45) * (Y[i] - centerYto45));
                    //求原夹角
                    double yuanJiajiao = Math.Atan2(Y[i] - centerYto45, X[i] - centerXto45) * 180 / Math.PI;
                    //角度变化为递减
                    double newJiajiao = yuanJiajiao - MinMianjiID;
                    //求新坐标并赋给新数组
                    minRing[i].X = xiebian * Math.Cos(newJiajiao * Math.PI / 180);
                    minRing[i].Y = xiebian * Math.Sin(newJiajiao * Math.PI / 180);
                }
                Data.xuanZhuanCenter[0] = centerXto45;
                Data.xuanZhuanCenter[1] = centerYto45;
                Data.xuanZhuanCenter[2] = MinMianjiID;
                return minRing;
            }
            else
            {
                Point[] minRing = new Point[X.Length];
                for (int i = 0; i < X.Length; i++)
                {
                    //求斜边长
                    double xiebian = Math.Sqrt((X[i] - centerXto90) * (X[i] - centerXto90) + (Y[i] - centerYto90) * (Y[i] - centerYto90));
                    //求原夹角
                    double yuanJiajiao = Math.Atan2(Y[i] - centerYto90, X[i] - centerXto90) * 180 / Math.PI;
                    //角度变化为递增
                    double newJiajiao = yuanJiajiao + (89 - MinMianjiID);
                    //求新坐标并赋给新数组
                    minRing[i].X = xiebian * Math.Cos(newJiajiao * Math.PI / 180);
                    minRing[i].Y = xiebian * Math.Sin(newJiajiao * Math.PI / 180);
                }
                Data.xuanZhuanCenter[0] = centerXto90;
                Data.xuanZhuanCenter[1] = centerYto90;
                Data.xuanZhuanCenter[2] = MinMianjiID;
                return minRing;
            }
        }
        /// <summary>
        /// 把求最小外接矩形时旋转的角度还原回来
        /// </summary>
        /// <param name="camPoint"></param>
        /// <returns></returns>
        public static Point[] huanYuan(Point[] camPoint)
        {
            double MinMianjiID = Data.xuanZhuanCenter[2];
            double[] X = new double[camPoint.Length];
            double[] Y = new double[camPoint.Length];
            double[] Z = new double[camPoint.Length];
            for (int i = 0; i < camPoint.Length; i++)
            {
                X[i] = camPoint[i].X;
                Y[i] = camPoint[i].Y;
                Z[i] = camPoint[i].Z;
            }
            Point[] yuanRing = new Point[camPoint.Length];
            if (MinMianjiID < 45)
            {
                double centerXto45 = Data.xuanZhuanCenter[0];
                double centerYto45 = Data.xuanZhuanCenter[1];

                for (int i = 0; i < camPoint.Length; i++)
                {
                    //求斜边长
                    double xiebian = Math.Sqrt((X[i] - centerXto45) * (X[i] - centerXto45) + (Y[i] - centerYto45) * (Y[i] - centerYto45));
                    //求原夹角
                    double yuanJiajiao = Math.Atan2(Y[i] - centerYto45, X[i] - centerXto45) * 180 / Math.PI;
                    //角度变化为递减
                    double newJiajiao = yuanJiajiao + MinMianjiID;
                    //求新坐标并赋给新数组
                    yuanRing[i].X = xiebian * Math.Cos(newJiajiao * Math.PI / 180);
                    yuanRing[i].Y = xiebian * Math.Sin(newJiajiao * Math.PI / 180);
                    yuanRing[i].Z = Z[i];
                }
                return yuanRing;
            }
            else
            {
                double centerXto90 = Data.xuanZhuanCenter[0];
                double centerYto90 = Data.xuanZhuanCenter[1];

                for (int i = 0; i < camPoint.Length; i++)
                {
                    //求斜边长
                    double xiebian = Math.Sqrt((X[i] - centerXto90) * (X[i] - centerXto90) + (Y[i] - centerYto90) * (Y[i] - centerYto90));
                    //求原夹角
                    double yuanJiajiao = Math.Atan2(Y[i] - centerYto90, X[i] - centerXto90) * 180 / Math.PI;
                    //角度变化为递增
                    double newJiajiao = yuanJiajiao + (90 - MinMianjiID);
                    //求新坐标并赋给新数组
                    yuanRing[i].X = xiebian * Math.Cos(newJiajiao * Math.PI / 180);
                    yuanRing[i].Y = xiebian * Math.Sin(newJiajiao * Math.PI / 180);
                    yuanRing[i].Z = Z[i];
                }
                return yuanRing;
            }
        }
        public static LineD[] huanYuan(LineD[] lines)
        {
            double MinMianjiID = Data.xuanZhuanCenter[2];
            LineD[] outLines = new LineD[lines.Length];
            if (MinMianjiID < 45)
            {
                double centerXto45 = Data.xuanZhuanCenter[0];
                double centerYto45 = Data.xuanZhuanCenter[1];

                for (int i = 0; i < lines.Length; i++)
                {
                    //求斜边长
                    double xiebian = Math.Sqrt((lines[i].Start.X - centerXto45) * (lines[i].Start.X - centerXto45) + (lines[i].Start.Y - centerYto45) * (lines[i].Start.Y - centerYto45));
                    //求原夹角
                    double yuanJiajiao = Math.Atan2(lines[i].Start.Y - centerYto45, lines[i].Start.X - centerXto45) * 180 / Math.PI;
                    //角度变化为递减
                    double newJiajiao = yuanJiajiao + MinMianjiID;
                    //求新坐标并赋给新数组
                    outLines[i].Start.X = xiebian * Math.Cos(newJiajiao * Math.PI / 180);
                    outLines[i].Start.Y = xiebian * Math.Sin(newJiajiao * Math.PI / 180);
                    outLines[i].Start.Z = lines[i].Start.Z;

                    //求斜边长
                    double xiebianE = Math.Sqrt((lines[i].End.X - centerXto45) * (lines[i].End.X - centerXto45) + (lines[i].End.Y - centerYto45) * (lines[i].End.Y - centerYto45));
                    //求原夹角
                    double yuanJiajiaoE = Math.Atan2(lines[i].End.Y - centerYto45, lines[i].End.X - centerXto45) * 180 / Math.PI;
                    //角度变化为递减
                    double newJiajiaoE = yuanJiajiaoE + MinMianjiID;
                    //求新坐标并赋给新数组
                    outLines[i].End.X = xiebianE * Math.Cos(newJiajiaoE * Math.PI / 180);
                    outLines[i].End.Y = xiebianE * Math.Sin(newJiajiaoE * Math.PI / 180);
                    outLines[i].End.Z = lines[i].End.Z;
                }
                return outLines;
            }
            else
            {
                double centerXto90 = Data.xuanZhuanCenter[0];
                double centerYto90 = Data.xuanZhuanCenter[1];
                for (int i = 0; i < lines.Length; i++)
                {
                    //求斜边长
                    double xiebian = Math.Sqrt((lines[i].Start.X - centerXto90) * (lines[i].Start.X - centerXto90) + (lines[i].End.Y - centerYto90) * (lines[i].End.Y - centerYto90));
                    //求原夹角
                    double yuanJiajiao = Math.Atan2(lines[i].End.Y - centerYto90, lines[i].Start.X - centerXto90) * 180 / Math.PI;
                    //角度变化为递增
                    double newJiajiao = yuanJiajiao + (90 - MinMianjiID);
                    //求新坐标并赋给新数组
                    outLines[i].Start.X = xiebian * Math.Cos(newJiajiao * Math.PI / 180);
                    outLines[i].Start.Y = xiebian * Math.Sin(newJiajiao * Math.PI / 180);
                    outLines[i].Start.Z = lines[i].Start.Z;

                    //求斜边长
                    double xiebianE = Math.Sqrt((lines[i].Start.X - centerXto90) * (lines[i].Start.X - centerXto90) + (lines[i].End.Y - centerYto90) * (lines[i].End.Y - centerYto90));
                    //求原夹角
                    double yuanJiajiaoE = Math.Atan2(lines[i].End.Y - centerYto90, lines[i].Start.X - centerXto90) * 180 / Math.PI;
                    //角度变化为递增
                    double newJiajiaoE = yuanJiajiaoE - MinMianjiID;
                    //求新坐标并赋给新数组
                    outLines[i].End.X = xiebianE * Math.Cos(newJiajiaoE * Math.PI / 180);
                    outLines[i].End.Y = xiebianE * Math.Sin(newJiajiaoE * Math.PI / 180);
                    outLines[i].End.Z = lines[i].End.Z;
                }
                return outLines;
            }
        }*/
        #endregion
        #region 第二版旋转算法
        private static double cX;
        private static double cY;
        private static double cJ;
        /// <summary>
        /// 求多边形最小外接矩形
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        public static Point[] minOutRec(Point[] polygon)
        {
            Point[] leftMin = polygon;
            double leftX = minPOINT(polygon, "X") - (maxPOINT(polygon, "Y") - minPOINT(polygon, "Y"));
            double leftY = minPOINT(polygon, "Y");
            double leftJiao = 0;
            #region left
            for (int j = 0; j < 450; j++)
            {
                Point[] temp = new Point[polygon.Length];
                for (int i = 0; i < polygon.Length; i++)
                {
                    //求斜边长
                    double xiebian = Math.Sqrt(Math.Pow((polygon[i].X - leftX), 2) + Math.Pow((polygon[i].Y - leftY), 2));
                    //求原夹角
                    double yuanJiajiao = Math.Atan2(polygon[i].Y - leftY, polygon[i].X - leftX) * 180 / Math.PI;
                    //角度变化为递减
                    double newJiajiao = yuanJiajiao + j * 0.1;
                    temp[i].X = leftX + xiebian * Math.Cos(newJiajiao * Math.PI / 180);
                    temp[i].Y = leftY + xiebian * Math.Sin(newJiajiao * Math.PI / 180);
                }
                if (outRecArea(temp) < outRecArea(leftMin))
                {
                    leftMin = temp;
                    leftJiao = j * 0.1;
                }
            }
            #endregion
            Point[] rightMin = polygon;
            double rightX = minPOINT(polygon, "X");
            double rightY = minPOINT(polygon, "Y") - (maxPOINT(polygon, "X") - minPOINT(polygon, "X"));
            double rightJiao = 0;
            #region right
            for (int j = 0; j < 450; j++)
            {
                Point[] temp = new Point[polygon.Length];
                for (int i = 0; i < polygon.Length; i++)
                {
                    //求斜边长
                    double xiebian = Math.Sqrt(Math.Pow((polygon[i].X - rightX), 2) + Math.Pow((polygon[i].Y - rightY), 2));
                    //求原夹角
                    double yuanJiajiao = Math.Atan2(polygon[i].Y - rightY, polygon[i].X - rightX) * 180 / Math.PI;
                    //角度变化为递减
                    double newJiajiao = yuanJiajiao - j * 0.1;
                    temp[i].X = rightX + xiebian * Math.Cos(newJiajiao * Math.PI / 180);
                    temp[i].Y = rightY + xiebian * Math.Sin(newJiajiao * Math.PI / 180);
                }
                if (outRecArea(temp) < outRecArea(rightMin))
                {
                    rightMin = temp;
                    rightJiao = j * 0.1;
                }
            }
            #endregion
            if (outRecArea(leftMin) < outRecArea(rightMin))
            {
                cX = leftX;
                cY = leftY;
                cJ = leftJiao * -1;
                return leftMin;
            }
            else
            {
                cX = rightX;
                cY = rightY;
                cJ = rightJiao;
                return rightMin;
            }
        }
        /// <summary>
        /// 还原多边形
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        public static Point[] backRec(Point[] polygon)
        {
            Point[] temp = new Point[polygon.Length];
            for (int i = 0; i < polygon.Length; i++)
            {
                //求斜边长
                double xiebian = Math.Sqrt(Math.Pow((polygon[i].X - cX), 2) + Math.Pow((polygon[i].Y - cY), 2));
                //求原夹角
                double yuanJiajiao = Math.Atan2(polygon[i].Y - cY, polygon[i].X - cX) * 180 / Math.PI;
                //角度变化为递减
                double newJiajiao = yuanJiajiao + cJ;
                temp[i].X = cX + xiebian * Math.Cos(newJiajiao * Math.PI / 180);
                temp[i].Y = cY + xiebian * Math.Sin(newJiajiao * Math.PI / 180);
                temp[i].Z = polygon[i].Z;
            }
            return temp;
        }
        public static LineD[] backRec(LineD[] lines)
        {
            LineD[] tempL = new LineD[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                Point[] tempP = new Point[2];
                tempP[0] = lines[i].Start;
                tempP[1] = lines[i].End;
                tempL[i].Start = backRec(tempP)[0];
                tempL[i].End = backRec(tempP)[1];
            }
            return tempL;
        }
        #endregion
        #region 第三版旋转
        /// <summary>
        /// 求一个2D的点旋转后的坐标,角度为正数时逆时针转,角度为负数时顺时针转.0度为X轴正方向
        /// </summary>
        /// <param name="cp"></param>
        /// <param name="tp"></param>
        /// <param name="jiao"></param>
        /// <returns></returns>
        public static Point resP2D(Point 旋转中心点, Point 目标点, double 旋转角度)
        {
            //接收结果
            Point a = new Point();
            //求出两点距离,也就是圆的半径
            double r = Math.Sqrt(Math.Pow(目标点.X - 旋转中心点.X, 2) + Math.Pow(目标点.Y - 旋转中心点.Y, 2));
            //求出斜率(或叫斜角) k ,目标点 - 中心点
            double k = Math.Atan2(目标点.Y - 旋转中心点.Y, 目标点.X - 旋转中心点.X) * 180 / Math.PI;
            a.X = 旋转中心点.X + r * Math.Cos((k + 旋转角度) * Math.PI / 180);
            a.Y = 旋转中心点.Y + r * Math.Sin((k + 旋转角度) * Math.PI / 180);
            return a;
        }
        /// <summary>
        /// 【求点在三维空间中旋转后得到的坐标】
        /// 【Y正方向为北】
        /// 【X正方向为东】
        /// 【Z为高度正方向为上】
        /// 【调用时需明确在哪个两个轴平面中旋转XYorXZorYZ】
        /// 【角度为正数时逆时针转,角度为负数时顺时针转.
        /// 0度为当前横轴向右】
        /// </summary>
        /// <param name="旋转中心点"></param>
        /// <param name="目标点"></param>
        /// <param name="旋转角度"></param>
        /// <param name="XYorXZorYZ"></param>
        /// <returns></returns>
        public static Point resP3D(Point 旋转中心点, Point 目标点, double 旋转角度, string XYorXZorYZ)
        {
            Point a = new Point();
            if (XYorXZorYZ == "XY")
            {
                double r = Math.Sqrt(Math.Pow(目标点.X - 旋转中心点.X, 2) + Math.Pow(目标点.Y - 旋转中心点.Y, 2));
                double k = Math.Atan2(目标点.Y - 旋转中心点.Y, 目标点.X - 旋转中心点.X) * 180 / Math.PI;
                a.X = 旋转中心点.X + r * Math.Cos((k + 旋转角度) * Math.PI / 180);
                a.Y = 旋转中心点.Y + r * Math.Sin((k + 旋转角度) * Math.PI / 180);
                a.Z = 目标点.Z;
            }
           else if (XYorXZorYZ == "XZ")
            {
                double r = Math.Sqrt(Math.Pow(目标点.X - 旋转中心点.X, 2) + Math.Pow(目标点.Z - 旋转中心点.Z, 2));
                double k = Math.Atan2(目标点.Z - 旋转中心点.Z, 目标点.X - 旋转中心点.X) * 180 / Math.PI;
                a.X = 旋转中心点.X + r * Math.Cos((k + 旋转角度) * Math.PI / 180);
                a.Z = 旋转中心点.Z + r * Math.Sin((k + 旋转角度) * Math.PI / 180);
                a.Y = 目标点.Y;
            }
            else if (XYorXZorYZ == "YZ")
            {
                double r = Math.Sqrt(Math.Pow(目标点.Y - 旋转中心点.Y, 2) + Math.Pow(目标点.Z - 旋转中心点.Z, 2));
                double k = Math.Atan2(目标点.Z - 旋转中心点.Z, 目标点.Y - 旋转中心点.Y) * 180 / Math.PI;
                a.Y = 旋转中心点.Y + r * Math.Cos((k + 旋转角度) * Math.PI / 180);
                a.Z = 旋转中心点.Z + r * Math.Sin((k + 旋转角度) * Math.PI / 180);
                a.X = 目标点.X;
            }
            return a;
        }
        #endregion

        #region overpast
        /// <summary>
        /// 求多边形某坐标轴的最大值
        /// </summary>
        /// <param name="point"></param>
        /// <param name="XorYorZ"></param>
        /// <returns></returns>
        private static double maxPOINT(Point[] point, string XorYorZ)
        {
            if (XorYorZ == "X")
            {
                double temp = point[0].X;
                for (int i = 0; i < point.Length; i++)
                {
                    if (point[i].X > temp)
                    { temp = point[i].X; }
                }
                return temp;
            }
            else if (XorYorZ == "Y")
            {
                double temp = point[0].Y;
                for (int i = 0; i < point.Length; i++)
                {
                    if (point[i].Y > temp)
                    { temp = point[i].Y; }
                }
                return temp;
            }
            else if (XorYorZ == "Z")
            {
                double temp = point[0].Z;
                for (int i = 0; i < point.Length; i++)
                {
                    if (point[i].Z > temp)
                    { temp = point[i].Z; }
                }
                return temp;
            }
            else
                return -1;
        }
        /// <summary>
        /// 求多边形某坐标轴的最小值
        /// </summary>
        /// <param name="point"></param>
        /// <param name="XorYorZ"></param>
        /// <returns></returns>
        private static double minPOINT(Point[] point, string XorYorZ)
        {
            if (XorYorZ == "X")
            {
                double temp = point[0].X;
                for (int i = 0; i < point.Length; i++)
                {
                    if (point[i].X < temp)
                    { temp = point[i].X; }
                }
                return temp;
            }
            else if (XorYorZ == "Y")
            {
                double temp = point[0].Y;
                for (int i = 0; i < point.Length; i++)
                {
                    if (point[i].Y < temp)
                    { temp = point[i].Y; }
                }
                return temp;
            }
            else if (XorYorZ == "Z")
            {
                double temp = point[0].Z;
                for (int i = 0; i < point.Length; i++)
                {
                    if (point[i].Z < temp)
                    { temp = point[i].Z; }
                }
                return temp;
            }
            else
                return -1;
        }
        /// <summary>
        /// 求多边形某坐标轴的平均值
        /// </summary>
        /// <param name="point"></param>
        /// <param name="XorYorZ"></param>
        /// <returns></returns>
        public static double auePOINT(Point[] point, string XorYorZ)
        {
            if (XorYorZ == "X")
            {
                int count = point.Length;
                double temp = 0;
                for (int i = 0; i < point.Length; i++)
                {
                    temp += point[i].X;
                }
                return temp / count;
            }
            else if (XorYorZ == "Y")
            {
                int count = point.Length;
                double temp = 0;
                for (int i = 0; i < point.Length; i++)
                {
                    temp += point[i].Y;
                }
                return temp / count;
            }
            else if (XorYorZ == "Z")
            {
                int count = point.Length;
                double temp = 0;
                for (int i = 0; i < point.Length; i++)
                {
                    temp += point[i].Z;
                }
                return temp / count;
            }
            else
                return -99999999999999;
        }
        /// <summary>
        /// 求一个多边形的外接矩形面积
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        private static double outRecArea(Point[] polygon)
        {
            double[] X = new double[polygon.Length];
            double[] Y = new double[polygon.Length];
            for (int i = 0; i < polygon.Length; i++)
            {
                X[i] = polygon[i].X;
                Y[i] = polygon[i].Y;
            }
            return (Math.Abs(Xuanzhuan.myMax(X) - Xuanzhuan.myMin(X))) + Math.Abs((Xuanzhuan.myMax(Y) - Xuanzhuan.myMin(Y)));
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
        private  static double myAue(double[] suzu)
        {
            double sum = 0;
            int count = 0;
            for (; count < suzu.Length; count++)
                sum += suzu[count];
            return sum / count;
        }
        #endregion
    }
}
