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
        public Player MainPlayer; //播放器对象
        public c_ProgressBar cProgressBar; //进度条对象
        public c_VolumeBar cVolumeBar; //音量条对象

        public string LabelText; //标签文字
        private Graphics pb_g_enter;//画布
        private Graphics pb_g_enter2;

        public PlayList pl_main; //播放列表对象

        private int def_height;//默认尺寸
        private int min_height;
        private Boolean is_Minisize;//界面是否在迷你模式

        //一些东西的初始化
        public MainFrom()
        {
            InitializeComponent();

            LabelText = "暂无播放任务";
            def_height = this.Height;
            min_height = this.Height - panel1.Height;
            is_Minisize = true;

            pb_g_enter = pb_Progress.CreateGraphics(); //初始化进度条们的画布
            pb_g_enter2 = pb_Volume.CreateGraphics();

            MainPlayer = new Player(this.Handle); //初始化播放器对象
            MainPlayer.call_StateChange += MainPlayer_call_StateChange;  //注册播放器改变播放状态的事件的响应函数

            pl_main = new PlayList(); // 初始化播放列表

            cProgressBar = new c_ProgressBar(pb_Progress.Width, pb_Progress.Height); //初始化进度条
            cProgressBar.pb_text = LabelText;
            cProgressBar.pb_maxvalue = 10;
            cProgressBar.pb_value = 0;

            cVolumeBar = new c_VolumeBar(pb_Volume.Width,pb_Volume.Height); //初始化音量条
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
            MainPlayer.Stop();
        }
        //Play/Pause button
        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (MainPlayer.PlayState != 1) { MainPlayer.Play(); } else { MainPlayer.Pause(); }
        }
//--------Events Checker--------------------------------------------------------------------------
        void MainPlayer_call_StateChange(object sender, PlayerStateMessage e) //响应播放状态改变的消息的函数
        {
            if (e.Message == 10)
            {
                LabelText = System.IO.Path.GetFileName(MainPlayer.FilePath);
                cProgressBar.pb_text = LabelText;
                cProgressBar.pb_maxvalue = (int)(MainPlayer.GetLength() * 1000);
            }else 
                if ((e.Message < 10) && (e.Message != 1)) 
                    { btnPlay.ImageIndex = 2; } else { btnPlay.ImageIndex = 1; }
            //throw new NotImplementedException();
        }
//------Window-size change------------------------------------------------------------------------
        public void to_Minisize(Boolean animate)
        {

            this.Height = min_height;
            is_Minisize = true;
        }

        public void to_NormalSize(Boolean animate)
        {

            this.Height = def_height;
            is_Minisize = false;
        }
