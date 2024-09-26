using QuodLib.IO.Symbolic;
using QuodLib.IO.Models;
using QuodLib.Strings;
using QuodLib.IO;
using System.Diagnostics;
using System.Threading.Channels;
using IOCL.Models;
using IOCL.Progress;

namespace IOCL
{
    public static class IO {
        /// <summary>
        /// Analyze and then immediately perform the backup.
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="commonPath"></param>
        /// <param name="sources"></param>
        /// <param name="skipSources"></param>
        /// <param name="status"></param>
        /// <param name="progress"></param>
        /// <param name="symbolicLink"></param>
        /// <param name="error"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static async Task<ResultStatus> CopyImmediateAsync(string destination, string commonPath, IList<string> sources, IList<string> skipSources, 
            SharedProgress sharedProgress, AnalyzeProgress analyzeProgress, CopyProgress copyProgress, 
            CancellationToken cancel
        ) {
            if (!Directory.Exists(destination))
                throw new ArgumentException($"Directory not found: {destination}", nameof(destination));

            if (!sources.Any())
                throw new ArgumentException($"Empty list", nameof(sources));

            Result<IOBulkOperation?> analysis = await AnalyzeAsync(destination, commonPath, 
                sources, skipSources, 
                sharedProgress, analyzeProgress, 
                cancel
            );

            if (analysis.Status == ResultStatus.Cancelled)
                return ResultStatus.Cancelled;

            ResultStatus result = await CopyAsync(analysis.Data!, sharedProgress, copyProgress, cancel);
            return result;
        }

        /// <summary>
        /// Analyze the data to be backed up.
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="commonPath"></param>
        /// <param name="sources"></param>
        /// <param name="skipSources"></param>
        /// <param name="sharedProgress"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static async Task<Result<IOBulkOperation?>> AnalyzeAsync(string destination, string commonPath, 
            IList<string> sources, IList<string> skipSources,
            SharedProgress sharedProgress, AnalyzeProgress analyzeProgress,
            CancellationToken cancel
        ) {
            if (!Directory.Exists(destination))
                throw new ArgumentException($"Directory not found: {destination}", nameof(destination));

            if (!sources.Any())
                throw new ArgumentException($"Empty list", nameof(sources));

            sharedProgress.Status.Report(new() {
                Status = "analyzing...",
                Working = true
            });

            if (cancel.IsCancellationRequested) {
                sharedProgress.Status.Report(new() {
                    Status = "Canceled",
                    Working = false
                });
                return new(null, ResultStatus.Cancelled);
            }

            IOBulkOperation? analysis = await AnalyzeAsync(sources, skipSources,
                itm => Paths.Resolve(destination, itm, commonPath),
                analyzeProgress.SymbolicLink, sharedProgress.Error, cancel
            );

            if (CheckCancelled(sharedProgress.Status, cancel))
                return new(null, ResultStatus.Cancelled);

            return new(analysis, ResultStatus.Complete);
        }

        /// <summary>
        /// Perform a backup, using the provided <paramref name="analysis"/>.
        /// </summary>
        /// <param name="analysis"></param>
        /// <param name="sharedProgress"></param>
        /// <param name="copyProgress"></param>
        /// <param name="error"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        public static async Task<ResultStatus> CopyAsync(IOBulkOperation analysis, SharedProgress sharedProgress, CopyProgress copyProgress,
            CancellationToken cancel
        ) {
            sharedProgress.Status.Report(new() {
                Status = "Copying...",
                Working = true
            });

            await analysis!.RunParallelAsync(copyProgress.Details, sharedProgress.Error, cancel);

            bool cancelled = CheckCancelled(sharedProgress.Status, cancel);
            return cancelled
                ? ResultStatus.Cancelled
                : ResultStatus.Complete;
        }

        /// <summary>
        /// Report (public) whether the operation was cancelled, then return true/false of that (private).
        /// </summary>
        /// <param name="status">The public-facing status</param>
        /// <param name="cancel"></param>
        /// <returns>The private status</returns>
        private static bool CheckCancelled(IProgress<StatModel> status, CancellationToken cancel) {
            if (cancel.IsCancellationRequested) {
                status.Report(new() {
                    Status = "Canceled",
                    Working = false
                });
                return true;
            }

            return false;
        }

        /// <summary>
        /// Scans directories and sub-directories, reporting filepaths and directories that need copied, along with a total size (in bytes).
        /// </summary>
        /// <param name="sources">List of directories to scan</param>
        /// <param name="skipSources">List of (sub-)directories to ignore</param>
        /// <param name="symbolicLink">Reports a <see cref="SymbolicLink"/></param>
        /// <param name="error">Reports an <see cref="IOErrorModel"/></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        private async static Task<IOBulkOperation?> AnalyzeAsync(IList<string> sources, IList<string> skipSources, Func<string, string> resolvePath, IProgress<SymbolicLink>? symbolicLink, IProgress<IOErrorModel> error, CancellationToken cancel) {
            List<IOOperation> copy = [];

            long size = 0;
            long count = 0;

            IProgress<FileInfo> file = new Progress<FileInfo>().OnChange((_, fI) => {
                size += fI.Length;
                if (fI.Attributes.HasFlag(FileAttributes.Directory))
                    throw new UnreachableException();

                count++;
                copy.Add(new FileOperation() {
                    SourcePath = fI.FullName,
                    TargetPath = resolvePath(fI.FullName),
                    Operation = FileOperationType.Copy,
                    Overwrite = OverwriteOption.IfNewer
                });
            });

            IProgress<DirectoryInfo> leafDirectory = new Progress<DirectoryInfo>().OnChange((_, dI)
                => copy.Add(new DirectoryMapOperation() {
                    SourcePath = dI.FullName,
                    TargetPath = resolvePath(dI.FullName)
                })
            );

            await IOTasks.TraverseFilesAsync(sources, file, error, cancel,
                new IOTasks.TraverseFilesAsyncOptions() {
                    SymbolicLink = symbolicLink,
                    LeafDirectory = leafDirectory,
                }.SkipSubdirectories(skipSources)
            );

            if (cancel.IsCancellationRequested)
                return null;

            return new IOBulkOperation { 
                Operations = copy,
                Size = size,
                Count = count
            };
        }
    }
}
