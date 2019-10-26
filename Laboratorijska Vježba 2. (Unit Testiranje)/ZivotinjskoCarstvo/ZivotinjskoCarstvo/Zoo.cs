using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZivotinjskoCarstvo
{

    /// <summary>
    /// Životinjsko carstvo ili zološki vrt je klasa koja omogućava dodavanje životinja i dodavanje hrane (životinja) 
    /// u jedan od dva kaveza koje zološki vrt ima na raspolaganju. 
    /// Životinja se može dodati samo ako nije hrana nekoj od životinja u kavezu.  
    /// Hrana se može dodati kada niti jedna od životinja u kavezu nije hrana za nju. 
    /// Do sezone jedenja životinje u kavezu ne jedu hranu. 
    /// Kada dođe do sezone jedenja onda se za svaku od životinja u kavezu pokušava naći hrana 
    /// tj. životinja koju jede   i koju životinja u kavezu i pojede.
    /// Potrebno je napisati 6 testova i 4 testa izuzetka. Obavezno koristiti setup i teardown. 
    /// Jedan od mogućih testova može biti provjera dodavanja životinju u (ne)postojeći kavez.
    ///  </summary>

    public class Zoo
    {
        public class Zivotinja
        {
            public Zivotinja(string Ime, string JedeZivotinje) { this.Ime = Ime; this.JedeZivotinje = JedeZivotinje; }
            public string Ime { get; set; }
            public string JedeZivotinje { get; set; }
        }

        List<Zivotinja> kavez1, kavez2;

        public Zoo()
        {
            kavez1 = new List<Zivotinja>();
            kavez2 = new List<Zivotinja>();
        }

        public List<Zivotinja> ZivotinjeUVrtu(int brKaveza)
        {
            switch (brKaveza)
            {
                case 1:
                    return kavez1;

                case 2:
                    return kavez2;

                default:
                    throw new FieldAccessException("Ne postoji takav kavez.");
            }
        }

        public bool ImaLiZivotinje(string Ime, int kavez)
        {

            if (kavez == 1)
                return kavez1.Count(zz => zz.Ime == Ime) > 0;

            else
                return kavez2.Count(zz => zz.Ime == Ime) > 0;
        }

        public bool DodajZivotinju(Zivotinja z, int kavez)
        {
            if (z == null)
                throw new ArgumentException("Koju zivotinju ubaciti ?");

            if (kavez == 1)
            {
                if (kavez1.Count == 0)
                {
                    kavez1.Add(z);
                    return true;
                }
                else
                {
                    foreach (Zivotinja ziv in kavez1)
                        if (z.Ime == ziv.JedeZivotinje)
                            return false;

                    kavez1.Add(z);
                    return true;
                }
            }

            else
            {
                if (kavez2.Count == 0)
                {
                    kavez2.Add(z);
                    return true;
                }

                else
                {
                    foreach (Zivotinja ziv in kavez2)
                        if (z.Ime == ziv.JedeZivotinje)
                            return false;

                    kavez2.Add(z);
                    return true;
                }
            }

            throw new IndexOutOfRangeException("Nema tog kaveza.");
        }

        public bool DodajHranu(Zivotinja z, int kavez)
        {
            if (kavez == 1)
            {
                if (kavez1.Count == 0)
                {
                    kavez1.Add(z);
                    return true;
                }

                else
                {
                    foreach (Zivotinja ziv in kavez1)
                        if (z.JedeZivotinje == ziv.Ime)
                            return false;

                    kavez1.Add(z);
                    return true;
                }
            }

            else
            {
                if (kavez2.Count == 0)
                {
                    kavez2.Add(z);
                    return true;
                }

                else
                {
                    foreach (Zivotinja ziv in kavez2)
                        if (z.JedeZivotinje == ziv.Ime)
                            return false;

                    kavez2.Add(z);
                    return true;
                }
            }

            throw new ArgumentException("Nema tog kaveza.");
        }

        public void SezonaJedenja(int kavez)
        {
            List<Zivotinja> pojedene = new List<Zivotinja>();
            List<Zivotinja> k = new List<Zivotinja>();

            if (kavez == 1)
                k = kavez1;

            else
                k = kavez2;

            foreach (Zivotinja z in k)
                foreach (Zivotinja h in k)
                    if (!pojedene.Contains(z) && !pojedene.Contains(h))
                        if (h.Ime == z.JedeZivotinje)
                            pojedene.Add(h);

            if (pojedene.Count == 0)
                throw new InvalidOperationException("Niko nikoga nije pojeo.");

            foreach (Zivotinja z in pojedene)
                k.Remove(k.FirstOrDefault(zz => zz.Ime == z.Ime));

        }
    }
}
