using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 采集方案参数.功能区;
namespace 采集方案参数
{
   public static class XuanZhuan
    {
        /// <summary>
        /// 返回最小外接矩形的X[],Y[]及中心点center[]
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns>角度</returns>
        public static double mixJuxing(double[] X, double[] Y,out double[] Xo,out double[] Yo,out double[] center)
        {
            //定义两个旋转中心
            //把旋转中心放在X的最小值,和Y最小值减数组X的差,得到一个可顺时针旋转45度且不跨象限的中心点
            double centerXto45 = myMin(X);
            double centerYto45 = myMin(Y) - myMax(X) - myMin(X);
            //把旋转中心放在X最小值减数组Y的差,和Y的最小值,得到一个可逆时针旋转45度且不跨象限的中心点
            double centerXto90 = myMin(X) - myMax(Y) - myMin(Y);
            double centerYto90 = myMin(Y);
            //拿到所有变化后图形的面积
            double[] allMianji = new double[900];
            //循环顺时针旋转45度时,每次变化时得到的面积(allMianji[0to449])
            for (int j = 0; j < 450; j++)
            {
                //变化密度为每次0.1度
                double jiao = j * 0.1;
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
                    newX[i] = xiebian * Math.Cos(newJiajiao * Math.PI / 180);
                    newY[i] = xiebian * Math.Sin(newJiajiao * Math.PI / 180);
                }
                //拿到顺时针旋转45度时,每次变化时得到的面积,变化过程为0到44.9度的450个值
                allMianji[j] = (myMax(newX) - myMin(newX)) * (myMax(newY) - myMin(newY));
            }//完成顺时针旋转

            //循环逆时针旋转45度时,每次变化时得到的面积(allMianji[899to450])
            for (int j = 0; j < 450; j++)
            {
                //变化密度为每次0.1度
                double jiao = j * 0.1;
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
                int k = 899 - j;
                //拿到逆时针旋转45度时,每次变化时得到的面积,变化过程为89.9到45度的450个值
                allMianji[k] = (myMax(newX) - myMin(newX)) * (myMax(newY) - myMin(newY));
            }//完成逆时针旋转
            //获得数组中最小值的下标
            int ss = Array.IndexOf(allMianji, myMin(allMianji));
           
            //以上为进行900次旋转,获得每次旋转后图形的外接矩形面积的数组
            //提取最小值,此值的下标既是其旋转角度
            //以下为重复旋转此角度,获得最小外接矩形中图形各点坐标及旋转中心坐标
            //将结果返回

            if (ss < 450)
            {
                double[] newX = new double[X.Length];
                double[] newY = new double[Y.Length];
                for (int i = 0; i < X.Length; i++)
                {
                    //求斜边长
                    double xiebian = Math.Sqrt((X[i] - centerXto45) * (X[i] - centerXto45) + (Y[i] - centerYto45) * (Y[i] - centerYto45));
                    //求原夹角
                    double yuanJiajiao = Math.Atan2(Y[i] - centerYto45, X[i] - centerXto45) * 180 / Math.PI;
                    //角度变化为递减
                    double newJiajiao = yuanJiajiao - ss * 0.1;
                    //求新坐标并赋给新数组
                    newX[i] = xiebian * Math.Cos(newJiajiao * Math.PI / 180);
                    newY[i] = xiebian * Math.Sin(newJiajiao * Math.PI / 180);
                }
                double[] temp = { centerXto45, centerYto45 };
                center = temp;
                Xo = newX;
                Yo = newY;
            }
            else
            {
                double[] newX = new double[X.Length];
                double[] newY = new double[Y.Length];
                for (int i = 0; i < X.Length; i++)
                {
                    //求斜边长
                    double xiebian = Math.Sqrt((X[i] - centerXto90) * (X[i] - centerXto90) + (Y[i] - centerYto90) * (Y[i] - centerYto90));
                    //求原夹角
                    double yuanJiajiao = Math.Atan2(Y[i] - centerYto90, X[i] - centerXto90) * 180 / Math.PI;
                    //角度变化为递增
                    double newJiajiao = yuanJiajiao + ss * 0.1;
                    //求新坐标并赋给新数组
                    newX[i] = xiebian * Math.Cos(newJiajiao * Math.PI / 180);
                    newY[i] = xiebian * Math.Sin(newJiajiao * Math.PI / 180);
                }
                double[] temp = { centerXto90, centerYto90 };
                center = temp;
                Xo = newX;
                Yo = newY;
            }
            return ss * 0.1;
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

