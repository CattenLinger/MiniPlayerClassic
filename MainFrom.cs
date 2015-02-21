using System;
using System.Collections.Generic;
using System.Collections;
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
        bool _progressbar_draw = true;
        bool _volumebar_draw = true;

        public Player MainPlayer; //播放器对象
        public c_ProgressBar cProgressBar; //进度条对象
        public c_VolumeBar cVolumeBar; //音量条对象

        public string LabelText; //标签文字
        private Graphics pb_g_enter;//画布
        private Graphics pb_g_enter2;

        List<PlayList> PlayLists;//用于管理多个播放列表

        private int def_height;//默认尺寸
        private int min_height;
        private Boolean is_Minisize;//界面是否在迷你模式

        private int newlists = 0;//新建列表名字的计数器，用于计算新建了多少列表方面命名

        enum playbackHeadState { ByIndex, Single, Single_Cycling, List_Cycling }
        private playbackHeadState playbackhead_state;
        private LinkedListNode<PlayListItem> playbackhead;
        private bool playback = false;
        private bool buttonaction = false;

        private Un4seen.Bass.BASSTimer ScuptrumTimer;

        //一些东西的初始化
        public MainFrom(string[] args)
        {
            InitializeComponent();

            LabelText = "暂无播放任务";
            def_height = this.Height;
            min_height = this.Height - tb_Lists.Height;
            is_Minisize = true;

            pb_g_enter = pb_Progress.CreateGraphics(); //初始化进度条们的画布
            pb_g_enter2 = pb_Volume.CreateGraphics();

            MainPlayer = new Player(this.Handle); //初始化播放器对象
            MainPlayer.StateChange += MainPlayer_StateChange;  //注册播放器改变播放状态的事件的响应函数
            MainPlayer.FileChange += MainPlayer_FileChange; //注册播放器改变文件的事件的响应函数
            MainPlayer.WaveFormFinished += MainPlayer_WaveFormFinished;

            PlayLists = new List<PlayList>(32);

            cProgressBar = new c_ProgressBar(pb_Progress.Width, pb_Progress.Height); //初始化进度条
            cProgressBar.ChangeTitle(LabelText);
            cProgressBar.pb_maxvalue = 10;
            cProgressBar.pb_value = 0;

            cVolumeBar = new c_VolumeBar(pb_Volume.Width,pb_Volume.Height); //初始化音量条
            cVolumeBar.ChangeLabel("音量");
            cVolumeBar.pb_maxvalue = 100;
            cVolumeBar.pb_value = 100;

            to_Minisize(false);//迷你模式
            tb_Lists.TabPages.Clear();
            refreshInterface();
            playbackhead_state = playbackHeadState.Single;

            loadargs(args);
            ScuptrumTimer = new Un4seen.Bass.BASSTimer(17);
            ScuptrumTimer.Tick += ScuptrumTimer_Tick;
            ScuptrumTimer.Start();
        }

        void MainPlayer_WaveFormFinished(object sender, EventArgs e)
        {
            cProgressBar.UpdateWaveForm(MainPlayer.waveform);
        }

        public void loadargs(string[] args)
        {
            if (args.Length != 0)
            {
                if (args.Length == 1)
                {
                    if (System.IO.Path.GetExtension(args[0]) == ".spl")
                    {
                        PlayLists.Add(new PlayList(args[0]));
                        tb_Lists.TabPages.Add(System.IO.Path.GetFileNameWithoutExtension(args[0]));
                        RefreshPlayList();
                        refreshInterface();
                    }
                    else
                    {
                        if (MainPlayer.LoadFile(args[0]))
                            MainPlayer.Play();
                    }
                }

                if (args.Length > 1)
                {
                    List<string> temp1 = new List<string>();
                    List<string> temp2 = new List<string>();
                    foreach (string filenames in args)
                    {
                        if (System.IO.Path.GetExtension(filenames) == ".spl")
                            temp1.Add(filenames);
                        else
                            temp2.Add(filenames);
                    }
                    if (temp2.Count != 0) tmNewList_Click(this, null);
                    foreach (string filenames in temp2)
                    {
                        PlayLists[tb_Lists.SelectedIndex].Add(new PlayListItem(filenames, ""));
                    }
                    foreach (string filenames in temp1)
                    {
                        PlayLists.Add(new PlayList(filenames));
                        tb_Lists.TabPages.Add(System.IO.Path.GetFileNameWithoutExtension(filenames));
                        RefreshPlayList();
                    }
                    refreshInterface();
                }
            }
        }

        //init
        private void MainFrom_Load(object sender, EventArgs e)
        {
        /* If you want to close the splash of Bass.Net you need to regist at 
         * www.un4seen.com and input the registration code.
         * (At the initialization of Player too.
         */ 
        //BassNet.Registration("your_email","your_code");
            
        }
        //Stop Button
        private void btnStop_Click(object sender, EventArgs e)
        {
            MainPlayer.Stop();
            buttonaction = true;
        }
        //Play/Pause button
        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (MainPlayer.PlayState != Player.PlayerStates.Playing)
                MainPlayer.Play();
            else 
                MainPlayer.Pause();
            buttonaction = true;
        }

        private void playbackactions()
        {
            switch (playbackhead_state)
            {
                case playbackHeadState.ByIndex:
                    playbackhead = playbackhead.Next;
                    if (playbackhead != null) 
                    {
                        if (MainPlayer.LoadFile(playbackhead.Value.FileAddress))
                            MainPlayer.Play();
                    }
                    else { playback = false; }
                    break;

                case playbackHeadState.List_Cycling:
                    if (playbackhead.Next == null)
                    {
                        if (playbackhead.List.First == null)
                        {
                            playback = false;
                            break;
                        }
                        playbackhead = playbackhead.List.First;
                    }
                    else { playbackhead = playbackhead.Next; }
                    if (MainPlayer.LoadFile(playbackhead.Value.FileAddress)) MainPlayer.Play();
                    break;

                case playbackHeadState.Single:
                    break;

                case playbackHeadState.Single_Cycling:
                    MainPlayer.Play();
                    break;
            }
        }
