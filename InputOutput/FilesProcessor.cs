using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace InputOutput
{
    public class FilesProcessor
    {
        public string Directory { get; }

        public FilesProcessor(string directory) => Directory = directory;

        public async Task PrintContentAsync(string fileName, Stream stream)
        {
            using (StreamWriter writer = new StreamWriter(stream, Encoding.Unicode))
            {
                string fullPath = Path.Combine(Directory, fileName);
                try
                {
                    await writer.WriteLineAsync(File.ReadAllText(fullPath));
                }
                catch (ArgumentException) { await writer.WriteLineAsync("Wrong name"); }
                catch (DirectoryNotFoundException) { await writer.WriteLineAsync($"Directory \'{Directory}\' not found"); }
                catch (FileNotFoundException) { await writer.WriteLineAsync($"File \'{fullPath}\' not found"); }
                catch (IOException) { await writer.WriteLineAsync($"Unable to read file \'{fullPath}\'"); }
            }
        }

        public IEnumerable<FileInfo> GetFiles()
        {
            string[] fileNames = System.IO.Directory.GetFiles(Directory);
            foreach (string fileName in fileNames)
                yield return new FileInfo
                {
                    FileName = Path.GetFileName(fileName),
                    Size = new System.IO.FileInfo(Path.Combine(Directory, fileName)).Length / 1000
                };
        }

        public async Task<bool> PrintFilesAsync(Stream stream)
        {
            using (StreamWriter writer = new StreamWriter(stream, Encoding.Unicode))
            {
                try
                {
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine(string.Format(new string('─', 96) + '┬' + new string('─', 11) + '┐'));
                    builder.AppendLine(string.Format("│ {0, -94} │ {1, 9} │", "File name", "Size"));
                    builder.AppendLine(string.Format('├' + new string('─', 96) + '┼' + new string('─', 11) + '┤'));
                    foreach (FileInfo info in GetFiles())
                        builder.AppendLine(string.Format("│ {0, -94} │ {1, 6} KB │", info.FileName, info.Size));
                    builder.AppendLine(string.Format('└' + new string('─', 96) + '┴' + new string('─', 11) + '┘'));
                    await writer.WriteLineAsync(builder.ToString());
                }
                catch (ArgumentException)
                {
                    await writer.WriteLineAsync("Wrong path");
                    return false;
                }
                catch (DirectoryNotFoundException)
                {
                    await writer.WriteLineAsync($"Directory \'{Directory}\' not found");
                    return false;
                }
                catch (UnauthorizedAccessException)
                {
                    await writer.WriteLineAsync($"Access error on \'{Directory}\'");
                    return false;
                }
                catch (IOException)
                {
                    await writer.WriteLineAsync("Unable to get files from \'{0}\'");
                    return false;
                }
                return true;
            }
        }
    }
}
