using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tsk.Infrastructure.FileIO
{
    public interface IFileReaderWriter
    {
        IEnumerable<string> ReadAllLines(string path);
        void WriteAllLines(string path, IEnumerable<string> lines);
    }
}