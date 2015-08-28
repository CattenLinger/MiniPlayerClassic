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
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("启动一些开发的时候会用到的功能\n可能会影响正常使用\n不建议正常使用时开启。\n\n这些功能有:\n"+
                            "在终端显示键盘按键值"+
                            "在终端显示窗体高度");
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
