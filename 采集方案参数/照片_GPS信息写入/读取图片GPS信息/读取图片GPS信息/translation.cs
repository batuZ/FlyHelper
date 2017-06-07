using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace 读取图片GPS信息
{
    /// <summary>
    /// Summary description for translation.
    /// </summary>
    public class Translation : Hashtable
    {
        /// <summary>
        /// 
        /// </summary>
        public Translation()
        {
            //IFD0 (main image) で使われているTag
            this.Add(0x010e, "ImageDescription");// 画像タイトル 
            this.Add(0x010f, "Make");//  メーカ 
            this.Add(0x0110, "Model");//  モデル           
            this.Add(0x011a, "XResolution");// 画像の幅方向の解像度（ピクセル） 
            this.Add(0x011b, "YResolution");// 画像の高さ方向の解像度（ピクセル）
            this.Add(0x0128, "ResolutionUnit");// 解像度の単位 
            this.Add(0x0131, "Software");//  使用したソフトウェア 
            this.Add(0x0132, "ExifDateTime");//  ファイル変更日時
            this.Add(0x013f, "PrimaryChromaticities");//  原色の色座標値           
            this.Add(0x8298, "Copyright");//  著作権表示 
            this.Add(0x8769, "ExifIFDPointer");// Exif IFD へのポインタ 
            //Exif SubIFD で使われているTag
            this.Add(0x829a, "ExposureTime");// 露出時間（秒） 
            this.Add(0x829d, "FNumber");// F値 
            this.Add(0x8822, "ExposureProgram");// 露出プログラム 
            this.Add(0x8824, "SpectralSensitivity");// スペクトル感度
            this.Add(0x8827, "ISOSpeedRatings");// ISOスピードレート 
            this.Add(0x9000, "ExifVersion");// Exifバージョン 
            this.Add(0x9003, "DateTimeOriginal");// オリジナル画像の生成日時 
            this.Add(0x9004, "DateTimeDigitized");// ディジタルデータの生成日時 
            this.Add(0x9101, "ComponentsConfiguration");// コンポーネントの意味 
            this.Add(0x9102, "CompressedBitsPerPixel");// 画像圧縮モード（ビット／ピクセル）
            this.Add(0x9201, "ShutterSpeedValue");// シャッタースピード（APEX） 
            this.Add(0x9202, "ApertureValue");// 絞り（APEX） 
            this.Add(0x9203, "BrightnessValue");//輝度（APEX） 
            this.Add(0x9204, "ExposureBiasValue");// 露出補正（APEX） 
            this.Add(0x9205, "MaxApertureValue");// レンズの最小F値（APEX） 
            this.Add(0x9206, "SubjectDistance");// 被写体距離（m） 
            this.Add(0x9207, "MeteringMode");//測光方式 
            this.Add(0x9208, "LightSource");// 光源 
            this.Add(0x9209, "Flash");// フラッシュ 
            this.Add(0x920a, "FocalLength");// レンズの焦点距離（mm） 
            this.Add(0x927c, "MakerNote"); //メーカ固有情報 
            this.Add(0x9286, "UserComment");// ユーザコメント 
            this.Add(0x9290, "SubSecTime");//ファイル変更日時の秒以下の値 
            this.Add(0x9291, "SubSecTimeOriginal");// 画像生成日時の秒以下の値 
            this.Add(0x9292, "SubSecTimeDigitized");// ディジタルデータ生成日時の秒以下の値 
            this.Add(0xa000, "FlashPixVersion");// 対応FlashPixのバージョン 
            this.Add(0xa001, "ColorSpace");// 色空間情報 
            this.Add(0xa002, "ExifImageWidth ");// メイン画像の幅 
            this.Add(0xa003, "ExifImageHeight ");//  メイン画像の高さ
            this.Add(0xa004, "RelatedSoundFile");// 関連音声ファイル名 
            this.Add(0xa005, "InteroperabilityIFDPointer");// 互換性IFDへのポインタ 
            this.Add(0xa20e, "FocalPlaneXResolution");// 焦点面の幅方向の解像度（ピクセル） 
            this.Add(0xa20f, "FocalPlaneYResolution");// 焦点面の高さ方向の解像度（ピクセル） 
            this.Add(0xa210, "FocalPlaneResolutionUnit");// 焦点面の解像度の単位  
            this.Add(0xa215, "ExposureIndex");// 露出インデックス 
            this.Add(0xa217, "SensingMethod");// 画像センサの方式 
            this.Add(0xa300, "FileSource");// 画像入力機器の種類 
            this.Add(0xa301, "SceneType");//シーンタイプ 
            this.Add(0xa302, "CFAPattern");//CFAパターン 
            //IFD1 (thumbnail image) で使われているTag
            this.Add(0x0100, "ImageWidth ");// 画像の幅（ピクセル） 
            this.Add(0x0101, "ImageHeight ");//  画像の高さ（ピクセル）
            this.Add(0x0102, "BitsPerSample");//  画素のビットの深さ（ビット） 
            this.Add(0x0103, "Compression");// 圧縮の種類 
            this.Add(0x0106, "PhotometricInterpretation");//  画素構成の種類   
            this.Add(0x0111, "StripOffsets");// イメージデータへのオフセット 
            this.Add(0x0112, "Orientation");// 画素の並び 
            this.Add(0x0115, "SamplesPerPixel");//  ピクセル毎のコンポーネント数 
            this.Add(0x0116, "RowsPerStrip");//  １ストリップあたりの行数 
            this.Add(0x0117, "StripByteCounts");// 各ストリップのサイズ（バイト）
            this.Add(0x011c, "PlanarConfiguration");//  画素データの並び
            this.Add(0x0201, "JPEGInterchangeFormat");//  JPEGサムネイルのSOIへのオフセット 
            this.Add(0x0202, "JPEGInterchangeFormatLength");//  JPEGサムネイルデータのサイズ（バイト
            this.Add(0x0211, "YCbCrCoefficients");//  色変換マトリックス係数 
            this.Add(0x0212, "YCbCrSubSampling");// 画素の比率構成  
            this.Add(0x0213, "YCbCrPositioning ");//  色情報のサンプリング
            this.Add(0x0214, "ReferenceBlackWhite");//  黒色と白色の値 
            //その他のTag
            this.Add(0x00fe, "NewSubfileType");
            this.Add(0x00ff, "SubfileType");
            this.Add(0x012d, "TransferFunction");//  諧調カーブ特性 
            this.Add(0x013b, "Artist");//  撮影者名
            this.Add(0x013d, "Predictor");
            this.Add(0x013e, "WhitePoint");//  ホワイトポイントの色座標値 
            this.Add(0x0142, "TileWidth");
            this.Add(0x0143, "TileLength");
            this.Add(0x0144, "TileOffsets");
            this.Add(0x0145, "TileByteCounts");
            this.Add(0x014a, "SubIFDs");
            this.Add(0x015b, "JPEGTables");
            this.Add(0x828d, "CFARepeatPatternDim");
            this.Add(0x828f, "BatteryLevel ");
            this.Add(0x83bb, "IPTC/NAA");
            this.Add(0x8773, "InterColorProfile");
            this.Add(0x8825, "GPSInfo");// GPS情報IFDへのポインタ
            this.Add(0x8828, "OECF");// 光電変換関数 
            this.Add(0x8829, "Interlace");
            this.Add(0x882a, "TimeZoneOffset");
            this.Add(0x882b, "SelfTimerMode");
            this.Add(0x920b, "FlashEnergy ");// フラッシュのエネルギー（BCPS）
            this.Add(0x920c, "SpatialFrequencyResponse");//空間周波数応答 
            this.Add(0x920d, "Noise");
            this.Add(0x9211, "ImageNumber");
            this.Add(0x9212, "SecurityClassification");
            this.Add(0x9213, "ImageHistory");
            this.Add(0x9214, "SubjectLocation");//被写体位置
            this.Add(0x9216, "TIFFEPStandardID");
            //Interoperability IFD で使われているTag
            this.Add(0x0001, "InteroperabilityIndex");
            this.Add(0x0002, "InteroperabilityVersion");
            this.Add(0x1000, "RelatedImageFileFormat");
        }
    }
    /// <summary>
    /// private class
    /// RATIONAL Two LONGs. The first LONG is the numerator and the second LONG expresses the denominator.
    /// </summary>
    internal class Rational
    {
        private int n;
        private int d;
        public Rational(int n, int d)
        {
            this.n = n;
            this.d = d;
            simplify(ref this.n, ref this.d);
        }
        public Rational(uint n, uint d)
        {
            this.n = Convert.ToInt32(n);
            this.d = Convert.ToInt32(d);
            simplify(ref this.n, ref this.d);
        }
        public Rational()
        {
            this.n = this.d = 0;
        }
        public string ToString(string sp)
        {
            if (sp == null) sp = "/";
            return n.ToString() + sp + d.ToString();
        }
        public double ToDouble()
        {
            if (d == 0)
                return 0.0;
            return Math.Round(Convert.ToDouble(n) / Convert.ToDouble(d), 2);
        }
        private void simplify(ref int a, ref int b)
        {
            if (a == 0 || b == 0)
                return;
            int gcd = euclid(a, b);
            a /= gcd;
            b /= gcd;
        }
        private int euclid(int a, int b)
        {
            if (b == 0)
                return a;
            else
                return euclid(b, a % b);
        }
    }
}