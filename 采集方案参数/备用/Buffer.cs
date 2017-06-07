using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 采集方案参数
{
    class Buffer
    {
        public static void bufferline(double[] inputX, double[] inputY, double Juli, out double[] subLineX, out double[] subLineY)
        {
            //调用subline(),用subX,subY接收平行线段的端点坐标
            double[] subX, subY;
            subline(inputX, inputY, Juli, out subX, out subY);
            //放交点用的
            double[] temX = new double[inputX.Length];
            double[] temY = new double[inputX.Length];

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
            subLineX = temX;
            subLineY = temY;
        }

        /// <summary>
        /// 求一个POLYGON向内或向外扩一定距离后得到的边的平行线段
        /// </summary>
        /// <param name="oriX"></param>
        /// <param name="oriY"></param>
        /// <param name="Juli"></param>
        /// <param name="subX"></param>
        /// <param name="subY"></param>
        static void subline(double[] oriX, double[] oriY, double Juli, out double[] subX, out double[] subY)
        { 
            //挖俩坑,放一会算出来的外扩点坐标,完事把这堆扔出去求交
            //进来的数组最后一个值和第一个值是一样的,所以要减掉
            //因为每个进来的点会和相邻的点计算,得到两条线临近的端点,所以要把进来的长度*2
            double[] temX = new double[(oriX.Length - 1) * 2];
            double[] temY = new double[(oriY.Length - 1) * 2];
            //把进来的点坐标循环起来,i是下标序号,是给进来的数组用的
            //j也是下标,是给出去的数组用的
            //这里长度减1的开涮和上面差不多
            //因为每次循环会产生两个点放在同一个数组里,所以j=+2 
            for (int i = 0, j = 0; i < oriX.Length - 1; i++, j += 2)
            {
                //i是给进来的第一个点用的,ii是给进来的第二个点用的,
                //j是给出去的第一个点用的.jj是给出去的第二个点用的
                int ii = i + 1, jj = j + 1; 
                //为了好看
                double p1X = oriX[i];
                double p1Y = oriY[i];
                double p2X = oriX[ii];
                double p2Y = oriY[ii];
                //这是进来的两个点的偏移量
                double moveX;
                double moveY;
                //当前两点形成的线段的斜率,临时用下
                double u;
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
                //情况七: P1到P2的斜率在  270 由右向左
                else if (p1X > p2X && p1Y == p2Y)
                {
                    temX[j] = p1X;
                    temY[j] = p1Y - Juli;
                    temX[jj] = p2X;
                    temY[jj] = p2Y - Juli;
                }
            }
            subX = temX;
            subY = temY;
        }
    }
}
