using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFileProcessorDI {
    public interface ITextFilrService {
        void Initialize(string fname);
        void Execute(string line);
        void Terminate();
    }
}
