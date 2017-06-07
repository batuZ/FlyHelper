using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 采集方案参数.功能区;

namespace 采集方案参数
{
    public partial class MastreWindow : Form
    {
        public MastreWindow()
        {
            InitializeComponent();
        }
        #region 获取窗体内参数
        //精度
        private void tetJingdu_Enter(object sender, EventArgs e)
        {
            zhuangtailan.Text = "精度是指照片上每个像素在实际中的大小,单位为厘米,必选!";
        }
        private void tetJingdu_Leave(object sender, EventArgs e)
        {
            try
            {
                Data.jingdu = Convert.ToDouble(tetJingdu.Text);
                label1.ForeColor = Color.Black;
            }
            catch
            {
                label1.ForeColor = Color.Red;
                // MessageBox.Show("精度是指照片上每个像素在实际中的大小，单位为厘米，请正确输入");
            }
            zhuangtailan.Text = "";
        }
        //面积
        private void tetMianji_Enter(object sender, EventArgs e)
        {
            zhuangtailan.Text = "此项为预估面积,单位为平方千米,必选!";
        }
        private void tetMianji_Leave(object sender, EventArgs e)
        {
            try
            {
                Data.mianji = Convert.ToDouble(tetMianji.Text);
                label2.ForeColor = Color.Black;
            }
            catch
            {
                label2.ForeColor = Color.Red;
                // MessageBox.Show("此项为预估面积，单位为平方千米，请正确输入");
            }
            zhuangtailan.Text = "";
        }
        //海拔高度
        private void checkHaiba_CheckedChanged(object sender, EventArgs e)
        {
            if (checkHaiba.Checked == true)
            {
                tethaiba.Enabled = true;
                zhuangtailan.Text = "此项为测区平均海拔高度，单位为米，用于输出航线方案。";
                butout.Enabled = true;
                tethaiba.Focus();
            }
            else
            {
                tethaiba.Enabled = false;
                zhuangtailan.Text = "";
                label5.ForeColor = Color.Black;
                butout.Enabled = false;
            }
        }
        private void tethaiba_Leave(object sender, EventArgs e)
        {
            try
            {
                Data.haiba = Convert.ToDouble(tethaiba.Text);
                label5.ForeColor = Color.Black;
            }
            catch
            {
                label5.ForeColor = Color.Red;
                //  MessageBox.Show("此项为测区平均海拔高度，单位为米，用于输出航线方案，请正确输入");
            }
            zhuangtailan.Text = "";
        }
        //航向 重叠率
        private void tetChongdieX_Enter(object sender, EventArgs e)
        {
            zhuangtailan.Text = "此项为 航向 重叠率百分比,必选!";
        }
        private void tetChongdieX_Leave(object sender, EventArgs e)
        {
            try
            {
                Data.chongdieX = Convert.ToInt32(tetChongdieX.Text);
                label8.ForeColor = Color.Black;
            }
            catch
            {
                label8.ForeColor = Color.Red;
                //  MessageBox.Show("此项为航向重叠率，单位为 %，请正确输入");
            }
            zhuangtailan.Text = "";
        }
        //旁向 重叠率
        private void tetChongdieY_Enter(object sender, EventArgs e)
        {
            zhuangtailan.Text = "此项为 旁向 重叠率百分比,必选!";
        }
        private void tetChongdieY_Leave(object sender, EventArgs e)
        {
            try
            {
                Data.chongdieY = Convert.ToInt32(tetChongdieY.Text);
                label13.ForeColor = Color.Black;
            }
            catch
            {
                label13.ForeColor = Color.Red;
                //  MessageBox.Show("此项为旁向重叠率，单位为 %，请正确输入");
            }
            zhuangtailan.Text = "";
        }
        //焦距
        private void tetJiaoju_Enter(object sender, EventArgs e)
        {
            zhuangtailan.Text = "此项为等效焦距，非物理焦距，请参考镜头参数正确输入,必选!";
        }
        private void tetJiaoju_Leave(object sender, EventArgs e)
        {
            try
            {
                Data.jiaoju = Convert.ToInt32(tetJiaoju.Text);
                label4.ForeColor = Color.Black;
            }
            catch
            {
                label4.ForeColor = Color.Red;
                //  MessageBox.Show("此项为等效焦距，非物理焦距，请参考镜头参数正确输入");
            }
            zhuangtailan.Text = "";
        }
        //长边像素
        private void tetChang_Enter(object sender, EventArgs e)
        {
            zhuangtailan.Text = "此项为图像长边像素数,必选!";
        }
        private void tetChang_Leave(object sender, EventArgs e)
        {
            try
            {
                Data.chang = Convert.ToDouble(tetChang.Text);
                label3.ForeColor = Color.Black;
            }
            catch
            {
                label3.ForeColor = Color.Red;
                //  MessageBox.Show("此项为图像长边像素数，请正确输入");
            }
            zhuangtailan.Text = "";
        }
        //短边像素
        private void tetKuan_Enter(object sender, EventArgs e)
        {
            zhuangtailan.Text = "此项为图像短边像素数,必选!";
        }
        private void tetKuan_Leave(object sender, EventArgs e)
        {
            try
            {
                Data.kuan = Convert.ToDouble(tetKuan.Text);
                label14.ForeColor = Color.Black;
            }
            catch
            {
                label14.ForeColor = Color.Red;
                //  MessageBox.Show("此项为图像短边像素数，请正确输入");
            }
            zhuangtailan.Text = "";
        }
        //飞行速度
        private void checkSudu_CheckedChanged(object sender, EventArgs e)
        {
            if (checkSudu.Checked == true)
            {
                tetSudu.Enabled = true;
                zhuangtailan.Text = "此项为飞机飞行速度，单位为 米/秒,用于计算触发时长。";
                tetSudu.Focus();
            }
            else
            {
                tetSudu.Enabled = false;
                zhuangtailan.Text = "";
                label6.ForeColor = Color.Black;
            }
        }
        private void tetSudu_Leave(object sender, EventArgs e)
        {
            try
            {
                Data.sudu = Convert.ToDouble(tetSudu.Text);
                label6.ForeColor = Color.Black;
            }
            catch
            {
                label6.ForeColor = Color.Red;
                //  MessageBox.Show("此项为飞机飞行速度，单位为 米/秒,用于计算触发时长,请正确输入");
            }
            zhuangtailan.Text = "";
        }
        //续航
        private void checkXuhang_CheckedChanged(object sender, EventArgs e)
        {
            if (checkXuhang.Checked == true)
            {
                tetXuhang.Enabled = true;
                zhuangtailan.Text = "飞机最大飞行时长。";
                tetXuhang.Focus();
            }
            else
            {
                tetXuhang.Enabled = false;
                zhuangtailan.Text = "";
                label7.ForeColor = Color.Black;
            }
        }
        private void tetXuhang_Leave(object sender, EventArgs e)
        {
            try
            {
                Data.xuhang = Convert.ToInt32(tetXuhang.Text);
                label7.ForeColor = Color.Black;
            }
            catch
            {
                label7.ForeColor = Color.Red;
            }
            zhuangtailan.Text = "";
        }
        #endregion

