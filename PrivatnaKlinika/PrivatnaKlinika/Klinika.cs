using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivatnaKlinika
{
    public class Klinika
    {
        List<Pacijent> pacijenti;
        List<MedicinskoOsoblje> mOsoblje;
        List<NemedicinskoOsoblje> nOsoblje;
        List<string> moguciPregledi;
        List<Ordinacija> ordinacije;

        public Klinika ()
        {
            pacijenti = new List<Pacijent>();
            mOsoblje = new List<MedicinskoOsoblje>();
            nOsoblje = new List<NemedicinskoOsoblje>();
            moguciPregledi = new List<string>();
            ordinacije = new List<Ordinacija>();
            moguciPregledi.Add("Opći");
            moguciPregledi.Add("Sistematski");
            moguciPregledi.Add("Ljekarski");
            Ordinacija o1 = new Ordinacija("Opća", false, false);
            Ordinacija o2 = new Ordinacija("Radiološka", false, false);
            Ordinacija o3 = new Ordinacija("Hirurška", false, false);
            ordinacije.Add(o1);
            ordinacije.Add(o2);
            ordinacije.Add(o3);
        }
        public List<Pacijent> Pacijenti
        {
            get
            {
                return pacijenti;
            }

            set
            {
                pacijenti = value;
            }
        }

        public List<MedicinskoOsoblje> MOsoblje
        {
            get
            {
                return mOsoblje;
            }

            set
            {
                mOsoblje = value;
            }
        }

        public List<NemedicinskoOsoblje> NOsoblje
        {
            get
            {
                return nOsoblje;
            }

            set
            {
                nOsoblje = value;
            }
        }

        public List<string> MoguciPregledi
        {
            get
            {
                return moguciPregledi;
            }

            set
            {
                moguciPregledi = value;
            }
        }

        public List<Ordinacija> Ordinacije
        {
            get
            {
                return ordinacije;
            }

            set
            {
                ordinacije = value;
            }
        }

        public void registracijaPacijenta(string ime, string prezime, DateTime rodenje, string maticni, string spol, string adresa, string brak, string zeljeniPregled, string brojKnjizice, DateTime prijem)
        {
            Pacijenti.Add(new Pacijent(ime, prezime, rodenje, maticni, spol, adresa, brak, zeljeniPregled, prijem, brojKnjizice));
        }
        public void registracijaPacijenta(Pacijent p)
        {
            Pacijenti.Add(p);
        }
        public void registracijaMOsoblja(string ime, string prezime, string username, string password, Ordinacija o)
        {
            MOsoblje.Add(new MedicinskoOsoblje(ime, prezime, username, password, o));
        }
        public void registracijaMOsoblja(string ime, string prezime, string username, string password)
        {
            MOsoblje.Add(new MedicinskoOsoblje(ime, prezime, username, password));
        }
		public void dodajMOsoblje(MedicinskoOsoblje m)
		{
			MOsoblje.Add(m);
		}
        public void anamneza(Pacijent p, string sadasnjeBolesti, string sadasnjeAlergije, string ranijeBolesti, string ranijeAlergije, string stanjePorodice, string zakljucak, MedicinskoOsoblje preuzeo)
        {
            p.Karton=new Karton(sadasnjeBolesti, sadasnjeAlergije, ranijeBolesti, ranijeAlergije, stanjePorodice, zakljucak, preuzeo);
        }
        public void zakaziPregled(Pacijent p, Ordinacija ordinacija)
        {
            if (ordinacija != null)
            {
                if (ordinacija.UKvaru) throw new InvalidOperationException("Ordinacija ne radi - aparatura u kvaru");
                else if (ordinacija.PrivremenoZatvori) throw new InvalidOperationException("Ordinacija ne radi - privremeno zatvorena");
                else ordinacija.dodajPacijenta(p);
            }
            List<ZakazaniPregled> zakazani = new List<ZakazaniPregled>();
            if (!p.ZeljeniPregled.Equals("Opći"))
            {
                if (p.ZeljeniPregled.Equals("Sistematski"))
                {
                    if (ordinacija.Ime == "Kardiološka")
                        zakazani.Add(new ZakazaniPregled(ordinacija));
                    else
                        zakazani.Add(new ZakazaniPregled(ordinacije.Find(o => o.Ime == "Kardiološka")));
                }
                else
                {
                    if (ordinacija.Ime == "Internistička" || p.BrojPosjeta < 10)
                        throw new InvalidOperationException("Pacijentu ne može biti pružena tražena usluga!");
                    else if (p.Rodenje.Year + 70 < DateTime.Now.Year)
                    {
                        ordinacija.PacijentiURedu.Insert(0, p);
                        zakazani.Add(new ZakazaniPregled(ordinacija));
                    }
                    else
                        zakazani.Add(new ZakazaniPregled(ordinacija));
                }
            }
            else zakazani.Add(new ZakazaniPregled(ordinacija));
            foreach (ZakazaniPregled z in zakazani) p.Karton.dodajZakazaniPregled(z);
        }
        public void vrsenjePregleda(Pacijent p, string misljenje, string rezultat, string terapija, string garancija, bool dugorocna, DateTime datum, ZakazaniPregled dodatniPregled, MedicinskoOsoblje doktor)
        {
            if (doktor.Username[doktor.Username.Length - 1] >= '0' && doktor.Username[doktor.Username.Length - 1] <= '9') throw new InvalidOperationException("Tehničar ne može vršiti pregled");
            p.Karton.dodajProsliPregled(new Pregled(misljenje, rezultat, terapija, garancija, dugorocna, datum));
            foreach (ZakazaniPregled z in p.Karton.ZakazaniPregledi)
            {
                if (z.Ime.Ime.Equals(doktor.Ordinacija.Ime))
                {
                    p.Karton.ZakazaniPregledi.Remove(z);
                    break;
                }
            }
            if (dodatniPregled!=null) p.Karton.dodajZakazaniPregled(dodatniPregled);
            doktor.Posjete++;
            p.BrojPosjeta++;
            doktor.Ordinacija.sljedeci();
        }

        public Racun izdajRacun(Pacijent p, int rate)
        {
            Racun r = new Racun();
            r.obracunaj(p, rate);
            return r;
        }
        public void odjaviPacijenta (string imeIPrezime)
        {
            foreach (Pacijent p in Pacijenti)
            {
                if ((p.Ime + " " + p.Prezime).Equals(imeIPrezime))
                {
                    Pacijenti.Remove(p);
                    break;
                }
            }
        }
        public void prijavaKvara (string ime)
        {
            foreach (Ordinacija o in Ordinacije)
            {
                if ((o.Ime).Equals(ime))
                {
                    o.UKvaru = true;
                    break;
                }
            }
        }
        public void privremenoZatvori(string ime)
        {
            foreach (Ordinacija o in Ordinacije)
            {
                if ((o.Ime).Equals(ime))
                {
                    o.PrivremenoZatvori = true;
                    break;
                }
            }
        }
        public void otvori(string ime)
        {
            foreach (Ordinacija o in Ordinacije)
            {
                if ((o.Ime).Equals(ime))
                {
                    o.PrivremenoZatvori = false;
                    o.UKvaru = false;
                    break;
                }
            }
        }
        public void dodajOrdinaciju(Ordinacija o)
        {
            Ordinacije.Add(o);
        }
        public void blokirajTerapiju (Pacijent p, string opis)
		{
			if (p!=null)
			p.Karton.dajNajnovijePreglede()[0].blokirajTerapiju(opis);
		}
        public interface IMetode
        {
            string historijaPacijenta(Pacijent p);
            string prikaziRanijeTerapije(Pacijent p);
            string trenutnaTerapija(Pacijent p);
        }
		public interface ILaboratorija
		{
			void testiraj(Pacijent p, double serum);
			string eritrociti(Pacijent p);
			string leukociti(Pacijent p);
			string krvnaGrupa(Pacijent p);
		}
    }
}
