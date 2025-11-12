using System;
using System.IO;

namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            // ファイルパスの指定（引数から取得）
            if (args.Length == 0) {
                Console.WriteLine("使用方法: Exercise01 <ファイルパス>");
                return;
            }

            string filePath = args[0];

            // ファイルが存在するか確認
            if (!File.Exists(filePath)) {
                Console.WriteLine("指定されたファイルが存在しません: " + filePath);
                return;
            }

            int count = 0;

            try {
                using (StreamReader reader = new StreamReader(filePath)) {
                    string line;
                    while ((line = reader.ReadLine()) != null) {
                        // " class " を含む行をカウント（前後に空白がある前提）
                        if (line.Contains(" class ")) {
                            count++;
                        }
                    }
                }

                Console.WriteLine($"\"class\" キーワードを含む行数: {count}");
            }
            catch (Exception ex) {
                Console.WriteLine("エラーが発生しました: " + ex.Message);
            }
        }
    }
}