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
        public PlayerAgency Agency1;
        public string FilePath;

        public MainFrom()
        {
            InitializeComponent();

            Agency1 = new PlayerAgency();
        }

        private void MainFrom_Load(object sender, EventArgs e)
        {
        /* If you want to close the splash of Bass.Net you need to regist at 
         * www.un4seen.com and input the registration code.
         * (At the initialization of PlayerAgency too.
         */ 
        //BassNet.Registration("your_email","your_code");
            
        }

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
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            Agency1.Pause();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            Agency1.Play();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Agency1.SetVolume((float)trackBar1.Value / (float)trackBar1.Maximum);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void trackBar2_MouseUp(object sender, MouseEventArgs e)
        {
            Agency1.SetPosition((double)trackBar2.Value / 1000);
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            trackBar2.Value = (int)(Agency1.GetPosition() * 1000);
        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show(Agency1.AgencyTextInfo());
        }
    }
}
