using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;
using System.IO;

using QuodLib;

using cG = QuodLib.WinForms.Drawing.classGraphics;

using WinFiles = QuodLib.WinForms.Files;
using Files = QuodLib.IO.Files;
using Symbolic = QuodLib.IO.Symbolic.Info;
using QuodLib.IO.Symbolic;

using QuodLib.Strings;
using QuodLib.Linq;
using IOCL;
using QuodLib.IO;
using QuodLib.WinForms.Linq.Extensions;

namespace SysBackup {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
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

        static readonly string DIR_DOC = Files.GetDir.Docs + @"\CodeGig\SysBackup";
        readonly string DIR_IMP_EXP = DIR_DOC + @"\Presets";

        private CancellationToken? Cancel { get; set; }


        private void Form1_Load(object sender, EventArgs e) {
            Directory.CreateDirectory(DIR_IMP_EXP);
        }

        private void btnDir_Inc_Click(object sender, EventArgs e) {
            FolderBrowserDialog fld = new();
            if (fld.ShowDialog() == DialogResult.OK)
                lvDir.Items.Add(MakeLine("+ " + fld.SelectedPath, "Folder"));
        }
        private void btnDir_Dsc_Click(object sender, EventArgs e) {
            FolderBrowserDialog fld = new();
            if (fld.ShowDialog() == DialogResult.OK)
                lvDir.Items.Add(MakeLine("- " + fld.SelectedPath, "Folder"));
        }
        private void btnDir_Forget_Click(object sender, EventArgs e) {
            if (lvDir.SelectedIndices.Count > 0)
                lvDir.Items.RemoveAt(lvDir.SelectedIndices[0]);
        }

        private void AddErr(string ttl, string dir, string info) {
            ListViewItem ln = new([ttl, dir, info]);
            lvErr.Items.Add(ln);
        }
        private ListViewItem MakeLine(string ttl, string typ) {
            return new ListViewItem([ttl, typ]);
        }

        private void mnu_tlExp_Click(object sender, EventArgs e) {
            List<string> data = [];
            string fl = WinFiles.SaveFile("Preset (.bkps)", "bkps", "Select a backup preset", DIR_IMP_EXP);
            if (fl == "") return;

            foreach (ListViewItem itm in lvDir.Items)
                data.Add(itm.SubItems[0].Text);

            Files.TextFile_Overwrite(fl, data.List_ToString());
        }

        private void mnuImp_tLRplc_Click(object sender, EventArgs e) {
            lvDir.Items.Clear();
            Import();
        }

        private void mnuImp_tlAppnd_Click(object sender, EventArgs e) {
            Import();
        }
        private void mnu_tlClr_Click(object sender, EventArgs e) {
            lvDir.Items.Clear();
        }
        private void Import() {
            string fl = WinFiles.OpenFile("Preset (.bkps)", "bkps", "Select a backup preset", DIR_IMP_EXP);
            if (fl == "") return;

            foreach (string ln in Files.TextFile_GetAllText(fl).GetLines())
                lvDir.Items.Add(MakeLine(ln, "Folder"));
        }

        private void btnDirBak_Click(object sender, EventArgs e) {
            FolderBrowserDialog fld = new FolderBrowserDialog();
            if (fld.ShowDialog() == DialogResult.OK)
                txtDirBak.Text = fld.SelectedPath;
        }

        private void lvErr_MouseDoubleClick(object sender, MouseEventArgs e) {
            if (lvErr.SelectedIndices[0] > 0) {
                ListViewItem itm = lvErr.SelectedItems[0];
                MessageBox.Show(itm.SubItems[1] + "\r\n\r\n" + itm.SubItems[2]);
            }
        }

        private void Controls_SetEnabled(bool value) {
            btnBak.Enabled = value;
            btnDirBak.Enabled = value;

            btnDir_Inc.Enabled = value;
            btnDir_Dsc.Enabled = value;
            btnDir_Forget.Enabled = value;

            backupsToolStripMenuItem.Enabled = value;

            txtPathIgnore.ReadOnly = !value;
            btnPathIgnore.Enabled = value;
        }
        private async void btnBak_Click(object sender, EventArgs e) {
            if (lvDir.Items.Count < 1)
                return;

            if (txtDirBak.Text.Length > 0 && (
                string.IsNullOrEmpty(txtPathIgnore.Text) ||
                !lvDir.Items
                    .AsEnumerable()
                    .Select(i => i.SubItems[0].Text)
                    .All(s => s.StartsWith(txtPathIgnore.Text))
            )) {
                await DoCopyAsync();
            }
        }

