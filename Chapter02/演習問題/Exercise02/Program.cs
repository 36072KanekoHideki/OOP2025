using DistanceConverter;

namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {

            PrintInchToMeterList(1, 10);

            //インチからメートルへの対応表を出力
            static void PrintInchToMeterList(int start, int end) {
                for (int Inch = start; Inch <= end; Inch++) {
                    double meter = InchConverter.ToMeter(Inch);
                    Console.WriteLine($"{Inch}inch = {meter:0.0000}m");

                }
            }
        }
    }
}
