namespace Test01 {
    public class ScoreCounter {
        private IEnumerable<Student> _score;

        // コンストラクタ
        public ScoreCounter(string filePath) {
            _score = ReadScore(filePath);
        }

        //メソッドの概要： 
        private static IEnumerable<Student> ReadScore(string filePath) {
            var score = new List<Student>();
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines) {
                string[] items = line.Split(',');
                Student scores = new Student() {
                    Name = items[0],
                    Subjet = items[1],
                    Score = int.Parse(items[2])
                };
                score.Add(scores);
            }
            return score;
        }

        //メソッドの概要： 
        public IDictionary<string, int> GetPerStudentScore() {
            var dict = new Dictionary<string, int>();
            foreach (var score in _score) {
                if (dict.ContainsKey(score.Subjet)) {
                    dict[score.Subjet] += score.Score;
                } else {
                    dict[score.Subjet] = score.Score;
                }
            }
            return dict;
        }
    }
}
