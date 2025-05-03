using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tsk.Infrastructure.FileIO
{
    public class FileReaderWriter : IFileReaderWriter
    {
        public IEnumerable<string> ReadAllLines(string path) =>
            File.ReadAllLines(path);


        public void WriteAllLines(string path, IEnumerable<string> lines) =>
            File.WriteAllLines(path, lines);
    }
}