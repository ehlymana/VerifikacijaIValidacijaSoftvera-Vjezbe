using CityLibrary.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibrary
{
    public abstract class Person
    {
        private string _firstName;
        private string _lastName;

        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public override bool Equals(Object obj)
        {
            if (obj is Person)
            {
                Person person = (Person)obj;
                return person.FirstName == FirstName && person.LastName == LastName;
            }
            return false;
        }

        public string FirstName
        {
            get
            {
                return _firstName;
            }

            set
            {
                if (!Validator.ValidateAlphaNmeric(value))
                {
                    throw new ArgumentException("Provided name is not valid");
                }
                _firstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return _lastName;
            }

            set
            {

                if (!Validator.ValidateAlphaNmeric(value))
                {
                    throw new ArgumentException("Provided name is not valid");
                }
                _lastName = value;
            }
        }
    }
}
