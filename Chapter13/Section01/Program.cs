namespace Section01 {
    internal class Program {
        static void Main(string[] args) {
            var groups = Library.Categories
                            .GroupJoin(Library.Books
                                    , c => c.Id
                                    , b => b.CategoryId,
                                    (c, books) => new {
                                        Category = c.Name,
                                        Books = books,
                                    });

            foreach (var book in books) {
                Console.WriteLine($"{book!.Title}年 {book!.category} ({book!.PublishedYear})");
            }
        }
    }
}
