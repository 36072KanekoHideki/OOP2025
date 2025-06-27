using System.Globalization;
using System.Text.RegularExpressions;

namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            var dateTime = DateTime.Now;
            DisplayDatePattern1(dateTime);
            DisplayDatePattern2(dateTime);
            DisplayDatePattern3(dateTime);
        }

        private static void DisplayDatePattern1(DateTime dateTime) {
            // 2024//03/09 19:03
            // string.Formatを使った例
            var dt1 = string.Format($"{dateTime:yyyy/MM/dd HH:mm}");
            Console.WriteLine(dt1);


        }

        private static void DisplayDatePattern2(DateTime dateTime) {
            // 2024年03月09日 19時03分09秒
            //DateTime.ToStringを使った例
            var dt2 = dateTime.ToString("yyyy年MM月dd日 HH時mm分ss秒");
            Console.WriteLine(dt2);
        }

        private static void DisplayDatePattern3(DateTime dateTime) {
            var culture = new CultureInfo("ja-JP");
            culture.DateTimeFormat.Calendar = new JapaneseCalendar();

            var dt3 = dateTime.ToString("gg y年 M月 d日(dddd)", culture);
            Console.WriteLine(dt3);
        }
    }
}
