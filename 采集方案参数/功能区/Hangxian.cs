using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 采集方案参数.功能区
{
    class Hangxian
    {
        //相机点位
        public static Point[] camPoint(Point[] minOutRec)
        {
            //转换数组
            int pointCornt = minOutRec.Length;
            double[] X = new double[pointCornt];
            double[] Y = new double[pointCornt];
            for (int i = 0; i < pointCornt; i++)
            {
                X[i] = minOutRec[i].X;
                Y[i] = minOutRec[i].Y;
            }
            #region 准备过程
            //找到图形物理中心
            double centerX = (Xuanzhuan.myMax(X) + Xuanzhuan.myMin(X)) / 2;
            double centerY = (Xuanzhuan.myMax(Y) + Xuanzhuan.myMin(Y)) / 2;
            // 航线条数tiao,触发次数cFcishu
            double tiao, cFcishu;
            //航线最小X hangXianMinX,航线最大X hangXianMaxX
            double hangXianMinX;
            //航线最小Y hangXianMinY,航线最大Y hangXianMaxY,
            double hangXianMinY;
            //判断 矩形方向,同时拿到航线条数tiao,触发次数cFcishu
            if (Xuanzhuan.myMax(X) - Xuanzhuan.myMin(X) >= Xuanzhuan.myMax(Y) - Xuanzhuan.myMin(Y))
            //横的
            {
                tiao = Math.Ceiling((Xuanzhuan.myMax(Y) - Xuanzhuan.myMin(Y)) / Data.fxcs[1]) + 1;
                cFcishu = Math.Ceiling((Xuanzhuan.myMax(X) - Xuanzhuan.myMin(X)) / Data.fxcs[2]) + 1;
            }
            else
            //竖的
            {
                tiao = Math.Ceiling((Xuanzhuan.myMax(X) - Xuanzhuan.myMin(X)) / Data.fxcs[1]) + 1;
                cFcishu = Math.Ceiling((Xuanzhuan.myMax(Y) - Xuanzhuan.myMin(Y)) / Data.fxcs[2]) + 1;
            }
            #endregion
            //判断 矩形方向,同时计算出相机触发时所处坐标位置
            List<Point> tem = new List<Point>();
            if (Xuanzhuan.myMax(X) - Xuanzhuan.myMin(X) >= Xuanzhuan.myMax(Y) - Xuanzhuan.myMin(Y))
            //横的
            {
                //拿到航线起点
                hangXianMinX = centerX - cFcishu / 2 * Data.fxcs[2];// 2触发距离
                hangXianMinY = centerY - tiao / 2 * Data.fxcs[1];// 1航线间距
                for (int i = 0; i < tiao; i++)
                {
                    for (int j = 0; j < cFcishu; j++)
                    {
                        Point ss = new Point();
                        ss.X = hangXianMinX + Data.fxcs[2] * j;// 2触发距离
                        ss.Y = hangXianMinY + Data.fxcs[1] * i;// 1航线间距
                        ss.Z = Data.fxcs[0] + Data.fxcs[3];
                        if (Buffer.withIn(minOutRec, ss))
                            tem.Add(ss);
                    }
                }
                return tem.ToArray();
            }
            else
            //竖的
            {
                //拿到航线起点
                hangXianMinX = centerX - tiao / 2 * Data.fxcs[1];
                hangXianMinY = centerY - cFcishu / 2 * Data.fxcs[2];
                for (int i = 0; i < tiao; i++)
                {
                    for (int j = 0; j < cFcishu; j++)
                    {
                        Point ss = new Point();
                        ss.X = hangXianMinX + Data.fxcs[1] * i;// 1航线间距
                        ss.Y = hangXianMinY + Data.fxcs[2] * j;// 2触发距离
                        ss.Z = Data.fxcs[0] + Data.fxcs[3];
                        if (Buffer.withIn(minOutRec, ss))
                            tem.Add(ss);
                    }
                }
                return tem.ToArray();
            }
        }

        //航线
        public static LineD[] hxLine(Point[] minOutRec)
        {
            //读取进来的轮廓线,把点坐标赋值给一对数组供计算
            int pointCornt = minOutRec.Length;
            double[] X = new double[pointCornt];
            double[] Y = new double[pointCornt];
            for (int i = 0; i < pointCornt; i++)
            {
                X[i] = minOutRec[i].X;
                Y[i] = minOutRec[i].Y;
            }
            //计算过程
            double centerX = (Xuanzhuan.myMax(X) + Xuanzhuan.myMin(X)) / 2;
            double centerY = (Xuanzhuan.myMax(Y) + Xuanzhuan.myMin(Y)) / 2;
            double tiao, cFcishu, hangXianMinX, hangXianMaxX, hangXianMinY, hangXianMaxY;
            //判断方向,拿到航线条数,和每条航线的触发次数
            if (Xuanzhuan.myMax(X) - Xuanzhuan.myMin(X) >= Xuanzhuan.myMax(Y) - Xuanzhuan.myMin(Y))//横的
            {
                tiao = Math.Ceiling((Xuanzhuan.myMax(Y) - Xuanzhuan.myMin(Y)) / Data.fxcs[1]) + 1;
                cFcishu = Math.Ceiling((Xuanzhuan.myMax(X) - Xuanzhuan.myMin(X)) / Data.fxcs[2]) + 1;
            }
            else//竖的
            {
                tiao = Math.Ceiling((Xuanzhuan.myMax(X) - Xuanzhuan.myMin(X)) / Data.fxcs[1]) + 1;
                cFcishu = Math.Ceiling((Xuanzhuan.myMax(Y) - Xuanzhuan.myMin(Y)) / Data.fxcs[2]) + 1;
            }
            //计算结果并返回
            List<LineD> res = new List<LineD>();
            if (Xuanzhuan.myMax(X) - Xuanzhuan.myMin(X) >= Xuanzhuan.myMax(Y) - Xuanzhuan.myMin(Y))
            //横的
            {
                //航线最大最小XY
                hangXianMinX = centerX - cFcishu / 2 * Data.fxcs[2];
                hangXianMaxX = centerX + cFcishu / 2 * Data.fxcs[2];
                hangXianMinY = centerY - tiao / 2 * Data.fxcs[1];
                //循环创建每一条航线
                for (int i = 0; i < tiao; i++)
                {
                    //线段sss 接收过程航线
                    LineD sss = new LineD();
                    sss.Start.X = hangXianMinX;
                    sss.Start.Y = hangXianMinY + Data.fxcs[1] * i;
                    sss.Start.Z = Data.fxcs[0] + Data.fxcs[3];
                    sss.End.X = hangXianMaxX;
                    sss.End.Y = hangXianMinY + Data.fxcs[1] * i;
                    sss.End.Z = Data.fxcs[0] + Data.fxcs[3];
                    //获得区域内的线
                    LineD cutLine = Buffer.cutLine(minOutRec, sss);
                    //把这条航线加到List中
                    res.Add(cutLine);
                }
                return res.ToArray();
            }
            else//竖的
            {
                //航线最大最小XY
                hangXianMinX = centerX - tiao / 2 * Data.fxcs[1];
                hangXianMinY = centerY - cFcishu / 2 * Data.fxcs[2];
                hangXianMaxY = centerY + cFcishu / 2 * Data.fxcs[2];
                //循环创建每一条航线
                for (int i = 0; i < tiao; i++)
                {
                    //线段sss 接收过程航线
                    LineD sss = new LineD();
                    sss.Start.X = hangXianMinX + Data.fxcs[1] * i;
                    sss.Start.Y = hangXianMinY;
                    sss.Start.Z = Data.fxcs[0] + Data.fxcs[3];
                    sss.End.X = hangXianMinX + Data.fxcs[1] * i;
                    sss.End.Y = hangXianMaxY;
                    sss.End.Z = Data.fxcs[0] + Data.fxcs[3];
                    LineD cutLine = Buffer.cutLine(minOutRec, sss);
                    //把这条航线加到List中
                    if (cutLine.Start.X == 0 && cutLine.Start.Y == 0 && cutLine.End.X == 0 && cutLine.End.Y == 0)
                    { continue; }
                    else
                    { res.Add(cutLine); }
                }
                return res.ToArray();
            }
        }
    
        //subCamera
        public static List<Point[]> subCenter(Point[] cameraPoint)
        {
            double Nor = Data.fxcs[0] * 0.1;
            double moX = (18.0 / Data.jiaoju) * Nor;//X偏移量 //Nor 放样距离
            double moY = (12.0 / Data.jiaoju) * Nor;//Y偏移量
            //参考点
            
            //记录结果
            List<Point[]> a = new List<Point[]>();
            //Point[,] b = new Point[a.Length, 5];//b[多边形数(也就是相机点数),四角点(5)]
            for (int i = 0; i < cameraPoint.Length; i++)
            {
                Point b = cameraPoint[i];
                Point[] c = new Point[5];
                c[0].X = b.X - moX;
                c[0].Y = b.Y + moY;
                c[0].Z = b.Z - Nor;

                c[1].X = b.X - moX;
                c[1].Y = b.Y - moY;
                c[1].Z = b.Z - Nor;

                c[2].X = b.X + moX;
                c[2].Y = b.Y - moY;
                c[2].Z = b.Z - Nor;

                c[3].X = b.X + moX;
                c[3].Y = b.Y + moY;
                c[3].Z = b.Z - Nor;

                c[4].X = b.X - moX;
                c[4].Y = b.Y + moY;
                c[4].Z = b.Z - Nor;
                a.Add(c);
            }
            return a;
        }
        public static List<Point[]> subLeft(List<Point[]> subCenter)
        {
            List<Point[]> c = new List<Point[]>();
            for (int i = 0; i < subCenter.Count; i++)
            {
                Point[] a = subCenter[i];
                Point[] b = new Point[a.Length];
                for (int j = 0; j < a.Length; j++)
                {
                    b[j] = Xuanzhuan.resP3D(Data.tempSub[i], a[j], -30, "XZ");
                }
                c.Add(b);
            }
            return c;
        }
        public static List<Point[]> subRight(List<Point[]> subCenter)
        {
            List<Point[]> c = new List<Point[]>();
            for (int i = 0; i < subCenter.Count; i++)
            {
                Point[] a = subCenter[i];
                Point[] b = new Point[a.Length];
                for (int j = 0; j < a.Length; j++)
                {
                    b[j] = Xuanzhuan.resP3D(Data.tempSub[i], a[j], 30, "XZ");
                }
                c.Add(b);
            }
            return c;
        }
        public static List<Point[]> subFront(List<Point[]> subCenter)
        {
            List<Point[]> c = new List<Point[]>();
            for (int i = 0; i < subCenter.Count; i++)
            {
                Point[] a = subCenter[i];
                Point[] b = new Point[a.Length];
                for (int j = 0; j < a.Length; j++)
                {
                    b[j] = Xuanzhuan.resP3D(Data.tempSub[i], a[j], 30, "YZ");
                }
                c.Add(b);
            }
            return c;
        }
        public static List<Point[]> subBack(List<Point[]> subCenter)
        {
            List<Point[]> c = new List<Point[]>();
            for (int i = 0; i < subCenter.Count; i++)
            {
                Point[] a = subCenter[i];
                Point[] b = new Point[a.Length];
                for (int j = 0; j < a.Length; j++)
                {
                    b[j] = Xuanzhuan.resP3D(Data.tempSub[i], a[j], -30, "YZ");
                }
                c.Add(b);
            }
            return c;
        }
    }
}
