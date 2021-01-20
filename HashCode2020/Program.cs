using System;
using System.Collections.Generic;

namespace HashCode2020
{
    class Program
    {
        static void Main(string[] args)
        {

            Dictionary<int, Book> books = new Dictionary<int, Book>();
            SortedSet<Library> libraries = new SortedSet<Library>(new LibraryComparer());

            HashSet<Book> processedBooks = new HashSet<Book>();
            Dictionary<int, Library> signedLibraries = new Dictionary<int, Library>();

            var LibraryCount = 0;
            var BookCount = 0;
            var ScanDay = 0;

            var a = 0.00005;
            var b = 0.00005;
            var c = 0.00005;

            // Process input file
            //string[] lines = System.IO.File.ReadAllLines(@"C:\Users\busra.ozis\source\repos\HashCode2020\HashCode2020\input\a_example.txt");
            //string[] lines = System.IO.File.ReadAllLines(@"C:\Users\busra.ozis\source\repos\HashCode2020\HashCode2020\input\b_read_on.txt");
            //string[] lines = System.IO.File.ReadAllLines(@"C:\Users\busra.ozis\source\repos\HashCode2020\HashCode2020\input\c_incunabula.txt");
            //string[] lines = System.IO.File.ReadAllLines(@"C:\Users\busra.ozis\source\repos\HashCode2020\HashCode2020\input\d_tough_choices.txt");
            //string[] lines = System.IO.File.ReadAllLines(@"C:\Users\busra.ozis\source\repos\HashCode2020\HashCode2020\input\e_so_many_books.txt");
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\busra.ozis\source\repos\HashCode2020\HashCode2020\input\f_libraries_of_the_world.txt");
            var LibraryInd = 0;
            var LineCount = 0;
            var bookCount1 = 0;
            var signUp = 0;
            var scanCapacity = 0;

            var totalbookscorenumber = 0;

            foreach (string line in lines)
            {

                string[] lineItems = line.Split(" ");

                if (LineCount == 0)
                {
                    BookCount = (int)Int64.Parse(lineItems[0]);
                    LibraryCount = (int)Int64.Parse(lineItems[1]);
                    ScanDay = (int)Int64.Parse(lineItems[2]);

                }
                else if (LineCount == 1)
                {
                    var bookId = 0;
                    foreach(string item in lineItems)
                    {
                        var score = (int)Int64.Parse(item);
                        var book = new Book(bookId, score);
                        books.Add(bookId, book);
                        bookId++;
                        totalbookscorenumber += score;

                    }

                }
                else if (LineCount % 2 == 0) 
                {

                    //library set up
                    bookCount1 = (int)Int64.Parse(lineItems[0]);
                    signUp = (int)Int64.Parse(lineItems[1]);
                    scanCapacity = (int)Int64.Parse(lineItems[2]);
                    
                }
                else
                {
                    List<int> bookIds = new List<int>();
                    SortedSet<Book> libBooks = new SortedSet<Book>(new BookComparer());
                    var totalScore = 0;
                    foreach (string item in lineItems)
                    {
                        var bookId = (int)Int64.Parse(item);
                        libBooks.Add(books[bookId]);
                        totalScore += books[bookId].Score;
                    }

                    double priority = CalculatePriority(a, b, c, totalScore, scanCapacity,signUp);

                    var lib = new Library(LibraryInd, signUp, scanCapacity, libBooks);
                    lib.SetPriority(priority);

                    libraries.Add(lib);
                    LibraryInd++;

                }
                LineCount++;

                if(LibraryCount == LibraryInd)
                {
                    break;
                }

            }

            var signedLibraryCount = 0;
            Console.WriteLine($"total book score number: {totalbookscorenumber}");
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\busra.ozis\source\repos\HashCode2020\HashCode2020\output\log_f.txt"))
            {
                var totalProcessedBookScore = 0;
                foreach (Library lib in libraries)
                {
                    file.WriteLine($"ScanDay Left: {ScanDay}");
                    if (ScanDay - lib.SignUpTime > 0)
                    {
                        signedLibraryCount++;

                        file.WriteLine($"Library Id: {lib.Id}");
                        file.WriteLine($"Library Priority: {lib.Priority}");
                        file.WriteLine($"Library Sign Up Time: {lib.SignUpTime}");
                        file.WriteLine($"Library Scan Capacity: {lib.ScanCapacity}");

                        var maxBook = (uint)((ScanDay - lib.SignUpTime) * lib.ScanCapacity);

                        file.WriteLine($"Max BookNumber that can be processed: {maxBook}");

                        var ind = 0;
                        SortedSet<Book> libBooks = new SortedSet<Book>(new BookComparer());
                        foreach (Book book in lib.Books)
                        {
                            
                            if (ind < maxBook)
                            {
                                //if not processed ind++
                                if (!processedBooks.Contains(book))
                                {
                                    file.WriteLine($"Processed Book Id: {book.Id}");
                                    processedBooks.Add(book);
                                    totalProcessedBookScore += book.Score;
                                    file.WriteLine($"Total Score: {totalProcessedBookScore}");
                                    ind++;
                                    libBooks.Add(book);
                                }

                            }
                            else
                            {
                                break;
                            }

                        }

                        var SignedLibrary = new Library(lib.Id, lib.SignUpTime, lib.ScanCapacity, libBooks);
                        signedLibraries.Add(SignedLibrary.Id, SignedLibrary);

                        ScanDay -= lib.SignUpTime;

                        if (ScanDay <= 0)
                            break;
                    }
                }

                using (System.IO.StreamWriter outputfile = new System.IO.StreamWriter(@"C:\Users\busra.ozis\source\repos\HashCode2020\HashCode2020\output\outputf.txt"))
                {
                    outputfile.WriteLine(signedLibraries.Count);
                    foreach (KeyValuePair<int, Library> entry in signedLibraries)
                    {
                        outputfile.WriteLine($"{entry.Key} {entry.Value.Books.Count}");
                        var count = 0;
                        foreach(Book book in entry.Value.Books)
                        {
                            count++;
                            outputfile.Write(book.Id);
                            if (count < entry.Value.Books.Count)
                                outputfile.Write(" ");
                        }
                        outputfile.WriteLine();

                    }
                }



                file.WriteLine($"Total Score:  {totalProcessedBookScore}");
            }

            Console.WriteLine($"Total Book Count  {BookCount}");
            Console.WriteLine($"Processed book count: {processedBooks.Count}");
        }

        static double CalculatePriority(double a, double b, double c, int totalScore, int scanCapacity, int signUpTime)
        {
            return ((a * totalScore * b * scanCapacity) / (c * signUpTime));
        }
    }
}
