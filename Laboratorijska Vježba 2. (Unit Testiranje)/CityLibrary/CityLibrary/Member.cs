using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CityLibrary
{
    public class Member:Person
    {

        private static readonly string ID_PREFIX = "BA-";
        private static readonly Regex ID_PATTERN = new Regex(ID_PREFIX +"[0-9]+");

        private static readonly Regex CITIZEN_ID_PATTERN = new Regex("[0-9]{13}");

        private string _id;

        private DateTime _memberSince;

        private string _citizenId;

        public Member(string firstname, string lastname, string citizenId) : base(firstname, lastname)
        {
            _memberSince = DateTime.Now;
            
        }
        public string ID
        {
            get
            {
                return _id;
            }
            set
            {

                if (value.Length < 13 || ID_PATTERN.IsMatch(value)==false)
                {
                    throw new ArgumentException();
                }

                _id = value;

            }
        }

        public string CitizenID
        {
            get
            {
                return _citizenId;
            }
            set
            {

                if (CITIZEN_ID_PATTERN.IsMatch(value)==false)
                {
                    throw new ArgumentException("Invalid Citizen ID");
                }

                _citizenId = value;
            }
        }
        public static String GenerateID()
        {
            Random random = new Random();
            return ID_PREFIX + random.Next(0, Int16.MaxValue);
        }

        

    }
}
