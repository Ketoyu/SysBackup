using QuodLib.IO.Models;

namespace IOCL.Progress {
    public class CopyProgress {
        public IProgress<IOProgressModel> Details { get; init; }
    }
}
