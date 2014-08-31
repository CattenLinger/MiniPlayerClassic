using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Un4seen.Bass;

namespace MiniPlayerClassic
{
    public partial class MainFrom : Form
    {
        public PlayerAgency Agency1;
        public string FilePath;

        public MainFrom()
        {
            InitializeComponent();

            Agency1 = new PlayerAgency(0, 0);
        }

        private void MainFrom_Load(object sender, EventArgs e)
        {
        /* If you want to close the splash of Bass.Net you need to regist at 
         * www.un4seen.com and input the registration code.
         * (At the initialization of PlayerAgency too.
         */ 
        //BassNet.Registration("your_email","your_code");
        }

        private void label2_Click(object sender, EventArgs e)
        {
            label2.Text = "ErrorCode: " + Agency1.ErrorCode.ToString();
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
            MessageBox.Show(Agency1.AgencyTextInfo());
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            Agency1.Pause();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            Agency1.Play();
        }

        private void toolStripSplitButton2_ButtonClick(object sender, EventArgs e)
        {

        }

        private void 删除所选ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
