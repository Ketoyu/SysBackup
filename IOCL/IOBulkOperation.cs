using QuodLib.IO.Models;

namespace IOCL {
    internal sealed class IOBulkOperation {
        public required IList<IOOperation> Operations { get; init; }
        public required long Size { get; init; }
        public required long Count { get; init; }
    }
}
