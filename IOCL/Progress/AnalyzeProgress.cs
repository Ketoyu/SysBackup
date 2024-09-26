using QuodLib.IO.Models;
using QuodLib.IO.Symbolic;

namespace IOCL.Progress {
    public class AnalyzeProgress {
        public IProgress<IOProgressModel> Details { get; init; }
        public IProgress<SymbolicLink> SymbolicLink { get; set; }
    }
}
