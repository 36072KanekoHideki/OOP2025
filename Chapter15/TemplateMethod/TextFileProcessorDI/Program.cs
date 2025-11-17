namespace TextFileProcessorDI {
    internal class Program {
        static void Main(string[] args) {
            var service = new LineCounterService();
            var Processor = new TextFileProcessor(service);
            Console.WriteLine("パスの入力：");
            Processor.Run(Console.ReadLine());  
        }
    }
}
