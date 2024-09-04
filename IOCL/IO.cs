﻿using QuodLib.IO.Symbolic;
using QuodLib.IO.Models;
using Symbolic = QuodLib.IO.Symbolic.Info;
using QuodLib.Strings;
using QuodLib.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using System.Diagnostics;

namespace IOCL {
    public static class IO {
        public static async Task CopyAsync(string destination, string commonPath, IList<string> sources, IList<string> skipSources, IProgress<StatModel> status, IProgress<IOProgressModel?> progress, IProgress<SymbolicLink>? symbolicLink, IProgress<IOErrorModel> error, CancellationToken cancel) {
            if (!Directory.Exists(destination))
                throw new ArgumentException($"Directory not found: {destination}", nameof(destination));

            if (!sources.Any())
                throw new ArgumentException($"Empty list", nameof(sources));

            status.Report(new() {
                Status = "analyzing...",
                Working = true
            });

            progress.Report(null);

            if (cancel.IsCancellationRequested) {
                status.Report(new() {
                    Status = "Canceled",
                    Working = false
                });
                return;
            }

            IOBulkOperation? analysis = await AnalyzeAsync(sources, skipSources, itm => ResolvePath(destination, itm, commonPath), symbolicLink, error, cancel);

            if (cancelled())
                return;

            status.Report(new() {
                Status = "Copying...",
                Working = true
            });

            await analysis!.RunAsync(progress, error, cancel);

            _ = cancelled();

            //---- (local methods) ----

            bool cancelled() {
                if (cancel.IsCancellationRequested) {
                    status.Report(new() {
                        Status = "Canceled",
                        Working = false
                    });
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Resolves the <paramref name="source"/> filepath minus the <paramref name="ignoreCommonPath"/> into the <paramref name="destination"/> directory.
        /// </summary>
        /// <param name="destination">The target directory</param>
        /// <param name="source">The source filepath</param>
        /// <param name="ignoreCommonPath">The path to trim from the <paramref name="source"/></param>
        /// <returns>The updated destination</returns>
        public static string ResolvePath(string destination, string source, string? ignoreCommonPath) {
            string source_ = !string.IsNullOrEmpty(ignoreCommonPath) && source.StartsWith(ignoreCommonPath)
                ? source.GetAfter(ignoreCommonPath)
                : source.FromIndex(3);

            if (source_[0] == '\\' || source_[0] == '/')
                source_ = source_.Substring(1);

            string final = Path.Combine(destination, source_);
            return final;
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
