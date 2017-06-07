using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.IO;
using System.Collections.Specialized;

namespace 读取图片GPS信息
{
    class EXIFMetaData
    {
        public EXIFMetaData()
        {
        }
        #region struct MetadataDetail
        /// <summary>
        /// 構造体
        /// </summary>
        public struct MetadataDetail
        {
            //public string RawValueAsString;//原文字列
            public string DisplayValue;//
        }
        #endregion
        #region EXIF Element
        /// <summary>
        ///  EXIF Elementを保存した
        /// </summary>
        public struct Metadata
        {
            //IFD0 (main image) で使われているTag
            public MetadataDetail ImageDescription;// 画像タイトル 
            public MetadataDetail Make;//  メーカ 
            public MetadataDetail Model;//  モデル   
            public MetadataDetail XResolution;// 画像の幅方向の解像度（ピクセル） 
            public MetadataDetail YResolution;// 画像の高さ方向の解像度（ピクセル）
            public MetadataDetail ResolutionUnit;// 解像度の単位 
            public MetadataDetail Software;//  使用したソフトウェア 
            public MetadataDetail ExifDateTime;//  ファイル変更日時
            public MetadataDetail PrimaryChromaticities;//  原色の色座標値   
            public MetadataDetail Copyright;//  著作権表示 
            public MetadataDetail ExifIFDPointer;// Exif IFD へのポインタ 
            //Exif SubIFD で使われているTag
            public MetadataDetail ExposureTime;// 露出時間（秒） 
            public MetadataDetail FNumber;// F値 
            public MetadataDetail ExposureProgram;// 露出プログラム 
            public MetadataDetail SpectralSensitivity;// スペクトル感度
            public MetadataDetail ISOSpeedRatings;// ISOスピードレート 
            public MetadataDetail ExifVersion;// Exifバージョン 
            public MetadataDetail DateTimeOriginal;// オリジナル画像の生成日時 
            public MetadataDetail DateTimeDigitized;// ディジタルデータの生成日時 
            public MetadataDetail ComponentsConfiguration;// コンポーネントの意味 
            public MetadataDetail CompressedBitsPerPixel;// 画像圧縮モード（ビット／ピクセル）
            public MetadataDetail ShutterSpeedValue;// シャッタースピード（APEX） 
            public MetadataDetail ApertureValue;// 絞り（APEX） 
            public MetadataDetail BrightnessValue;//輝度（APEX） 
            public MetadataDetail ExposureBiasValue;// 露出補正（APEX） 
            public MetadataDetail MaxApertureValue;// レンズの最小F値（APEX） 
            public MetadataDetail SubjectDistance;// 被写体距離（m） 
            public MetadataDetail MeteringMode;//測光方式 
            public MetadataDetail LightSource;// 光源 
            public MetadataDetail Flash;// フラッシュ 
            public MetadataDetail FocalLength;// レンズの焦点距離（mm） 
            public MetadataDetail MakerNote; //メーカ固有情報 
            public MetadataDetail UserComment;// ユーザコメント 
            public MetadataDetail SubSecTime;//ファイル変更日時の秒以下の値 
            public MetadataDetail SubSecTimeOriginal;// 画像生成日時の秒以下の値 
            public MetadataDetail SubSecTimeDigitized;// ディジタルデータ生成日時の秒以下の値 
            public MetadataDetail FlashPixVersion;// 対応FlashPixのバージョン 
            public MetadataDetail ColorSpace;// 色空間情報 
            public MetadataDetail ExifImageWidth;// メイン画像の幅 
            public MetadataDetail ExifImageHeight;//  メイン画像の高さ
            public MetadataDetail RelatedSoundFile;// 関連音声ファイル名 
            public MetadataDetail InteroperabilityIFDPointer;// 互換性IFDへのポインタ 
            public MetadataDetail FocalPlaneXResolution;// 焦点面の幅方向の解像度（ピクセル） 
            public MetadataDetail FocalPlaneYResolution;// 焦点面の高さ方向の解像度（ピクセル） 
            public MetadataDetail FocalPlaneResolutionUnit;// 焦点面の解像度の単位  
            public MetadataDetail ExposureIndex;// 露出インデックス 
            public MetadataDetail SensingMethod;// 画像センサの方式 
            public MetadataDetail FileSource;// 画像入力機器の種類 
            public MetadataDetail SceneType;//シーンタイプ 
            public MetadataDetail CFAPattern;//CFAパターン 
            //IFD1 (thumbnail image) で使われているTag
            public MetadataDetail ImageWidth;// 画像の幅（ピクセル） 
            public MetadataDetail ImageHeight;//  画像の高さ（ピクセル）
            public MetadataDetail BitsPerSample;//  画素のビットの深さ（ビット） 
            public MetadataDetail Compression;// 圧縮の種類 
            public MetadataDetail PhotometricInterpretation;//  画素構成の種類   
            public MetadataDetail StripOffsets;// イメージデータへのオフセット 
            public MetadataDetail Orientation;// 画素の並び 
            public MetadataDetail SamplesPerPixel;//  ピクセル毎のコンポーネント数 
            public MetadataDetail RowsPerStrip;//  １ストリップあたりの行数 
            public MetadataDetail StripByteCounts;// 各ストリップのサイズ（バイト）
            public MetadataDetail PlanarConfiguration;//  画素データの並び
            public MetadataDetail JPEGInterchangeFormat;//  JPEGサムネイルのSOIへのオフセット 
            public MetadataDetail JPEGInterchangeFormatLength;//  JPEGサムネイルデータのサイズ（バイト
            public MetadataDetail YCbCrCoefficients;//  色変換マトリックス係数 
            public MetadataDetail YCbCrSubSampling;// 画素の比率構成  
            public MetadataDetail YCbCrPositioning;//  色情報のサンプリング
            public MetadataDetail ReferenceBlackWhite;//  黒色と白色の値 
            //その他
            public MetadataDetail NewSubfileType;
            public MetadataDetail SubfileType;
            public MetadataDetail TransferFunction;//  諧調カーブ特性 
            public MetadataDetail Artist;//  撮影者名
            public MetadataDetail Predictor;
            public MetadataDetail WhitePoint;//  ホワイトポイントの色座標値 ;
            public MetadataDetail TileWidth;
            public MetadataDetail TileLength;
            public MetadataDetail TileOffsets;
            public MetadataDetail TileByteCounts;
            public MetadataDetail SubIFDs;
            public MetadataDetail JPEGTables;
            public MetadataDetail CFARepeatPatternDim;
            public MetadataDetail BatteryLevel;
            public MetadataDetail IPTCNAA;
            public MetadataDetail InterColorProfile;
            public MetadataDetail GPSInfo;// GPS情報IFDへのポインタ
            public MetadataDetail OECF;// 光電変換関数 
            public MetadataDetail Interlace;
            public MetadataDetail TimeZoneOffset;
            public MetadataDetail SelfTimerMode;
            public MetadataDetail FlashEnergy;// フラッシュのエネルギー（BCPS）
            public MetadataDetail SpatialFrequencyResponse;//空間周波数応答 
            public MetadataDetail Noise;
            public MetadataDetail ImageNumber;
            public MetadataDetail SecurityClassification;
            public MetadataDetail ImageHistory;
            public MetadataDetail SubjectLocation;//被写体位置
            public MetadataDetail TIFFEPStandardID;
            //Interoperability IFD で使われているTag
            public MetadataDetail InteroperabilityIndex;
            public MetadataDetail InteroperabilityVersion;
            public MetadataDetail RelatedImageFileFormat;
        }
        #endregion
        #region EXIF情報を取得
        public Metadata GetEXIFMetaData(string PhotoName)
        {
            // 
            Bitmap MyImage = (Bitmap)Bitmap.FromFile(PhotoName);
            EXIFextractor exifExtractor = new EXIFextractor(ref MyImage, "/n");
            NameValueCollection items = new NameValueCollection();
            foreach (Pair pair in exifExtractor)
            {
                items.Add(pair.First, pair.Second);
            }
            Metadata MyMetadata = new Metadata();
            try
            {
                MyMetadata.ImageDescription.DisplayValue = items.Get("ImageDescription");// 画像タイトル 
                MyMetadata.Make.DisplayValue = items.Get("Make");//  メーカ 
                MyMetadata.Model.DisplayValue = items.Get("Model");//  モデル   
                //MyMetadata.XResolution.DisplayValue = items.Get("XResolution");// 画像の幅方向の解像度（ピクセル） 
                //MyMetadata.YResolution.DisplayValue = items.Get("YResolution");// 画像の高さ方向の解像度（ピクセル）
                MyMetadata.XResolution.DisplayValue = MyImage.HorizontalResolution.ToString();
                MyMetadata.YResolution.DisplayValue = MyImage.VerticalResolution.ToString();
                MyMetadata.ResolutionUnit.DisplayValue = items.Get("ResolutionUnit");// 解像度の単位 
                MyMetadata.Software.DisplayValue = items.Get("Software");//  使用したソフトウェア 
                MyMetadata.ExifDateTime.DisplayValue = items.Get("ExifDateTime");//  ファイル変更日時
                MyMetadata.PrimaryChromaticities.DisplayValue = items.Get("PrimaryChromaticities");//  原色の色座標値   
                MyMetadata.Copyright.DisplayValue = items.Get("Copyright");//  著作権表示 
                MyMetadata.ExifIFDPointer.DisplayValue = items.Get("ExifIFDPointer");// Exif IFD へのポインタ 
                MyMetadata.ExposureTime.DisplayValue = items.Get("ExposureTime");// 露出時間（秒） 
                MyMetadata.FNumber.DisplayValue = items.Get("FNumber");// F値 
                MyMetadata.ExposureProgram.DisplayValue = items.Get("ExposureProgram");// 露出プログラム 
                MyMetadata.SpectralSensitivity.DisplayValue = items.Get("SpectralSensitivity");// スペクトル感度
                MyMetadata.ISOSpeedRatings.DisplayValue = items.Get("ISOSpeedRatings");// ISOスピードレート 
                MyMetadata.ExifVersion.DisplayValue = items.Get("ExifVersion");// Exifバージョン 
                MyMetadata.DateTimeOriginal.DisplayValue = items.Get("DateTimeOriginal");// オリジナル画像の生成日時 
                MyMetadata.DateTimeDigitized.DisplayValue = items.Get("DateTimeDigitized");// ディジタルデータの生成日時 
                MyMetadata.ComponentsConfiguration.DisplayValue = items.Get("ComponentsConfiguration");// コンポーネントの意味 
                MyMetadata.CompressedBitsPerPixel.DisplayValue = items.Get("CompressedBitsPerPixel");// 画像圧縮モード（ビット／ピクセル）
                MyMetadata.ShutterSpeedValue.DisplayValue = items.Get("ShutterSpeedValue");// シャッタースピード（APEX） 
                MyMetadata.ApertureValue.DisplayValue = items.Get("ApertureValue");// 絞り（APEX） 
                MyMetadata.BrightnessValue.DisplayValue = items.Get("BrightnessValue");//輝度（APEX） 
                MyMetadata.ExposureBiasValue.DisplayValue = items.Get("ExposureBiasValue");// 露出補正（APEX） 
                MyMetadata.MaxApertureValue.DisplayValue = items.Get("MaxApertureValue");// レンズの最小F値（APEX） 
                MyMetadata.SubjectDistance.DisplayValue = items.Get("SubjectDistance");// 被写体距離（m） 
                MyMetadata.MeteringMode.DisplayValue = items.Get("MeteringMode");//測光方式 
                MyMetadata.LightSource.DisplayValue = items.Get("LightSource");// 光源 
                MyMetadata.Flash.DisplayValue = items.Get("Flash");// フラッシュ 
                MyMetadata.FocalLength.DisplayValue = items.Get("FocalLength");// レンズの焦点距離（mm） 
                MyMetadata.MakerNote.DisplayValue = items.Get("MakerNote"); //メーカ固有情報 
                MyMetadata.UserComment.DisplayValue = items.Get("UserComment");// ユーザコメント 
                MyMetadata.SubSecTime.DisplayValue = items.Get("SubSecTime");//ファイル変更日時の秒以下の値 
                MyMetadata.SubSecTimeOriginal.DisplayValue = items.Get("SubSecTimeOriginal");// 画像生成日時の秒以下の値 
                MyMetadata.SubSecTimeDigitized.DisplayValue = items.Get("SubSecTimeDigitized");// ディジタルデータ生成日時の秒以下の値 
                MyMetadata.FlashPixVersion.DisplayValue = items.Get("FlashPixVersion");// 対応FlashPixのバージョン 
                MyMetadata.ColorSpace.DisplayValue = items.Get("ColorSpace");// 色空間情報 
                MyMetadata.ExifImageWidth.DisplayValue = items.Get("ExifImageWidth ");// メイン画像の幅 
                MyMetadata.ExifImageHeight.DisplayValue = items.Get("ExifImageHeight ");//  メイン画像の高さ
                MyMetadata.RelatedSoundFile.DisplayValue = items.Get("RelatedSoundFile");// 関連音声ファイル名 
                MyMetadata.InteroperabilityIFDPointer.DisplayValue = items.Get("InteroperabilityIFDPointer");// 互換性IFDへのポインタ 
                MyMetadata.FocalPlaneXResolution.DisplayValue = items.Get("FocalPlaneXResolution");// 焦点面の幅方向の解像度（ピクセル） 
                MyMetadata.FocalPlaneYResolution.DisplayValue = items.Get("FocalPlaneYResolution");// 焦点面の高さ方向の解像度（ピクセル） 
                MyMetadata.FocalPlaneResolutionUnit.DisplayValue = items.Get("FocalPlaneResolutionUnit");// 焦点面の解像度の単位  
                MyMetadata.ExposureIndex.DisplayValue = items.Get("ExposureIndex");// 露出インデックス 
                MyMetadata.SensingMethod.DisplayValue = items.Get("SensingMethod");// 画像センサの方式 
                MyMetadata.FileSource.DisplayValue = items.Get("FileSource");// 画像入力機器の種類 
                MyMetadata.SceneType.DisplayValue = items.Get("シーSceneType");//シーンタイプ 
                MyMetadata.CFAPattern.DisplayValue = items.Get("CFACFAPattern");//CFAパターン 
                MyMetadata.ImageWidth.DisplayValue = items.Get("ImageWidth");// 画像の幅（ピクセル） 
                MyMetadata.ImageHeight.DisplayValue = items.Get("ImageHeight");//  画像の高さ（ピクセル）
                MyMetadata.BitsPerSample.DisplayValue = items.Get("BitsPerSample");//  画素のビットの深さ（ビット） 
                MyMetadata.Compression.DisplayValue = items.Get("Compression");// 圧縮の種類 
                MyMetadata.PhotometricInterpretation.DisplayValue = items.Get("PhotometricInterpretation");//  画素構成の種類   
                MyMetadata.StripOffsets.DisplayValue = items.Get("StripOffsets");// イメージデータへのオフセット 
                MyMetadata.Orientation.DisplayValue = items.Get("Orientation");// 画素の並び 
                MyMetadata.SamplesPerPixel.DisplayValue = items.Get("SamplesPerPixel");//  ピクセル毎のコンポーネント数 
                MyMetadata.RowsPerStrip.DisplayValue = items.Get("RowsPerStrip");//  １ストリップあたりの行数 
                MyMetadata.StripByteCounts.DisplayValue = items.Get("StripByteCounts");// 各ストリップのサイズ（バイト）
                MyMetadata.PlanarConfiguration.DisplayValue = items.Get("PlanarConfiguration");//  画素データの並び
                MyMetadata.JPEGInterchangeFormat.DisplayValue = items.Get("JPEGInterchangeFormat");//  JPEGサムネイルのSOIへのオフセット 
                MyMetadata.JPEGInterchangeFormatLength.DisplayValue = items.Get("JPEGInterchangeFormatLength");//  JPEGサムネイルデータのサイズ（バイト
                MyMetadata.YCbCrCoefficients.DisplayValue = items.Get("YCbCrCoefficients");//  色変換マトリックス係数 
                MyMetadata.YCbCrSubSampling.DisplayValue = items.Get("YCbCrSubSampling");// 画素の比率構成  
                MyMetadata.YCbCrPositioning.DisplayValue = items.Get("YCbCrPositioning ");//  色情報のサンプリング
                MyMetadata.ReferenceBlackWhite.DisplayValue = items.Get("ReferenceBlackWhite");//  黒色と白色の値 
                MyMetadata.NewSubfileType.DisplayValue = items.Get("NewSubfileType");
                MyMetadata.SubfileType.DisplayValue = items.Get("SubfileType");
                MyMetadata.TransferFunction.DisplayValue = items.Get("TransferFunction");//  諧調カーブ特性 
                MyMetadata.Artist.DisplayValue = items.Get("Artist");//  撮影者名
                MyMetadata.Predictor.DisplayValue = items.Get("Predictor");
                MyMetadata.WhitePoint.DisplayValue = items.Get("WhitePoint");//  ホワイトポイントの色座標値 
                MyMetadata.TileWidth.DisplayValue = items.Get("TileWidth");
                MyMetadata.TileLength.DisplayValue = items.Get("TileLength");
                MyMetadata.TileOffsets.DisplayValue = items.Get("TileOffsets");
                MyMetadata.TileByteCounts.DisplayValue = items.Get("TileByteCounts");
                MyMetadata.SubIFDs.DisplayValue = items.Get("SubIFDs");
                MyMetadata.JPEGTables.DisplayValue = items.Get("JPEGTables");
                MyMetadata.CFARepeatPatternDim.DisplayValue = items.Get("CFARepeatPatternDim");
                MyMetadata.BatteryLevel.DisplayValue = items.Get("BatteryLevel ");
                MyMetadata.IPTCNAA.DisplayValue = items.Get("IPTCNAA");
                MyMetadata.InterColorProfile.DisplayValue = items.Get("InterColorProfile");
                MyMetadata.GPSInfo.DisplayValue = items.Get("GPSInfo");// GPS情報IFDへのポインタ
                MyMetadata.OECF.DisplayValue = items.Get("OECF");// 光電変換関数 
                MyMetadata.Interlace.DisplayValue = items.Get("Interlace");
                MyMetadata.TimeZoneOffset.DisplayValue = items.Get("TimeZoneOffset");
                MyMetadata.SelfTimerMode.DisplayValue = items.Get("SelfTimerMode");
                MyMetadata.FlashEnergy.DisplayValue = items.Get("FlashEnergy ");// フラッシュのエネルギー（BCPS）
                MyMetadata.SpatialFrequencyResponse.DisplayValue = items.Get("SpatialFrequencyResponse");//空間周波数応答 
                MyMetadata.Noise.DisplayValue = items.Get("Noise ");
                MyMetadata.ImageNumber.DisplayValue = items.Get("ImageNumber");
                MyMetadata.SecurityClassification.DisplayValue = items.Get("SecurityClassification");
                MyMetadata.ImageHistory.DisplayValue = items.Get("ImageHistory");
                MyMetadata.SubjectLocation.DisplayValue = items.Get("SubjectLocation");//被写体位置
                MyMetadata.TIFFEPStandardID.DisplayValue = items.Get("TIFFEPStandardID");
                MyMetadata.InteroperabilityIndex.DisplayValue = items.Get("InteroperabilityIndex");
                MyMetadata.InteroperabilityVersion.DisplayValue = items.Get("InteroperabilityVersion");
                MyMetadata.RelatedImageFileFormat.DisplayValue = items.Get("RelatedImageFileFormat");
            }
            catch
            {
            }
            items.Clear();
            return MyMetadata;
        }
        #endregion
    }
}