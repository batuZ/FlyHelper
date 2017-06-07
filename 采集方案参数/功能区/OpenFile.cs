using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 采集方案参数.功能区
{
    class OpenFile
    {
        public static Point[] openToPoint(string filePath)
        {
            //shp or kml
            OSGeo.OGR.Ogr.RegisterAll();
            OSGeo.OGR.DataSource fileDs = null;
            if (filePath.IndexOf(".shp") > -1)
            {
                OSGeo.OGR.Driver shpDr = OSGeo.OGR.Ogr.GetDriverByName("ESRI shapefile");
                fileDs = shpDr.Open(filePath, 0);
                OSGeo.OGR.Layer fileLayer = fileDs.GetLayerByIndex(0);
                OSGeo.OGR.Feature fileFeat = fileLayer.GetFeature(0);
                OSGeo.OGR.Geometry filgGeom = fileFeat.GetGeometryRef();
                OSGeo.OGR.Geometry subGeom = filgGeom.GetGeometryRef(0);
                //file to point[]
                Point[] temp = new Point[subGeom.GetPointCount()];
                for (int i = 0, j = subGeom.GetPointCount() - 1; i < subGeom.GetPointCount(); i++, j--)
                {
                    temp[i].X = subGeom.GetX(j);
                    temp[i].Y = subGeom.GetY(j);
                    temp[i].Z = subGeom.GetZ(j);
                }
                //send Spat to Data
                Data.oriSpat = fileLayer.GetSpatialRef();
                return temp;
            }
            else //if (filePath.IndexOf(".kml") > -1)
            {
                OSGeo.OGR.Driver kmlDr = OSGeo.OGR.Ogr.GetDriverByName("KML");
                fileDs = kmlDr.Open(filePath, 0);
                OSGeo.OGR.Layer fileLayer = fileDs.GetLayerByIndex(0);
                OSGeo.OGR.Feature fileFeat = fileLayer.GetFeature(0);
                OSGeo.OGR.Geometry filgGeom = fileFeat.GetGeometryRef();
                OSGeo.OGR.Geometry subGeom = filgGeom.GetGeometryRef(0);
                //file to point[]
                Point[] temp = new Point[subGeom.GetPointCount()];
                for (int i = 0; i < subGeom.GetPointCount(); i++)
                {
                    temp[i].X = subGeom.GetX(i);
                    temp[i].Y = subGeom.GetY(i);
                    temp[i].Z = subGeom.GetZ(i);
                }
                //send Spat to Data
                Data.oriSpat = fileLayer.GetSpatialRef();
                return temp;
            }
        }
        public static bool IsWGS(Point[] oriPoint)
        { 
         string filePrj = null;
            if (Data.oriSpat != null)
            {
                Data.oriSpat.ExportToWkt(out filePrj);
            }
            else if (Math.Abs(Xuanzhuan.auePOINT(oriPoint, "X")) < 180 && Math.Abs(Xuanzhuan.auePOINT(oriPoint, "Y")) < 90)
            {
                filePrj = "WGS84";
            }
            else
            {
                filePrj = "Meter";
            }
                if (filePrj.IndexOf("Meter") > -1)
                {
                    return false;
                }
                else
                    return true ;
        }
    }
}
