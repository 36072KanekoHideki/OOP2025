namespace Section01 {
    internal class Program {
        static void Main(string[] args) {
            List<object> list = [
                new GreetingMorning(),
                new GreetingAfternoon(),
                new GreetingEvening(),
                ];

            foreach(var obj in list) {
                string msg = obj.GetMessage();
                Console.WriteLine(msg);
            }
        }
    }

    class GreetingMorning {
        public string GetMessage() => "おはよう";
    }

    class GreetingAfternoon {
        public string GetMessage() => "こんにちは";
    }

    class GreetingEvening {
        public string GetMessage() => "こんばんは";
    }

}
