using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 采集方案参数.功能区
{
    class Buffer
    {
        /// <summary>
        /// 获取多边形外扩线
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        public static Point[] bufferLine(Point[] polygon)
        {
            Point[] subpoint = subline(polygon);
            int subpointCount = subpoint.Length;
            //调用subline(),用subX,subY接收平行线段的端点坐标
            double[] subX = new double[subpointCount];
            double[] subY = new double[subpointCount];
            for (int i = 0; i < subpointCount; i++)
            {
                subX[i] = subpoint[i].X;
                subY[i] = subpoint[i].Y;
            }
            int pointCount = polygon.Length;
            //放交点用的
            double[] temX = new double[pointCount];
            double[] temY = new double[pointCount];
            // 两点式直线方程:    
            //(y-y1)/(y2-y1)=(x-x1)/(x2-x1)   (x1≠x2，y1≠y2)
            //(y-y3)/(y4-y3)=(x-x3)/(x4-x3)   (x3≠x4，y3≠y4)
            //y = (x-x1)*(y2-y1)/(x2-x1)+y1
            //x = (y-y3)*(x4-x3)/(y4-y3)+x3
            for (int i = 0, ii = 1; i < subX.Length; i += 2, ii++)
            {
                int j = i;
                double X, Y, x1, y1, x2, y2, x3, y3, x4, y4;
                x1 = subX[j];
                y1 = subY[j];
                j++;
                x2 = subX[j];
                y2 = subY[j];
                try
                {
                    j++;
                    x3 = subX[j];
                    y3 = subY[j];
                    j++;
                    x4 = subX[j];
                    y4 = subY[j];
                }
                catch
                {
                    x3 = subX[0];
                    y3 = subY[0];
                    x4 = subX[1];
                    y4 = subY[1];
                }
                if (x1 == x2)
                    x2 = x2 + 0.0000001;
                else if (y3 == y4)
                    y4 = y4 + 0.0000001;
                double a = (y2 - y1) / (x2 - x1);
                double b = (x4 - x3) / (y4 - y3);
                //Y = (X - x1) * (y2 - y1) / (x2 - x1) + y1;
                //Y = (X - x1) * a + y1;
                //Y = X * a - x1 * a + y1;
                //X = (Y - y3) * (x4 - x3) / (y4 - y3) + x3;
                //X = (Y - y3) * b + x3;
                //X = Y * b - y3 * b + x3;
                //X = (X * a - x1 * a + y1) * b - y3 * b + x3;
                //X = X * a * b - x1 * a * b + y1 * b - y3 * b + x3;
                //X-X * a * b =  - x1 * a * b + y1 * b - y3 * b + x3;
                X = (-x1 * a * b + y1 * b - y3 * b + x3) / (1 - a * b);
                Y = (X - x1) * (y2 - y1) / (x2 - x1) + y1;
                temX[ii] = X;
                temY[ii] = Y;
            }
            temX[0] = temX[temX.Length - 1];
            temY[0] = temY[temY.Length - 1];

            Point[] tem = new Point[temX.Length];
            for (int i = 0; i < temX.Length; i++)
            {
                tem[i].X = temX[i];
                tem[i].Y = temY[i];
            }
            return tem;
        }

        /// <summary>
        /// 求一个POLYGON向内或向外扩一定距离后得到的边的平行线段
        /// </summary>
        private static Point[] subline(Point[] ringLine)
        {
            int pointCount = ringLine.Length;
            double Juli = Data.fxcs[0];
            //挖俩坑,放一会算出来的外扩点坐标,完事把这堆扔出去求交
            //进来的数组最后一个值和第一个值是一样的,所以要减掉
            //因为每个进来的点会和相邻的点计算,得到两条线临近的端点,所以要把进来的长度*2
            Point[] tem = new Point[(pointCount - 1) * 2];
            //把进来的点坐标循环起来,i是下标序号,是给进来的数组用的
            //j也是下标,是给出去的数组用的
            //这里长度减1的开涮和上面差不多
            //因为每次循环会产生两个点放在同一个数组里,所以j=+2 
            for (int i = 0, j = 0; i < pointCount - 1; i++, j += 2)
            {
                //i是给进来的第一个点用的,ii是给进来的第二个点用的,
                //j是给出去的第一个点用的.jj是给出去的第二个点用的
                int ii = i + 1, jj = j + 1;
                //为了好看
                double p1X = ringLine[i].X;
                double p1Y = ringLine[i].Y;
                double p2X = ringLine[ii].X;
                double p2Y = ringLine[ii].Y;

                //当前两点形成的线段的斜率,临时用下
                double u = Math.Atan2(Math.Abs(p1X - p2X), Math.Abs(p1Y - p2Y)) * 180 / Math.PI;
                //这是进来的两个点的偏移量
                double moveX = Juli * Math.Cos(u * Math.PI / 180);
                double moveY = Juli * Math.Sin(u * Math.PI / 180);

                //for KML
                if (p1Y < p2Y)//当p1在p2上面时
                {//X++ 
                    tem[j].X = p1X + moveX;
                    tem[jj].X = p2X + moveX;
                }
                else if (p1Y > p2Y)//当p1在p2下面时
                {//X-- 
                    tem[j].X = p1X - moveX;
                    tem[jj].X = p2X - moveX;
                }
                else// 当两点Y值相同时
                {//X+0
                    tem[j].X = p1X;
                    tem[jj].X = p2X;
                }
                if (p1X > p2X)
                { //Y++
                    tem[j].Y = p1Y + moveY;
                    tem[jj].Y = p2Y + moveY;
                }
                else if (p1X < p2X)
                { //Y--
                    tem[j].Y = p1Y - moveY;
                    tem[jj].Y = p2Y - moveY;
                }
                else
                { //Y+0
                    tem[j].Y = p1Y;
                    tem[jj].Y = p2Y;
                }

                #region 第一版算法
                //double[] temX = new double[(pointCount - 1) * 2];
                //double[] temY = new double[(pointCount - 1) * 2];

                /*
                //情况一: P1到P2的斜率在 0=>90
                if (p1X < p2X && p1Y < p2Y)
                {
                    u = Math.Atan2((p2X - p1X), (p2Y - p1Y)) * 180 / Math.PI;
                    moveX = Juli * Math.Cos(u * Math.PI / 180);
                    moveY = Juli * Math.Sin(u * Math.PI / 180);
                    temX[j] = p1X - moveX;
                    temY[j] = p1Y + moveY;
                    temX[jj] = p2X - moveX;
                    temY[jj] = p2Y + moveY;
                }
                //情况二: P1到P2的斜率在 270=>360
                else if (p1X > p2X && p1Y < p2Y)
                {
                    u = 90 - (Math.Atan2((p1X - p2X), (p2Y - p1Y)) * 180 / Math.PI);
                    moveX = Juli * Math.Sin(u * Math.PI / 180);
                    moveY = Juli * Math.Cos(u * Math.PI / 180);
                    temX[j] = p1X - moveX;
                    temY[j] = p1Y - moveY;
                    temX[jj] = p2X - moveX;
                    temY[jj] = p2Y - moveY;
                }
                //情况三: P1到P2的斜率在  90=>180
                else if (p1X < p2X && p1Y > p2Y)
                {
                    u = 90 - (Math.Atan2((p2X - p1X), (p1Y - p2Y)) * 180 / Math.PI);
                    moveX = Juli * Math.Sin(u * Math.PI / 180);
                    moveY = Juli * Math.Cos(u * Math.PI / 180);
                    temX[j] = p1X + moveX;
                    temY[j] = p1Y + moveY;
                    temX[jj] = p2X + moveX;
                    temY[jj] = p2Y + moveY;
                }
                //情况四: P1到P2的斜率在    90=>270
                else if (p1X > p2X && p1Y > p2Y)
                {
                    u = Math.Atan2((p1X - p2X), (p1Y - p2Y)) * 180 / Math.PI;
                    moveX = Juli * Math.Cos(u * Math.PI / 180);
                    moveY = Juli * Math.Sin(u * Math.PI / 180);
                    temX[j] = p1X + moveX;
                    temY[j] = p1Y - moveY;
                    temX[jj] = p2X + moveX;
                    temY[jj] = p2Y - moveY;
                }
                //情况五: P1到P2的斜率在 0.360 由下向上
                else if (p1X == p2X && p1Y < p2Y)
                {
                    temX[j] = p1X - Juli;
                    temY[j] = p1Y;
                    temX[jj] = p2X - Juli;
                    temY[jj] = p2Y;
                }
                //情况六: P1到P2的斜率在  180 由上向下
                else if (p1X == p2X && p1Y > p2Y)
                {
                    temX[j] = p1X + Juli;
                    temY[j] = p1Y;
                    temX[jj] = p2X + Juli;
                    temY[jj] = p2Y;
                }
                //情况七: P1到P2的斜率在  90 由左向右
                else if (p1X < p2X && p1Y == p2Y)
                {
                    temX[j] = p1X;
                    temY[j] = p1Y + Juli;
                    temX[jj] = p2X;
                    temY[jj] = p2Y + Juli;
                }
                //情况八: P1到P2的斜率在  270 由右向左
                else if (p1X > p2X && p1Y == p2Y)
                {
                    temX[j] = p1X;
                    temY[j] = p1Y - Juli;
                    temX[jj] = p2X;
                    temY[jj] = p2Y - Juli;
                }
            }
            Point[] tem = new Point[temX.Length];
            for (int i = 0; i < temX.Length; i++)
            {
                tem[i].X = temX[i];
                tem[i].Y = temY[i];
            }*/
                #endregion
            }
            return tem;
        }

        /// <summary>
        /// 返回一个线段和一个多边形的公共部份
        /// </summary>
        /// <param name="minOutRec"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public static LineD cutLine(Point[] minOutRec, LineD line)
        {
            LineD newLine = new LineD();
            for (int j = 0; j < minOutRec.Length - 1; j++)
            {
                //把进来的多边形线段化为线段 ringD
                LineD ringD = new LineD();
                ringD.Start.X = minOutRec[j].X;
                ringD.Start.Y = minOutRec[j].Y;
                ringD.End.X = minOutRec[j + 1].X;
                ringD.End.Y = minOutRec[j + 1].Y;

                //判断起点有没有被赋值
                bool temp = newLine.Start.X == 0 && newLine.Start.Y == 0;
                //判断过程航线sss与多边形中的线段是否相交
                if (Buffer.withIn(ringD, line) && temp)
                {
                    //如果相交,且ddd起点未被赋过值,把交点赋值给成品航线ddd的起点
                    newLine.Start = jiaoD(line, ringD);
                }
                else if (Buffer.withIn(ringD, line) && !temp)
                {
                    //如果相交,且ddd起点被赋过值,把交点赋值给成品航线ddd的终点
                    newLine.End = jiaoD(line, ringD);
                }
            }
            return newLine;
        }


        /// <summary>
        /// 两直线求交,返回交点 线公式 AX+BY+C1=0;
        /// </summary>
        /// <param name="lineA_z"></param>
        /// <param name="lineB"></param>
        /// <returns></returns>
        public static Point jiaoD(LineD lineA_z, LineD lineB)
        {
            //定义交点
            Point res = new Point();
            double X1 = 0, X2 = 0, X3 = 0, X4 = 0, Y1 = 0, Y2 = 0, Y3 = 0, Y4 = 0;
            if (Math.Abs(lineA_z.Start.X - lineA_z.End.X) > 0.01)
            {
                //lineA的两点坐标
                X1 = lineA_z.Start.X;
                Y1 = lineA_z.Start.Y;
                X2 = lineA_z.End.X;
                Y2 = lineA_z.End.Y;
                //lineB的两点坐标
                X3 = lineB.Start.X;
                Y3 = lineB.Start.Y;
                X4 = lineB.End.X;
                Y4 = lineB.End.Y;
            }
            else if (Math.Abs(lineB.Start.X - lineB.End.X) > 0.01)
            {
                //lineB的两点坐标
                X1 = lineB.Start.X;
                Y1 = lineB.Start.Y;
                X2 = lineB.End.X;
                Y2 = lineB.End.Y;
                //lineA的两点坐标
                X3 = lineA_z.Start.X;
                Y3 = lineA_z.Start.Y;
                X4 = lineA_z.End.X;
                Y4 = lineA_z.End.Y;
            }
            else
            {
                MessageBox.Show("求交过程中被输入两条平行线,无法获得交点");
            }
            //lineA套入直线公式
            double A1 = Y2 - Y1;
            double B1 = X1 - X2;
            double C1 = X2 * Y1 - X1 * Y2;
            //lineB套入直线公式
            double A2 = Y4 - Y3;
            double B2 = X3 - X4;
            double C2 = X4 * Y3 - X3 * Y4;
            //A1*X+B1*Y+C1=0;
            //A2*X+B2*Y+C2=0;

            //解出交点

            // 1 提出lineA中的未知数Y
            //      Y*B1 = -A1*X - C1;
            //      Y = -X*A1/B1 - C1/B1;
            // 2 把Y 套入 lineB的公式中
            //      A2*X+B2*(-A1/B1 * X - C1/B1)+C2=0;
            // 3 提出lineB的公式中的未知数X
            //      A2*X + B2*(-X*A1/B1 - C1/B1) + C2=0;
            //      double g =B2*(A1/B1);
            //      double h =B2*(C1/B1);
            //      A2*X + (-B2*(A1/B1) * X - B2*(C1/B1)) + C2=0;
            //      A2*X + (-g * X - h) + C2=0;
            //      A2*X - g * X =h-C2;
            //      (A2-g)*X=h-C2;
            //      X=(h-C2)/(A2-g);
            res.X = (B2 * (C1 / B1) - C2) / (A2 - B2 * (A1 / B1));
            //4 把X套入步骤 1的公式
            res.Y = -res.X * A1 / B1 - C1 / B1;
            res.Z = lineA_z.Start.Z;
            return res;
        }

        /// <summary>
        /// 判断点是否在多边形内
        /// </summary>
        /// <param name="point"></param>
        /// <param name="ringLine"></param>
        public static bool withIn(Point[] polygon, Point point)
        {
            int jiaodianCount = 0;
            for (int i = 0; i < polygon.Length - 1; i++)
            {
                //把进来的多边形线段化为线段 ringD
                LineD tempXD = new LineD();
                tempXD.Start.X = polygon[i].X;
                tempXD.Start.Y = polygon[i].Y;
                tempXD.End.X = polygon[i + 1].X;
                tempXD.End.Y = polygon[i + 1].Y;
                bool tiaojian = false;
                if (tempXD.Start.Y < point.Y && tempXD.End.Y > point.Y)
                {
                    tiaojian = true;
                }
                else if (tempXD.Start.Y > point.Y && tempXD.End.Y < point.Y)
                {
                    tiaojian = true;
                }
                else
                {
                    continue;
                }
                double X, Y = point.Y, X1 = tempXD.Start.X, Y1 = tempXD.Start.Y, X2 = tempXD.End.X, Y2 = tempXD.End.Y;
                X = (X2 - X1) * ((Y - Y1) / (Y2 - Y1)) + X1;
                if (X >= point.X && tiaojian)
                {
                    jiaodianCount++;
                }
            }
            if (jiaodianCount % 2 == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 判断点是否在线上
        /// </summary>
        /// <param name="point"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public static bool withIn(Point point, LineD line)
        {
            double X = point.X;
            double Y = point.Y;
            //line的两点坐标
            double X1 = line.Start.X;
            double Y1 = line.Start.Y;
            double X2 = line.End.X;
            double Y2 = line.End.Y;
            //lineA套入直线公式
            double A = Y2 - Y1;
            double B = X1 - X2;
            double C = X2 * Y1 - X1 * Y2;
            return Math.Abs(A * X + B * Y + C) < 0.01;
        }

        /// <summary>
        /// 判断两条线段是否相交。
        /// </summary>
        /// <param name="lineA">线段A</param>
        /// <param name="lineB">线段B</param>
        /// <returns>相交返回真，否则返回假。</returns>
        public static bool withIn(LineD lineA, LineD lineB)
        {
            return CheckCrose(lineA, lineB) && CheckCrose(lineB, lineA);
        }
        /// <summary>
        /// 判断直线2的两点是否在直线1的两边。
        /// </summary>
        /// <param name="line1">直线1</param>
        /// <param name="line2">直线2</param>
        /// <returns></returns>
        private static bool CheckCrose(LineD line1, LineD line2)
        {
            Point v1 = new Point();
            Point v2 = new Point();
            Point v3 = new Point();

            v1.X = line2.Start.X - line1.End.X;
            v1.Y = line2.Start.Y - line1.End.Y;

            v2.X = line2.End.X - line1.End.X;
            v2.Y = line2.End.Y - line1.End.Y;

            v3.X = line1.Start.X - line1.End.X;
            v3.Y = line1.Start.Y - line1.End.Y;

            return (CrossMul(v1, v3) * CrossMul(v2, v3) <= 0);
        }
        /// <summary>
        /// 计算两个向量的叉乘。
        /// </summary>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <returns></returns>
        private static double CrossMul(Point pt1, Point pt2)
        {
            return pt1.X * pt2.Y - pt1.Y * pt2.X;
        }
    }
}
