using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibrary
{
    class IssuedBook
    {
        private Book _book;
        private Member  _member;
        private DateTime _issuedOn;
        private DateTime _returnBy;
        private DateTime? _returnedOn;



        public IssuedBook(Member member, Book book,DateTime returnBy)
        {
            _member = member;
            _book = book;
            _issuedOn = DateTime.Now;

            int dateComparasion = DateTime.Compare(_issuedOn, returnBy);
            if (dateComparasion >= 1)
            {
                throw new ArgumentException("Date when book was issued cannot be easlier than return date");
            }
            _returnBy = returnBy;

        }

        public Book Book {
            get
            {
                return _book;
            }
        }

        public Member Member
        {
            get
            {
                return _member;
            }
        }

        public DateTime ReturnBy
        {
            get
            {
                return _returnBy;
            }
        }
        public DateTime IssuedOn
        {
            get
            {
                return _issuedOn;
            }
        }

        public void ReturnBook()
        {
            if (IsReturned){
                throw new InvalidOperationException("Book already returned");
            }

            _returnedOn = DateTime.Now;
        }

        public int Overtime
        {
            get
            {
                DateTime? returned = (_returnedOn == null ? DateTime.Now : _returnedOn);
                int days = (_issuedOn - returned.Value).Days;

                return days < 0 ? 0 : days;
            }
        }
        public bool IsReturned
        {
            get
            {
                return _returnedOn != null;
            }
        }
    }
}
