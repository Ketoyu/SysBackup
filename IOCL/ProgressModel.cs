using QuodLib.Strings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOCL {
    public struct ProgressModel {
        public required long CurrentSize { get; init; }
        public required long CurrentCount { get; init; }
        public required long SourceSize { get; init; }
        public required long SourceCount { get; init; }

        public bool Success { get; init; }

        private decimal? _sizePercent;
        public decimal SizePercent
            => _sizePercent ??= Success
                    ? Math.Floor(CurrentSize * 100 / (decimal)SourceSize)
                    : (Math.Round(CurrentSize / (decimal)SourceSize, 2) * 100);

        private string? currentBytes;
        public string CurrentBytes
            => currentBytes ??= CompressSize(CurrentSize);

        private string? totalBytes;
        public string SourceBytes
            => totalBytes ??= CompressSize(SourceSize);

        private static string CompressSize(long size)
            => Misc.Size_Compress(size, 1024, 3, Misc.SizeNames_Bytes, false);
    }
}
