namespace SysBackup
{
	partial class Form1
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
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            label1 = new System.Windows.Forms.Label();
            lvErr = new System.Windows.Forms.ListView();
            clmErrTtl = new System.Windows.Forms.ColumnHeader();
            clmErrDir = new System.Windows.Forms.ColumnHeader();
            clmErrInfo = new System.Windows.Forms.ColumnHeader();
            label2 = new System.Windows.Forms.Label();
            lvDir = new System.Windows.Forms.ListView();
            clmTtl = new System.Windows.Forms.ColumnHeader();
            clmTyp = new System.Windows.Forms.ColumnHeader();
            btnDir_Inc = new System.Windows.Forms.Button();
            btnDir_Forget = new System.Windows.Forms.Button();
            btnDir_Dsc = new System.Windows.Forms.Button();
            mnuMn = new System.Windows.Forms.MenuStrip();
            backupsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            mnu_SlImp = new System.Windows.Forms.ToolStripMenuItem();
            mnuImp_tLRplc = new System.Windows.Forms.ToolStripMenuItem();
            mnuImp_tlAppnd = new System.Windows.Forms.ToolStripMenuItem();
            mnu_tlExp = new System.Windows.Forms.ToolStripMenuItem();
            mnu_tlClr = new System.Windows.Forms.ToolStripMenuItem();
            btnCopy = new System.Windows.Forms.Button();
            lblStat = new System.Windows.Forms.Label();
            txtDirBak = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            btnDirBak = new System.Windows.Forms.Button();
            lblPrg = new System.Windows.Forms.Label();
            txtPathIgnore = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            btnPathIgnore = new System.Windows.Forms.Button();
            mnuMn.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = System.Drawing.Color.FromArgb(58, 136, 198);
            label1.Location = new System.Drawing.Point(12, 543);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(40, 15);
            label1.TabIndex = 8;
            label1.Text = "Errors:";
            // 
            // lvErr
            // 
            lvErr.BackColor = System.Drawing.Color.FromArgb(31, 31, 31);
            lvErr.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { clmErrTtl, clmErrDir, clmErrInfo });
            lvErr.ForeColor = System.Drawing.Color.FromArgb(58, 136, 198);
            lvErr.FullRowSelect = true;
            lvErr.Location = new System.Drawing.Point(12, 561);
            lvErr.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            lvErr.MultiSelect = false;
            lvErr.Name = "lvErr";
            lvErr.Size = new System.Drawing.Size(459, 154);
            lvErr.TabIndex = 7;
            lvErr.UseCompatibleStateImageBehavior = false;
            lvErr.View = System.Windows.Forms.View.Details;
            lvErr.MouseDoubleClick += lvErr_MouseDoubleClick;
            // 
            // clmErrTtl
            // 
            clmErrTtl.Text = "Error";
            clmErrTtl.Width = 150;
            // 
            // clmErrDir
            // 
            clmErrDir.Text = "Directory/File";
            clmErrDir.Width = 235;
            // 
            // clmErrInfo
            // 
            clmErrInfo.Text = "Info";
            clmErrInfo.Width = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = System.Drawing.Color.FromArgb(58, 136, 198);
            label2.Location = new System.Drawing.Point(9, 90);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(67, 15);
            label2.TabIndex = 10;
            label2.Text = "Backup list:";
            // 
            // lvDir
            // 
            lvDir.BackColor = System.Drawing.Color.FromArgb(31, 31, 31);
            lvDir.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { clmTtl, clmTyp });
            lvDir.ForeColor = System.Drawing.Color.FromArgb(58, 136, 198);
            lvDir.FullRowSelect = true;
            lvDir.Location = new System.Drawing.Point(12, 111);
            lvDir.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            lvDir.MultiSelect = false;
            lvDir.Name = "lvDir";
            lvDir.Size = new System.Drawing.Size(459, 392);
            lvDir.TabIndex = 11;
            lvDir.UseCompatibleStateImageBehavior = false;
            lvDir.View = System.Windows.Forms.View.Details;
            // 
            // clmTtl
            // 
            clmTtl.Text = "Title";
            clmTtl.Width = 328;
            // 
            // clmTyp
            // 
            clmTyp.Text = "Type";
            clmTyp.Width = 45;
            // 
            // btnDir_Inc
            // 
            btnDir_Inc.BackColor = System.Drawing.SystemColors.Control;
            btnDir_Inc.Location = new System.Drawing.Point(89, 87);
            btnDir_Inc.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnDir_Inc.Name = "btnDir_Inc";
            btnDir_Inc.Size = new System.Drawing.Size(27, 27);
            btnDir_Inc.TabIndex = 12;
            btnDir_Inc.Text = "+";
            btnDir_Inc.UseVisualStyleBackColor = false;
            btnDir_Inc.Click += btnDir_Inc_Click;
            // 
            // btnDir_Forget
            // 
            btnDir_Forget.BackColor = System.Drawing.SystemColors.Control;
            btnDir_Forget.Location = new System.Drawing.Point(433, 87);
            btnDir_Forget.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnDir_Forget.Name = "btnDir_Forget";
            btnDir_Forget.Size = new System.Drawing.Size(38, 27);
            btnDir_Forget.TabIndex = 13;
            btnDir_Forget.Text = "Del";
            btnDir_Forget.UseVisualStyleBackColor = false;
            btnDir_Forget.Click += btnDir_Forget_Click;
            // 
            // btnDir_Dsc
            // 
            btnDir_Dsc.BackColor = System.Drawing.SystemColors.Control;
            btnDir_Dsc.Location = new System.Drawing.Point(122, 87);
            btnDir_Dsc.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnDir_Dsc.Name = "btnDir_Dsc";
            btnDir_Dsc.Size = new System.Drawing.Size(27, 27);
            btnDir_Dsc.TabIndex = 14;
            btnDir_Dsc.Text = "-";
            btnDir_Dsc.UseVisualStyleBackColor = false;
            btnDir_Dsc.Click += btnDir_Dsc_Click;
            // 
            // mnuMn
            // 
            mnuMn.BackColor = System.Drawing.Color.FromArgb(63, 63, 63);
            mnuMn.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { backupsToolStripMenuItem });
            mnuMn.Location = new System.Drawing.Point(0, 0);
            mnuMn.Name = "mnuMn";
            mnuMn.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            mnuMn.Size = new System.Drawing.Size(481, 24);
            mnuMn.TabIndex = 15;
            mnuMn.Text = "menuStrip1";
            // 
            // backupsToolStripMenuItem
            // 
            backupsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { mnu_SlImp, mnu_tlExp, mnu_tlClr });
            backupsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(58, 136, 198);
            backupsToolStripMenuItem.Name = "backupsToolStripMenuItem";
            backupsToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            backupsToolStripMenuItem.Text = "Backups";
            // 
            // mnu_SlImp
            // 
            mnu_SlImp.BackColor = System.Drawing.Color.FromArgb(63, 63, 63);
            mnu_SlImp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { mnuImp_tLRplc, mnuImp_tlAppnd });
            mnu_SlImp.ForeColor = System.Drawing.Color.FromArgb(58, 136, 198);
            mnu_SlImp.Name = "mnu_SlImp";
            mnu_SlImp.Size = new System.Drawing.Size(110, 22);
            mnu_SlImp.Text = "Import";
            // 
            // mnuImp_tLRplc
            // 
            mnuImp_tLRplc.BackColor = System.Drawing.Color.FromArgb(63, 63, 63);
            mnuImp_tLRplc.ForeColor = System.Drawing.Color.FromArgb(58, 136, 198);
            mnuImp_tLRplc.Name = "mnuImp_tLRplc";
            mnuImp_tLRplc.Size = new System.Drawing.Size(116, 22);
            mnuImp_tLRplc.Text = "Replace";
            mnuImp_tLRplc.Click += mnuImp_tLRplc_Click;
            // 
            // mnuImp_tlAppnd
            // 
            mnuImp_tlAppnd.BackColor = System.Drawing.Color.FromArgb(63, 63, 63);
            mnuImp_tlAppnd.ForeColor = System.Drawing.Color.FromArgb(58, 136, 198);
            mnuImp_tlAppnd.Name = "mnuImp_tlAppnd";
            mnuImp_tlAppnd.Size = new System.Drawing.Size(116, 22);
            mnuImp_tlAppnd.Text = "Append";
            mnuImp_tlAppnd.Click += mnuImp_tlAppnd_Click;
            // 
            // mnu_tlExp
            // 
            mnu_tlExp.BackColor = System.Drawing.Color.FromArgb(63, 63, 63);
            mnu_tlExp.ForeColor = System.Drawing.Color.FromArgb(58, 136, 198);
            mnu_tlExp.Name = "mnu_tlExp";
            mnu_tlExp.Size = new System.Drawing.Size(110, 22);
            mnu_tlExp.Text = "Export";
            mnu_tlExp.Click += mnu_tlExp_Click;
            // 
            // mnu_tlClr
            // 
            mnu_tlClr.BackColor = System.Drawing.Color.FromArgb(63, 63, 63);
            mnu_tlClr.ForeColor = System.Drawing.Color.FromArgb(58, 136, 198);
            mnu_tlClr.Name = "mnu_tlClr";
            mnu_tlClr.Size = new System.Drawing.Size(110, 22);
            mnu_tlClr.Text = "Clear";
            mnu_tlClr.Click += mnu_tlClr_Click;
            // 
            // btnCopy
            // 
            btnCopy.BackColor = System.Drawing.SystemColors.Control;
            btnCopy.Location = new System.Drawing.Point(210, 58);
            btnCopy.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnCopy.Name = "btnCopy";
            btnCopy.Size = new System.Drawing.Size(102, 27);
            btnCopy.TabIndex = 16;
            btnCopy.Text = "RUN BACKUP";
            btnCopy.UseVisualStyleBackColor = false;
            btnCopy.Click += btnCopy_Click;
            // 
            // lblStat
            // 
            lblStat.AutoSize = true;
            lblStat.ForeColor = System.Drawing.Color.FromArgb(58, 136, 198);
            lblStat.Location = new System.Drawing.Point(12, 507);
            lblStat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblStat.Name = "lblStat";
            lblStat.Size = new System.Drawing.Size(45, 15);
            lblStat.TabIndex = 17;
            lblStat.Text = "Status: ";
            // 
            // txtDirBak
            // 
            txtDirBak.Location = new System.Drawing.Point(162, 2);
            txtDirBak.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtDirBak.Name = "txtDirBak";
            txtDirBak.ReadOnly = true;
            txtDirBak.Size = new System.Drawing.Size(263, 23);
            txtDirBak.TabIndex = 18;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = System.Drawing.Color.FromArgb(63, 63, 63);
            label3.ForeColor = System.Drawing.Color.FromArgb(58, 136, 198);
            label3.Location = new System.Drawing.Point(136, 5);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(22, 15);
            label3.TabIndex = 19;
            label3.Text = "To:";
            // 
            // btnDirBak
            // 
            btnDirBak.BackColor = System.Drawing.SystemColors.Control;
            btnDirBak.Location = new System.Drawing.Point(433, 0);
            btnDirBak.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnDirBak.Name = "btnDirBak";
            btnDirBak.Size = new System.Drawing.Size(47, 27);
            btnDirBak.TabIndex = 20;
            btnDirBak.Text = ". . .";
            btnDirBak.UseVisualStyleBackColor = false;
            btnDirBak.Click += btnDirBak_Click;
            // 
            // lblPrg
            // 
            lblPrg.AutoSize = true;
            lblPrg.ForeColor = System.Drawing.Color.FromArgb(58, 136, 198);
            lblPrg.Location = new System.Drawing.Point(12, 524);
            lblPrg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblPrg.Name = "lblPrg";
            lblPrg.Size = new System.Drawing.Size(55, 15);
            lblPrg.TabIndex = 21;
            lblPrg.Text = "Progress:";
            // 
            // txtPathIgnore
            // 
            txtPathIgnore.Location = new System.Drawing.Point(162, 31);
            txtPathIgnore.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtPathIgnore.Name = "txtPathIgnore";
            txtPathIgnore.Size = new System.Drawing.Size(263, 23);
            txtPathIgnore.TabIndex = 22;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = System.Drawing.Color.FromArgb(63, 63, 63);
            label4.ForeColor = System.Drawing.Color.FromArgb(58, 136, 198);
            label4.Location = new System.Drawing.Point(35, 34);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(123, 15);
            label4.TabIndex = 23;
            label4.Text = "Ignore common path:";
            // 
            // btnPathIgnore
            // 
            btnPathIgnore.BackColor = System.Drawing.SystemColors.Control;
            btnPathIgnore.Location = new System.Drawing.Point(433, 28);
            btnPathIgnore.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnPathIgnore.Name = "btnPathIgnore";
            btnPathIgnore.Size = new System.Drawing.Size(47, 27);
            btnPathIgnore.TabIndex = 24;
            btnPathIgnore.Text = "Auto";
            btnPathIgnore.UseVisualStyleBackColor = false;
            btnPathIgnore.Click += btnPathIgnore_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(47, 47, 47);
            ClientSize = new System.Drawing.Size(481, 726);
            Controls.Add(btnPathIgnore);
            Controls.Add(txtPathIgnore);
            Controls.Add(label4);
            Controls.Add(lblPrg);
            Controls.Add(btnDirBak);
            Controls.Add(txtDirBak);
            Controls.Add(label3);
            Controls.Add(lblStat);
            Controls.Add(btnCopy);
            Controls.Add(btnDir_Dsc);
            Controls.Add(btnDir_Forget);
            Controls.Add(btnDir_Inc);
            Controls.Add(lvDir);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lvErr);
            Controls.Add(mnuMn);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MainMenuStrip = mnuMn;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "Form1";
            Text = "SysBackup";
            Load += Form1_Load;
            mnuMn.ResumeLayout(false);
            mnuMn.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListView lvErr;
		private System.Windows.Forms.ColumnHeader clmErrTtl;
		private System.Windows.Forms.ColumnHeader clmErrDir;
		private System.Windows.Forms.ColumnHeader clmErrInfo;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListView lvDir;
		private System.Windows.Forms.ColumnHeader clmTtl;
		private System.Windows.Forms.ColumnHeader clmTyp;
		private System.Windows.Forms.Button btnDir_Inc;
		private System.Windows.Forms.Button btnDir_Forget;
		private System.Windows.Forms.Button btnDir_Dsc;
		private System.Windows.Forms.MenuStrip mnuMn;
		private System.Windows.Forms.ToolStripMenuItem backupsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem mnu_SlImp;
		private System.Windows.Forms.ToolStripMenuItem mnuImp_tLRplc;
		private System.Windows.Forms.ToolStripMenuItem mnuImp_tlAppnd;
		private System.Windows.Forms.ToolStripMenuItem mnu_tlExp;
		private System.Windows.Forms.Button btnCopy;
		private System.Windows.Forms.Label lblStat;
        private System.Windows.Forms.ToolStripMenuItem mnu_tlClr;
        private System.Windows.Forms.TextBox txtDirBak;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDirBak;
        private System.Windows.Forms.Label lblPrg;
        private System.Windows.Forms.TextBox txtPathIgnore;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnPathIgnore;
    }
}

