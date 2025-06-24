using Microsoft.VisualBasic;
using System.Globalization;
using System.Security.Authentication;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Section01 {
    internal class Program {
        static void Main(string[] args) {

            //var today = new DateTime(2025,7,12); //日付
            //var now = DateTime.Now;   //日付と時刻

            //Console.WriteLine($"Today:{today.Month}");
            //Console.WriteLine($"Now:{now}");

            //①自分の生年月日は何曜日かをプログラムを書いて調べる
            Console.WriteLine("日付を入力");


            Console.WriteLine("西暦を入力してください:");
            var year = int.Parse(Console.ReadLine());

            Console.WriteLine("月を入力してください:");
            var month = int.Parse(Console.ReadLine());

            Console.WriteLine("日を入力してください:");
            var day = int.Parse(Console.ReadLine());

            var birthday = new DateTime(year, month, day);


            var cluture = new  CultureInfo("ja-JP");
            cluture.DateTimeFormat.Calendar = new JapaneseCalendar();

            var str = birthday.ToString("ggyy年M月d日", cluture);
            var shortDayOfWeek = cluture.DateTimeFormat.GetShortestDayName(birthday.DayOfWeek);

            Console.WriteLine(str + birthday.ToString("ddd曜日", cluture));


            //var birthday = new DateTime(2006, 1, 2);

            //Console.WriteLine($"Birth:{birthday.DayOfWeek}");

            //③生まれてから〇〇〇〇日目です

            var today = DateTime.Today;
            var daysSinceBirth = (today - birthday).Days;
            Console.WriteLine($"生まれてから {daysSinceBirth} 日目です");

            //④あなたは〇〇歳です

            var age = today.Year - birthday.Year;
            if (today < birthday.AddYears(age)) {
                age--; 
            }
            Console.WriteLine($"あなたは {age} 歳です");

            //⑤1月1日から何日目か

            var dayOfYear = birthday.DayOfYear;
            Console.WriteLine($"この日は1月1日から数えて {dayOfYear} 日目です");

            //②閏年の判定プログラムを作成する
            var isLeapYear = DateTime.IsLeapYear(birthday.Year);
            if (isLeapYear) {
                Console.WriteLine(year + "年は閏年です");
            } else {
                Console.WriteLine(year + "年は平年です");
            }
        }
    }
}
