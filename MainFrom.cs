using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace MiniPlayerClassic
{
    public partial class MainFrom : Form
    {
        public Player Agency1;
        public c_ProgressBar cProgressBar;
        public string LabelText;

        //init
        public MainFrom()
        {
            InitializeComponent();
            LabelText = "暂无播放任务";
            Agency1 = new Player();
            cProgressBar = new c_ProgressBar(pb_Progress.Width, pb_Progress.Height);
            cProgressBar.pb_text = LabelText;
        }
        //init
        private void MainFrom_Load(object sender, EventArgs e)
        {
        /* If you want to close the splash of Bass.Net you need to regist at 
         * www.un4seen.com and input the registration code.
         * (At the initialization of PlayerAgency too.
         */ 
        //BassNet.Registration("your_email","your_code");
            
        }
        //Stop Button
        private void btnStop_Click(object sender, EventArgs e)
        {
            Agency1.Stop();
        }
        
        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg1 = new OpenFileDialog();
            dlg1.ShowDialog();
            Agency1.LoadFile(dlg1.FileName);
            LabelText = System.IO.Path.GetFileName(dlg1.FileName);

            cProgressBar.pb_maxvalue = (int)(Agency1.GetLength() * 1000);
            cProgressBar.pb_text = LabelText;
        }
        //Play/Pause button
        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (Agency1.PlayState != 1) { Agency1.Play(); } else { Agency1.Pause(); }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Agency1.SetVolume((float)trackBar1.Value / (float)trackBar1.Maximum);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {

        }

        private void trackBar2_MouseUp(object sender, MouseEventArgs e)
        {

        }

        //Events Checker
        private void tmrEvents_Tick(object sender, EventArgs e)
        {
            double temp;

            if (Agency1.PlayState != 1)
            { btnPlay.ImageIndex = 2; }
            else { btnPlay.ImageIndex = 1; }

            temp = Agency1.GetPosition();
            if (temp == -1) { return; }

            cProgressBar.pb_value = (int)(temp * 1000);
        }

        private void pb_Progress_MouseDown(object sender, MouseEventArgs e)
        {
            tmrEvents.Enabled = false;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            { cProgressBar.pb_value = (int)((float)cProgressBar.pb_maxvalue * ((float)e.X / (float)cProgressBar.width)); }
        }

        private void pb_Progress_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            { cProgressBar.pb_value = (int)((float)cProgressBar.pb_maxvalue * ((float)e.X / (float)cProgressBar.width)); }
        }

        private void pb_Progress_MouseUp(object sender, MouseEventArgs e)
        {
            tmrEvents.Enabled = true;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            { Agency1.SetPosition((double)cProgressBar.pb_value / 1000); }
        }


        private void tmrBars_Tick(object sender, EventArgs e)
        {
            cProgressBar.DrawBar(pb_Progress.CreateGraphics());
        }
    }
}
