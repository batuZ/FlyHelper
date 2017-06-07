using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 采集方案参数.功能区
{
    class CoorT
    {
        private static double[] wgs84ToMercator(double[] wgsPoint)
        {
            OSGeo.OSR.SpatialReference wgs84Spat = new OSGeo.OSR.SpatialReference
                ("GEOGCS[\"GCS_WGS_1984\",DATUM[\"WGS_1984\",SPHEROID[\"WGS_84\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]]");
            OSGeo.OSR.SpatialReference MercatorSpat = new OSGeo.OSR.SpatialReference
                ("PROJCS[\"WGS_1984_Web_Mercator\",GEOGCS[\"GCS_WGS_1984_Major_Auxiliary_Sphere\",DATUM[\"WGS_1984_Major_Auxiliary_Sphere\",SPHEROID[\"WGS_1984_Major_Auxiliary_Sphere\",6378137.0,0.0]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Mercator_1SP\"],PARAMETER[\"False_Easting\",0.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",0.0],PARAMETER[\"latitude_of_origin\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",\"3785\"]]");
            OSGeo.OSR.CoordinateTransformation coorTr = new OSGeo.OSR.CoordinateTransformation(wgs84Spat, MercatorSpat);
            coorTr.TransformPoint(wgsPoint);
            double[] MercatorPoint = new double[wgsPoint.Length];
            MercatorPoint = wgsPoint;
            return MercatorPoint;
        }
        private static double[] mercatorToWgs84(double[] mercatorPoint)
        {
            OSGeo.OSR.SpatialReference wgs84Spat = new OSGeo.OSR.SpatialReference
                   ("GEOGCS[\"GCS_WGS_1984\",DATUM[\"WGS_1984\",SPHEROID[\"WGS_84\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]]");
            OSGeo.OSR.SpatialReference MercatorSpat = new OSGeo.OSR.SpatialReference
                ("PROJCS[\"WGS_1984_Web_Mercator\",GEOGCS[\"GCS_WGS_1984_Major_Auxiliary_Sphere\",DATUM[\"WGS_1984_Major_Auxiliary_Sphere\",SPHEROID[\"WGS_1984_Major_Auxiliary_Sphere\",6378137.0,0.0]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Mercator_1SP\"],PARAMETER[\"False_Easting\",0.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",0.0],PARAMETER[\"latitude_of_origin\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",\"3785\"]]");
            OSGeo.OSR.CoordinateTransformation coorTr = new OSGeo.OSR.CoordinateTransformation(MercatorSpat, wgs84Spat);
            coorTr.TransformPoint(mercatorPoint);
            double[] wgsPoint = new double[mercatorPoint.Length];
            wgsPoint = mercatorPoint;
            return wgsPoint;
        }

        /// <summary>
        /// 进来的数据如果的WGS84的就转成平面坐标
        /// </summary>
        /// <param name="oriPoint"></param>
        /// <returns></returns>
        public static Point[] wgs84ToMercator(Point[] oriPoint)
        {
            Point[] tem = new Point[oriPoint.Length];
            for (int j = 0; j < oriPoint.Length; j++)
            {
                double[] temp = { oriPoint[j].X, oriPoint[j].Y, oriPoint[j].Z };
                CoorT.wgs84ToMercator(temp);
                tem[j].X = temp[0];
                tem[j].Y = temp[1];
                tem[j].Z = temp[2];
            }
            return tem;
        }
        /// <summary>
        /// 用于还原坐标系
        /// </summary>
        /// <param name="rePro"></param>
        /// <returns></returns>
        public static Point[] MercatorToWgs84(Point[] rePro)
        {
            Point[] tem = new Point[rePro.Length];
            for (int j = 0; j < rePro.Length; j++)
            {
                double[] temp = { rePro[j].X, rePro[j].Y, rePro[j].Z };
                CoorT.mercatorToWgs84(temp);
                tem[j].X = temp[0];
                tem[j].Y = temp[1];
                tem[j].Z = temp[2];
            }
            return tem;
        }
        public static LineD[] MercatorToWgs84(LineD[] rePro)
        {
            LineD[] tem = new LineD[rePro.Length];
            for (int j = 0; j < rePro.Length; j++)
            {
                double[] tempS = { rePro[j].Start.X, rePro[j].Start.Y, rePro[j].Start.Z };
                double[] tempE = { rePro[j].End.X, rePro[j].End.Y, rePro[j].End.Z };
                CoorT.mercatorToWgs84(tempS);
                CoorT.mercatorToWgs84(tempE);
                tem[j].Start.X = tempS[0];
                tem[j].Start.Y = tempS[1];
                tem[j].Start.Z = tempS[2];
                tem[j].End.X = tempE[0];
                tem[j].End.Y = tempE[1];
                tem[j].End.Z = tempE[2];
            }
            return tem;
        }
    }

}
