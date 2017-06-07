using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using 采集方案参数.功能区;

namespace 采集方案参数
{
    public partial class WinOutput1 : Form
    {
        public WinOutput1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            openpath.Text = openFileDialog1.FileName.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            savepath.Text = saveFileDialog1.FileName.ToString();
            Data.savePath = saveFileDialog1.FileName;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //各选项是否为空,空则不响应
            if (openFileDialog1.FileName == "" || saveFileDialog1.FileName == "") //选项是否为空
            { return; }
            if (!checkBoxHangxian.Checked && !checkBoxCam.Checked)
            { return; }
            //黑掉按键开始计算
            button3.Enabled = false;

            //判断格式\输出一个Layer转换为point[]\重投影
            Point[] oriFanwei = OpenFile.openToPoint(openFileDialog1.FileName);
            Point[] testFanwei = null;
            if (OpenFile.IsWGS(oriFanwei))
            { testFanwei = CoorT.wgs84ToMercator(oriFanwei); }
            else
            { testFanwei = oriFanwei; }

            //是否需要加Buffer\把范围传出去
            Point[] myFanwei = null;
            if (checkBoxBuffer.Checked)//Buffer选项
            {
                myFanwei = 采集方案参数.功能区.Buffer.bufferLine(testFanwei);
            }
            else
            {
                myFanwei = testFanwei;
            }

            //求最小外接矩形
            Point[] minOutRec = Xuanzhuan.minOutRec(myFanwei);

            //求相机点位
            if (checkBoxCam.Checked)
            {
                Point[] tempCam = Hangxian.camPoint(minOutRec);
                //获取subCameras的旋转中心 --> tempSub
                Data.tempSub = tempCam;
                //旋转回原坐标
                Data.相机点 = Xuanzhuan.backRec(tempCam);
                Data.fxcs[6] = Data.相机点.Length;
                //还原坐标系
                if (OpenFile.IsWGS(oriFanwei))
                { Data.相机点 = CoorT.MercatorToWgs84(Data.相机点); }
            }
         
            //计算航线
            if (checkBoxHangxian.Checked)
            {
                LineD[] tempLines = Hangxian.hxLine(minOutRec);
                //旋转回原坐标
                Data.航线 = Xuanzhuan.backRec(tempLines);
                for (int i = 0; i < Data.航线.Length; i++)
                {
                    Data.fxcs[5] += JiSuan.lineLength(Data.航线[i]);
                }
                //还原坐标系
                if (OpenFile.IsWGS(oriFanwei))
                { Data.航线 = CoorT.MercatorToWgs84(Data.航线); }
            }


            //还原范围
            if (checkBoxBuffer.Checked)
            {
                Data.Buffer范围 = Xuanzhuan.backRec(minOutRec);
                Data.fxcs[4] = JiSuan.polyArea(Data.Buffer范围);

                if (OpenFile.IsWGS(oriFanwei))
                { Data.Buffer范围 = CoorT.MercatorToWgs84(Data.Buffer范围); }
            }

            //输出Subcamera
            if (checkBoxSub.Checked)
            {
                List<Point[]> temC = Hangxian.subCenter(Data.tempSub);
                List<Point[]> temCC = new List<Point[]>();
                List<Point[]> temL = Hangxian.subLeft(temC);
                List<Point[]> temLL = new List<Point[]>();
                List<Point[]> temR = Hangxian.subRight(temC);
                List<Point[]> temRR = new List<Point[]>();
                List<Point[]> temF = Hangxian.subFront(temC);
                List<Point[]> temFF = new List<Point[]>();
                List<Point[]> temB = Hangxian.subBack(temC);
                List<Point[]> temBB = new List<Point[]>();
                if (OpenFile.IsWGS(oriFanwei))
                {
                    for (int i = 0; i < temC.Count; i++)
                    {
                        temCC.Add(CoorT.MercatorToWgs84(Xuanzhuan.backRec(temC[i])));
                        temLL.Add(CoorT.MercatorToWgs84(Xuanzhuan.backRec(temL[i])));
                        temRR.Add(CoorT.MercatorToWgs84(Xuanzhuan.backRec(temR[i])));
                        temFF.Add(CoorT.MercatorToWgs84(Xuanzhuan.backRec(temF[i])));
                        temBB.Add(CoorT.MercatorToWgs84(Xuanzhuan.backRec(temB[i])));
                    }
                }
                else
                {
                    for (int i = 0; i < temC.Count; i++)
                    {
                        temCC.Add(Xuanzhuan.backRec(temC[i]));
                        temLL.Add(Xuanzhuan.backRec(temL[i]));
                        temRR.Add(Xuanzhuan.backRec(temR[i]));
                        temFF.Add(Xuanzhuan.backRec(temF[i]));
                        temBB.Add(Xuanzhuan.backRec(temB[i]));
                    }
                }
                Data.subCenter = temCC;
                Data.subLeft = temLL;
                Data.subRight = temRR;
                Data.subFront = temFF;
                Data.subBack = temBB;
            }

            //第七步.输出文件
            OutPutFile.saveFile();
            MessageBox.Show("输出成功");
            Close();
        }

        private void checkBoxCam_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCam.Checked)
            {
                checkBoxSub.Enabled = true;
            }
            else
            {
                checkBoxSub.Enabled = false;
                checkBoxSub.Checked = false;
            }
        }
    }
}
