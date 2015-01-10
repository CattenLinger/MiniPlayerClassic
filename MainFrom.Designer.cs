namespace MiniPlayerClassic
{
    partial class MainFrom
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrom));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbtnAdd = new System.Windows.Forms.ToolStripSplitButton();
            this.tmOpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tmOpenList = new System.Windows.Forms.ToolStripMenuItem();
            this.tmAddList = new System.Windows.Forms.ToolStripMenuItem();
            this.tbtnRemove = new System.Windows.Forms.ToolStripSplitButton();
            this.tmDeleteItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tmDeleteListFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.tmCloseList = new System.Windows.Forms.ToolStripMenuItem();
            this.tmEmptyList = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tbtnList = new System.Windows.Forms.ToolStripSplitButton();
            this.tmSaveList = new System.Windows.Forms.ToolStripMenuItem();
            this.tmSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.tmNewList = new System.Windows.Forms.ToolStripMenuItem();
            this.tbtnPlayMode = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tbtnSetting = new System.Windows.Forms.ToolStripButton();
            this.tbtnModeChange = new System.Windows.Forms.ToolStripButton();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.pb_Volume = new System.Windows.Forms.PictureBox();
            this.pb_Progress = new System.Windows.Forms.PictureBox();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.tmrPGBar = new System.Windows.Forms.Timer(this.components);
            this.tmrVBar = new System.Windows.Forms.Timer(this.components);
            this.tb_Lists = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Volume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Progress)).BeginInit();
            this.tb_Lists.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "forward_button.png");
            this.imageList1.Images.SetKeyName(1, "pause.png");
            this.imageList1.Images.SetKeyName(2, "play.png");
            this.imageList1.Images.SetKeyName(3, "plus.png");
            this.imageList1.Images.SetKeyName(4, "rewind_button.png");
            this.imageList1.Images.SetKeyName(5, "stop_alt.png");
            this.imageList1.Images.SetKeyName(6, "up_alt.png");
            this.imageList1.Images.SetKeyName(7, "music.png");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.btnPrev);
            this.panel1.Controls.Add(this.pb_Volume);
            this.panel1.Controls.Add(this.pb_Progress);
            this.panel1.Controls.Add(this.btnPlay);
            this.panel1.Controls.Add(this.btnStop);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbtnAdd,
            this.tbtnRemove,
            this.toolStripSeparator1,
            this.tbtnList,
            this.tbtnPlayMode,
            this.toolStripSeparator3,
            this.tbtnSetting,
            this.tbtnModeChange});
            this.toolStrip1.Name = "toolStrip1";
            // 
            // tbtnAdd
            // 
            this.tbtnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnAdd.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmOpenFile,
            this.toolStripSeparator4,
            this.tmOpenList,
            this.tmAddList});
            resources.ApplyResources(this.tbtnAdd, "tbtnAdd");
            this.tbtnAdd.Name = "tbtnAdd";
            this.tbtnAdd.ButtonClick += new System.EventHandler(this.tbtnAdd_ButtonClick);
            // 
            // tmOpenFile
            // 
            this.tmOpenFile.Name = "tmOpenFile";
            resources.ApplyResources(this.tmOpenFile, "tmOpenFile");
            this.tmOpenFile.Click += new System.EventHandler(this.tbtnAdd_ButtonClick);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // tmOpenList
            // 
            this.tmOpenList.Name = "tmOpenList";
            resources.ApplyResources(this.tmOpenList, "tmOpenList");
            // 
            // tmAddList
            // 
            this.tmAddList.Name = "tmAddList";
            resources.ApplyResources(this.tmAddList, "tmAddList");
            // 
            // tbtnRemove
            // 
            this.tbtnRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnRemove.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmDeleteItem,
            this.tmDeleteListFile,
            this.toolStripMenuItem6,
            this.tmCloseList,
            this.tmEmptyList});
            resources.ApplyResources(this.tbtnRemove, "tbtnRemove");
            this.tbtnRemove.Name = "tbtnRemove";
            this.tbtnRemove.ButtonClick += new System.EventHandler(this.tbtnRemove_ButtonClick);
            // 
            // tmDeleteItem
            // 
            this.tmDeleteItem.Name = "tmDeleteItem";
            resources.ApplyResources(this.tmDeleteItem, "tmDeleteItem");
            // 
            // tmDeleteListFile
            // 
            this.tmDeleteListFile.Name = "tmDeleteListFile";
            resources.ApplyResources(this.tmDeleteListFile, "tmDeleteListFile");
            this.tmDeleteListFile.Click += new System.EventHandler(this.tmDeleteListFile_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            resources.ApplyResources(this.toolStripMenuItem6, "toolStripMenuItem6");
            // 
            // tmCloseList
            // 
            this.tmCloseList.Name = "tmCloseList";
            resources.ApplyResources(this.tmCloseList, "tmCloseList");
            this.tmCloseList.Click += new System.EventHandler(this.tmCloseList_Click);
            // 
            // tmEmptyList
            // 
            this.tmEmptyList.Name = "tmEmptyList";
            resources.ApplyResources(this.tmEmptyList, "tmEmptyList");
            this.tmEmptyList.Click += new System.EventHandler(this.tmEmptyList_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // tbtnList
            // 
            this.tbtnList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnList.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmSaveList,
            this.tmSaveAs,
            this.toolStripMenuItem5,
            this.tmNewList});
            resources.ApplyResources(this.tbtnList, "tbtnList");
            this.tbtnList.Name = "tbtnList";
            this.tbtnList.Click += new System.EventHandler(this.tbtnList_Click);
            // 
            // tmSaveList
            // 
            this.tmSaveList.Name = "tmSaveList";
            resources.ApplyResources(this.tmSaveList, "tmSaveList");
            // 
            // tmSaveAs
            // 
            this.tmSaveAs.Name = "tmSaveAs";
            resources.ApplyResources(this.tmSaveAs, "tmSaveAs");
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            resources.ApplyResources(this.toolStripMenuItem5, "toolStripMenuItem5");
            // 
            // tmNewList
            // 
            this.tmNewList.Name = "tmNewList";
            resources.ApplyResources(this.tmNewList, "tmNewList");
            this.tmNewList.Click += new System.EventHandler(this.tmNewList_Click);
            // 
            // tbtnPlayMode
            // 
            this.tbtnPlayMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripSeparator2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4});
            resources.ApplyResources(this.tbtnPlayMode, "tbtnPlayMode");
            this.tbtnPlayMode.Name = "tbtnPlayMode";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            resources.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            resources.ApplyResources(this.toolStripMenuItem4, "toolStripMenuItem4");
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // tbtnSetting
            // 
            this.tbtnSetting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tbtnSetting, "tbtnSetting");
            this.tbtnSetting.Name = "tbtnSetting";
            // 
            // tbtnModeChange
            // 
            this.tbtnModeChange.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tbtnModeChange, "tbtnModeChange");
            this.tbtnModeChange.Name = "tbtnModeChange";
            this.tbtnModeChange.Click += new System.EventHandler(this.tbtnModeChange_Click);
            // 
            // btnNext
            // 
            resources.ApplyResources(this.btnNext, "btnNext");
            this.btnNext.ImageList = this.imageList1;
            this.btnNext.Name = "btnNext";
            this.btnNext.UseVisualStyleBackColor = true;
            // 
            // btnPrev
            // 
            resources.ApplyResources(this.btnPrev, "btnPrev");
            this.btnPrev.ImageList = this.imageList1;
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.UseVisualStyleBackColor = true;
            // 
            // pb_Volume
            // 
            resources.ApplyResources(this.pb_Volume, "pb_Volume");
            this.pb_Volume.Name = "pb_Volume";
            this.pb_Volume.TabStop = false;
            this.pb_Volume.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pb_Volume_MouseDown);
            this.pb_Volume.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pb_Volume_MouseMove);
            this.pb_Volume.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pb_Volume_MouseUp);
            // 
            // pb_Progress
            // 
            resources.ApplyResources(this.pb_Progress, "pb_Progress");
            this.pb_Progress.Name = "pb_Progress";
            this.pb_Progress.TabStop = false;
            this.pb_Progress.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pb_Progress_MouseDown);
            this.pb_Progress.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pb_Progress_MouseMove);
            this.pb_Progress.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pb_Progress_MouseUp);
            // 
            // btnPlay
            // 
            resources.ApplyResources(this.btnPlay, "btnPlay");
            this.btnPlay.ImageList = this.imageList1;
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnStop
            // 
            resources.ApplyResources(this.btnStop, "btnStop");
            this.btnStop.ImageList = this.imageList1;
            this.btnStop.Name = "btnStop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // tmrPGBar
            // 
            this.tmrPGBar.Enabled = true;
            this.tmrPGBar.Interval = 30;
            this.tmrPGBar.Tick += new System.EventHandler(this.tmrPGBars_Tick);
            // 
            // tmrVBar
            // 
            this.tmrVBar.Enabled = true;
            this.tmrVBar.Interval = 17;
            this.tmrVBar.Tick += new System.EventHandler(this.tmrVBar_Tick);
            // 
            // tb_Lists
            // 
            this.tb_Lists.Controls.Add(this.tabPage1);
            resources.ApplyResources(this.tb_Lists, "tb_Lists");
            this.tb_Lists.Multiline = true;
            this.tb_Lists.Name = "tb_Lists";
            this.tb_Lists.SelectedIndex = 0;
            this.tb_Lists.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listView1);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            resources.ApplyResources(this.listView1, "listView1");
            this.listView1.Name = "listView1";
            this.listView1.TileSize = new System.Drawing.Size(300, 26);
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Tile;
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // MainFrom
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tb_Lists);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainFrom";
            this.Load += new System.EventHandler(this.MainFrom_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Volume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Progress)).EndInit();
            this.tb_Lists.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.PictureBox pb_Progress;
        private System.Windows.Forms.Timer tmrPGBar;
        private System.Windows.Forms.PictureBox pb_Volume;
        private System.Windows.Forms.Timer tmrVBar;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSplitButton tbtnAdd;
        private System.Windows.Forms.ToolStripMenuItem tmOpenFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem tmOpenList;
        private System.Windows.Forms.ToolStripMenuItem tmAddList;
        private System.Windows.Forms.ToolStripSplitButton tbtnRemove;
        private System.Windows.Forms.ToolStripMenuItem tmDeleteItem;
        private System.Windows.Forms.ToolStripMenuItem tmDeleteListFile;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem tmCloseList;
        private System.Windows.Forms.ToolStripMenuItem tmEmptyList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSplitButton tbtnList;
        private System.Windows.Forms.ToolStripMenuItem tmSaveList;
        private System.Windows.Forms.ToolStripMenuItem tmSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem tmNewList;
        private System.Windows.Forms.ToolStripSplitButton tbtnPlayMode;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tbtnSetting;
        private System.Windows.Forms.ToolStripButton tbtnModeChange;
        private System.Windows.Forms.TabControl tb_Lists;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListView listView1;

    }
}

