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
            this.tmrPGBar = new System.Windows.Forms.Timer(this.components);
            this.menu_FileOpen = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mAddFile = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tb_Lists = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbtnAdd = new System.Windows.Forms.ToolStripSplitButton();
            this.tmAddList = new System.Windows.Forms.ToolStripMenuItem();
            this.tmOpenList = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tmOpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tbtnRemove = new System.Windows.Forms.ToolStripSplitButton();
            this.tmDeleteItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tmEmptyList = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.tmCloseList = new System.Windows.Forms.ToolStripMenuItem();
            this.tmDeleteListFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tbtnList = new System.Windows.Forms.ToolStripSplitButton();
            this.tmSaveList = new System.Windows.Forms.ToolStripMenuItem();
            this.tmSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.tmNewList = new System.Windows.Forms.ToolStripMenuItem();
            this.tbtnPlayMode = new System.Windows.Forms.ToolStripSplitButton();
            this.tmPlaytheList = new System.Windows.Forms.ToolStripMenuItem();
            this.tmListRepeat = new System.Windows.Forms.ToolStripMenuItem();
            this.tmSingleRepeat = new System.Windows.Forms.ToolStripMenuItem();
            this.tmSingle = new System.Windows.Forms.ToolStripMenuItem();
            this.tmShuffle = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tbtnModeChange = new System.Windows.Forms.ToolStripButton();
            this.tbtnSettings = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.pb_Volume = new System.Windows.Forms.PictureBox();
            this.pb_Progress = new System.Windows.Forms.PictureBox();
            this.menu_FileOpen.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tb_Lists.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Volume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Progress)).BeginInit();
            this.SuspendLayout();
            // 
            // tmrPGBar
            // 
            this.tmrPGBar.Enabled = true;
            this.tmrPGBar.Interval = 17;
            this.tmrPGBar.Tick += new System.EventHandler(this.tmrPGBars_Tick);
            // 
            // menu_FileOpen
            // 
            this.menu_FileOpen.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menu_FileOpen.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mAddFile});
            this.menu_FileOpen.Name = "contextMenuStrip1";
            this.menu_FileOpen.Size = new System.Drawing.Size(125, 26);
            // 
            // mAddFile
            // 
            this.mAddFile.Name = "mAddFile";
            this.mAddFile.Size = new System.Drawing.Size(124, 22);
            this.mAddFile.Text = "打开文件";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tb_Lists, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.toolStrip1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(365, 494);
            this.tableLayoutPanel2.TabIndex = 25;
            this.tableLayoutPanel2.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel2_Paint);
            // 
            // tb_Lists
            // 
            this.tb_Lists.Controls.Add(this.tabPage1);
            this.tb_Lists.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_Lists.Location = new System.Drawing.Point(3, 142);
            this.tb_Lists.Name = "tb_Lists";
            this.tb_Lists.SelectedIndex = 0;
            this.tb_Lists.Size = new System.Drawing.Size(359, 349);
            this.tb_Lists.TabIndex = 35;
            this.tb_Lists.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(351, 323);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(3, 3);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(345, 317);
            this.listBox1.TabIndex = 0;
            this.listBox1.DoubleClick += new System.EventHandler(this.listBox1_DoubleClick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbtnAdd,
            this.tbtnRemove,
            this.toolStripSeparator1,
            this.tbtnList,
            this.tbtnPlayMode,
            this.toolStripSeparator3,
            this.tbtnModeChange,
            this.tbtnSettings});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(2, 100);
            this.toolStrip1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(361, 39);
            this.toolStrip1.TabIndex = 33;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tbtnAdd
            // 
            this.tbtnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnAdd.DropDownButtonWidth = 10;
            this.tbtnAdd.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmAddList,
            this.tmOpenList,
            this.toolStripMenuItem1,
            this.tmOpenFile});
            this.tbtnAdd.Image = ((System.Drawing.Image)(resources.GetObject("tbtnAdd.Image")));
            this.tbtnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnAdd.Name = "tbtnAdd";
            this.tbtnAdd.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tbtnAdd.Size = new System.Drawing.Size(47, 36);
            this.tbtnAdd.Text = "toolStripButton1";
            this.tbtnAdd.ToolTipText = "打开文件";
            this.tbtnAdd.ButtonClick += new System.EventHandler(this.tbtnAdd_ButtonClick);
            // 
            // tmAddList
            // 
            this.tmAddList.Name = "tmAddList";
            this.tmAddList.Size = new System.Drawing.Size(152, 22);
            this.tmAddList.Text = "追加列表";
            this.tmAddList.Click += new System.EventHandler(this.tmAddList_Click);
            // 
            // tmOpenList
            // 
            this.tmOpenList.Name = "tmOpenList";
            this.tmOpenList.Size = new System.Drawing.Size(152, 22);
            this.tmOpenList.Text = "打开列表";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // tmOpenFile
            // 
            this.tmOpenFile.Name = "tmOpenFile";
            this.tmOpenFile.Size = new System.Drawing.Size(152, 22);
            this.tmOpenFile.Text = "添加文件";
            // 
            // tbtnRemove
            // 
            this.tbtnRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnRemove.DropDownButtonWidth = 15;
            this.tbtnRemove.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmDeleteItem,
            this.tmEmptyList,
            this.toolStripMenuItem6,
            this.tmCloseList,
            this.tmDeleteListFile});
            this.tbtnRemove.Image = ((System.Drawing.Image)(resources.GetObject("tbtnRemove.Image")));
            this.tbtnRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnRemove.Name = "tbtnRemove";
            this.tbtnRemove.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tbtnRemove.Size = new System.Drawing.Size(52, 36);
            this.tbtnRemove.Text = "toolStripDropDownButton1";
            this.tbtnRemove.ToolTipText = "移除选项";
            this.tbtnRemove.ButtonClick += new System.EventHandler(this.tbtnRemove_ButtonClick);
            // 
            // tmDeleteItem
            // 
            this.tmDeleteItem.Name = "tmDeleteItem";
            this.tmDeleteItem.Size = new System.Drawing.Size(152, 22);
            this.tmDeleteItem.Text = "移除项目";
            this.tmDeleteItem.Click += new System.EventHandler(this.tbtnRemove_ButtonClick);
            // 
            // tmEmptyList
            // 
            this.tmEmptyList.Name = "tmEmptyList";
            this.tmEmptyList.Size = new System.Drawing.Size(152, 22);
            this.tmEmptyList.Text = "清空列表";
            this.tmEmptyList.Click += new System.EventHandler(this.tmEmptyList_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(149, 6);
            // 
            // tmCloseList
            // 
            this.tmCloseList.Name = "tmCloseList";
            this.tmCloseList.Size = new System.Drawing.Size(152, 22);
            this.tmCloseList.Text = "关闭列表";
            this.tmCloseList.Click += new System.EventHandler(this.tmCloseList_Click);
            // 
            // tmDeleteListFile
            // 
            this.tmDeleteListFile.Name = "tmDeleteListFile";
            this.tmDeleteListFile.Size = new System.Drawing.Size(152, 22);
            this.tmDeleteListFile.Text = "删除列表文件";
            this.tmDeleteListFile.Click += new System.EventHandler(this.tmDeleteListFile_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // tbtnList
            // 
            this.tbtnList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnList.DropDownButtonWidth = 10;
            this.tbtnList.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmSaveList,
            this.tmSaveAs,
            this.toolStripMenuItem5,
            this.tmNewList});
            this.tbtnList.Image = global::MiniPlayerClassic.Properties.Resources.document;
            this.tbtnList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnList.Name = "tbtnList";
            this.tbtnList.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tbtnList.Size = new System.Drawing.Size(47, 36);
            this.tbtnList.ToolTipText = "列表选项";
            // 
            // tmSaveList
            // 
            this.tmSaveList.Name = "tmSaveList";
            this.tmSaveList.Size = new System.Drawing.Size(152, 22);
            this.tmSaveList.Text = "保存列表";
            this.tmSaveList.Click += new System.EventHandler(this.tmSaveList_Click);
            // 
            // tmSaveAs
            // 
            this.tmSaveAs.Name = "tmSaveAs";
            this.tmSaveAs.Size = new System.Drawing.Size(152, 22);
            this.tmSaveAs.Text = "列表另存为";
            this.tmSaveAs.Click += new System.EventHandler(this.tmSaveAs_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(149, 6);
            // 
            // tmNewList
            // 
            this.tmNewList.Name = "tmNewList";
            this.tmNewList.Size = new System.Drawing.Size(152, 22);
            this.tmNewList.Text = "新建列表";
            this.tmNewList.Click += new System.EventHandler(this.tmNewList_Click);
            // 
            // tbtnPlayMode
            // 
            this.tbtnPlayMode.DropDownButtonWidth = 10;
            this.tbtnPlayMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmPlaytheList,
            this.tmListRepeat,
            this.tmSingleRepeat,
            this.tmSingle,
            this.tmShuffle});
            this.tbtnPlayMode.Image = global::MiniPlayerClassic.Properties.Resources.single;
            this.tbtnPlayMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnPlayMode.Name = "tbtnPlayMode";
            this.tbtnPlayMode.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tbtnPlayMode.Size = new System.Drawing.Size(103, 36);
            this.tbtnPlayMode.Text = "单曲播放";
            this.tbtnPlayMode.ButtonClick += new System.EventHandler(this.tbtnPlayMode_ButtonClick);
            // 
            // tmPlaytheList
            // 
            this.tmPlaytheList.Name = "tmPlaytheList";
            this.tmPlaytheList.Size = new System.Drawing.Size(152, 22);
            this.tmPlaytheList.Text = "顺序播放";
            this.tmPlaytheList.Click += new System.EventHandler(this.tmPlaytheList_Click);
            // 
            // tmListRepeat
            // 
            this.tmListRepeat.Name = "tmListRepeat";
            this.tmListRepeat.Size = new System.Drawing.Size(152, 22);
            this.tmListRepeat.Text = "列表循环";
            this.tmListRepeat.Click += new System.EventHandler(this.tmListRepeat_Click);
            // 
            // tmSingleRepeat
            // 
            this.tmSingleRepeat.Name = "tmSingleRepeat";
            this.tmSingleRepeat.Size = new System.Drawing.Size(152, 22);
            this.tmSingleRepeat.Text = "单曲循环";
            this.tmSingleRepeat.Click += new System.EventHandler(this.tmSingleRepeat_Click);
            // 
            // tmSingle
            // 
            this.tmSingle.Name = "tmSingle";
            this.tmSingle.Size = new System.Drawing.Size(152, 22);
            this.tmSingle.Text = "单曲播放";
            this.tmSingle.Click += new System.EventHandler(this.tmSingle_Click);
            // 
            // tmShuffle
            // 
            this.tmShuffle.Name = "tmShuffle";
            this.tmShuffle.Size = new System.Drawing.Size(152, 22);
            this.tmShuffle.Text = "随机播放";
            this.tmShuffle.Click += new System.EventHandler(this.tmSuffle_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
            // 
            // tbtnModeChange
            // 
            this.tbtnModeChange.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tbtnModeChange.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnModeChange.Image = global::MiniPlayerClassic.Properties.Resources.arrow_down;
            this.tbtnModeChange.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnModeChange.Name = "tbtnModeChange";
            this.tbtnModeChange.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tbtnModeChange.Size = new System.Drawing.Size(36, 36);
            this.tbtnModeChange.ToolTipText = "请先新建列表";
            this.tbtnModeChange.Click += new System.EventHandler(this.tbtnModeChange_Click);
            this.tbtnModeChange.MouseEnter += new System.EventHandler(this.tbtnList_MouseEnter);
            // 
            // tbtnSettings
            // 
            this.tbtnSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnSettings.Image = global::MiniPlayerClassic.Properties.Resources.settings;
            this.tbtnSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnSettings.Name = "tbtnSettings";
            this.tbtnSettings.Size = new System.Drawing.Size(36, 36);
            this.tbtnSettings.ToolTipText = "设置（还没完工）";
            this.tbtnSettings.Click += new System.EventHandler(this.tbtnSettings_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.Controls.Add(this.btnStop, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnNext, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnPrev, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnPlay, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.pb_Volume, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pb_Progress, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(365, 100);
            this.tableLayoutPanel1.TabIndex = 29;
            // 
            // btnStop
            // 
            this.btnStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStop.Image = global::MiniPlayerClassic.Properties.Resources.stop;
            this.btnStop.Location = new System.Drawing.Point(173, 50);
            this.btnStop.Margin = new System.Windows.Forms.Padding(0);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(48, 50);
            this.btnStop.TabIndex = 27;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.SystemColors.Control;
            this.btnNext.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNext.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnNext.Image = global::MiniPlayerClassic.Properties.Resources.next;
            this.btnNext.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnNext.Location = new System.Drawing.Point(317, 50);
            this.btnNext.Margin = new System.Windows.Forms.Padding(0);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(48, 50);
            this.btnNext.TabIndex = 26;
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.BackColor = System.Drawing.SystemColors.Control;
            this.btnPrev.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPrev.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPrev.Image = global::MiniPlayerClassic.Properties.Resources.previous;
            this.btnPrev.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPrev.Location = new System.Drawing.Point(269, 50);
            this.btnPrev.Margin = new System.Windows.Forms.Padding(0);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(48, 50);
            this.btnPrev.TabIndex = 25;
            this.btnPrev.UseVisualStyleBackColor = false;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.BackColor = System.Drawing.SystemColors.Control;
            this.btnPlay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPlay.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPlay.Image = global::MiniPlayerClassic.Properties.Resources.play;
            this.btnPlay.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPlay.Location = new System.Drawing.Point(221, 50);
            this.btnPlay.Margin = new System.Windows.Forms.Padding(0);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(48, 50);
            this.btnPlay.TabIndex = 24;
            this.btnPlay.UseVisualStyleBackColor = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // pb_Volume
            // 
            this.pb_Volume.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb_Volume.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pb_Volume.Location = new System.Drawing.Point(0, 50);
            this.pb_Volume.Margin = new System.Windows.Forms.Padding(0);
            this.pb_Volume.Name = "pb_Volume";
            this.pb_Volume.Size = new System.Drawing.Size(173, 50);
            this.pb_Volume.TabIndex = 18;
            this.pb_Volume.TabStop = false;
            this.pb_Volume.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pb_Volume_MouseDown);
            this.pb_Volume.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pb_Volume_MouseMove);
            this.pb_Volume.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pb_Volume_MouseUp);
            // 
            // pb_Progress
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.pb_Progress, 5);
            this.pb_Progress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb_Progress.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pb_Progress.Location = new System.Drawing.Point(0, 0);
            this.pb_Progress.Margin = new System.Windows.Forms.Padding(0);
            this.pb_Progress.Name = "pb_Progress";
            this.pb_Progress.Size = new System.Drawing.Size(365, 50);
            this.pb_Progress.TabIndex = 17;
            this.pb_Progress.TabStop = false;
            this.pb_Progress.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pb_Progress_MouseDown);
            this.pb_Progress.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pb_Progress_MouseMove);
            this.pb_Progress.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pb_Progress_MouseUp);
            // 
            // MainFrom
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 494);
            this.Controls.Add(this.tableLayoutPanel2);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "MainFrom";
            this.Text = " ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFrom_FormClosing);
            this.Load += new System.EventHandler(this.MainFrom_Load);
            this.ResizeEnd += new System.EventHandler(this.MainFrom_ResizeEnd);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainFrom_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainFrom_DragEnter);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainFrom_KeyUp);
            this.menu_FileOpen.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tb_Lists.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_Volume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Progress)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer tmrPGBar;
        private System.Windows.Forms.ContextMenuStrip menu_FileOpen;
        private System.Windows.Forms.ToolStripMenuItem mAddFile;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.PictureBox pb_Volume;
        private System.Windows.Forms.PictureBox pb_Progress;
        private System.Windows.Forms.TabControl tb_Lists;
        internal System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSplitButton tbtnAdd;
        private System.Windows.Forms.ToolStripMenuItem tmAddList;
        private System.Windows.Forms.ToolStripMenuItem tmOpenList;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tmOpenFile;
        private System.Windows.Forms.ToolStripSplitButton tbtnRemove;
        private System.Windows.Forms.ToolStripMenuItem tmDeleteItem;
        private System.Windows.Forms.ToolStripMenuItem tmEmptyList;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem tmCloseList;
        private System.Windows.Forms.ToolStripMenuItem tmDeleteListFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSplitButton tbtnList;
        private System.Windows.Forms.ToolStripMenuItem tmSaveList;
        private System.Windows.Forms.ToolStripMenuItem tmSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem tmNewList;
        private System.Windows.Forms.ToolStripSplitButton tbtnPlayMode;
        private System.Windows.Forms.ToolStripMenuItem tmPlaytheList;
        private System.Windows.Forms.ToolStripMenuItem tmListRepeat;
        private System.Windows.Forms.ToolStripMenuItem tmSingleRepeat;
        private System.Windows.Forms.ToolStripMenuItem tmSingle;
        private System.Windows.Forms.ToolStripMenuItem tmShuffle;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tbtnModeChange;
        private System.Windows.Forms.ToolStripButton tbtnSettings;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListBox listBox1;
    }
}

