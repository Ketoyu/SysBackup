using QuodLib.IO.Models;

namespace IOCL {
    /// <summary>
    /// Pending instruction to create a target directory based on a source directory.
    /// </summary>
    internal sealed class DirectoryMapOperation : DirectoryCreateOperation {
        /// <summary>
        /// The source directory which was used to generate the target path.
        /// </summary>
        public required string SourcePath { get; init; }
    }
}
