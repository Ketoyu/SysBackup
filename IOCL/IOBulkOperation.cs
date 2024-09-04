using QuodLib.IO;
using QuodLib.IO.Models;
using System.Diagnostics;

namespace IOCL {
    internal sealed class IOBulkOperation {
        public required IList<IOOperation> Operations { get; init; }
        public required long Size { get; init; }
        public required long Count { get; init; }

        /// <summary>
        /// Run the full list of <see cref="Operations"/>.
        /// </summary>
        /// <param name="progress"></param>
        /// <param name="error"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        /// <exception cref="UnreachableException"></exception>
        public async Task RunAsync(IProgress<IOProgressModel?> progress, IProgress<IOErrorModel> error, CancellationToken cancel) {
            long sizeDestination = 0;
            long countDestination = 0;
            IProgress<long> pDest = new Progress<long>().OnChange((_, add) => {
                sizeDestination += add;
                countDestination++;
            });

            IProgress<bool> pProg = new Progress<bool>().OnChange((_, success) => {
                progress.Report(new IOProgressModel {
                    SourceSize = Size,
                    SourceCount = Count,
                    CurrentSize = sizeDestination,
                    CurrentCount = countDestination,
                    Success = success
                });
            });

            //Copy folders & files
            await Parallel.ForEachAsync(Operations.ToArray(), cancel, (itm, pcancel) => {
                try {
                    itm.Run();
                    if (itm is FileOperation opFile) {
                        pDest.Report(new FileInfo(opFile.SourcePath).Length);
                        //status.Report(new($"Copying: {cF.Filename_GetPath(itm)}", true));
                    }

                    pProg.Report(true);
                } catch (Exception ex) {
                    if (itm is DirectoryMapOperation opDir)
                        error.Report(new(PathType.Folder, opDir.SourcePath, ex));
                    else if (itm is FileOperation opFile)
                        error.Report(new(PathType.File, opFile.SourcePath, ex));
                    else
                        throw new UnreachableException();

                    pProg.Report(false);
                }

                return ValueTask.CompletedTask;
            });
        }
    }
}
