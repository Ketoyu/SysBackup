namespace IOCL.Models {
    public class Result<TData>(TData? data, ResultStatus status) {
        public TData? Data { get; init; } = data;

        public ResultStatus Status { get; init; } = status;
    }
}
