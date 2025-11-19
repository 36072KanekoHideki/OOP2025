using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFileProcessorDI {
    //P362　問題15.1
    internal class LineToHalfNumberService : ITextFilrService {
        public void Initialize(string fname) {
        }

        public void Execute(string line) {

            string result = new string(line.Select(c => ('０' < c && c <= '９') ? (char)(c - '０' + '0') : c).ToArray());
            Console.WriteLine(result);
        }

        public void Terminate() {
        }
    }
}