        #region 输出计算报告！
        private void butJisuan_Click(object sender, EventArgs e)
        {
            #region
            if (tetJingdu.Text == "")
            {
                MessageBox.Show("请定义精度要求");
                tetJingdu.Focus();
                return;
            }
            else if (tetMianji.Text == "")
            {
                MessageBox.Show("请定义面积");
                tetMianji.Focus();
                return;
            }
            else if (tetChongdieX.Text == "")
            {
                MessageBox.Show("请定义航向重叠率");
                tetChongdieX.Focus();
                return;
            }
            else if (tetChongdieY.Text == "")
            {
                MessageBox.Show("请定义旁向重叠率");
                tetChongdieY.Focus();
                return;
            }
            else if (tetJiaoju.Text == "")
            {
                MessageBox.Show("请定义等效焦距");
                tetJiaoju.Focus();
                return;
            }
            else if (tetChang.Text == "")
            {
                MessageBox.Show("请定义画幅长边像素数");
                tetChang.Focus();
                return;
            }
            else if (tetKuan.Text == "")
            {
                MessageBox.Show("请定义画幅短边像素数");
                tetKuan.Focus();
                return;
            }
            else if (checkHaiba.Checked == true && tethaiba.Text == "")
            {
                MessageBox.Show("请输入测区平均海拔高度");
                tethaiba.Focus();
                return;
            }
            else if (checkSudu.Checked == true && tetSudu.Text == "")
            {
                MessageBox.Show("请输入飞行速度");
                tetSudu.Focus();
                return;
            }
            else if (checkXuhang.Checked == true && tetXuhang.Text == "")
            {
                MessageBox.Show("请输入续航时间,单位为分钟");
                tetXuhang.Focus();
                return;
            }
            #endregion
            Data.jingdu = Convert.ToDouble(tetJingdu.Text);
            Data.mianji = Convert.ToDouble(tetMianji.Text);
            //Data.haiba = Convert.ToDouble(tethaiba.Text);
            Data.chongdieX = Convert.ToInt32(tetChongdieX.Text);
            Data.chongdieY = Convert.ToInt32(tetChongdieY.Text);
            Data.jiaoju = Convert.ToInt32(tetJiaoju.Text);
            Data.chang = Convert.ToDouble(tetChang.Text);
            Data.kuan = Convert.ToDouble(tetKuan.Text);


            /////////////////////////////////////////////////////////////////////////////


            tetBaoguao.Text = "相对飞行高度:\t" + JiSuan.gaodu().ToString() + "米"
                + "\r\n\r\n航线间距:\t" + Convert.ToString(JiSuan.jianju()) + "米"
                + "\r\n\r\n触发距离:\t" + Convert.ToString(JiSuan.chufaJL()) + "米"
                + "\r\n\r\n飞行总里程:\t" + JiSuan.zonglicheng().ToString("0.00") + "千米"
                + "\r\n\r\n总照片量:\t" + Convert.ToString(JiSuan.zongzhaopian()) + "张"
                + "\r\n\r\n-----------------------------------\r\n\r\n";
            if (checkSudu.Checked == true)
            {
                tetBaoguao.Text += "触发时间:\t" + Convert.ToString(JiSuan.chufaSJ()) + "秒\r\n\r\n";
            }
            if (checkXuhang.Checked == true)
            {
                tetBaoguao.Text += "预计起降数:\t" + Convert.ToString(JiSuan.jiaciXH()) + "架次";
            }
        }
        #endregion