//--------Events Checker--------------------------------------------------------------------------
        void MainPlayer_StateChange(object sender, Player.PlayerStateChange e) //响应播放状态改变的消息的函数
        {
            if (e.Message != Player.PlayerStates.Playing)
                btnPlay.ImageIndex = 2; 
            else 
                btnPlay.ImageIndex = 1;

            if (playback && !buttonaction && MainPlayer.PlayState != Player.PlayerStates.Playing)
            {
                if (PlayLists.Count == 0)
                    playback = false;
                else
                    playbackactions();
            }
            buttonaction = false;
        }

        void MainPlayer_FileChange(object sender, Player.PlayerFileChange e)
        { 
            if(e.Message != "")
            {
                LabelText = System.IO.Path.GetFileName(MainPlayer.FilePath);
                cProgressBar.ChangeTitle(LabelText);
                cProgressBar.pb_maxvalue = (int)(MainPlayer.GetLength() * 1000);
                MainPlayer.GetWaveForm(cProgressBar.width, cProgressBar.height);
            }
        }
//------Window interface change-------------------------------------------------------------------
        public void to_Minisize(Boolean animate)//迷你尺寸
        {

            this.Height = min_height;
            is_Minisize = true;
        }

        public void to_NormalSize(Boolean animate)//正常尺寸
        {

            this.Height = def_height;
            is_Minisize = false;
        }

        public void refreshInterface()//刷新界面元素的设置
        {
            if (tb_Lists.TabCount == 0)
            {
                this.Text = "MiniPlayer";
                tbtnRemove.Enabled = false;
                tbtnPlayMode.Enabled = false;
                if (!is_Minisize) to_Minisize(true);
                tbtnModeChange.Enabled = false;
                tbtnModeChange.ToolTipText = "切换界面模式\n（请先新建列表）";
                tmAddList.Enabled = false;
                playback = false;
            }
            else
            {
                if (PlayLists[tb_Lists.SelectedIndex].FilePath != "")
                    tb_Lists.SelectedTab.Text = System.IO.Path.GetFileNameWithoutExtension(PlayLists[tb_Lists.SelectedIndex].FilePath);

                this.Text = "MiniPlayer - " + tb_Lists.SelectedTab.Text;
                listView1.Parent = tb_Lists.SelectedTab;
                tbtnPlayMode.Enabled = true;
                tbtnRemove.Enabled = true;
                if (is_Minisize) to_NormalSize(true);
                tbtnModeChange.Enabled = true;
                tbtnModeChange.ToolTipText = "切换界面模式";
                tmAddList.Enabled = true;
                playback = true;
            }
        }
