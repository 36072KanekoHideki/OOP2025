using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;

namespace Exercise01 {
    public class Program {
        static void Main(string[] args) {
            //歌データを入れるリストオブジェクトを生成
            var songs = new List<Song>();
            //***** 曲の登録 *****を出力
            Console.WriteLine("***** 曲の登録 *****");

            //何があるかわからないので無限ループ
            while (true) {
                //"曲名"を出力
                Console.Write("曲名:");
                //入力された曲名を取得
                string? title = Console.ReadLine();

                //endが入力されたら登録終了
                if (title.Equals("end",StringComparison.OrdinalIgnoreCase))
                    break;

                //"アーティスト名"を出力
                Console.Write("アーティスト名:");
                //入力されたアーティスト名を取得
                string? artistName = Console.ReadLine();

                //"演奏時間（秒）"を出力
                Console.Write("演奏時間（秒）:");
                //入力された演奏時間を取得
                int length = int.Parse(Console.ReadLine());

                //songインスタンスを生成
                //Song song = new Song(title, artistName, length);
                Song song = new Song() {
                    Title = title,
                    ArtistName = artistName,
                    Length = length
                };

                //歌データを入れるリストオブジェクトへ登録
                songs.Add(song);

                Console.WriteLine();   //改行
            }

            printSongs(songs);
        }

        //2.1.4
        private static void printSongs(IEnumerable<Song> songs) {
#if false
            foreach (var song in songs) {
                var minutes = song.Length / 60;
                var seconds = song.Length % 60;
                Console.WriteLine($"{song.Title}, {song.ArtistName} {minutes}:{seconds:00}");
            }
#else
            //TimeSpan構造体を使った場合
            foreach (var song in songs) {
                var timespan = TimeSpan.FromSeconds(song.Length);
                Console.WriteLine($"{song.Title}, {song.ArtistName} {timespan.Minutes}:{timespan.Seconds:00}");

            }
            //または、以下でも可
            //foreach (var song in songs) {
            //    Console.WriteLine(@"{0}, {1}, {2:m\:ss}",
            //        song.Title, song.ArtistName, TimeSpan.FromSeconds(song.Length));
            //}
#endif
            Console.WriteLine();
        }
    }
}
