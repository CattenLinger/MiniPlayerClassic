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
        public c_VolumeBar cVolumeBar;

        public string LabelText;
        private Graphics pb_g_enter;
        private Graphics pb_g_enter2;

        //init
        public MainFrom()
        {
            InitializeComponent();

            LabelText = "暂无播放任务";
            pb_g_enter = pb_Progress.CreateGraphics();
            pb_g_enter2 = pb_Volume.CreateGraphics();

            Agency1 = new Player();

            cProgressBar = new c_ProgressBar(pb_Progress.Width, pb_Progress.Height);
            cProgressBar.pb_text = LabelText;
            cProgressBar.pb_maxvalue = 10;
            cProgressBar.pb_value = 0;

            cVolumeBar = new c_VolumeBar(pb_Volume.Width,pb_Volume.Height);
            cVolumeBar.pb_text = "音量";
            cVolumeBar.pb_maxvalue = 100;
            cVolumeBar.pb_value = 100;
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
        //Events Checker
        private void tmrEvents_Tick(object sender, EventArgs e)
        {

            if (Agency1.PlayState != 1)
            { btnPlay.ImageIndex = 2; }
            else { btnPlay.ImageIndex = 1; }
     
        }

        #region About Drawing the ProgressBar
        private void pb_Progress_MouseDown(object sender, MouseEventArgs e)
        {
            tmrPGBar.Enabled = false;
            int temp;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                temp = (int)((float)cProgressBar.pb_maxvalue * ((float)e.X / (float)cProgressBar.width));
                cProgressBar.pb_value = temp;
                cProgressBar.pb_text2 = Agency1.trans_Time
                    (cProgressBar.pb_value, Player.t_formate.full_minute) +
                    "|" + Agency1.trans_Time(cProgressBar.pb_maxvalue - cProgressBar.pb_value, Player.t_formate.full_minute);
                cProgressBar.DrawBar(pb_g_enter);
            }
        }

        private void pb_Progress_MouseMove(object sender, MouseEventArgs e)
        {
            int temp;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                temp = (int)((float)cProgressBar.pb_maxvalue * ((float)e.X / (float)cProgressBar.width));
                cProgressBar.pb_value = temp;
                cProgressBar.pb_text2 = Agency1.trans_Time
                    (cProgressBar.pb_value, Player.t_formate.full_minute) + 
                    "|" + Agency1.trans_Time(cProgressBar.pb_maxvalue - cProgressBar.pb_value, Player.t_formate.full_minute);
                cProgressBar.DrawBar(pb_g_enter);
            }
        }

        private void pb_Progress_MouseUp(object sender, MouseEventArgs e)
        {
            tmrPGBar.Enabled = true;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            { Agency1.SetPosition((double)cProgressBar.pb_value / 1000); }
        }
        #endregion

        #region Bar's Timer
        private void tmrPGBars_Tick(object sender, EventArgs e)
        {
            double temp;
            temp = Agency1.GetPosition();
            if (temp == -1) { temp = 0; }
            cProgressBar.pb_value = (int)(temp * 1000);
            cProgressBar.DrawBar(pb_g_enter);
            cProgressBar.pb_text2 = Agency1.trans_Time
                (cProgressBar.pb_value, Player.t_formate.full_minute) +
                "|" + Agency1.trans_Time(cProgressBar.pb_maxvalue - cProgressBar.pb_value, Player.t_formate.full_minute);
        }

        private void tmrVBar_Tick(object sender, EventArgs e)
        {
            cVolumeBar.DrawBar(pb_g_enter2);
            int left = 0, right = 0;
            Agency1.GetLevel(ref left,ref right);
            cVolumeBar.tellitlevel(left,right);
        }

        #endregion
        #region VolumeBar
        private void pb_Volume_MouseDown(object sender, MouseEventArgs e)
        {
            //tmrVBar.Enabled = false;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                cVolumeBar.pb_value = (int)((float)cVolumeBar.pb_maxvalue * ((float)e.X/(float)pb_Volume.Width));
                Agency1.SetVolume((float)cVolumeBar.pb_value / (float)cVolumeBar.pb_maxvalue);
                cVolumeBar.DrawBar(pb_g_enter2);
            }
        }

        private void pb_Volume_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                cVolumeBar.pb_value = (int)((float)cVolumeBar.pb_maxvalue * ((float)e.X / (float)pb_Volume.Width));
                Agency1.SetVolume((float)cVolumeBar.pb_value / (float)cVolumeBar.pb_maxvalue);
                cVolumeBar.DrawBar(pb_g_enter2);
            }
        }

        private void pb_Volume_MouseUp(object sender, MouseEventArgs e)
        {
            //tmrVBar.Enabled = true;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                cVolumeBar.pb_value = (int)((float)cVolumeBar.pb_maxvalue * ((float)e.X / (float)pb_Volume.Width));
                Agency1.SetVolume((float)cVolumeBar.pb_value / (float)cVolumeBar.pb_maxvalue);
                cVolumeBar.DrawBar(pb_g_enter2);
            }
        }
        #endregion
    }
}
