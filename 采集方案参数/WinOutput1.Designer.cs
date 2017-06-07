namespace 采集方案参数
{
    partial class WinOutput1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.savepath = new System.Windows.Forms.TextBox();
            this.checkBoxBuffer = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openpath = new System.Windows.Forms.TextBox();
            this.checkBoxHangxian = new System.Windows.Forms.CheckBox();
            this.checkBoxCam = new System.Windows.Forms.CheckBox();
            this.checkBoxSub = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // savepath
            // 
            this.savepath.Location = new System.Drawing.Point(20, 61);
            this.savepath.Name = "savepath";
            this.savepath.Size = new System.Drawing.Size(245, 21);
            this.savepath.TabIndex = 0;
            // 
            // checkBoxBuffer
            // 
            this.checkBoxBuffer.AutoSize = true;
            this.checkBoxBuffer.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxBuffer.Location = new System.Drawing.Point(143, 102);
            this.checkBoxBuffer.Name = "checkBoxBuffer";
            this.checkBoxBuffer.Size = new System.Drawing.Size(84, 16);
            this.checkBoxBuffer.TabIndex = 1;
            this.checkBoxBuffer.Text = "启用安全区\r\n";
            this.checkBoxBuffer.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxBuffer.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(281, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "导入范围";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(281, 59);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "储存位置";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(281, 105);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 35);
            this.button3.TabIndex = 2;
            this.button3.Text = "导出";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "D:/";
            this.openFileDialog1.Filter = "shp file|*.shp|KML|*.kml";
            this.openFileDialog1.FilterIndex = 2;
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "shp file|*.shp|KML|*.kml|Mission Planner\\TXT|*.txt|大疆AWM|*.awm";
            this.saveFileDialog1.FilterIndex = 2;
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // openpath
            // 
            this.openpath.Location = new System.Drawing.Point(20, 22);
            this.openpath.Name = "openpath";
            this.openpath.Size = new System.Drawing.Size(245, 21);
            this.openpath.TabIndex = 0;
            // 
            // checkBoxHangxian
            // 
            this.checkBoxHangxian.AutoSize = true;
            this.checkBoxHangxian.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxHangxian.Checked = true;
            this.checkBoxHangxian.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxHangxian.Location = new System.Drawing.Point(31, 102);
            this.checkBoxHangxian.Name = "checkBoxHangxian";
            this.checkBoxHangxian.Size = new System.Drawing.Size(48, 16);
            this.checkBoxHangxian.TabIndex = 1;
            this.checkBoxHangxian.Text = "航线";
            this.checkBoxHangxian.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxHangxian.UseVisualStyleBackColor = true;
            // 
            // checkBoxCam
            // 
            this.checkBoxCam.AutoSize = true;
            this.checkBoxCam.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxCam.Location = new System.Drawing.Point(31, 124);
            this.checkBoxCam.Name = "checkBoxCam";
            this.checkBoxCam.Size = new System.Drawing.Size(72, 16);
            this.checkBoxCam.TabIndex = 1;
            this.checkBoxCam.Text = "相机点位";
            this.checkBoxCam.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxCam.UseVisualStyleBackColor = true;
            this.checkBoxCam.CheckedChanged += new System.EventHandler(this.checkBoxCam_CheckedChanged);
            // 
            // checkBoxSub
            // 
            this.checkBoxSub.AutoSize = true;
            this.checkBoxSub.Enabled = false;
            this.checkBoxSub.Location = new System.Drawing.Point(143, 124);
            this.checkBoxSub.Name = "checkBoxSub";
            this.checkBoxSub.Size = new System.Drawing.Size(60, 16);
            this.checkBoxSub.TabIndex = 3;
            this.checkBoxSub.Text = "分相机";
            this.checkBoxSub.UseVisualStyleBackColor = true;
            // 
            // WinOutput1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 152);
            this.Controls.Add(this.checkBoxSub);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBoxCam);
            this.Controls.Add(this.checkBoxHangxian);
            this.Controls.Add(this.checkBoxBuffer);
            this.Controls.Add(this.savepath);
            this.Controls.Add(this.openpath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WinOutput1";
            this.Text = "输出方案";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox savepath;
        private System.Windows.Forms.CheckBox checkBoxBuffer;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TextBox openpath;
        private System.Windows.Forms.CheckBox checkBoxHangxian;
        private System.Windows.Forms.CheckBox checkBoxCam;
        private System.Windows.Forms.CheckBox checkBoxSub;
    }
}