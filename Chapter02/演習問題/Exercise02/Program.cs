using DistanceConverter;

namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {

            Console.WriteLine("１：インチからメートル\n２：メートルからインチ");
            int start = int.Parse(Console.ReadLine());

            if (args.Length >= 1 && args[0] == "-tom") {
                PrintInchToMeterList(1, 10);
            } else {
                PrintMeterToInchList(1, 10);
            }

            //インチからメートルへの対応表を出力
            static void PrintInchToMeterList(int start, int end) {
                for (int Inch = start; Inch <= end; Inch++) {
                    double meter = InchConverter.ToMeter(Inch);
                    Console.WriteLine($"{Inch}inch = {meter:0.0000}m");

                }
            }
            //メートルからインチへの対応表を出力
            static void PrintMeterToInchList(int start, int end) {
                for (int meter = start; meter <= end; meter++) {
                    double Inch = InchConverter.FromMeter(meter);
                    Console.WriteLine($"{meter}m = {Inch:0.0000}inch");
                }
            }
        }
    }
}