//------------------------------------------------------------------------------------------------

        #region About Drawing the ProgressBar //这里因为都是些很易懂的过程就懒得注释了
        private void pb_Progress_MouseDown(object sender, MouseEventArgs e) 
        {
            tmrPGBar.Enabled = false;
            int temp;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                temp = (int)((float)cProgressBar.pb_maxvalue * ((float)e.X / (float)cProgressBar.width));
                cProgressBar.pb_value = temp;
                cProgressBar.pb_text2 = (MainPlayer.trans_Time
                    (cProgressBar.pb_value, Player.t_formate.full_minute) +
                    "|" + MainPlayer.trans_Time(cProgressBar.pb_maxvalue - cProgressBar.pb_value, Player.t_formate.full_minute));
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
                cProgressBar.pb_text2 = (MainPlayer.trans_Time
                    (cProgressBar.pb_value, Player.t_formate.full_minute) + 
                    "|" + MainPlayer.trans_Time(cProgressBar.pb_maxvalue - cProgressBar.pb_value, Player.t_formate.full_minute));
                cProgressBar.DrawBar(pb_g_enter);
            }
        }

        private void pb_Progress_MouseUp(object sender, MouseEventArgs e)
        {
            tmrPGBar.Enabled = true;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            { MainPlayer.SetPosition((double)cProgressBar.pb_value / 1000); }
        }
        #endregion

        #region Bar's Timer
        private void tmrPGBars_Tick(object sender, EventArgs e)
        {
            double temp;
            temp = MainPlayer.GetPosition();
            if (temp == -1) { temp = 0; }
            cProgressBar.pb_value = (int)(temp * 1000);
            cProgressBar.DrawBar(pb_g_enter);
            cProgressBar.pb_text2 = MainPlayer.trans_Time(cProgressBar.pb_value, Player.t_formate.full_minute)
                                    + "|" + MainPlayer.trans_Time(cProgressBar.pb_maxvalue - cProgressBar.pb_value, Player.t_formate.full_minute);
            
        }

        private void tmrVBar_Tick(object sender, EventArgs e)
        {
            cVolumeBar.DrawBar(pb_g_enter2);
            int left = 0, right = 0;
            MainPlayer.GetLevel(ref left,ref right);
            cVolumeBar.tellitlevel(left,right);
            MainPlayer.getData(cVolumeBar.fft_data);
        }

        #endregion

        #region VolumeBar
        private void pb_Volume_MouseDown(object sender, MouseEventArgs e)
        {
            //tmrVBar.Enabled = false;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                cVolumeBar.pb_value = (int)((float)cVolumeBar.pb_maxvalue * ((float)e.X/(float)pb_Volume.Width));
                MainPlayer.SetVolume((float)cVolumeBar.pb_value / (float)cVolumeBar.pb_maxvalue);
                cVolumeBar.DrawBar(pb_g_enter2);
            }
        }

        private void pb_Volume_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                cVolumeBar.pb_value = (int)((float)cVolumeBar.pb_maxvalue * ((float)e.X / (float)pb_Volume.Width));
                MainPlayer.SetVolume((float)cVolumeBar.pb_value / (float)cVolumeBar.pb_maxvalue);
                cVolumeBar.DrawBar(pb_g_enter2);
            }
        }

        private void pb_Volume_MouseUp(object sender, MouseEventArgs e)
        {
            //tmrVBar.Enabled = true;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                cVolumeBar.pb_value = (int)((float)cVolumeBar.pb_maxvalue * ((float)e.X / (float)pb_Volume.Width));
                MainPlayer.SetVolume((float)cVolumeBar.pb_value / (float)cVolumeBar.pb_maxvalue);
                cVolumeBar.DrawBar(pb_g_enter2);
            }
        }
        #endregion

        private void tbtnAdd_ButtonClick(object sender, EventArgs e) //“添加文件”按钮
        {
            OpenFileDialog dlg1 = new OpenFileDialog(); //创建一个文件打开窗口对象
            dlg1.Multiselect = true; //允许文件打开窗口多选
            dlg1.ShowDialog(); //显示这个窗口

            if (dlg1.FileNames.Length <= 0) { return; } //如果没有选择文件就退出函数
            else if (dlg1.FileNames.Length == 1) { MainPlayer.LoadFile(dlg1.FileNames[0]); } //如果只有一个文件就让播放器打开这个文件

            for (int i = 0; i < dlg1.FileNames.Length; i++) //把文件添加到播放列表里
            {
                pl_main.Add(new PlayListItem(dlg1.FileNames[i], ""));
            }

            RefreshPlayList();//刷新播放列表
        }

        private void tbtnRemove_ButtonClick(object sender, EventArgs e) //“删除选项”按钮
        {
            int i;
            ListViewItem item; 
            if (listView1.SelectedItems.Count < 1) { return; }
            for (i = 0; i < listView1.SelectedItems.Count; i++)
            {
                item = listView1.SelectedItems[i];
                pl_main.Remove(listView1.Items.IndexOf(item));
                listView1.Items.Remove(item);
            }

            RefreshPlayList();
        }

        private void RefreshPlayList()//刷新播放列表过程
        {
            LinkedListNode<PlayListItem> marked = pl_main.list.First; //创建一个节点对象
            listView1.Items.Clear();//清空列表控件
            for (int i = 0; i < pl_main.list.Count; i++) //循环扫描链表并添加选项
            {
                listView1.Items.Add(System.IO.Path.GetFileName(marked.Value.FileAddress),7);
                marked = marked.Next;
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            PlayListItem marked; //创建一个列表选项对象
            if (listView1.SelectedItems.Count <= 0) { return; } //如果没选中项目就退出过程
            marked = pl_main.GetItem(listView1.Items.IndexOf(listView1.SelectedItems[0])); //把对象从链表中读取出来
            if (MainPlayer.LoadFile(marked.FileAddress)) { MainPlayer.Play(); } //载入文件
        }

        private void tmEmptyList_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count == 0) { return; }
            if (MessageBox.Show("要清空列表？\n此操作不可恢复哦。",
                "清空列表？",MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No) { return; };

            pl_main.list.Clear();
            listView1.Items.Clear();
        }

    }
}