//------------------------------------------------------------------------------------------------

        #region About Drawing the ProgressBar //这里因为都是些很易懂的过程就懒得注释了
        private void pb_Progress_MouseDown(object sender, MouseEventArgs e) 
        {
            _progressbar_draw = false;
            buttonaction = false;
            int temp;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                temp = (int)((float)cProgressBar.pb_maxvalue * ((float)e.X / (float)cProgressBar.width));
                cProgressBar.pb_value = temp;
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
                cProgressBar.DrawBar(pb_g_enter);
            }
        }

        private void pb_Progress_MouseUp(object sender, MouseEventArgs e)
        {
            _progressbar_draw = true;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            { MainPlayer.SetPosition((double)cProgressBar.pb_value / 1000); }
        }
        #endregion

        #region Bar's Timer

        void ScuptrumTimer_Tick(object sender, EventArgs e)
        {
            double temp;
            temp = MainPlayer.GetPosition();
            if (temp == -1) { temp = 0; }

            if(_progressbar_draw)
            {
                cProgressBar.pb_value = (int)(temp * 1000);
                cProgressBar.DrawBar(pb_g_enter);
            }

            if(_volumebar_draw)
            {
                cVolumeBar.DrawBar(pb_g_enter2);
                int left = 0, right = 0;
                MainPlayer.GetLevel(ref left, ref right);
                cVolumeBar.tellitlevel(left, right);
                MainPlayer.getData(ref cVolumeBar.fft_data);
            }

        }

        private void tmrPGBars_Tick(object sender, EventArgs e)
        {
            double temp;
            temp = MainPlayer.GetPosition();
            if (temp == -1) { temp = 0; }
            cProgressBar.pb_value = (int)(temp * 1000);
            cProgressBar.DrawBar(pb_g_enter);

            cVolumeBar.DrawBar(pb_g_enter2);
            int left = 0, right = 0;
            MainPlayer.GetLevel(ref left, ref right);
            cVolumeBar.tellitlevel(left, right);
            MainPlayer.getData(ref cVolumeBar.fft_data);
        }

        private void tmrVBar_Tick(object sender, EventArgs e)
        {
            
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
                //cVolumeBar.DrawBar(pb_g_enter2);
            }
        }

        private void pb_Volume_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                cVolumeBar.pb_value = (int)((float)cVolumeBar.pb_maxvalue * ((float)e.X / (float)pb_Volume.Width));
                MainPlayer.SetVolume((float)cVolumeBar.pb_value / (float)cVolumeBar.pb_maxvalue);
                //cVolumeBar.DrawBar(pb_g_enter2);
            }
        }

        private void pb_Volume_MouseUp(object sender, MouseEventArgs e)
        {
            //tmrVBar.Enabled = true;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                cVolumeBar.pb_value = (int)((float)cVolumeBar.pb_maxvalue * ((float)e.X / (float)pb_Volume.Width));
                MainPlayer.SetVolume((float)cVolumeBar.pb_value / (float)cVolumeBar.pb_maxvalue);
                //cVolumeBar.DrawBar(pb_g_enter2);
            }
        }
        #endregion

        private void tbtnAdd_ButtonClick(object sender, EventArgs e) //“添加文件”按钮
        {
            OpenFileDialog dlg1 = new OpenFileDialog(); //创建一个文件打开窗口对象
            dlg1.Filter = "All Acceptable files|*.mp3;*.ogg;*.wav|MP3 File|*.mp3|OGG File|*.ogg|Wave File|*.wav";
            dlg1.Multiselect = true; //允许文件打开窗口多选
            dlg1.ShowDialog(); //显示这个窗口

            if (dlg1.FileNames.Length <= 0) return; //如果没有选择文件就退出函数
            else if ((dlg1.FileNames.Length == 1) && (PlayLists.Count == 0))
            {
                MainPlayer.LoadFile(dlg1.FileNames[0]);
                return;
            }//如果只有一个文件就让播放器打开这个文件

            if (PlayLists.Count == 0) tmNewList_Click(this, null);
            for (int i = 0; i < dlg1.FileNames.Length; i++)
            {
                listView1.Items.Add(dlg1.FileNames[i]);
                PlayLists[tb_Lists.SelectedIndex].Add(new PlayListItem(dlg1.FileNames[i],""));
            }
            
            RefreshPlayList();//刷新播放列表
            System.GC.Collect();
        }

        private void tbtnRemove_ButtonClick(object sender, EventArgs e) //“删除选项”按钮
        {
            int i;
            ListViewItem item;
            if (listView1.SelectedItems.Count < 1) { return; }//如果没有选中项目就返回

            for (i = listView1.SelectedItems.Count - 1;i >= 0; i--)//倒序删除，避免了节点移位导致不能正确删除节点
            {
                item = listView1.SelectedItems[i];
                PlayLists[tb_Lists.SelectedIndex].Remove(listView1.Items.IndexOf(item));
                listView1.Items.Remove(item);
            }
            
            RefreshPlayList();
        }

        private void RefreshPlayList()//刷新播放列表过程
        {
            listView1.Items.Clear();//清空列表控件
            if (PlayLists.Count != 0)
            {
                LinkedListNode<PlayListItem> marked = PlayLists[tb_Lists.SelectedIndex].Items.First; //创建一个节点对象
                for (int i = 0; i < PlayLists[tb_Lists.SelectedIndex].Count; i++) //循环扫描链表并添加选项
                {
                    listView1.Items.Add(System.IO.Path.GetFileName(marked.Value.FileAddress));
                    marked = marked.Next;
                }
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            playbackhead = PlayLists[tb_Lists.SelectedIndex].GetNode(listView1.Items.IndexOf(listView1.SelectedItems[0])); //把对象从链表中读取出来
            if (MainPlayer.LoadFile(playbackhead.Value.FileAddress)) { MainPlayer.Play(); } //载入文件
        }

        private void tmEmptyList_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count == 0) { return; }//如果列表没有选中项就返回
            if (MessageBox.Show("要清空列表？\n此操作不可恢复哦。",
                "清空列表？",MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No) { return; };

            PlayLists[tb_Lists.SelectedIndex].Clear();
            listView1.Items.Clear();
        }

        private void tmCloseList_Click(object sender, EventArgs e)
        {
            if (PlayLists[tb_Lists.SelectedIndex].OperationCount != 0)
            { 
                switch(MessageBox.Show("列表已修改，保存？", "列表", MessageBoxButtons.YesNoCancel))
                {
                    case System.Windows.Forms.DialogResult.Cancel:
                        return;

                    case System.Windows.Forms.DialogResult.Yes:
                        tmSaveList_Click(sender, e);
                        break;
                }
            }

            PlayLists.RemoveAt(tb_Lists.SelectedIndex);
            RefreshPlayList();
            tb_Lists.TabPages.Remove(tb_Lists.SelectedTab);
            refreshInterface();
        }

        private void tmNewList_Click(object sender, EventArgs e)
        {
            PlayLists.Add(new PlayList());//创建一个播放列表

            tb_Lists.TabPages.Add("*未命名列表" + newlists++.ToString());
            if (tb_Lists.TabCount == 1)
            {
                listView1.Parent = tb_Lists.SelectedTab;
            }
            refreshInterface();
        }

        private void tmDeleteListFile_Click(object sender, EventArgs e)//删除列表操作
        {
            if (MessageBox.Show("删除列表文件吗？\n此操作不可恢复哦！", "删除列表", 
                MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            return;

            if(PlayLists[tb_Lists.SelectedIndex].FilePath != "")
                if(System.IO.File.Exists(PlayLists[tb_Lists.SelectedIndex].FilePath))
                    System.IO.File.Delete(PlayLists[tb_Lists.SelectedIndex].FilePath);

            PlayLists.RemoveAt(tb_Lists.SelectedIndex);
            tb_Lists.TabPages.Remove(tb_Lists.SelectedTab);
            RefreshPlayList();

        }

        private void tbtnList_Click(object sender, EventArgs e)
        {
            tbtnList.ShowDropDown();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView1.Parent = tb_Lists.SelectedTab;
            RefreshPlayList();
            refreshInterface();
        }

        private void tbtnModeChange_Click(object sender, EventArgs e)
        {
            if (is_Minisize)
                to_NormalSize(true);
            else
                to_Minisize(true);
        }

        private void tmOpenList_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg1 = new OpenFileDialog();
            dlg1.Filter = "Simple List File|*.spl";
            dlg1.ShowDialog();
            if (dlg1.FileName != "")
            {
                PlayLists.Add(new PlayList(dlg1.FileName));
                tb_Lists.TabPages.Add(System.IO.Path.GetFileNameWithoutExtension(dlg1.FileName));
                RefreshPlayList();
                refreshInterface();
                tb_Lists.SelectedIndex = tb_Lists.TabCount - 1;
                listView1.Parent = tb_Lists.SelectedTab;
            }
        }

        private void tmAddList_Click(object sender, EventArgs e)
        {
            if(PlayLists.Count != 0)
            {
                OpenFileDialog dlg1 = new OpenFileDialog();
                dlg1.Filter = "Simple List File|*.spl";
                dlg1.ShowDialog();
                string tmp1 = dlg1.FileName;
                if(tmp1 != "")
                {
                    PlayLists[tb_Lists.SelectedIndex].AddFromFile(tmp1);
                    RefreshPlayList();
                }
            }
        }

        private void tmSaveList_Click(object sender, EventArgs e)
        {
            if(PlayLists[tb_Lists.SelectedIndex].FilePath == "")
            {
                SaveFileDialog dlg1 = new SaveFileDialog();
                dlg1.Filter = "Simple List File|*.spl";
                dlg1.ShowDialog();
                string temp1 = dlg1.FileName;
                if (dlg1.FileName != "")
                {
                    if (!PlayLists[tb_Lists.SelectedIndex].SaveToFile(dlg1.FileName))
                    {
                        MessageBox.Show("存储列表时发生错误。", "列表");
                    }
                }
            }
            else
            {
                if (!PlayLists[tb_Lists.SelectedIndex].SaveToFile(PlayLists[tb_Lists.SelectedIndex].FilePath))
                {
                    MessageBox.Show("存储列表时发生错误。", "列表");
                }
            }
            
            refreshInterface();
        }

        private void tmSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg1 = new SaveFileDialog();
            dlg1.Filter = "Simple List File|*.spl";
            dlg1.ShowDialog();
            string temp1 = dlg1.FileName;
            if (dlg1.FileName != "")
            {
                if (!PlayLists[tb_Lists.SelectedIndex].SaveToFile(dlg1.FileName))
                {
                    MessageBox.Show("存储列表时发生错误。", "列表");
                }
            }
            refreshInterface();
        }

        private void MainFrom_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(PlayLists.Count != 0)
            {
                for (int i = 0; i < PlayLists.Count; i++)
                {
                    if(PlayLists[i].OperationCount != 0)
                    {
                        if (MessageBox.Show("有列表内容曾经更改。\n是否退出？", "正要退出", 
                            MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                        {
                            e.Cancel = true;
                            break;
                        }
                    }
                }
            }

            ScuptrumTimer.Stop();
            ScuptrumTimer.Dispose();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            toolStripMenuItem1.Checked = true;
            toolStripMenuItem2.Checked = false;
            toolStripMenuItem3.Checked = false;
            toolStripMenuItem4.Checked = false;
            playbackhead_state = playbackHeadState.ByIndex;
            tbtnPlayMode.Text = toolStripMenuItem1.Text;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            toolStripMenuItem1.Checked = false;
            toolStripMenuItem2.Checked = true;
            toolStripMenuItem3.Checked = false;
            toolStripMenuItem4.Checked = false;
            playbackhead_state = playbackHeadState.List_Cycling;
            tbtnPlayMode.Text = toolStripMenuItem2.Text;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            toolStripMenuItem1.Checked = false;
            toolStripMenuItem2.Checked = false;
            toolStripMenuItem3.Checked = true;
            toolStripMenuItem4.Checked = false;
            playbackhead_state = playbackHeadState.Single;
            tbtnPlayMode.Text = toolStripMenuItem3.Text;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            toolStripMenuItem1.Checked = false;
            toolStripMenuItem2.Checked = false;
            toolStripMenuItem3.Checked = false;
            toolStripMenuItem4.Checked = true;
            playbackhead_state = playbackHeadState.Single_Cycling;
            tbtnPlayMode.Text = toolStripMenuItem4.Text;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (playbackhead.Next == null) return;
            playbackhead = playbackhead.Next;
            if (playbackhead != null)
            {
                buttonaction = true;
                if (MainPlayer.LoadFile(playbackhead.Value.FileAddress)) { MainPlayer.Play(); }
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (playbackhead.Previous == null) return;
            playbackhead = playbackhead.Previous;
            if (playbackhead != null)
            {
                buttonaction = true;
                if (MainPlayer.LoadFile(playbackhead.Value.FileAddress)) { MainPlayer.Play(); }
            }
        }

        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void tmrChecker_Tick(object sender, EventArgs e)
        {

        }

    }
}
