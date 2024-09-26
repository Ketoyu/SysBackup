using IOCL.Models;
using QuodLib.IO.Models;
using QuodLib.IO.Symbolic;

namespace IOCL.Progress {
    public class SharedProgress {
        public IProgress<StatModel> Status { get; init; }
        public IProgress<IOErrorModel> Error { get; init; }
    }
}
