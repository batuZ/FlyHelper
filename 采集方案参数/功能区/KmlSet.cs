using System;
using System.Collections.Generic;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace 采集方案参数.功能区
{
    class KmlSet
    {
        public static string all()
        {

            string saveN = Data.savePath.Substring(Data.savePath.LastIndexOf ("\\")+1);
            #region fileBegin
            string fileBegin = string.Format(@"<?xml version=""1.0"" encoding=""UTF-8""?>
<kml xmlns=""http://www.opengis.net/kml/2.2"" xmlns:gx=""http://www.google.com/kml/ext/2.2"" xmlns:kml=""http://www.opengis.net/kml/2.2"" xmlns:atom=""http://www.w3.org/2005/Atom"">
<Document>
	<name>{0}</name>
	
	#msn_camera   camera007
	<StyleMap id=""msn_camera"">								
		<Pair>
			<key>normal</key>
			<styleUrl>#sn_camera</styleUrl>
		</Pair>
		<Pair>
			<key>highlight</key>
			<styleUrl>#sh_camera</styleUrl>
		</Pair>
	</StyleMap>
			<Style id=""sn_camera"">
				<IconStyle>
					<color>ff00ff00</color>
					<scale>0.6</scale>
					<Icon>
						<href>http://maps.google.com/mapfiles/kml/shapes/camera.png</href>
					</Icon>
					<hotSpot x=""0.5"" y=""0"" xunits=""fraction"" yunits=""fraction""/>
				</IconStyle>
				<LabelStyle>
					<color>00ffffff</color>
					<scale>0.1</scale>
				</LabelStyle>
				<ListStyle>
				</ListStyle>
				<LineStyle>
					<color>7f00ff00</color>
					<width>0.9</width>
				</LineStyle>
			</Style>
			<Style id=""sh_camera"">
				<IconStyle>
					<color>ff00ff00</color>
					<scale>0.709091</scale>
					<Icon>
						<href>http://maps.google.com/mapfiles/kml/shapes/camera.png</href>
					</Icon>
					<hotSpot x=""0.5"" y=""0"" xunits=""fraction"" yunits=""fraction""/>
				</IconStyle>
				<LabelStyle>
					<color>00ffffff</color>
					<scale>0.1</scale>
				</LabelStyle>
				<ListStyle>
				</ListStyle>
				<LineStyle>
					<color>7f00ff00</color>
					<width>0.9</width>
				</LineStyle>
			</Style>


  	#m_ylw-pushpin   line003
	<StyleMap id=""m_ylw-pushpin"">
		<Pair>
			<key>normal</key>
			<styleUrl>#s_ylw-pushpin</styleUrl>
		</Pair>
		<Pair>
			<key>highlight</key>
			<styleUrl>#s_ylw-pushpin_hl</styleUrl>
		</Pair>
	</StyleMap>
			<Style id=""s_ylw-pushpin"">
				<IconStyle>
					<scale>1.1</scale>
					<Icon>
						<href>http://maps.google.com/mapfiles/kml/pushpin/ylw-pushpin.png</href>
					</Icon>
					<hotSpot x=""20"" y=""2"" xunits=""pixels"" yunits=""pixels""/>
				</IconStyle>
				<LineStyle>
					<color>ffffaa00</color>
					<width>2.1</width>
				</LineStyle>
			</Style>
			<Style id=""s_ylw-pushpin_hl"">
				<IconStyle>
					<scale>1.3</scale>
					<Icon>
						<href>http://maps.google.com/mapfiles/kml/pushpin/ylw-pushpin.png</href>
					</Icon>
					<hotSpot x=""20"" y=""2"" xunits=""pixels"" yunits=""pixels""/>
				</IconStyle>
				<LineStyle>
					<color>ffffaa00</color>
					<width>2.1</width>
				</LineStyle>
			</Style>
	
	
	
	#msn_ylw-pushpin  center001
	<StyleMap id=""msn_ylw-pushpin"">
		<Pair>
			<key>normal</key>
			<styleUrl>#sn_ylw-pushpin</styleUrl>
		</Pair>
		<Pair>
			<key>highlight</key>
			<styleUrl>#sh_ylw-pushpin</styleUrl>
		</Pair>
	</StyleMap>
			<Style id=""sn_ylw-pushpin"">
				<IconStyle>
					<scale>1.1</scale>
					<Icon>
						<href>http://maps.google.com/mapfiles/kml/pushpin/ylw-pushpin.png</href>
					</Icon>
					<hotSpot x=""20"" y=""2"" xunits=""pixels"" yunits=""pixels""/>
				</IconStyle>
				<LineStyle>
					<color>ff00aa00</color>
					<width>1.1</width>
				</LineStyle>
				<PolyStyle>
					<color>3300ff00</color>
				</PolyStyle>
			</Style>
			<Style id=""sh_ylw-pushpin"">
				<IconStyle>
					<scale>1.3</scale>
					<Icon>
						<href>http://maps.google.com/mapfiles/kml/pushpin/ylw-pushpin.png</href>
					</Icon>
					<hotSpot x=""20"" y=""2"" xunits=""pixels"" yunits=""pixels""/>
				</IconStyle>
				<LineStyle>
					<color>ff00aa00</color>
					<width>1.1</width>
				</LineStyle>
				<PolyStyle>
					<color>3300ff00</color>
				</PolyStyle>
			</Style>


			#msn_ylw-pushpin0   范围
	<StyleMap id=""msn_ylw-pushpin0"">
		<Pair>
			<key>normal</key>
			<styleUrl>#sn_ylw-pushpin1</styleUrl>
		</Pair>
		<Pair>
			<key>highlight</key>
			<styleUrl>#sh_ylw-pushpin1</styleUrl>
		</Pair>
	</StyleMap>
			<Style id=""sn_ylw-pushpin1"">
				<IconStyle>
					<scale>1.1</scale>
					<Icon>
						<href>http://maps.google.com/mapfiles/kml/pushpin/ylw-pushpin.png</href>
					</Icon>
					<hotSpot x=""20"" y=""2"" xunits=""pixels"" yunits=""pixels""/>
				</IconStyle>
				<LineStyle>
					<color>ff0000ff</color>
					<width>2.2</width>
				</LineStyle>
				<PolyStyle>
					<color>3300ffff</color>
				</PolyStyle>
			</Style>
			<Style id=""sh_ylw-pushpin1"">
				<IconStyle>
					<scale>1.3</scale>
					<Icon>
						<href>http://maps.google.com/mapfiles/kml/pushpin/ylw-pushpin.png</href>
					</Icon>
					<hotSpot x=""20"" y=""2"" xunits=""pixels"" yunits=""pixels""/>
				</IconStyle>
				<LineStyle>
					<color>ff0000ff</color>
					<width>2.2</width>
				</LineStyle>
				<PolyStyle>
					<color>3300ffff</color>
				</PolyStyle>
			</Style>
", saveN);
            #endregion

            string shuoMing = shuoMingS();
            
            string cameras = camerasS();
            string lines = linesS();
            string polygons = polygonsS();
            string subCameras = subCameraS();
            #region fileBudy
            string fileBudy = string.Format(@"
	<Folder>
		<name>飞行方案</name>
		<open>1</open>
		<description>{0}</description>
		<Folder>
			<name>Cameras</name>
			{1}
		</Folder>
		<Folder>
			<name>Lines</name>
			{2}
		</Folder>
		<Folder>
			<name>Fanwei</name>
			{3}
		</Folder>
            {4}
	</Folder>
</Document>
</kml>
", shuoMing, cameras, lines, polygons, subCameras);
            #endregion

            return fileBegin + fileBudy;
        }
        private static string shuoMingS()
        {
            var mianji = (Data.fxcs[4]*0.000001).ToString("0.00") + "平方千米";
            var gaodu = Data.fxcs[0].ToString("0.0") + "米";
            var jianju = Data.fxcs[1].ToString("0.0") + "米";
            var chufa = Data.fxcs[2].ToString("0.0") + "米";
            var zongchang = (Data.fxcs[5]*0.001).ToString("0.00") + "千米";
            var zongzhaopian = Data.fxcs[6].ToString() + " X5 张";
            string jieshao = string.Format(@"
面积:     {0}
飞行高度: {1}
航线间距: {2}
触发距离: {3}
航线总长: {4}
照片总量: {5}
", mianji, gaodu, jianju, chufa, zongchang, zongzhaopian);
            return jieshao;
        }
        private static string camerasS()
        {
            if (Data.相机点 == null)
            { return ""; }

            string cameras = null;
            for (int i = 0; i < Data.相机点.Length; i++)
            {
                Point point = Data.相机点[i];
                cameras += string.Format(@"<Placemark>
				<name>camera{0}</name>
				<LookAt>
					<longitude>{1}</longitude>
					<latitude>{2}</latitude>
					<altitude>0</altitude>
					<heading>0</heading>
					<tilt>0</tilt>
					<range>{3}</range>
					<gx:altitudeMode>relativeToSeaFloor</gx:altitudeMode>
				</LookAt>
				<styleUrl>#msn_camera</styleUrl>
				<Point>
					<extrude>1</extrude>
					<altitudeMode>absolute</altitudeMode>
					<gx:drawOrder>1</gx:drawOrder>
					<coordinates>{1},{2},{3}</coordinates>
				</Point>
			</Placemark>
", i, point.X, point.Y, point.Z);
            }
            return cameras;
        }
        private static string linesS()
        {
            if (Data.航线 == null)
            { return ""; }
            string lines = null;
            for (int i = 0; i < Data.航线.Length; i++)
            {
                LineD li = Data.航线[i];
                lines += string.Format(@"<Placemark>
				<name>line{0}</name>
				<styleUrl>#m_ylw-pushpin</styleUrl>
				<LineString>
					<tessellate>1</tessellate>
					<altitudeMode>absolute</altitudeMode>
					<coordinates>
						{1},{2},{3} {4},{5},{6} 
					</coordinates>
				</LineString>
			</Placemark>
", i, li.Start.X, li.Start.Y, li.Start.Z, li.End.X, li.End.Y, li.End.Z);
            }
            return lines;
        }
        private static string polygonsS()
        {
            if (Data.Buffer范围 == null)
            { return ""; }
            string coor = null;
            for (int i = 0; i < Data.Buffer范围.Length; i++)
            { 
                string x = Data.Buffer范围[i].X.ToString();
                string y = Data.Buffer范围[i].Y.ToString();
                string z=  Data.Buffer范围[i].Z.ToString();
                coor += x + "," + y + "," + z + "\t";
            }
            string polygons =string.Format( @"			<Placemark>
				<name>范围</name>
				<styleUrl>#msn_ylw-pushpin0</styleUrl>
				<Polygon>
					<tessellate>1</tessellate>
					<outerBoundaryIs>
						<LinearRing>
							<coordinates>
                                {0}
                            </coordinates>
						</LinearRing>
					</outerBoundaryIs>
				</Polygon>
			</Placemark>
",coor);
            return polygons;
        }
        private static string subCameraS()
        {
            if (Data.subCenter == null)
            { return ""; }
            string subCenter = subCenterS();
            string subLeft = subLeftS();
            string subRight = subRightS();
            string subFront = subFrontS();
            string subBack = subBackS();
            string a =string.Format(@"
		<Folder>
			<name>SubCameras</name>
			<Folder>
				<name>Center</name>
				<open>1</open>
				{0}
			</Folder>
            <Folder>
				<name>Left</name>
				<open>1</open>
				{1}
			</Folder>
            <Folder>
				<name>Right</name>
				<open>1</open>
				{2}
			</Folder>
            <Folder>
				<name>Front</name>
				<open>1</open>
				{3}
			</Folder>
            <Folder>
				<name>Back</name>
				<open>1</open>
				{4}
			</Folder>
		</Folder>",subCenter,subLeft,subRight,subFront,subBack);
            return a;
        }
        private static string subCenterS()
        {
            string thisStr = null;
            List<Point[]> thisList = Data.subCenter;
            for (int i = 0; i < thisList.Count; i++)
            {
                string coor = null;
                for (int j = 0; j < 5; j++)
                {
                    coor += thisList[i][j].X.ToString() + "," + thisList[i][j].Y.ToString() + "," + thisList[i][j].Z.ToString() + " ";
                }
                thisStr += string.Format(@"<Placemark>
					<name>Center{0}</name>
					<styleUrl>#msn_ylw-pushpin</styleUrl>
					<Polygon>
						<tessellate>1</tessellate>
                        <altitudeMode>absolute</altitudeMode>
						<outerBoundaryIs>
							<LinearRing>
								<coordinates>
									{1}
								</coordinates>
							</LinearRing>
						</outerBoundaryIs>
					</Polygon>
				</Placemark>
", i,coor);
            }
            return thisStr;
        }
        private static string subLeftS()
        {
            string thisStr = null;
            List<Point[]> thisList = Data.subLeft;
            for (int i = 0; i < thisList.Count; i++)
            {
                string coor = null;
                for (int j = 0; j < 5; j++)
                {
                    coor += thisList[i][j].X.ToString() + "," + thisList[i][j].Y.ToString() + "," + thisList[i][j].Z.ToString() + " ";
                }
                thisStr += string.Format(@"<Placemark>
					<name>Left{0}</name>
					<styleUrl>#msn_ylw-pushpin</styleUrl>
					<Polygon>
						<tessellate>1</tessellate>
                        <altitudeMode>absolute</altitudeMode>
						<outerBoundaryIs>
							<LinearRing>
								<coordinates>
									{1}
								</coordinates>
							</LinearRing>
						</outerBoundaryIs>
					</Polygon>
				</Placemark>
", i, coor);
            }
            return thisStr;
        }
        private static string subRightS()
        {
            string thisStr = null;
            List<Point[]> thisList = Data.subRight;
            for (int i = 0; i < thisList.Count; i++)
            {
                string coor = null;
                for (int j = 0; j < 5; j++)
                {
                    coor += thisList[i][j].X.ToString() + "," + thisList[i][j].Y.ToString() + "," + thisList[i][j].Z.ToString() + " ";
                }
                thisStr += string.Format(@"<Placemark>
					<name>Right{0}</name>
					<styleUrl>#msn_ylw-pushpin</styleUrl>
					<Polygon>
						<tessellate>1</tessellate>
                        <altitudeMode>absolute</altitudeMode>
						<outerBoundaryIs>
							<LinearRing>
								<coordinates>
									{1}
								</coordinates>
							</LinearRing>
						</outerBoundaryIs>
					</Polygon>
				</Placemark>
", i, coor);
            }
            return thisStr;
        }
        private static string subFrontS()
        {
            string thisStr = null;
            List<Point[]> thisList = Data.subFront;
            for (int i = 0; i < thisList.Count; i++)
            {
                string coor = null;
                for (int j = 0; j < 5; j++)
                {
                    coor += thisList[i][j].X.ToString() + "," + thisList[i][j].Y.ToString() + "," + thisList[i][j].Z.ToString() + " ";
                }
                thisStr += string.Format(@"<Placemark>
					<name>Front{0}</name>
					<styleUrl>#msn_ylw-pushpin</styleUrl>
					<Polygon>
						<tessellate>1</tessellate>
                        <altitudeMode>absolute</altitudeMode>
						<outerBoundaryIs>
							<LinearRing>
								<coordinates>
									{1}
								</coordinates>
							</LinearRing>
						</outerBoundaryIs>
					</Polygon>
				</Placemark>
", i, coor);
            }
            return thisStr;
        }
        private static string subBackS()
        {
            string thisStr = null;
            List<Point[]> thisList = Data.subBack;
            for (int i = 0; i < thisList.Count; i++)
            {
                string coor = null;
                for (int j = 0; j < 5; j++)
                {
                    coor += thisList[i][j].X.ToString() + "," + thisList[i][j].Y.ToString() + "," + thisList[i][j].Z.ToString() + " ";
                }
                thisStr += string.Format(@"<Placemark>
					<name>Back{0}</name>
					<styleUrl>#msn_ylw-pushpin</styleUrl>
					<Polygon>
						<tessellate>1</tessellate>
                        <altitudeMode>absolute</altitudeMode>
						<outerBoundaryIs>
							<LinearRing>
								<coordinates>
									{1}
								</coordinates>
							</LinearRing>
						</outerBoundaryIs>
					</Polygon>
				</Placemark>
", i, coor);
            }
            return thisStr;
        }
    }
}
