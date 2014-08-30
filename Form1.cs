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
        
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Agency1.Pause();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg1 = new OpenFileDialog();
            dlg1.ShowDialog();
            FilePath = dlg1.FileName;
            Agency1.LoadFile(FilePath);
            label1.Text = FilePath;
            MessageBox.Show(Agency1.AgencyTextInfo());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Agency1.Play();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Agency1.Stop();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            label2.Text = "ErrorCode: " + Agency1.ErrorCode.ToString();
        }
    }
}
