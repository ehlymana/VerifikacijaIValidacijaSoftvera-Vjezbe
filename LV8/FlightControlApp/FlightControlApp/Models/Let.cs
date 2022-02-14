using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlApp.Models
{
    public enum VrstaLeta
    {
        [Display(Name = "Komercijalni")]
        Komercijalni,
        [Display(Name = "VIP")]
        VIP,
        [Display(Name = "Privatni")]
        Privatni,
        [Display(Name = "Vojni")]
        Vojni
    }

    public class ValidateDate : ValidationAttribute
    {
        protected override ValidationResult IsValid
                         (object date, ValidationContext validationContext)
        {
            return ((DateTime)date >= DateTime.Now.AddDays(1) && (DateTime)date <= DateTime.Now.AddDays(30))
                ? ValidationResult.Success
                : new ValidationResult("Možete rezervisati samo letove između 1 i 30 dana nakon dana rezervacije!");
        }
    }

    public class Let
    {
        string brojLeta;
        VrstaLeta vrsta;
        DateTime odlazak, povratak;
        bool plaćeno;

        [Key]
        [Required]
        [StringLength(6, ErrorMessage = "Broj leta ima tačno 6 karaktera!")]
        [RegularExpression(@"([A-Z][A-Z])-([0-9][0-9][0-9])",
         ErrorMessage = "Broj leta je u formatu AA-123")]
        [DisplayName("Broj leta:")]
        public string BrojLeta { get => brojLeta; set => brojLeta = value; }

        [Required]
        [EnumDataType (typeof(VrstaLeta))]
        [DisplayName("Vrsta leta:")]
        public VrstaLeta Vrsta { get => vrsta; set => vrsta = value; }

        [Required]
        [ValidateDate]
        [DataType(DataType.Date)]
        [DisplayName("Datum odlaska:")]
        public DateTime? Odlazak { get => odlazak; set => odlazak = (DateTime)value; }

        [Required]
        [ValidateDate]
        [DataType(DataType.Date)]
        [DisplayName("Datum povratka:")]
        public DateTime? Povratak { get => povratak; set => povratak = (DateTime)value; }

        [Required]
        [DisplayName("Već je izvršeno plaćanje")]
        public bool Plaćeno { get => plaćeno; set => plaćeno = value; }
    }
}
