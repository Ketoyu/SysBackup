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

using WinFiles = QuodLib.WinForms.IO.Files;
using Symbolic = QuodLib.IO.Symbolic.Info;
using QuodLib.IO.Symbolic;

using QuodLib.Strings;
using QuodLib.Linq;
using IOCL;
using QuodLib.IO;
using QuodLib.WinForms.Linq;
using QuodLib.IO.Models;
using QuodLib.WinForms.IO;
using IOCL.Models;
using IOCL.Progress;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SysBackup
{
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

        static readonly string DIR_DOC = Directories.Special.Documents + @"\CodeGig\SysBackup";
        readonly string DIR_IMP_EXP = DIR_DOC + @"\Presets";

        private CancellationToken? Cancel { get; set; }


        private void Form1_Load(object sender, EventArgs e) {
            Directory.CreateDirectory(DIR_IMP_EXP);
        }

        private void btnDir_Inc_Click(object sender, EventArgs e) {
            using FolderBrowserDialog fld = new();
            if (fld.TryBrowse(out string? path))
                lvDir.Items.Add(MakeLine($"+ {path!}", "Folder"));
        }
        private void btnDir_Dsc_Click(object sender, EventArgs e) {
            using FolderBrowserDialog fld = new();
            if (fld.TryBrowse(out string? path))
                lvDir.Items.Add(MakeLine($"- {path!}", "Folder"));
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
            using SaveFileDialog dialog = new() {
                Title = "Select a backup preset",
                InitialDirectory = DIR_IMP_EXP,
                Filter = WinFiles.BuildExtensionFilter("Preset", ".bkps")
            };
            if (!dialog.TryBrowse(out string? file))
                return;

            List<string> data = lvDir.Items
                .ToEnumerable()
                .Select(itm => itm.SubItems[0].Text)
                .ToKnownList(lvDir.Items.Count);

            File.WriteAllText(file!, data.List_ToString());
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
            using OpenFileDialog dialog = new() {
                Title = "Select a backup preset",
                InitialDirectory = DIR_IMP_EXP,
                Filter = WinFiles.BuildExtensionFilter("Preset", ".bkps")
            };
            if (!dialog.TryBrowse(out string? file))
                return;

            foreach (string ln in File.ReadAllText(file!).GetLines())
                lvDir.Items.Add(MakeLine(ln, "Folder"));
        }

        private void btnDirBak_Click(object sender, EventArgs e) {
            using FolderBrowserDialog fld = new();
            if (fld.TryBrowse(out string? path))
                txtDirBak.Text = path!;
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
                    .ToEnumerable()
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
                .ToEnumerable()
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

        private class CopyInput {
            public required string Destination { get; init; }
            public required string IgnorePath { get; init; }
            public List<string> Copy { get; }  = [];
            public List<string> Ignore { get; } = [];
            public List<SymbolicLink> SymbolicLinks { get; } = [];
        }


        private CopyInput GetInputs() {
            lblStat.Text = "Status: processing...";
            Controls_SetEnabled(false);

            lvErr.Items.Clear();

            Cancel ??= new();

            CopyInput inputs = new() {
                Destination = txtDirBak.Text,
                IgnorePath = txtPathIgnore.Text
            };

            foreach (string item in lvDir.Items
                .ToEnumerable()
                .Select(i => i.SubItems[0].Text)
            ) {
                string directory = item.FromIndex(2); //trim prefixes { "+ ", "- " }

                try {
                    if (Symbolic.TryGet(directory, out SymbolicLink? link) != SymbolicLinkType.None) {
                        inputs.SymbolicLinks.Add(link!);
                        continue;
                    }
                } catch (Exception ex) {
                    AddErr("Folder", directory, ex.ToString());
                    continue;
                }

                if (directory.StartsWith("- ")) {
                    inputs.Ignore.Add(directory);
                    continue;
                }

                inputs.Copy.Add(directory);
            }

            return inputs;
        }

        private SharedProgress BuildSharedProgress()
            => new() {
                Status = new Progress<StatModel>().OnChange((_, d) => {
                    lblStat.Text = d.Status;
                    Controls_SetEnabled(!d.Working);
                }),
                Error = new Progress<IOErrorModel>().OnChange((_, d)
                    => AddErr(d.PathType.ToString(), d.Path, d.Error.ToString()))
            };

        private AnalyzeProgress BuildAnalyzeProgress()
            => new() {
                Details = new Progress<IOProgressModel>().OnChange((_, d) => {
                    lblPrg.Text = $"Progress: _% (_/{d.SourceBytes}) | (_ of {d.SourceCount.ToCommaString()} files)";
                })
            };

        private CopyProgress BuildCopyProgress()
            => new() {
                Details = new Progress<IOProgressModel>().OnChange((_, d) => {
                    lblPrg.Text = $"Progress: {d.SizePercent}% ({d.CurrentBytes}/{d.SourceBytes}) | ({d.CurrentCount.ToCommaString()} of {d.SourceCount.ToCommaString()} files)";
                })
            };

        private Task<Result<IOBulkOperation?>> DoAnalyzeAsync(CopyInput inputs, SharedProgress sharedProgress) {
            AnalyzeProgress analyzeProgress = BuildAnalyzeProgress();
            analyzeProgress.SymbolicLink = new Progress<SymbolicLink>().OnChange((_, d)
                => inputs.SymbolicLinks.Add(d));

            lblPrg.Text = "Progress: ";

            return IO.AnalyzeAsync(inputs.Destination, inputs.Destination, inputs.Copy, inputs.Ignore, 
                sharedProgress, analyzeProgress, new CancellationToken());
        }

        private async Task DoCopyAsync(CopyInput inputs, IOBulkOperation operations, SharedProgress sharedProgress) {
            CopyProgress copyProgress = BuildCopyProgress();

            await IO.CopyAsync(operations, sharedProgress, copyProgress, new CancellationToken());

            Complete(inputs.Destination, inputs.SymbolicLinks);
        }

        private async Task DoCopyImmediateAsync(CopyInput inputs, SharedProgress sharedProgress) {
            AnalyzeProgress analyzeProgress = BuildAnalyzeProgress();
            analyzeProgress.SymbolicLink = new Progress<SymbolicLink>().OnChange((_, d)
                => inputs.SymbolicLinks.Add(d));

            CopyProgress copyProgress = BuildCopyProgress();

            lblPrg.Text = "Progress: ";

            await IO.CopyImmediateAsync(inputs.Destination,
                inputs.IgnorePath,
                inputs.Copy,
                inputs.Ignore,
                sharedProgress,
                analyzeProgress,
                copyProgress,
                (CancellationToken)Cancel!
            );

            Complete(inputs.Destination, inputs.SymbolicLinks);
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
                    File.WriteAllText(
                        filename("errors", ".log"),
                        errors.List_ToString()
                    );

                if (unstopcp.Any())
                    File.WriteAllText(
                        filename("retry", ".ucp"),
                        unstopcp.List_ToString()
                    );
            }

            if (symbolicLinks.Any()) {
                lblStat.Text = "Status: exporting list of symbolic links...";
                File.WriteAllText(
                    filename("symbolic links", ".log"),
                    string.Join("\r\n", symbolicLinks!.Select(l => $"{l.Source}={l.Target}"))
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
