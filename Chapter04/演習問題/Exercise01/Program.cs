
using System.ComponentModel;
using System.Linq;

namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            List<string> langs = [
                "C#", "Java", "Ruby", "PHP", "Python", "TypeScript",
                "JavaScript", "Swift", "Go",
];

            Exercise1(langs);
            Console.WriteLine("---");
            Exercise2(langs);
            Console.WriteLine("---");
            Exercise3(langs);
        }

        private static void Exercise1(List<string> langs) {

            //foreach
            var selected = langs.Where(s => s.Contains('S'));
            foreach (var lang in selected) {
                Console.WriteLine(lang);
            }

            //for
            for(int i = 0; i < langs.Count; i++) {
                if (langs[1].Contains('S'))
                    Console.WriteLine(langs[1]);
            }

            //while
            int index = 0;
            while (index < langs.Count) {
                if (langs[1].Contains('S'))
                    Console.WriteLine(langs[1]);
                index++;
            }
        }
     
        private static void Exercise2(List<string> langs) {
            var selected = langs.Where(s => s.Contains('S'));
            foreach (var lang in selected) {
                Console.WriteLine(lang);
            }
        }

        private static void Exercise3(List<string> langs) {
            Console.WriteLine(langs.Find(s => s.Length == 10));
        }
    }
}
