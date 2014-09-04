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
        public string FilePath;

        //init
        public MainFrom()
        {
            InitializeComponent();

            Agency1 = new Player();
            cProgressBar = new c_ProgressBar(pb_Progress.Width, pb_Progress.Height);
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
            FilePath = dlg1.FileName;
            Agency1.LoadFile(FilePath);
            label1.Text = FilePath;
            
            trackBar2.Maximum = (int)(Agency1.GetLength() * 1000);
            cProgressBar.pb_maxvalue = (int)(Agency1.GetLength() * 1000);
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
            tmrEvents.Enabled = false;
        }

        private void trackBar2_MouseUp(object sender, MouseEventArgs e)
        {
            Agency1.SetPosition((double)trackBar2.Value / 1000);
            tmrEvents.Enabled = true;
        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show(Agency1.AgencyTextInfo());
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
            trackBar2.Value = (int)(temp * 1000);
            cProgressBar.pb_value = (int)(temp * 1000);
        }

        private void pb_Progress_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void pb_Progress_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void pb_Progress_MouseUp(object sender, MouseEventArgs e)
        {

        }


        private void tmrBars_Tick(object sender, EventArgs e)
        {
            cProgressBar.DrawBar(pb_Progress.CreateGraphics());
        }
    }
}
