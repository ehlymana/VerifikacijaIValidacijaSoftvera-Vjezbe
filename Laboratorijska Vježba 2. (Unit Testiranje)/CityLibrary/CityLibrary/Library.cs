using CityLibrary.Exceptions;
using CityLibrary.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibrary
{
    public class Library
    {
        private List<LibraryBook> _books = null;
        private List<Member> _members;
        private List<IssuedBook> _issuedBooks;

        private string _name;

        /// <summary>
        /// Constucts new library instance. 
        /// </summary>
        /// <param name="libraryName">Name of the library being created. 
        /// If library name is not alpha-numberic string, ArgumentException is thrown
        /// </param>
        public Library(string libraryName)
        {
            if (!Validator.ValidateAlphaNmeric(libraryName))
            {
                throw new ArgumentException("Invalid library name provided");

            }
            _name = libraryName;
            _books = new List<LibraryBook>();
            _members = new List<Member>();
            _issuedBooks = new List<IssuedBook>();
        }
        /// <summary>
        /// Adds book to list of available books. If book with ISBN is already in library, 
        /// instead of adding new book, quantity is increased.
        /// </summary>
        /// <param name="book">Book to be added to library</param>
        /// <param name="quantity">Number of books being added</param>
        /// <param name="arrivedOn">When Book arrived in library</param>
        /// <returns>true if book is added to the library. Otherwise method returns false</returns>
        public bool AddBook(Book book,uint quantity, DateTime arrivedOn)
        {
            LibraryBook libraryBook = null;
            try
            {
                libraryBook = FindBook(book.ISBN);
                if (libraryBook != null)
                {
                    libraryBook.IncreaseQuantity();
                    return true;
                }

            }
            catch (ArgumentException exception)
            {
                // TODO use logging, not Console
                Console.WriteLine(exception.StackTrace.ToString()); ;
            }
            finally{
            }
            libraryBook = new LibraryBook(book);
            libraryBook.Quantity = quantity;
            libraryBook.ArrivedOn = arrivedOn;
            _books.Add(libraryBook);
            return true;

        }
        
        /// <summary>
        /// Removes books from list of available books. 
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns></returns>
        public bool RemoveBook(string isbn)
        {
            return _books.Remove(FindBook(isbn));
        }

        /// <summary>
        /// Decreese quantity of book specified by ISBN. If resuling quantity is below 0 book is removed from library.
        /// </summary>
        /// <param name="isbn">ISBN of the book</param>
        /// <param name="quantity">Quantity to be decreesed for</param>
        /// <returns>true on success</returns>
        public bool DecreeseQuantity(string isbn, uint quantity){
            LibraryBook book = FindBook(isbn);
            if (book.Quantity - quantity < 0)
            {
                return RemoveBook(isbn);
            }

            book.Quantity -= quantity;
            return true;

        }

        /// <summary>
        /// Method returns LibraryBook instance for specified ISBN
        /// </summary>
        /// <param name="isbn">ISBN</param>
        /// <returns>Book for specified ISBN</returns>
        public LibraryBook FindBook(string isbn)
        {
            return _books.Find(s => s.ISBN == isbn);
        }

        public List<LibraryBook> FindByAuthor(string firstName, string lastname)
        {
            firstName = firstName.ToLower();
            lastname = lastname.ToLower();

            var books = (from b in _books
                         where b.Author.FirstName.ToLower().Contains(firstName) && b.Author.LastName.ToLower().Contains(lastname)
                         select b).ToList<LibraryBook>();
            return books;
        }

        public string AddMember(string firstName, string lastName, string citizenId)
        {
            bool isIdUnique = true;
            string ID = string.Empty;
            while (isIdUnique)
            {
                ID = Member.GenerateID();
                bool exists =_members.Exists(m => m.ID == ID);
                if (!exists){
                    break;
                }
            }
            Member member = new Member(firstName, lastName, citizenId);
            _members.Add(member);
            return ID;
        }

        public bool IssueBook(string userId, string isbn,DateTime returnBy)
        {
            Member member = FindMember(userId);
            Book book = FindBook(isbn);

            if (!HaveMoreBooks(isbn))
            {
                throw new BookNotAvailableException("Book not available");
            }

            _issuedBooks.Add(new IssuedBook(member,book,returnBy));
            return true;
            
        }

        public Member FindMember(string userId)
        {
            return _members.Find(m => m.ID == userId);
        }

        public bool HaveMoreBooks(string isbn) 
        {
            int issuedCount = 0;
            foreach (IssuedBook book in _issuedBooks)
            {
                if (book.IsReturned == false)
                {
                    issuedCount++;
                }
            }

            LibraryBook libraryBook = FindBook(isbn);
            return (libraryBook.Quantity > issuedCount);
        }


        public bool ReturnBook(string isbn, string userId)
        {
            foreach (IssuedBook book in _issuedBooks)
            {
                if (book.Book.ISBN == isbn && book.Member.ID == userId && book.IsReturned==false)
                {
                    book.ReturnBook();
                }
            }

            throw new NoIssuedBookFoundException("No book issued");

        }

        public int NumberOfBooks
        {
            get
            {
                return _books.Count;
            }
        }

    }

}