        private void btnPathIgnore_Click(object sender, EventArgs e) {
            if (lvDir.Items.Count == 0)
                return;

            List<string> items = lvDir.Items
                .AsEnumerable()
                .Select(i => i.SubItems[0].Text)
                .ToKnownList(lvDir.Items.Count);

            if (items.Count == 1) {
                txtPathIgnore.Text = items.First().FromIndex(2);
                return;
            }

            int? index = Query.IndexOfDivergence(items);
            if (index == null || index < 0)
                txtPathIgnore.Text = string.Empty;
            else
                txtPathIgnore.Text = items.First().TowardIndex((int)index!);
        }

        private async Task DoCopyAsync() {
            lblStat.Text = "Status: processing...";
            Controls_SetEnabled(false);

            lvErr.Items.Clear();

            List<string> copy = [];
            List<string> ignore = [];

            Cancel ??= new();

            List<SymbolicLink> symLinks = [];

            foreach (string itm in lvDir.Items
                .AsEnumerable()
                .Select(i => i.SubItems[0].Text)
            ) {
                string dir = itm;
                dir = dir.FromIndex(2);

                try {
                    if (Symbolic.TryGet(dir, out SymbolicLink? link) != SymbolicLinkType.None) {
                        symLinks.Add(link!);
                        continue;
                    }
                } catch (Exception ex) {
                    AddErr("Folder", dir, ex.ToString());
                    continue;
                }

                if (dir.StartsWith("- ")) {
                    ignore.Add(dir);
                    continue;
                }

                copy.Add(dir);
            }

            string destination = txtDirBak.Text;

            await IO.CopyAsync(destination,
                txtPathIgnore.Text,
                copy,
                ignore,
                new Progress<StatModel>().OnChange((_, d) => {
                    lblStat.Text = d.Status;
                    Controls_SetEnabled(!d.Working);
                }),
                new Progress<ProgressModel?>().OnChange((_, dn) => {
                    if (dn == null)
                        lblPrg.Text = $"Progress: ";
                    else {
                        ProgressModel d = (ProgressModel)dn!;
                        lblPrg.Text = $"Progress: {d!.SizePercent}% ({d.CurrentBytes}/{d.SourceBytes}) | ({d.CurrentCount.ToCommaString()} of {d.SourceCount.ToCommaString()} files)";
                    }
                }),
                new Progress<SymbolicLink>().OnChange((_, d)
                    => symLinks.Add(d)),
                new Progress<IOErrorModel>().OnChange((_, d)
                    => AddErr(d.PathType.ToString(), d.Path, d.Error.ToString())),
                (CancellationToken)Cancel!
            );

            Complete(destination, symLinks);
        }

        void Complete(string destination, List<SymbolicLink> symbolicLinks) {
            if (lvErr.Items.Count > 0) {
                lblStat.Text = "Status: exporting errors...";
                List<string> errors = [];
                List<string> unstopcp = [];
                foreach (ListViewItem itm in lvErr.Items) {
                    if (!string.IsNullOrEmpty(itm.SubItems[1]?.Text)) {
                        errors.Add(itm.SubItems[0] + "\t" + itm.SubItems[1] + "\t" + itm.SubItems[2]);
                        unstopcp.Add(itm.SubItems[1].Text + "*" + destination + @"\" + itm.SubItems[1].Text.FromIndex(3));
                    }
                }

                if (errors.Any())
                    Files.TextFile_Overwrite(
                        filename("errors", ".log"),
                        errors.List_ToString()
                    );

                if (unstopcp.Any())
                    Files.TextFile_Overwrite(
                        filename("retry", ".ucp"),
                        unstopcp.List_ToString()
                    );
            }

            if (symbolicLinks.Any()) {
                lblStat.Text = "Status: exporting list of symbolic links...";
                Files.TextFile_Overwrite(
                    filename("symbolic links", ".log"),
                    string.Join("\r\n", symbolicLinks!.Select(l => $"{l.SourceLocation}={l.Destination}"))
                );
            }

            lblStat.Text = "Status: complete.";
            Console.Beep();
            MessageBox.Show("Complete.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Controls_SetEnabled(true);

            //---- (local methods) ----

            string filename(string type, string extension)
                => $"{destination}\\SysBackup {type} {timestamp()}{extension}";

            static string timestamp()
                => classDatetime.Date_ToString(DateTime.Now, 5, false);
        }

    } // </form>
}
