using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Drawing.Imaging;
using System.Reflection;
using System.IO; 

namespace 读取图片GPS信息
{
/// <summary>
/// EXIFextractor Class
/// </summary>
public class EXIFextractor : IEnumerable
{
/// <summary>
/// Get the individual property value by supplying property name  
/// </summary>
public object this[string index]
{
get
{
return propertiesHash[index];
}
} 
private System.Drawing.Bitmap bmp; 
private string data; //全てのEXIF名と値(項目間に/nで隔てる) 
private Translation myHash; // EXIFタグと名のHashTable  
private Hashtable propertiesHash;// EXIFタグと値のHashTable
internal int Count
{
get
{
return this.propertiesHash.Count;
}
}
//
string separateString;
/// <summary>
/// 
/// </summary>
public void setTag(int id, string data)
{
Encoding ascii = Encoding.ASCII;
this.setTag(id, data.Length, 0x2, ascii.GetBytes(data));
}
/// <summary>
/// setTag
/// </summary>
public void setTag(int id, int len, short type, byte[] data)
{
PropertyItem item = CreatePropertyItem(type, id, len, data);
this.bmp.SetPropertyItem(item);
buildDB(this.bmp.PropertyItems);
}
/// <summary>
/// 
/// </summary>
private static PropertyItem CreatePropertyItem(short type, int tag, int len, byte[] value)
{
PropertyItem item; 

// Loads a PropertyItem from a Jpeg image stored in the assembly as a resource.
Assembly assembly = Assembly.GetExecutingAssembly();
Stream emptyBitmapStream = assembly.GetManifestResourceStream("EXIFextractor.decoy.jpg");
System.Drawing.Image empty = System.Drawing.Image.FromStream(emptyBitmapStream); 

item = empty.PropertyItems[0]; 

// Copies the data to the property item.
item.Type = type;
item.Len = len;
item.Id = tag;
item.Value = new byte[value.Length];
value.CopyTo(item.Value, 0); 

return item;
}
/// <summary>
/// 
/// </summary>
public EXIFextractor(ref System.Drawing.Bitmap bmp, string separate)
{
propertiesHash = new Hashtable();
this.bmp = bmp;
this.separateString = separate; 
myHash = new Translation();
buildDB(this.bmp.PropertyItems);
}
string msp = ""; 

public EXIFextractor(ref System.Drawing.Bitmap bmp, string separate, string msp)
{
propertiesHash = new Hashtable();
this.separateString = separate;
this.msp = msp;
this.bmp = bmp; 
myHash = new Translation();
this.buildDB(bmp.PropertyItems);
}
public static PropertyItem[] GetExifProperties(string fileName)
{
FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
System.Drawing.Image image = System.Drawing.Image.FromStream(stream,
/* useEmbeddedColorManagement = */ true,
/* validateImageData = */ false);
return image.PropertyItems;
}
public EXIFextractor(string file, string separate, string msp)
{
propertiesHash = new Hashtable();
this.separateString = separate;
this.msp = msp; 
myHash = new Translation();
this.buildDB(GetExifProperties(file));
} 
/// <summary>
/// データタイプによって、値を取得する。
/// </summary>
private void buildDB(System.Drawing.Imaging.PropertyItem[] parr)
{
propertiesHash.Clear();
//全てのEXIF名と値(項目間に/nで隔てる)
data = ""; 

//ASCII　Encoding
Encoding ascii = Encoding.ASCII;
//メタデータプロパティ
foreach (System.Drawing.Imaging.PropertyItem item in parr)
{
string itemValue = "";
string itemName = (string)myHash[item.Id];
// tag not found. skip it
if (itemName == null) continue;
data += itemName + ": ";
//1 = BYTE An 8-bit unsigned integer.,
if (item.Type == 0x1)
{
itemValue = item.Value[0].ToString();
}
//2 = ASCII An 8-bit byte containing one 7-bit ASCII code. The final byte is terminated with NULL.,
else if (item.Type == 0x2)// string  
{ 

itemValue = ascii.GetString(item.Value).Trim('/0');
}
//3 = SHORT A 16-bit (2 -byte) unsigned integer,
else if (item.Type == 0x3)
{ // lookup table  
switch (item.Id)
{
case 0x0112: // Orientation 画像の方向 
{
switch (convertToInt16U(item.Value))
{
case 1: itemValue = "水平(普通)"; break; //1 = Horizontal (normal) 
case 2: itemValue = "水平鏡像"; break; //2 = Mirror horizontal 
case 3: itemValue = "回転180"; break; //3 = Rotate 180 
case 4: itemValue = "垂直鏡像"; break; //4 = Mirror vertical 
case 5: itemValue = "水平鏡像回転270"; break;//5 = Mirror horizontal and rotate 270 CW 
case 6: itemValue = "回転90"; break; //6 = Rotate 90 CW 
case 7: itemValue = "水平鏡像回転90"; break;//7 = Mirror horizontal and rotate 90 CW 
case 8: itemValue = "回転270"; break;//8 = Rotate 270 CW
default: itemValue = "未定義"; break; 
}
}
break; 

case 0x8827: // ISO
itemValue = "ISO-" + convertToInt16U(item.Value).ToString();
break;
case 0xA217: // センサー方式: SensingMethod  
{
switch (convertToInt16U(item.Value))
{
case 1: itemValue = "未定義"; break; //Not defined
case 2: itemValue = "単版カラーセンサー"; break; //One-chip color area sensor
case 3: itemValue = "2板カラーセンサー"; break; //Two-chip color area sensor
case 4: itemValue = "3板カラーセンサー"; break; //Three-chip color area sensor
case 5: itemValue = "色順次カラーセンサー"; break; //Color sequential area sensor
case 7: itemValue = "3線リニアセンサー"; break; //Trilinear sensor
case 8: itemValue = "色順次リニアセンサー"; break; //Color sequential linear sensor
default: itemValue = "保留されます"; break; //reserved
} 
}
break;
case 0xA001://ColorSpace
{
if (convertToInt16U(item.Value) == 1)
{
itemValue = "sRGB";
}
else
{
itemValue = "未調整";//Uncalibrated
}

}
break;
case 0x8822: // 露出プログラム: ExposureProgram  
switch (convertToInt16U(item.Value))
{
case 0: itemValue = "未定義"; break; // Not defined  
case 1: itemValue = "手動"; break; // Manual 
case 2: itemValue = "ノーマルプログラム"; break; // Normal program 
case 3: itemValue = "露出優先"; break;// Aperture priority
case 4: itemValue = "シャッター優先"; break; // Shutter priority
case 5: itemValue = "creativeプログラム（被写界深度方向にバイアス）"; break; // Creative program (biased toward depth of field)
case 6: itemValue = "actionプログラム（シャッタースピード高速側にバイアス）"; break;// Action program (biased toward fast shutter speed)
case 7: itemValue = "ポートレイトモード（クローズアップ撮影、背景はフォーカス外す）"; break; // Portrait mode (for closeup photos with the background out of focus)
case 8: itemValue = "ランドスケープモード（landscape 撮影、背景はフォーカス合う）"; break; // Landscape mode (for landscape photos with the background in focus)
default: itemValue = "保留されます"; break; // reserved
}
break;
case 0x9207: // 測光方式: MeteringMode  
switch (convertToInt16U(item.Value))
{
case 0: itemValue = "不明"; break; // unknown 
case 1: itemValue = "平均"; break; //Average 
case 2: itemValue = "中央重点"; break; // CenterWeightedAverage 
case 3: itemValue = "スポット"; break; // Spot
case 4: itemValue = "マルチスポット"; break; // MultiSpot
case 5: itemValue = "分割測光"; break; // Pattern
case 6: itemValue = "部分測光"; break; // Partial
case 255: itemValue = "その他"; break; // Other
default: itemValue = "保留されます"; break; // reserved
}
break;
case 0x9208: // 光源: LightSource  
{
switch (convertToInt16U(item.Value))
{
case 0: itemValue = "不明"; break; //unknown
case 1: itemValue = "昼光"; break; //Daylight
case 2: itemValue = "蛍光灯"; break; //Fluorescent
case 3: itemValue = "タングステン"; break; //Tungsten
case 4: itemValue = "フラッシュ*2.2"; break; //flash*2.2
case 9: itemValue = "晴れ*2.2"; break;
case 10: itemValue = "曇り*2.2"; break;
case 11: itemValue = "日陰*2.2"; break;
case 12: itemValue = "Daylight 蛍光灯(D 5700-7100K)*2.2"; break;
case 13: itemValue = "Day white 蛍光灯(N 4600-5400K)*2.2"; break;
case 14: itemValue = "Cool white 蛍光灯(W 3900-4500K)*2.2"; break;
case 15: itemValue = "White 蛍光灯(WW 3200-3700K)*2.2"; break;
case 17: itemValue = "標準光A"; break; //Standard light A
case 18: itemValue = "標準光 B"; break; //Standard light B
case 19: itemValue = "標準光 C"; break; //Standard light C
case 20: itemValue = "D55"; break;
case 21: itemValue = "D65"; break;
case 22: itemValue = "D75"; break;
case 23: itemValue = "D50*2.2"; break;
case 24: itemValue = "ISOスタジオ?タングステン*2.2"; break;
case 255: itemValue = "その他"; break; //other
default: itemValue = "保留されます"; break; //reserved
}
}
break;
case 0x9209://フラッシュ: Flash 
{
switch (convertToInt16U(item.Value))
{
case 0: itemValue = "フラッシュなし"; break; //No Flash 
case 1: itemValue = "フラッシュ発光"; break; //Flash fired
case 5: itemValue = "発光したが反射光は検出できなかった"; break; //Fired, Return not detected 
case 7: itemValue = "発光したが反射光は検出できなかった"; break; //Fired, Return detected 
case 8: itemValue = "オン、非発光"; break; //On, Did not fire
case 9: itemValue = "オン"; break; // On
case 24: itemValue = "自動、非発光"; break;// Auto, Did not fire 
case 25: itemValue = "自動、発光"; break;// Auto, Fired
case 32: itemValue = "フラッシュ機能なし"; break;// No flash function 
default: itemValue = "未定義"; break; //reserved
}
}
break;
//Exif Version 2.2で新たに規定された項目。
case 0xA401://　スペシャル?エフェクト: CustomRendered  
{
switch (convertToInt16U(item.Value))
{
//スペシャル?エフェクト利用の有無。
case 0: itemValue = "ノーマル処理"; break;
case 1: itemValue = "エフェクトあり"; break;
default: itemValue = "未定義"; break; //reserved
}
}
break;
case 0xA402://　露光モード: ExposureMode  
{
switch (convertToInt16U(item.Value))
{
case 0: itemValue = "自動露光"; break;
case 1: itemValue = "手動露光"; break; 
case 2: itemValue = "自動ブラケット"; break;
default: itemValue = "未定義"; break; //reserved
}
}
break;
case 0xA403://ホワイト?バランス: WhiteBalance  
{
switch (convertToInt16U(item.Value))
{
case 0: itemValue = "自動ホワイト?バランス"; break;
case 1: itemValue = "手動ホワイト?バランス"; break;
default: itemValue = "未定義"; break; //reserved
}
}
break;
case 0xA406://撮影シーン?タイプ: SceneCaptureType  
{
switch (convertToInt16U(item.Value))
{
case 0: itemValue = "スタンダード"; break;
case 1: itemValue = "ランドスケープ"; break;
case 2: itemValue = "ポートレイト"; break;
case 3: itemValue = "夜景"; break;
default: itemValue = "未定義"; break; //reserved
}
}
break; 
case 0xA408://コントラスト: Contrast  
{
switch (convertToInt16U(item.Value))
{
case 0: itemValue = "ノーマル"; break;
case 1: itemValue = "ソフト"; break;
case 2: itemValue = "ハード"; break;
default: itemValue = "未定義"; break; //reserved
}
}
break;
case 0xA409://飽和状態: Saturation  
{
switch (convertToInt16U(item.Value))
{
case 0: itemValue = "ノーマル"; break;
case 1: itemValue = "低飽和"; break;
case 2: itemValue = "高飽和"; break;
default: itemValue = "未定義"; break; //reserved
}
}
break;
case 0xA40A://シャープネス: Sharpness  
{
switch (convertToInt16U(item.Value))
{
case 0: itemValue = "ノーマル"; break;
case 1: itemValue = "ソフト"; break;
case 2: itemValue = "ハード"; break;
case 3: itemValue = "夜景"; break;
case 4: itemValue = "夜景"; break;
default: itemValue = "未定義"; break; //reserved
}
}
break;
case 0xA40C://被写体撮影モード: SubjectDistanceRange  
{
switch (convertToInt16U(item.Value))
{
case 0: itemValue = "不明"; break;
case 1: itemValue = "マクロ"; break;
case 2: itemValue = "近景"; break;
case 3: itemValue = "遠景"; break;
case 4: itemValue = "夜景"; break;
default: itemValue = "未定義"; break; //reserved
}
}
break;
default:
itemValue = convertToInt16U(item.Value).ToString();
break;
}
}
//4 = LONG A 32-bit (4 -byte) unsigned integer,
else if (item.Type == 0x4)
{ 
itemValue = convertToInt32U(item.Value).ToString();
}
//5 = RATIONAL Two LONGs. The first LONG is the numerator and the second LONG expresses the//denominator.,
else if (item.Type == 0x5)
{
// rational
byte[] n = new byte[item.Len / 2];
byte[] d = new byte[item.Len / 2];
Array.Copy(item.Value, 0, n, 0, item.Len / 2);
Array.Copy(item.Value, item.Len / 2, d, 0, item.Len / 2);
uint a = convertToInt32U(n);
uint b = convertToInt32U(d);
Rational r = new Rational(a, b); 
//convert here  
switch (item.Id)
{
case 0x9202: // 絞り（APEX） aperture
itemValue = "F" + Math.Round(Math.Pow(Math.Sqrt(2), r.ToDouble()), 2).ToString();
break;
case 0x920A://レンズ焦点距離: FocalLength  
itemValue = r.ToDouble().ToString()+" mm";
break;
case 0x829A://露出時間: ExposureTime  
itemValue = r.ToString("/") + " 秒";
break;
case 0x829D: // F-number
itemValue = "F/" + r.ToDouble().ToString();
break;
//Exif Version 2.2で新たに規定された項目。
case 0xA407://ゲイン?コントロール: GainControl  
{
switch ((int)r.ToDouble())
{
case 0: itemValue = "コントロールなし"; break;
case 1: itemValue = "低ゲイン?アップ"; break;
case 2: itemValue = "高ゲイン?アップ"; break;
case 3: itemValue = "低ゲイン?ダウン"; break;
case 4: itemValue = "高ゲイン?ダウン"; break;
default: itemValue = "未定義"; break; //reserved
}
}
break;
default:
itemValue = r.ToString("/");
break;
} 

}
//7 = UNDEFINED An 8-bit byte that can take any value depending on the field definition,
else if (item.Type == 0x7)
{
switch (item.Id)
{
case 0xA300://ファイルソース: FileSource  
{
if (item.Value[0] == 3)
{
itemValue = "デジタルカメラ";
}
else
{
itemValue = "保留されます";
}
break;
}
case 0xA301://シーンタイプ: SceneType  
if (item.Value[0] == 1)
// a directly photographed image
itemValue = "直接撮影された画像";
else
//Not a directly photographed image
itemValue = "直接撮影された画像ではない";
break;
case 0x9101://各コンポーネントの意味: ComponentsConfiguration  
//　0x04050600　:　RGB / 0x01020300　:　YCbCr 
string temp = BitConverter.ToString(item.Value).Substring(0,11);
switch (temp)
{
case "01-02-03-00":
itemValue = "RGB";
break;
case "04-05-06-00":
itemValue = "YCbCr";
break;
default:
itemValue = "未定義";
break;
}
break;
case 0x9286://ユーザーコメント 
//　ただし始めの8バイトは文字コードを示す。
string usercomment= BitConverter.ToString(item.Value).Substring(0,23);
switch (usercomment)
{
case "41-53-43-49-49-00-00-00":
itemValue = "ASCII";
break;
case "4A-49-53-00-00-00-00-00":
itemValue = "JIS";
break;
case "55-4E-49-43-4F-44-45-00":
itemValue = "Unicode";
break;
default:
itemValue = "未定義";
break;
}
break;
default:
itemValue = "-";
break;
}
} 
//9 = SLONG A 32-bit (4 -byte) signed integer (2's complement notation),
else if (item.Type == 0x9)
{
itemValue = convertToInt32(item.Value).ToString();
}
//10 = SRATIONAL Two SLONGs. The first SLONG is the numerator and the second SLONG is the
//denominator.
else if (item.Type == 0xA)
{
// unsigned rational
byte[] n = new byte[item.Len / 2];
byte[] d = new byte[item.Len / 2];
Array.Copy(item.Value, 0, n, 0, item.Len / 2);
Array.Copy(item.Value, item.Len / 2, d, 0, item.Len / 2);
int a = convertToInt32(n);
int b = convertToInt32(d);
Rational r = new Rational(a, b); 

// convert here
switch (item.Id)
{
case 0x9201: // シャッタースピード: ShutterSpeedValue 
itemValue = "1/" + Math.Round(Math.Pow(2, r.ToDouble()), 2).ToString()+"sec";
break;
case 0x9203://輝度値: BrightnessValue  
itemValue = Math.Round(r.ToDouble(), 4).ToString();
break;
default:
itemValue = r.ToString("/");
break;
}
}
// リストに追加する。
if (propertiesHash[itemName] == null)
propertiesHash.Add(itemName, itemValue);
// cat it too
data += itemValue;
data += this.separateString;
}
}
/// <summary>
/// 全ての項目と項目の値(項目間に/nで隔てる)を取得する。
/// </summary>
/// <returns></returns>
public override string ToString()
{
return data;
} 
/// <summary>
///　16-bit (2 -byte) signed short
/// </summary>
int convertToInt16(byte[] arr)
{
if (arr.Length != 2)
return 0;
else
return arr[1] << 8 | arr[0];
}
/// <summary>
/// SHORT：16-bit (2 -byte) unsigned integer
/// </summary>
uint convertToInt16U(byte[] arr)
{
if (arr.Length != 2)
return 0;
else
return Convert.ToUInt16(arr[1] << 8 | arr[0]);
} 

/// <summary>
/// SLONG: 32-bit (4 -byte) signed integer (2's complement notation)
/// </summary>
int convertToInt32(byte[] arr)
{
if (arr.Length != 4)
return 0;
else
return arr[3] << 24 | arr[2] << 16 | arr[1] << 8 | arr[0];
}
/// <summary>
/// LONG: 32-bit (4 -byte) unsigned integer
/// </summary>
uint convertToInt32U(byte[] arr)
{
if (arr.Length != 4)
return 0;
else
return Convert.ToUInt32(arr[3] << 24 | arr[2] << 16 | arr[1] << 8 | arr[0]);
} 
#region IEnumerable Members
public IEnumerator GetEnumerator()
{
// TODO: Add EXIFextractor.GetEnumerator implementation
return (new EXIFextractorEnumerator(this.propertiesHash));
}
#endregion
} 

#region EXIFextractorEnumerator class
/// <summary>
///dont touch this class. its for IEnumerator
/// </summary>
class EXIFextractorEnumerator : IEnumerator
{
Hashtable exifTable;
IDictionaryEnumerator index; 

internal EXIFextractorEnumerator(Hashtable exif)
{
this.exifTable = exif;
this.Reset();
index = exif.GetEnumerator();
} 

#region IEnumerator Members
public void Reset()
{
this.index = null;
} 
public object Current
{
get
{
return (new Pair(this.index.Key, this.index.Value));
}
} 

public bool MoveNext()
{
if (index != null && index.MoveNext())
return true;
else
return false;
} 
#endregion
}
#endregion 

#region Pair class
public class Pair
{
public string First;
public string Second;
public Pair(object key, object value)
{
this.First = key.ToString();
this.Second = value.ToString();
}
}
#endregion
}
