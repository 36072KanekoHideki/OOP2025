using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFileProcessorDI {
    public class TextFileProcessor {
        private ITextFilrService _service;

        //コンストラクター
        public TextFileProcessor(ITextFilrService service) {
            _service = service;
        }

        public void Run(string fileName) {
            _service.Initialize(fileName);

            var lines = File.ReadLines(fileName);
            foreach (var line in lines) {
                _service.Execute(line);
            }
            _service.Terminate();
        }
    }
}
