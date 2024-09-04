namespace IOCL {
    internal sealed class Analysis {
        public required IReadOnlyList<string> Paths { get; init; }
        public required long Size { get; init; }
        public required long Count { get; init; }
    }
}
