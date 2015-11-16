using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MiniPlayerClassic.Core;

namespace MiniPlayerClassic
{
    public partial class frmSettings : Form
    {
        ConfigManager conmgr;

        public frmSettings(ConfigManager configManager)
        {
            InitializeComponent();
            this.conmgr = configManager;
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            if (!conmgr.Read())
            {
                conmgr.SetDefault();
                if (!conmgr.Save())
                {
                    MessageBox.Show("配置文件读取出错。");
                    Dispose();
                    return;
                }
            }

            refreshInterface();
        }

        private void refreshInterface()
        {
            chkDevelopMode.Checked = conmgr.IsDeveloperMode;
            chkCreateWhenOpen.Checked = conmgr.NewListAtLaunch;
            chkForceWindow.Checked = conmgr.WindowAlwaysTop;
            chkRemeberLast.Checked = conmgr.RememberLists;
            tbListFolder.Text = conmgr.ListFilesPath;
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("启动一些开发的时候会用到的功能\n可能会影响正常使用\n不建议正常使用时开启。\n\n这些功能有:\n"+
                            "在终端显示键盘按键值"+
                            "在终端显示窗体高度");
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            conmgr.IsDeveloperMode = chkDevelopMode.Checked;
            conmgr.NewListAtLaunch = chkCreateWhenOpen.Checked;
            conmgr.WindowAlwaysTop = chkForceWindow.Checked;
            conmgr.RememberLists = chkRemeberLast.Checked;
            conmgr.ListFilesPath = tbListFolder.Text;
            if(!conmgr.Save()) MessageBox.Show("保存失败");
            Dispose();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            conmgr.SetDefault();
            refreshInterface();
        }
    }
}
