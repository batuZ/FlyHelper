using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 采集方案参数.功能区
{
    class OutPutFile
    {
        public static void saveFile()
        {
            if (Data.savePath.IndexOf(".kml") > -1)
            {
                Kmlfile();
            }
            else if (Data.savePath.IndexOf(".shp") > -1)
            {
                shpFile();
            }
            else if (Data.savePath.IndexOf(".txt") > -1)
            {
                txtFileL();
            }
            else
            {
                awmFile();
            }
        }

        static void Kmlfile()
        {
            string All = KmlSet.all();
            System.IO.StreamWriter file = new System.IO.StreamWriter(Data.savePath);
            file.Write(All);
            file.Close();
        }

        static void shpFile()
        {
            

            string path = Data.savePath.Substring(0, Data.savePath.LastIndexOf("."));
            string fileName = Data.savePath.Substring(Data.savePath.LastIndexOf("."));
            OSGeo.OGR.Ogr.RegisterAll();
            OSGeo.OGR.Driver dr = OSGeo.OGR.Ogr.GetDriverByName("ESRI shapefile");

            if (Data.相机点 != null)
            {
                OSGeo.OGR.DataSource dsCamera = dr.CreateDataSource(path + "_Point" + fileName, null);
                OSGeo.OGR.Layer camLayer = dsCamera.CreateLayer("cameras", null, OSGeo.OGR.wkbGeometryType.wkbPoint25D, null);
                Point[] temp = Data.相机点;
                OSGeo.OGR.FeatureDefn newFeatDf = new OSGeo.OGR.FeatureDefn("camer");
                for (int i = 0; i < Data.相机点.Length; i++)
                {
                    OSGeo.OGR.Feature camFeat = new OSGeo.OGR.Feature(newFeatDf);
                    OSGeo.OGR.Geometry camGeom = new OSGeo.OGR.Geometry(OSGeo.OGR.wkbGeometryType.wkbPoint25D);
                    camGeom.AddPoint(temp[i].X, temp[i].Y, temp[i].Z);
                    camFeat.SetGeometry(camGeom);
                    camLayer.CreateFeature(camFeat);
                }
                camLayer.Dispose();
                dsCamera.Dispose();
            }

            if (Data.航线 != null)
            {
                OSGeo.OGR.DataSource dsLines = dr.CreateDataSource(path + "_Line" + fileName, null);
                OSGeo.OGR.Layer lineLayer = dsLines.CreateLayer("Lines", null, OSGeo.OGR.wkbGeometryType.wkbLineString25D, null);
                LineD[] tempHX = Data.航线;
                OSGeo.OGR.FeatureDefn newFeat = new OSGeo.OGR.FeatureDefn("lines");///j还有问题 
                for (int j = 0; j < Data.航线.Length; j++)
                {
                    OSGeo.OGR.Feature lineFeat = new OSGeo.OGR.Feature(newFeat);
                    OSGeo.OGR.Geometry lineGeom = new OSGeo.OGR.Geometry(OSGeo.OGR.wkbGeometryType.wkbLineString25D);
                    lineGeom.AddPoint(tempHX[j].Start.X, tempHX[j].Start.Y, tempHX[j].Start.Z);
                    lineGeom.AddPoint(tempHX[j].End.X, tempHX[j].End.Y, tempHX[j].End.Z);
                    lineFeat.SetGeometry(lineGeom);
                    lineLayer.CreateFeature(lineFeat);
                }
                lineLayer.Dispose();
                dsLines.Dispose();
            }

            if (Data.Buffer范围 != null)
            {
                OSGeo.OGR.DataSource dsPoly = dr.CreateDataSource(path + "_Polygon" + fileName, null);
                OSGeo.OGR.Layer polyLayer = dsPoly.CreateLayer("fanwei", null, OSGeo.OGR.wkbGeometryType.wkbPolygon25D, null);
                Point[] tempFW = Data.Buffer范围;
                OSGeo.OGR.FeatureDefn polyFeatDf = new OSGeo.OGR.FeatureDefn("测区");
                OSGeo.OGR.Feature polyFeat = new OSGeo.OGR.Feature(polyFeatDf);
                OSGeo.OGR.Geometry polyGeom = new OSGeo.OGR.Geometry(OSGeo.OGR.wkbGeometryType.wkbPolygon25D);
                OSGeo.OGR.Geometry subGeom = new OSGeo.OGR.Geometry(OSGeo.OGR.wkbGeometryType.wkbLinearRing);
                for (int j = 0; j < tempFW.Length; j++)
                {
                    subGeom.AddPoint(tempFW[j].X, tempFW[j].Y, tempFW[j].Z);
                }
                polyGeom.AddGeometry(subGeom);
                polyFeat.SetGeometry(polyGeom);
                polyLayer.CreateFeature(polyFeat);
                polyLayer.Dispose();
                dsPoly.Dispose();
            }

            if (Data.subCenter != null)
            {
                List<Point[]> all = new List<Point[]>();
                all.AddRange(Data.subCenter);
                all.AddRange(Data.subLeft);
                all.AddRange(Data.subRight);
                all.AddRange(Data.subFront);
                all.AddRange(Data.subBack);
                OSGeo.OGR.DataSource dssub = dr.CreateDataSource(path + "_subCameras" + fileName, null);
                OSGeo.OGR.Layer subLayer = dssub.CreateLayer("subCamera", null, OSGeo.OGR.wkbGeometryType.wkbPolygon25D, null);
                OSGeo.OGR.FeatureDefn subFeatDf = new OSGeo.OGR.FeatureDefn("分相机");
                for (int count = 0; count < all.Count; count++)
                {
                    Point[] poly = all[count];
                    OSGeo.OGR.Feature subFeat = new OSGeo.OGR.Feature(subFeatDf);
                    OSGeo.OGR.Geometry suGeom = new OSGeo.OGR.Geometry(OSGeo.OGR.wkbGeometryType.wkbPolygon25D);
                    OSGeo.OGR.Geometry subbGeom = new OSGeo.OGR.Geometry(OSGeo.OGR.wkbGeometryType.wkbLinearRing);
                    for (int i = 0; i < poly.Length; i++)
                    {
                        subbGeom.AddPoint(poly[i].X, poly[i].Y, poly[i].Z);
                    }
                    suGeom.AddGeometry(subbGeom);
                    subFeat.SetGeometry(suGeom);
                    subLayer.CreateFeature(subFeat);
                    suGeom.Empty();
                    subFeat.Dispose();
                }
                subLayer.Dispose();
                dssub.Dispose();
            }
        }

        static void txtFileL()
        {
            string all = "QGC WPL 110";
            for (int i = 0, j = 0; i < Data.航线.Length; i++, j += 2)
            {
                LineD a = Data.航线[i];
                string pointId = null;
                string weiDu = null;
                string jingDu = null;
                string gaoDu = null;
                string aPoint = null;
                if (i % 2 == 0)
                {
                    pointId = j.ToString();
                    weiDu = a.Start.Y.ToString();
                    jingDu = a.Start.X.ToString();
                    gaoDu = a.Start.Z.ToString();
                    aPoint = string.Format(@"
{0}	0	10	16	0.000000	0.000000	0.000000	0.000000	{1}	{2}	{3}	1", pointId, weiDu, jingDu, gaoDu);
                    all += aPoint;

                    pointId = (j + 1).ToString();
                    weiDu = a.End.Y.ToString();
                    jingDu = a.End.X.ToString();
                    gaoDu = a.End.Z.ToString();
                    aPoint = string.Format(@"
{0}	0	10	16	0.000000	0.000000	0.000000	0.000000	{1}	{2}	{3}	1", pointId, weiDu, jingDu, gaoDu);
                    all += aPoint;
                }
                else
                {
                    pointId = j.ToString();
                    weiDu = a.End.Y.ToString();
                    jingDu = a.End.X.ToString();
                    gaoDu = a.End.Z.ToString();
                    aPoint = string.Format(@"
{0}	0	10	16	0.000000	0.000000	0.000000	0.000000	{1}	{2}	{3}	1", pointId, weiDu, jingDu, gaoDu);
                    all += aPoint;

                    pointId = (j + 1).ToString();
                    weiDu = a.Start.Y.ToString();
                    jingDu = a.Start.X.ToString();
                    gaoDu = a.Start.Z.ToString();
                    aPoint = string.Format(@"
{0}	0	10	16	0.000000	0.000000	0.000000	0.000000	{1}	{2}	{3}	1", pointId, weiDu, jingDu, gaoDu);
                    all += aPoint;
                }
            }
            System.IO.File.WriteAllText(Data.savePath, all, Encoding.UTF8);
        }

        static void txtFileP()
        {
            string all = "QGC WPL 110";
            for (int i = 0; i < Data.相机点.Length; i++)
            {
                Point a = Data.相机点[i];
                string pointId = i.ToString();
                string weiDu = a.Y.ToString();
                string jingDu = a.X.ToString();
                string gaoDu = a.Z.ToString();
                string aPoint = string.Format(@"
{0}	0	10	16	0.000000	0.000000	0.000000	0.000000	{1}	{2}	{3}	1", pointId, weiDu, jingDu, gaoDu);
                all += aPoint;
            }
            System.IO.File.WriteAllText(Data.savePath, all, Encoding.UTF8);
        }

        static void awmFile()
        {
            string all = @"<?xml version=""1.0"" encoding=""utf-16"" standalone=""yes""?>
<Mission MissionTimeLmt=""65535"" IsPatrol=""Continuous"" StartWayPointIndex=""0"" VerticalSpeedLimit=""1.5"">";
            for (int i = 0, j = 0; i < Data.航线.Length; i++, j += 2)
            {
                LineD a = Data.航线[i];
                string pointId = null;
                string weiDu = null;
                string jingDu = null;
                string gaoDu = null;
                string aPoint = null;
                if (i % 2 == 0)
                {
                    pointId = j.ToString();
                    weiDu = a.Start.Y.ToString();
                    jingDu = a.Start.X.ToString();
                    gaoDu = a.Start.Z.ToString();
                    aPoint = string.Format(@"
  <WayPoint id=""{0}"">
    <Latitude>{1}</Latitude>
    <Longitude>{2}</Longitude>
    <Altitude>{3}</Altitude>
    <Speed>4</Speed>
    <TimeLimit>36000</TimeLimit>
    <YawDegree>360</YawDegree>
    <HoldTime>3</HoldTime>
    <StartDelay>0</StartDelay>
    <Period>0</Period>
    <RepeatTime>0</RepeatTime>
    <RepeatDistance>0</RepeatDistance>
    <TurnMode>StopAndTurn</TurnMode>
  </WayPoint>", pointId, weiDu, jingDu, gaoDu);
                    all += aPoint;

                    pointId = (j + 1).ToString();
                    weiDu = a.End.Y.ToString();
                    jingDu = a.End.X.ToString();
                    gaoDu = a.End.Z.ToString();
                    aPoint = string.Format(@"
  <WayPoint id=""{0}"">
    <Latitude>{1}</Latitude>
    <Longitude>{2}</Longitude>
    <Altitude>{3}</Altitude>
    <Speed>4</Speed>
    <TimeLimit>36000</TimeLimit>
    <YawDegree>360</YawDegree>
    <HoldTime>3</HoldTime>
    <StartDelay>0</StartDelay>
    <Period>0</Period>
    <RepeatTime>0</RepeatTime>
    <RepeatDistance>0</RepeatDistance>
    <TurnMode>StopAndTurn</TurnMode>
  </WayPoint>", pointId, weiDu, jingDu, gaoDu);
                    all += aPoint;
                }
                else
                {
                    pointId = j.ToString();
                    weiDu = a.End.Y.ToString();
                    jingDu = a.End.X.ToString();
                    gaoDu = a.End.Z.ToString();
                    aPoint = string.Format(@"
  <WayPoint id=""{0}"">
    <Latitude>{1}</Latitude>
    <Longitude>{2}</Longitude>
    <Altitude>{3}</Altitude>
    <Speed>4</Speed>
    <TimeLimit>36000</TimeLimit>
    <YawDegree>360</YawDegree>
    <HoldTime>3</HoldTime>
    <StartDelay>0</StartDelay>
    <Period>0</Period>
    <RepeatTime>0</RepeatTime>
    <RepeatDistance>0</RepeatDistance>
    <TurnMode>StopAndTurn</TurnMode>
  </WayPoint>", pointId, weiDu, jingDu, gaoDu);
                    all += aPoint;

                    pointId = (j + 1).ToString();
                    weiDu = a.Start.Y.ToString();
                    jingDu = a.Start.X.ToString();
                    gaoDu = a.Start.Z.ToString();
                    aPoint = string.Format(@"
  <WayPoint id=""{0}"">
    <Latitude>{1}</Latitude>
    <Longitude>{2}</Longitude>
    <Altitude>{3}</Altitude>
    <Speed>4</Speed>
    <TimeLimit>36000</TimeLimit>
    <YawDegree>360</YawDegree>
    <HoldTime>3</HoldTime>
    <StartDelay>0</StartDelay>
    <Period>0</Period>
    <RepeatTime>0</RepeatTime>
    <RepeatDistance>0</RepeatDistance>
    <TurnMode>StopAndTurn</TurnMode>
  </WayPoint>", pointId, weiDu, jingDu, gaoDu);
                    all += aPoint;
                }
            }
            string end = all + @"
</Mission>";

            System.IO.File.WriteAllText(Data.savePath, end, Encoding.UTF8);
        }

    }
}