        private void button2_Click(object sender, EventArgs e)
        {
            Data.fxcs[0] = JiSuan.gaodu();
            Data.fxcs[1] = JiSuan.jianju();
            Data.fxcs[2] = JiSuan.chufaJL();
            try
            {
                Data.fxcs[3] = Convert.ToDouble(tethaiba.Text);
            }
            catch
            {
                MessageBox.Show("请认真告诉我测区平均海拔高度!");
                tethaiba.Focus();
                return;
            }
            
            WinOutput1 FormShuchu = new WinOutput1();
            FormShuchu.ShowDialog();
        }

        #region 照片加GPS
      


        //获得文件夹路径/////////////
        string dirPath = "";
        private void buttonGPS_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = false;
            if (folderDlg.ShowDialog() == DialogResult.OK)
                dirPath = folderDlg.SelectedPath;
            textBox1.Text = dirPath;
        }
        //获得表路径/////////////
        string excelPath = "";
        DataSet _myDs;
        private void buttonGPS3_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            textBox2.Text = openFileDialog1.FileName;
        }

        private void buttonGPS4_Click(object sender, EventArgs e)
        {
            int _allCount = 0;
            int _exCount = 0;

            excelPath = textBox2.Text;
            Picturexif kk = new Picturexif();
            _myDs = kk.ExcelToDS2(excelPath);
            try
            {
                foreach (DataTable dt in _myDs.Tables)
                {
                    int _startRow = 1;
                    double tempNum = 0;
                    for (int m = 0; m < dt.Columns.Count; m++)
                    {
                        if (double.TryParse(dt.Rows[0].ItemArray[m].ToString(), out tempNum))
                        {
                            _startRow = 0;
                            break;
                        }
                    }
                    _allCount = dt.Rows.Count - _startRow;
                    for (int i = _startRow; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        string picName = dr.ItemArray[0].ToString();
                        double x = Convert.ToDouble(dr.ItemArray[1]);
                        double y = Convert.ToDouble(dr.ItemArray[2]);
                        double z = Convert.ToDouble(dr.ItemArray[3]);
                        Picturexif kkk = new Picturexif();
                        string _picname = kkk.FindFilepathUnderDir(new DirectoryInfo(dirPath), picName);
                        if (_picname != null)
                        {
                            Picturexif jj = new Picturexif();
                            jj.AddInfo2Image(dirPath + "\\" + _picname, x, y, z);
                            _exCount++;
                        }
                    }
                }
                MessageBox.Show("Excel表格共" + _allCount + "条记录; \n 总共执行了" + _exCount + "条数据!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        #endregion

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                tetChang.Text = "7360";
                tetKuan.Text = "4912";
            }
            if (comboBox1.SelectedIndex == 1)
            {
                tetChang.Text = "5472";
                tetKuan.Text = "3648";
            }
            if (comboBox1.SelectedIndex == 2)
            {
                tetChang.Text = "4912";
                tetKuan.Text = "3264";
            }
            if (comboBox1.SelectedIndex == 3)
            {
                tetChang.Text = "7952";
                tetKuan.Text = "5304";
            }
        }

    }
        
}
