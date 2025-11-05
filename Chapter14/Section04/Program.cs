using System.Threading.Tasks;

namespace Section04 {
    internal class Program {
        static async Task Main(string[] args) {
            HttpClient hc = new HttpClient(); 
            await GetHtmlExample(hc);
        }

        static async Task GetHtmlExample(HttpClient httpClient) {
            var url = "https://ja.wikipedia.org/wiki/%E3%82%A6%E3%82%A3%E3%82%AD";
            var text = await httpClient.GetStringAsync(url);
            Console.WriteLine(text);
        }
    }
}
