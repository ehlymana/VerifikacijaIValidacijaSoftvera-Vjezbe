using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibrary
{
    public class LibraryBook: Book
    {
        private uint _quantity;
        private DateTime _arrivedOn;

        private DateTime _modifiedOn;

        public LibraryBook(Book book){
            Author = book.Author;
            ISBN = book.ISBN;
            Title = book.Title;
            YearPublished = book.YearPublished;
            Publisher = book.Publisher;
        }
        public uint Quantity
        {
            get
            {
                return _quantity;
            }

            set
            {
                _modifiedOn = DateTime.Now;
                _quantity = value;
            }
        }
        
        public DateTime ArrivedOn
        {
            get
            {
                return _arrivedOn;
            }
            set
            {
                _arrivedOn = value;
            }
        }

        public void IncreaseQuantity()
        {
            Quantity++;
        }

        public void DecreaseQuantity()
        {
            Quantity--;
        }

        public void IncreaseQuantityBy(uint value)
        {
            Quantity += value;
        }
    }
}
