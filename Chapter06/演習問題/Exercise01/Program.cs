using System.Globalization;
using System.Text.Json;

namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {

            Console.WriteLine("入力");
            var str1 = Console.ReadLine();
            var str2 = Console.ReadLine();

            var cultureinfo = new CultureInfo("ja-JP");
            if (String.Compare(str1, str2, cultureinfo, CompareOptions.IgnoreCase) == 0)
                Console.WriteLine("等しい");
            else
                Console.WriteLine("等しくありません");
        }
    }
}
