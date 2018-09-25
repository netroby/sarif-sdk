﻿using Microsoft.CodeAnalysis.Sarif;
using Microsoft.CodeAnalysis.Sarif.Readers;
using Microsoft.CodeAnalysis.Sarif.Writers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace SarifDeferredSample
{
    // NOTE: Not being submitted. This is a sample of usage of the Deferred Collections.

    internal class Program
    {
        private const float BytesPerMB = (float)(1024 * 1024);

        private static void Main(string[] args)
        {
            EchoNewWriter(args[0], args[1]);
            //ReadTest(true, args[0]);
        }

        private static void EchoLog(string filePath, string outputPath, int resultRepeatCount = 1)
        {
            SarifLog log = null;

            JsonSerializer serializer = new JsonSerializer();
            serializer.ContractResolver = SarifDeferredContractResolver.Instance;
            serializer.Formatting = Formatting.Indented;

            Measure(() =>
            {
                using (JsonTextReader reader = new JsonPositionedTextReader(filePath))
                {
                    log = serializer.Deserialize<SarifLog>(reader);
                }

                return $"Loaded {filePath} ({(new FileInfo(filePath).Length / BytesPerMB):n1} MB)";
            });

            Measure(() =>
            {
                using (JsonTextWriter writer = new JsonTextWriter(new StreamWriter(outputPath)))
                {
                    serializer.Serialize(writer, log);
                }

                return $"Wrote {filePath} copy.";
            });
        }

        private static void EchoNewWriter(string filePath, string outputPath, int resultRepeatCount = 1)
        {
            SarifLog log = null;

            JsonSerializer serializer = new JsonSerializer();
            serializer.ContractResolver = SarifContractResolver.Instance;
            serializer.Formatting = Formatting.Indented;

            Measure(() =>
            {
                using (JsonTextReader reader = new JsonPositionedTextReader(filePath))
                {
                    log = serializer.Deserialize<SarifLog>(reader);
                }

                return $"Loaded {filePath} ({(new FileInfo(filePath).Length / BytesPerMB):n1} MB)";
            });

            Measure(() =>
            {
                // Capture and clear Runs from the log
                IList<Run> outputRuns = log.Runs;
                log.Runs = null;

                // Make a copy without runs to output
                SarifLog outputLog = new SarifLog(log);

                using (SarifWriter writer = new SarifWriter(serializer, new FileSystemStreamProvider(), outputPath, log))
                {
                    foreach (Run run in outputRuns)
                    {
                        // Get the large collections
                        IDictionary<string, FileData> files = run.Files;
                        IList<Result> results = run.Results;

                        // Clear the copy on the run itself
                        run.Files = null;
                        run.Results = null;

                        // Write the base run
                        writer.Write(run);

                        // Write the files
                        if (files != null)
                        {
                            foreach (KeyValuePair<string, FileData> file in files)
                            {
                                writer.Write(file.Key, file.Value);
                            }
                        }

                        // Write the results
                        if (results != null)
                        {
                            foreach (Result result in results)
                            {
                                writer.Write(result);
                            }
                        }
                    }
                }

                return $"Wrote {filePath} copy.";
            });
        }

        private static void Measure(Func<string> action)
        {
            long ramBefore = GC.GetTotalMemory(true);
            Stopwatch w = Stopwatch.StartNew();

            string message = action();

            w.Stop();
            long ramAfter = GC.GetTotalMemory(true);

            Console.WriteLine($"{message} in {w.ElapsedMilliseconds:n0}ms and {(ramAfter - ramBefore) / (BytesPerMB):n1}MB RAM.");
        }

        private static string SeekAndRead(string filePath, long position)
        {
            char[] buffer = new char[500];
            using (StreamReader reader = new StreamReader(filePath))
            {
                reader.BaseStream.Seek(position, SeekOrigin.Begin);
                int length = reader.Read(buffer, 0, buffer.Length);
                return new string(buffer, 0, length);
            }
        }

        private static void ReadTest(bool deferred, string filePath)
        {
            SarifLog log = null;

            Console.WriteLine($"Loading {filePath}{(deferred ? " (deferred)" : "")}...");

            Measure(() =>
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.ContractResolver = (deferred ? SarifDeferredContractResolver.Instance : SarifContractResolver.Instance);

                using (JsonTextReader reader = (deferred ? new JsonPositionedTextReader(filePath) : new JsonTextReader(new StreamReader(filePath))))
                {
                    log = serializer.Deserialize<SarifLog>(reader);
                }

                return $"Loaded {filePath} ({(new FileInfo(filePath).Length / BytesPerMB):n1} MB)";
            });

            Run run = log.Runs[0];
            Measure(() =>
            {
                int messageLengthTotal = 0;

                // Fastest: Enumerate
                foreach (Result result in run.Results)
                {
                    messageLengthTotal += result?.Message?.Text?.Length ?? 0;
                }

                // Slower: Count and indexer
                //int messageCount = run.Results.Count;
                //for (int i = 0; i < messageCount; ++i)
                //{
                //    Result result = run.Results[i];
                //    messageLengthTotal += result?.Message?.Text?.Length ?? 0;
                //}

                return $"Enumerated {run.Results.Count:n0} Results message total {messageLengthTotal / BytesPerMB:n0}MB";
            });

            Measure(() =>
            {
                int fileCount = 0;
                int uriLengthTotal = 0;

                if (run.Files != null)
                {
                    // Fastest: Enumerate
                    foreach (var item in run.Files)
                    {
                        FileData file = item.Value;
                        uriLengthTotal += file?.FileLocation?.Uri?.OriginalString?.Length ?? 0;
                        fileCount++;
                    }

                    // Slower: Keys and indexer
                    //foreach (var key in run.Files.Keys)
                    //{
                    //    FileData file = run.Files[key];
                    //    uriLengthTotal += file?.FileLocation?.Uri?.OriginalString?.Length ?? 0;
                    //    fileCount++;
                    //}
                }

                return $"Enumerated {fileCount:n0} Files, URI total {uriLengthTotal / BytesPerMB:n0}MB.";
            });

            GC.KeepAlive(log);
        }

    }
}
