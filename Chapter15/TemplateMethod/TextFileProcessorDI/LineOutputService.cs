using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFileProcessorDI {
    public class LineOutputService : ITextFilrService {
        private int _count;
        public void Execute(string line) {
            if (_count < 20) {
                Console.WriteLine(line);
            }
            _count++;
        }

        public void Initialize(string fname) {
            _count = 0;

        }

        public void Terminate() {

        }
    }
}
