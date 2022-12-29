using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Zadaca420212022.Models
{
    public enum Edicija
    {
        [Display(Name = "Mehke korice")]
        E1, 
        [Display(Name = "Tvrde korice")]
        E2,
        [Display(Name = "Kolekcionarsko izdanje")]
        E3,
        [Display(Name = "Alternativna verzija")]
        E4
    }

    public class ValidateDate : ValidationAttribute
    {
        protected override ValidationResult IsValid
                         (object date, ValidationContext validationContext)
        {
            if ((int)date < 1000)
                return new ValidationResult("Datum izdavanja knjige ne smije biti stariji od 1000. godine!");
            else if ((int)date > DateTime.Now.Year)
                return new ValidationResult("Datum izdavanja knjige ne smije biti u budućnosti!");
            else
                return ValidationResult.Success;
        }
    }

    public class Knjiga
    {
        [Key]
        [Required]
        [DisplayName("ISBN")]
        [RegularExpression(@"^(97(8|9))?\d{9}(\d|X)$", ErrorMessage = "Moguće je unijeti samo validan ISBN broj!")]
        public string ISBN { get; set; }

        [Required]
        [StringLength(int.MaxValue, MinimumLength = 5, ErrorMessage = "Morate unijeti barem 5 karaktera!")]
        [DisplayName("Naziv knjige:")]
        public string NazivKnjige { get; set; }

        [Required]
        [StringLength(int.MaxValue, MinimumLength = 3, ErrorMessage = "Morate unijeti barem 3 karaktera!")]
        [DisplayName("Ime i prezime autora:")]
        public string ImeAutora { get; set; }

        [Required]
        [ValidateDate]
        [DisplayName("Godina izdavanja:")]
        public int GodinaIzdavanja { get; set; }

        [Required]
        [EnumDataType(typeof(Edicija))]
        [DisplayName("Edicija knjige:")]
        public Edicija EdicijaKnjige { get; set; }

        [DisplayName("Opis knjige:")]
        public string OpisKnjige { get; set; }
    }
}
