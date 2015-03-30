namespace MiniPlayerClassic
{
    partial class frmSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chkForceWindow = new System.Windows.Forms.CheckBox();
            this.chkRemeberLast = new System.Windows.Forms.CheckBox();
            this.chkCreateWhenOpen = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.chkDevelopMode = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbListFolder = new System.Windows.Forms.TextBox();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chkForceWindow
            // 
            this.chkForceWindow.AutoSize = true;
            this.chkForceWindow.Location = new System.Drawing.Point(16, 14);
            this.chkForceWindow.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkForceWindow.Name = "chkForceWindow";
            this.chkForceWindow.Size = new System.Drawing.Size(84, 24);
            this.chkForceWindow.TabIndex = 0;
            this.chkForceWindow.Text = "窗体前置";
            this.chkForceWindow.UseVisualStyleBackColor = true;
            // 
            // chkRemeberLast
            // 
            this.chkRemeberLast.AutoSize = true;
            this.chkRemeberLast.Location = new System.Drawing.Point(16, 45);
            this.chkRemeberLast.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkRemeberLast.Name = "chkRemeberLast";
            this.chkRemeberLast.Size = new System.Drawing.Size(126, 24);
            this.chkRemeberLast.TabIndex = 1;
            this.chkRemeberLast.Text = "记住上次的列表";
            this.chkRemeberLast.UseVisualStyleBackColor = true;
            // 
            // chkCreateWhenOpen
            // 
            this.chkCreateWhenOpen.AutoSize = true;
            this.chkCreateWhenOpen.Location = new System.Drawing.Point(150, 45);
            this.chkCreateWhenOpen.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkCreateWhenOpen.Name = "chkCreateWhenOpen";
            this.chkCreateWhenOpen.Size = new System.Drawing.Size(140, 24);
            this.chkCreateWhenOpen.TabIndex = 2;
            this.chkCreateWhenOpen.Text = "默认新建一个列表";
            this.chkCreateWhenOpen.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(12, 131);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(239, 44);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(258, 132);
            this.btnReset.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(49, 43);
            this.btnReset.TabIndex = 4;
            this.btnReset.Text = "重置";
            this.btnReset.UseVisualStyleBackColor = true;
            // 
            // chkDevelopMode
            // 
            this.chkDevelopMode.AutoSize = true;
            this.chkDevelopMode.Location = new System.Drawing.Point(108, 15);
            this.chkDevelopMode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkDevelopMode.Name = "chkDevelopMode";
            this.chkDevelopMode.Size = new System.Drawing.Size(98, 24);
            this.chkDevelopMode.TabIndex = 5;
            this.chkDevelopMode.Text = "多功能模式";
            this.chkDevelopMode.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(206, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "这是什么？";
            this.label1.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "默认列表存储位置";
            // 
            // tbListFolder
            // 
            this.tbListFolder.Location = new System.Drawing.Point(12, 97);
            this.tbListFolder.Name = "tbListFolder";
            this.tbListFolder.Size = new System.Drawing.Size(263, 26);
            this.tbListFolder.TabIndex = 8;
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Location = new System.Drawing.Point(281, 97);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(27, 26);
            this.btnSelectFolder.TabIndex = 9;
            this.btnSelectFolder.Text = "...";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 184);
            this.Controls.Add(this.btnSelectFolder);
            this.Controls.Add(this.tbListFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkDevelopMode);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.chkCreateWhenOpen);
            this.Controls.Add(this.chkRemeberLast);
            this.Controls.Add(this.chkForceWindow);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSettings";
            this.Text = "设置";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkForceWindow;
        private System.Windows.Forms.CheckBox chkRemeberLast;
        private System.Windows.Forms.CheckBox chkCreateWhenOpen;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.CheckBox chkDevelopMode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbListFolder;
        private System.Windows.Forms.Button btnSelectFolder;
    }
}