using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemZaGrijanje
{
    public interface ITermostat
    {
        public List<double> Temperature 
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public double dajTemperaturu()
        {
            throw new NotImplementedException();
        }
    }
}
