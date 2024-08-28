using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;
using System.IO;

using libSnippetsCS;

using cG = libSnippetsCS.classGraphics;
using cF = libSnippetsCS.classFiles;
using cM = libSnippetsCS.classMath;
using cSM = libSnippetsCS.Strings.StringsMisc;

using libSnippetsCS.Strings;

namespace SysBackup
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		delegate void Work();

		class SuperStack<T>
		{
			Stack<Stack<T>> mother = new Stack<Stack<T>>();
			Stack<T> child = new Stack<T>();

			public SuperStack() {}
			public void Push(T obj)
			{
				if (child.Count < int.MaxValue) child.Push(obj);
				else {
					mother.Push(child);
					child = new Stack<T>();
					child.Push(obj);
				}
			}
			public T Pop()
			{
				 if (child.IsEmpty()) {
					if (mother.IsEmpty()) return default(T);
					else
						child = mother.Pop();
				 }

				 return child.Pop();
			}
			public bool IsEmpty()
			{
				return child.IsEmpty() && mother.IsEmpty();
			}
		}

		readonly Color CLR_T_NORM = cG.CColor(58, 136, 198);
		readonly Color CLR_C_NORM = cG.MColor(31);
		readonly Color CLR_C_DIS = cG.CColor(130, 135, 144);
		readonly Color CLR_B_NORM = cG.MColor(47);

		readonly Color CLR_FLD_T_NORM = cG.CColor(58, 136, 198);
		readonly Color CLR_FLD_T_ERR = cG.CColor(58, 136, 198);
		readonly Color CLR_FLD_B_NORM = cG.MColor(39);
		readonly Color CLR_FLD_B_ERR = cG.CColor(81, 31, 31);

		readonly Color CLR_FL_T_NORM = cG.CColor(58, 136, 198);
		readonly Color CLR_FL_T_ERR = cG.CColor(58, 136, 198);
		readonly Color CLR_FL_B_NORM = cG.MColor(31);
		readonly Color CLR_FL_B_ERR = cG.CColor(57, 27, 27);

		static readonly string DIR_DOC = cF.GetDir.Docs + @"\CodeGig\SysBackup";
		readonly string DIR_IMP_EXP = DIR_DOC + @"\Presets";

		private void Form1_Load(object sender, EventArgs e)
		{
			cF.Dir_MakeIfNotExist(DIR_IMP_EXP);
		}

		private void btnDir_Inc_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog fld = new FolderBrowserDialog();
			if (fld.ShowDialog() == DialogResult.OK)
				lvDir.Items.Add(MakeLine("+ " + fld.SelectedPath, "Folder"));
		}
		private void btnDir_Dsc_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog fld = new FolderBrowserDialog();
			if (fld.ShowDialog() == DialogResult.OK)
				lvDir.Items.Add(MakeLine("- " + fld.SelectedPath, "Folder"));
		}
		private void btnDir_Forget_Click(object sender, EventArgs e)
		{
			if (lvDir.SelectedIndices.Count > 0)
				lvDir.Items.RemoveAt(lvDir.SelectedIndices[0]);
		}

		private void AddErr(string ttl, string dir, string info)
		{
			ListViewItem ln = new ListViewItem(new string[] {ttl, dir, info});
			lvErr.Items.Add(ln);
		}
		private ListViewItem MakeLine(string ttl, string typ)
		{
			return new ListViewItem(new string[] {ttl, typ });
		}

		private void mnu_tlExp_Click(object sender, EventArgs e)
		{
			List<string> data = new List<string>();
			string fl = cF.SaveFile("Preset (.bkps)", "bkps", "Select a backup preset", DIR_IMP_EXP);
			if (fl == "") return;

			foreach (ListViewItem itm in lvDir.Items)
				data.Add(itm.SubItems[0].Text);

			cF.TextFile_Overwrite(fl, data.List_ToString());
		}

		private void mnuImp_tLRplc_Click(object sender, EventArgs e)
		{
			lvDir.Items.Clear();
			Import();
		}

		private void mnuImp_tlAppnd_Click(object sender, EventArgs e)
		{
			Import();
		}
		private void mnu_tlClr_Click(object sender, EventArgs e)
		{
			lvDir.Items.Clear();
		}
		private void Import()
		{
			string fl = cF.OpenFile("Preset (.bkps)", "bkps", "Select a backup preset", DIR_IMP_EXP);
			if (fl == "") return;

			foreach (string ln in cF.TextFile_GetAllText(fl).GetLines())
				lvDir.Items.Add(MakeLine(ln, "Folder"));
		}

		private void btnDirBak_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog fld = new FolderBrowserDialog();
			if (fld.ShowDialog() == DialogResult.OK)
				txtDirBak.Text = fld.SelectedPath;
		}

		private void lvErr_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (lvErr.SelectedIndices[0] > 0) {
				ListViewItem itm = lvErr.SelectedItems[0];
				MessageBox.Show(itm.SubItems[1] + "\r\n\r\n" + itm.SubItems[2]);
			}
		}
		
		private void Controls_SetEnabled(bool doEn)
		{
			btnBak.Enabled = doEn;
			btnDirBak.Enabled = doEn;

			btnDir_Inc.Enabled = doEn;
			btnDir_Dsc.Enabled = doEn;
			btnDir_Forget.Enabled = doEn;

			backupsToolStripMenuItem.Enabled = doEn;
		}
		private void btnBak_Click(object sender, EventArgs e)
		{
			if (txtDirBak.Text != "") {
				/*Task tskMstr = new Task(() => {
					DoCopy();
				});
				tskMstr.Start();
				await tskMstr;*/

				new Thread( () => DoCopy()).Start();
			}
		}

		private void DoCopy()
		{
			bool halt = false;

			string dirBak = "";
			long srcSze = 0, dstSze = 0;

			FormAwait(() => {
				dirBak = txtDirBak.Text;

				if (dirBak == "" || lvDir.Items.Count == 0 || !Directory.Exists(dirBak)) {
					halt = true;
					return;
				}

				lblStat.Text = "Status: analyzing...";
				lblPrg.Text = "Progress: ";
				lvErr.Items.Clear();
				Controls_SetEnabled(false);
			});

			if (halt) return;

			Stack<string> stkDirs_init = new Stack<string>(),
					stkDir_root = new Stack<string>();

			SuperStack<string> stkCpy = new SuperStack<string>();

			List<string> lstIgnore = new List<string>();
			List<string> lvItms = new List<string>();

			FormAwait(() => {
				//initial dir-list
				foreach (ListViewItem itm in lvDir.Items)
					lvItms.Add(itm.SubItems[0].Text);
			});
				
			foreach (string itm in lvItms) {
				string dir = itm;
				bool ignore = dir.StartsWith("- ");
				dir = dir.FromIndex(2);

				try {
					if (cF.Dir_IsSymbolic(dir)) continue;
				} catch (Exception ex) {
					FormAwait(() => {
						AddErr("Folder", dir, ex.ToString());
					});
					continue;
				}

				if (ignore) {
					lstIgnore.Add(dir);
					continue;
				}

				stkDirs_init.Push(dir);
			}

			//nested dir-list
			while (!stkDirs_init.IsEmpty())
			{
				stkDir_root.Push(stkDirs_init.Pop()); //hold one root-dir.

				while (!stkDir_root.IsEmpty()) //for (that root-dir)
				{
					bool isEnd = true;
					string root = stkDir_root.Pop();

					//ignore
					try {
						if (cF.Dir_IsSymbolic(root) || lstIgnore.Contains(root)) continue;
					} catch (Exception ex) {
						FormAwait(() => {
							AddErr("Folder", root, ex.ToString());
						});
						continue;
					}

					//all files
					try {
						Parallel.ForEach(Directory.GetFiles(root), fl => {
							try {
								FileInfo fI = new FileInfo(fl);

								//Ignore symbolic
								if (!fI.IsSymbolic()) {
									//Include
									srcSze += fI.Length;
									stkCpy.Push(fl);
								}
							} catch (Exception ex) {
								FormAwait(() => {
									AddErr("File", fl, ex.ToString());
								});
							}
						});
					} catch (Exception ex) {
						FormAwait(() => {
							AddErr("File", root, ex.ToString());
						});
					}

					//subdirectories
					try {
						Parallel.ForEach(Directory.GetDirectories(root), subdir => {
							//Ignore symbolic
							try {
								if (! (cF.Dir_IsSymbolic(subdir) || lstIgnore.Contains(subdir)) ) {
									//Include
									stkDir_root.Push(subdir);
									isEnd = false;
								}
							} catch (Exception ex) {
								FormAwait(() => {
									AddErr("File", subdir, ex.ToString());
								});
							}
						});
					} catch (Exception ex) {
						FormAwait(() => {
							AddErr("File", root, ex.ToString());
						});
					}

					if (isEnd) stkCpy.Push(root);
					
				}
			}
				
			FormAwait(() => {
				lblStat.Text = "Status: beginning to copy...";
			});

			//do copy
			List<string> lstCpy = new List<string>();

			while (!stkCpy.IsEmpty()) {
				string itm = stkCpy.Pop();
				if ((itm ?? "") != "")
					lstCpy.Add(itm);
			}

			lstCpy.Reverse();

			FormAwait(() => {
				lblStat.Text = "Copying...";
			});

			string s_srcSze = cSM.Size_Compress(srcSze, 1024, 3, cSM.SizeNames_Bytes, false);

			List<Task> tasks = new List<Task>();

			//Copy folders (as empty)
			foreach(string itm in lstCpy) {
				//tasks.Add(new Task(() => {
					string dest = dirBak + @"\" + itm.FromIndex(3);
					bool isDir = Directory.Exists(itm);
					try {
						if (isDir)
							cF.Dir_MakeIfNotExist(dest);
						else {
							cF.Dir_MakeIfNotExist(cF.Filename_GetPath(dest));
							cF.File_CopyIfNewer(itm, dest);
							dstSze += (new FileInfo(itm)).Length;
							/*FormAwait(() => {
								lblStat.Text = "Copying: " + cF.Filename_GetPath(itm);
							});*/
						}

						FormAwait(() => {
							lblPrg.Text = "Progress: " + Math.Floor(dstSze  * 100 / (decimal)srcSze) + "%    (" + cSM.Size_Compress(dstSze, 1024, 3, cSM.SizeNames_Bytes, false) + "/" + s_srcSze + ")";
						});
					} catch (Exception ex) {
						FormAwait(() => {
							AddErr(isDir ? "Folder" : "File", itm, ex.ToString());
							lblPrg.Text = "Progress: " + (Math.Round(dstSze / (decimal)srcSze, 2) * 100) + "%    (" + cSM.Size_Compress(dstSze, 1024, 3, cSM.SizeNames_Bytes, false) + "/" + s_srcSze + ")";
						});
					}
				//}));
			}

			//await Task.WhenAll(tasks);

			FormAwait(() => {
				if (lvErr.Items.Count > 0) {
					lblStat.Text = "Status: exporting errors...";
					List<string> errors = new List<string>();
					List<string> unstopcp = new List<string>();
					foreach (ListViewItem itm in lvErr.Items) {
						if ((itm.SubItems[1]?.Text ?? "") != "") {
							errors.Add(itm.SubItems[0] + "\t" + itm.SubItems[1] + "\t" + itm.SubItems[2]);
							unstopcp.Add(itm.SubItems[1].Text + "*" + dirBak + @"\" + itm.SubItems[1].Text.FromIndex(3));
						}
					}

					cF.TextFile_Overwrite(dirBak + @"\SysBackup errors " + classDatetime.Date_ToString(DateTime.Now, 5, false) + ".log", errors.List_ToString());
					cF.TextFile_Overwrite(dirBak + @"\SysBackup retry " + classDatetime.Date_ToString(DateTime.Now, 5, false) + ".ucp", unstopcp.List_ToString());
				}
				
				lblStat.Text = "Status: complete.";
				Console.Beep();
				MessageBox.Show("Complete.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
				Controls_SetEnabled(true);
			});
		}
		
		void FormAwait(Work func) {
			bool working = true;
			Action act = new Action(() => {
				func();
				working = false;
			});
			this.BeginInvoke(act);
			while (working);
		}
	} // </form>
}
