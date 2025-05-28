using System;

namespace Section01 {
    internal class Program {
        static void Main(string[] args) {

            var books = Books.GetBooks();

            //①本の平均金額を表示
            Console.WriteLine(books.Average(x => x.Price));

            //②本のページ合計を表示
            Console.WriteLine(books.Sum(x => x.Pages));

            //③金額の安い書籍名と金額を表示
            var book = books.Where(x => x.Price == books.Min(b => b.Price));
            foreach (var item in book) {
                Console.WriteLine(item.Title + " : " + item.Price);
            }

            //④ページ数が多い書籍名とページ数を表示
            var boo = books.Where(x => x.Pages == books.Max(b => b.Pages));
            foreach (var item in boo) {
                Console.WriteLine(item.Title + " : " + item.Pages);
            }
            //⑤タイトルに「物語」が含まれている書籍名をすべて表示
            var titles = books.Where(x => x.Title.Contains("物語"));
                foreach(var item in titles) {
                Console.WriteLine(item.Title);
            }

            }
    }
}
