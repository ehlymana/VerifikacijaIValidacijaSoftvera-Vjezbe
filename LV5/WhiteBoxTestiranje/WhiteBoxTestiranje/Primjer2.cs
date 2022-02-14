using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Priprema_za_Zadaću_3
{
    public class Primjer2
    {
        int numberOfTheDay(string day)
        {
            if (day == "Monday") return 1;
            else if (day == "Tuesday") return 2;
            else if (day == "Wednesday") return 3;
            else if (day == "Thursday") return 4;
            else if (day == "Friday") return 5;
            else if (day == "Saturday") return 6;
            else return 7;
        }

        int numberOfTheDayRefactored(IDay day)
        {
            return day.getNumber();
        }

    }

    public interface IDay
    {
        int getNumber();
    }

    class Monday : IDay
    {
        string name = "Monday";
        public int getNumber()
        {
            return 1;
        }
    }

    class Tuesday : IDay
    {
        string name = "Tuesday";
        public int getNumber()
        {
            return 2;
        }
    }

    class Wednesday : IDay
    {
        string name = "Wednesday";
        public int getNumber()
        {
            return 3;
        }
    }

    class Thursday : IDay
    {
        string name = "Thursday";
        public int getNumber()
        {
            return 4;
        }
    }

    class Friday : IDay
    {
        string name = "Friday";
        public int getNumber()
        {
            return 5;
        }
    }

    class Saturday : IDay
    {
        string name = "Saturday";
        public int getNumber()
        {
            return 6;
        }
    }

    class Sunday : IDay
    {
        string name = "Sunday";
        public int getNumber()
        {
            return 7;
        }
    }
}
