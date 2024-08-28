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
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.lvErr = new System.Windows.Forms.ListView();
			this.clmErrTtl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.clmErrDir = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.clmErrInfo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.label2 = new System.Windows.Forms.Label();
			this.lvDir = new System.Windows.Forms.ListView();
			this.clmTtl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.clmTyp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.btnDir_Inc = new System.Windows.Forms.Button();
			this.btnDir_Forget = new System.Windows.Forms.Button();
			this.btnDir_Dsc = new System.Windows.Forms.Button();
			this.mnuMn = new System.Windows.Forms.MenuStrip();
			this.backupsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mnu_SlImp = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuImp_tLRplc = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuImp_tlAppnd = new System.Windows.Forms.ToolStripMenuItem();
			this.mnu_tlExp = new System.Windows.Forms.ToolStripMenuItem();
			this.btnBak = new System.Windows.Forms.Button();
			this.lblStat = new System.Windows.Forms.Label();
			this.mnu_tlClr = new System.Windows.Forms.ToolStripMenuItem();
			this.txtDirBak = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.btnDirBak = new System.Windows.Forms.Button();
			this.lblPrg = new System.Windows.Forms.Label();
			this.mnuMn.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(136)))), ((int)(((byte)(198)))));
			this.label1.Location = new System.Drawing.Point(10, 447);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(37, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "Errors:";
			// 
			// lvErr
			// 
			this.lvErr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
			this.lvErr.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmErrTtl,
            this.clmErrDir,
            this.clmErrInfo});
			this.lvErr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(136)))), ((int)(((byte)(198)))));
			this.lvErr.FullRowSelect = true;
			this.lvErr.HideSelection = false;
			this.lvErr.Location = new System.Drawing.Point(10, 463);
			this.lvErr.MultiSelect = false;
			this.lvErr.Name = "lvErr";
			this.lvErr.Size = new System.Drawing.Size(394, 134);
			this.lvErr.TabIndex = 7;
			this.lvErr.UseCompatibleStateImageBehavior = false;
			this.lvErr.View = System.Windows.Forms.View.Details;
			this.lvErr.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvErr_MouseDoubleClick);
			// 
			// clmErrTtl
			// 
			this.clmErrTtl.Text = "Error";
			this.clmErrTtl.Width = 150;
			// 
			// clmErrDir
			// 
			this.clmErrDir.Text = "Directory/File";
			this.clmErrDir.Width = 235;
			// 
			// clmErrInfo
			// 
			this.clmErrInfo.Text = "Info";
			this.clmErrInfo.Width = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(136)))), ((int)(((byte)(198)))));
			this.label2.Location = new System.Drawing.Point(8, 55);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(62, 13);
			this.label2.TabIndex = 10;
			this.label2.Text = "Backup list:";
			// 
			// lvDir
			// 
			this.lvDir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
			this.lvDir.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmTtl,
            this.clmTyp});
			this.lvDir.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(136)))), ((int)(((byte)(198)))));
			this.lvDir.FullRowSelect = true;
			this.lvDir.HideSelection = false;
			this.lvDir.Location = new System.Drawing.Point(10, 73);
			this.lvDir.MultiSelect = false;
			this.lvDir.Name = "lvDir";
			this.lvDir.Size = new System.Drawing.Size(394, 340);
			this.lvDir.TabIndex = 11;
			this.lvDir.UseCompatibleStateImageBehavior = false;
			this.lvDir.View = System.Windows.Forms.View.Details;
			// 
			// clmTtl
			// 
			this.clmTtl.Text = "Title";
			this.clmTtl.Width = 328;
			// 
			// clmTyp
			// 
			this.clmTyp.Text = "Type";
			this.clmTyp.Width = 45;
			// 
			// btnDir_Inc
			// 
			this.btnDir_Inc.Location = new System.Drawing.Point(76, 52);
			this.btnDir_Inc.Name = "btnDir_Inc";
			this.btnDir_Inc.Size = new System.Drawing.Size(23, 23);
			this.btnDir_Inc.TabIndex = 12;
			this.btnDir_Inc.Text = "+";
			this.btnDir_Inc.UseVisualStyleBackColor = true;
			this.btnDir_Inc.Click += new System.EventHandler(this.btnDir_Inc_Click);
			// 
			// btnDir_Forget
			// 
			this.btnDir_Forget.Location = new System.Drawing.Point(371, 52);
			this.btnDir_Forget.Name = "btnDir_Forget";
			this.btnDir_Forget.Size = new System.Drawing.Size(33, 23);
			this.btnDir_Forget.TabIndex = 13;
			this.btnDir_Forget.Text = "Del";
			this.btnDir_Forget.UseVisualStyleBackColor = true;
			this.btnDir_Forget.Click += new System.EventHandler(this.btnDir_Forget_Click);
			// 
			// btnDir_Dsc
			// 
			this.btnDir_Dsc.Location = new System.Drawing.Point(105, 52);
			this.btnDir_Dsc.Name = "btnDir_Dsc";
			this.btnDir_Dsc.Size = new System.Drawing.Size(23, 23);
			this.btnDir_Dsc.TabIndex = 14;
			this.btnDir_Dsc.Text = "-";
			this.btnDir_Dsc.UseVisualStyleBackColor = true;
			this.btnDir_Dsc.Click += new System.EventHandler(this.btnDir_Dsc_Click);
			// 
			// mnuMn
			// 
			this.mnuMn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
			this.mnuMn.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backupsToolStripMenuItem});
			this.mnuMn.Location = new System.Drawing.Point(0, 0);
			this.mnuMn.Name = "mnuMn";
			this.mnuMn.Size = new System.Drawing.Size(412, 24);
			this.mnuMn.TabIndex = 15;
			this.mnuMn.Text = "menuStrip1";
			// 
			// backupsToolStripMenuItem
			// 
			this.backupsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_SlImp,
            this.mnu_tlExp,
            this.mnu_tlClr});
			this.backupsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(136)))), ((int)(((byte)(198)))));
			this.backupsToolStripMenuItem.Name = "backupsToolStripMenuItem";
			this.backupsToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
			this.backupsToolStripMenuItem.Text = "Backups";
			// 
			// mnu_SlImp
			// 
			this.mnu_SlImp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
			this.mnu_SlImp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuImp_tLRplc,
            this.mnuImp_tlAppnd});
			this.mnu_SlImp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(136)))), ((int)(((byte)(198)))));
			this.mnu_SlImp.Name = "mnu_SlImp";
			this.mnu_SlImp.Size = new System.Drawing.Size(180, 22);
			this.mnu_SlImp.Text = "Import";
			// 
			// mnuImp_tLRplc
			// 
			this.mnuImp_tLRplc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
			this.mnuImp_tLRplc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(136)))), ((int)(((byte)(198)))));
			this.mnuImp_tLRplc.Name = "mnuImp_tLRplc";
			this.mnuImp_tLRplc.Size = new System.Drawing.Size(180, 22);
			this.mnuImp_tLRplc.Text = "Replace";
			this.mnuImp_tLRplc.Click += new System.EventHandler(this.mnuImp_tLRplc_Click);
			// 
			// mnuImp_tlAppnd
			// 
			this.mnuImp_tlAppnd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
			this.mnuImp_tlAppnd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(136)))), ((int)(((byte)(198)))));
			this.mnuImp_tlAppnd.Name = "mnuImp_tlAppnd";
			this.mnuImp_tlAppnd.Size = new System.Drawing.Size(180, 22);
			this.mnuImp_tlAppnd.Text = "Append";
			this.mnuImp_tlAppnd.Click += new System.EventHandler(this.mnuImp_tlAppnd_Click);
			// 
			// mnu_tlExp
			// 
			this.mnu_tlExp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
			this.mnu_tlExp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(136)))), ((int)(((byte)(198)))));
			this.mnu_tlExp.Name = "mnu_tlExp";
			this.mnu_tlExp.Size = new System.Drawing.Size(180, 22);
			this.mnu_tlExp.Text = "Export";
			this.mnu_tlExp.Click += new System.EventHandler(this.mnu_tlExp_Click);
			// 
			// btnBak
			// 
			this.btnBak.Location = new System.Drawing.Point(180, 27);
			this.btnBak.Name = "btnBak";
			this.btnBak.Size = new System.Drawing.Size(87, 23);
			this.btnBak.TabIndex = 16;
			this.btnBak.Text = "RUN BACKUP";
			this.btnBak.UseVisualStyleBackColor = true;
			this.btnBak.Click += new System.EventHandler(this.btnBak_Click);
			// 
			// lblStat
			// 
			this.lblStat.AutoSize = true;
			this.lblStat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(136)))), ((int)(((byte)(198)))));
			this.lblStat.Location = new System.Drawing.Point(10, 416);
			this.lblStat.Name = "lblStat";
			this.lblStat.Size = new System.Drawing.Size(43, 13);
			this.lblStat.TabIndex = 17;
			this.lblStat.Text = "Status: ";
			// 
			// mnu_tlClr
			// 
			this.mnu_tlClr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
			this.mnu_tlClr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(136)))), ((int)(((byte)(198)))));
			this.mnu_tlClr.Name = "mnu_tlClr";
			this.mnu_tlClr.Size = new System.Drawing.Size(180, 22);
			this.mnu_tlClr.Text = "Clear";
			this.mnu_tlClr.Click += new System.EventHandler(this.mnu_tlClr_Click);
			// 
			// txtDirBak
			// 
			this.txtDirBak.Location = new System.Drawing.Point(139, 2);
			this.txtDirBak.Name = "txtDirBak";
			this.txtDirBak.ReadOnly = true;
			this.txtDirBak.Size = new System.Drawing.Size(226, 20);
			this.txtDirBak.TabIndex = 18;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
			this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(136)))), ((int)(((byte)(198)))));
			this.label3.Location = new System.Drawing.Point(117, 4);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(23, 13);
			this.label3.TabIndex = 19;
			this.label3.Text = "To:";
			// 
			// btnDirBak
			// 
			this.btnDirBak.Location = new System.Drawing.Point(371, 0);
			this.btnDirBak.Name = "btnDirBak";
			this.btnDirBak.Size = new System.Drawing.Size(40, 23);
			this.btnDirBak.TabIndex = 20;
			this.btnDirBak.Text = ". . .";
			this.btnDirBak.UseVisualStyleBackColor = true;
			this.btnDirBak.Click += new System.EventHandler(this.btnDirBak_Click);
			// 
			// lblPrg
			// 
			this.lblPrg.AutoSize = true;
			this.lblPrg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(136)))), ((int)(((byte)(198)))));
			this.lblPrg.Location = new System.Drawing.Point(10, 431);
			this.lblPrg.Name = "lblPrg";
			this.lblPrg.Size = new System.Drawing.Size(51, 13);
			this.lblPrg.TabIndex = 21;
			this.lblPrg.Text = "Progress:";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
			this.ClientSize = new System.Drawing.Size(412, 606);
			this.Controls.Add(this.lblPrg);
			this.Controls.Add(this.btnDirBak);
			this.Controls.Add(this.txtDirBak);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.lblStat);
			this.Controls.Add(this.btnBak);
			this.Controls.Add(this.btnDir_Dsc);
			this.Controls.Add(this.btnDir_Forget);
			this.Controls.Add(this.btnDir_Inc);
			this.Controls.Add(this.lvDir);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lvErr);
			this.Controls.Add(this.mnuMn);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MainMenuStrip = this.mnuMn;
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.Text = "SysBackup";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.mnuMn.ResumeLayout(false);
			this.mnuMn.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

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
		private System.Windows.Forms.Button btnBak;
		private System.Windows.Forms.Label lblStat;
        private System.Windows.Forms.ToolStripMenuItem mnu_tlClr;
        private System.Windows.Forms.TextBox txtDirBak;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDirBak;
        private System.Windows.Forms.Label lblPrg;
    }
}

