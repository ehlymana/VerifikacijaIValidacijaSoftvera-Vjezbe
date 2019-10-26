using CityLibrary.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CityLibrary
{
    public class Book
    {
        // example: ISBN 1-56389-668-0
        private const String ISBN_PATTERN = @"ISBN\x20(?=.{13}$)\d{1,5}([- ])\d{1,7}\1\d{1,6}\1(\d|X)$";

        private string _isbn;
        private string _title;
        private Author _author;
        private int _yearPublished;
        private string _publisher;

        public string ISBN
        {
            get
            {
                return _isbn;
            }

            set
            {
                Regex regex = new Regex(ISBN_PATTERN);
                if (!regex.IsMatch(value)){
                    throw new ArgumentException("Invalid ISBN supplied");
                }

                _isbn = value;
            }
        }

        public string Title {
            get {
                return _title;
            }
            set {
                if (!Validator.ValidateAlphaNmeric(value)){
                    throw new ArgumentException("Invalid ISBN supplied");

                }

                _title = value;
            }
        }

        public Author Author {
            get{
                return _author;
            }
            set{
                _author = value;
            }
        }
        
        public int YearPublished {
            get {
                return  _yearPublished;
            }
            set {
                if (value>DateTime.Now.Year || value<0){
                    throw new ArgumentOutOfRangeException("Provided year is out of range");
                }
                _yearPublished = value;
            }
        }

        public String Publisher
        {
            get
            {
                return _publisher;
            }
            set
            {
                if (Validator.ValidateAlphaNmeric(value) == false)
                {
                    throw new ArgumentException("Publisher name is invalid");
                }

                _publisher = value;
            }
        }

        public override bool Equals(Object obj)
        {
            if (obj is Book)
            {
                Book book = (Book)obj;
                return book.ISBN == ISBN;
            }
            return false;
            
        }

    }
}
