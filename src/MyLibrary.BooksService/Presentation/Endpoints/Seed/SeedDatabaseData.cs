using MyLibrary.BooksService.Modules.Books.Entities;

namespace MyLibrary.BooksService.Presentation.Endpoints.Seed
{
    public sealed class SeedDatabaseData
    {
        public static List<Book> GetBooks()
        {
            // Genre objects
            var scienceFiction = new Genre("Science Fiction", "Books that explore futuristic and imaginative concepts.");
            var mystery = new Genre("Mystery", "Books that involve solving a puzzle or crime.");
            var romance = new Genre("Romance", "Books centered around love and relationships.");
            var fantasy = new Genre("Fantasy", "Books set in magical worlds and realms.");

            // Author objects
            var isaacAsimov = new Author("Isaac Asimov", "A prolific science fiction writer.", "https://cdn.britannica.com/82/195182-050-97684526/Isaac-Asimov-1979.jpg");
            var agathaChristie = new Author("Agatha Christie", "Famous for her detective novels.", "https://f.i.uol.com.br/fotografia/2019/01/04/15466155705c2f7b12063d0_1546615570_3x2_md.jpg");
            var janeAusten = new Author("Jane Austen", "Known for her classic romance novels.", "https://romanceshistoricos.com.br/wp-content/uploads/2019/07/valkirias-janeausten-1.png");
            var jkRowling = new Author("J.K. Rowling", "Author of the Harry Potter series.", "https://s2.glbimg.com/XsbfFRDdcLdvQyLaQPsvHvbAYvs=/e.glbimg.com/og/ed/f/original/2020/11/10/50414449_1979923558782917_748461144397578240_o.jpg");
            var georgeMartin = new Author("George R.R. Martin", "Author of the A Song of Ice and Fire series.", "https://cdn.britannica.com/05/223205-050-8931FF28/American-writer-George-RR-Martin-2011.jpg");
            var stephenKing = new Author("Stephen King", "Famous for his horror and suspense novels.", "https://cdn.britannica.com/20/217720-050-857D712B/American-novelist-Stephen-King-2004.jpg");

            // Book objects
            var foundation = new Book("Foundation", isaacAsimov, scienceFiction, "A classic sci-fi series.");
            var orientExpress = new Book("Murder on the Orient Express", agathaChristie, mystery, "A famous detective novel.");
            var pridePrejudice = new Book("Pride and Prejudice", janeAusten, romance, "A timeless love story.");
            var sorcerersStone = new Book("Harry Potter and the Sorcerer's Stone", jkRowling, fantasy, "The first book in the Harry Potter series.");
            var gameOfThrones = new Book("A Game of Thrones", georgeMartin, fantasy, "The first book in the A Song of Ice and Fire series.");
            var theShining = new Book("The Shining", stephenKing, mystery, "A classic horror novel.");
            var dune = new Book("Dune", isaacAsimov, scienceFiction, "A science fiction masterpiece.");
            var senseSensibility = new Book("Sense and Sensibility", janeAusten, romance, "Another classic by Jane Austen.");
            var rogerAckroyd = new Book("Murder of Roger Ackroyd", agathaChristie, mystery, "A famous Hercule Poirot mystery.");
            var chamberSecrets = new Book("Harry Potter and the Chamber of Secrets", jkRowling, fantasy, "The second book in the Harry Potter series.");
            var clashKings = new Book("A Clash of Kings", georgeMartin, fantasy, "The second book in the A Song of Ice and Fire series.");
            var it = new Book("It", stephenKing, mystery, "A terrifying tale of a shape-shifting clown.");
            var iRobot = new Book("I, Robot", isaacAsimov, scienceFiction, "A collection of science fiction stories.");
            var emma = new Book("Emma", janeAusten, romance, "Another classic novel by Jane Austen.");
            var deathNile = new Book("Death on the Nile", agathaChristie, mystery, "A Hercule Poirot mystery set on a cruise.");

            return new List<Book>
            {
                foundation,
                orientExpress,
                pridePrejudice,
                sorcerersStone,
                gameOfThrones,
                theShining,
                dune,
                senseSensibility,
                rogerAckroyd,
                chamberSecrets,
                clashKings,
                it,
                iRobot,
                emma,
                deathNile
            };
        }
    }
}
