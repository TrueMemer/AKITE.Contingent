using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AKITE.Contingent.Helpers;

namespace AKITE.Contingent.Models
{
    public class Passport
    {
        public int Id { get; set; }
        public int? Type { get; set; }
        public string Number { get; set; }
        public string Place { get; set; }
        public DateTime? Date { get; set; }
    }

    public class Address
    {
        public int Id { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string FlatNum { get; set; }
    }

    public partial class Student : BaseBindable, ICloneable
    {
        public Student()
        {
        }

        public static List<string> Genders = new List<string>
        {
            "Мужской", "Женский"
        };

        public static List<string> PassportTypes = new List<string>
        {
            "Свидительство о рождении", "Паспорт Украины", "Паспорт ЛНР", "Паспорт ДНР", "Паспорт РФ", "Другой"
        };

        public static List<string> StudyForms = new List<string>
        {
            "Очная", "Заочная", "Экстернат", "Индивидуальная"
        };

        public static List<string> Degrees = new List<string>
        {
            "Средняя общая", "Средняя профессиональная", "Высшая"
        };

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MidName   { get; set; }
        public DateTime? Birthday { get; set; }
        public string Education { get; set; }
        public string CertNum { get; set; }
        public string AttNum { get; set; }
        public string Language { get; set; }
        public int? Gender { get; set; }
        public DateTime CreationDate { get; set; }

        public Passport Passport { get; set; }

        private bool _addressesIdentical;
        public bool AddressesIdentical
        {
            get => _addressesIdentical;
            set
            {
                _addressesIdentical = value;
                OnPropertyChanged();
                OnPropertyChanged("FacticalAddress");
            }
        }

        public string GenderName => Gender.HasValue ? Student.Genders[Gender.Value] : "";

        private Address _facticalAddress;
        public Address FacticalAddress
        {
            get => AddressesIdentical ? RegistrationAddress : _facticalAddress;
            set
            {
                if (!AddressesIdentical) _facticalAddress = value;
            }
        }
        public Address RegistrationAddress { get; set; }

        public int? CaseNum { get; set; }

        public int? StudyForm { get; set; }
        public string StudyFormName => StudyForm.HasValue ? Student.StudyForms[StudyForm.Value] : "";

        public int? Degree { get; set; }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public float? AverageGrade { get; set; }

        private int _groupIndex;
        public int GroupIndex
        {
            get => _groupIndex;
            set
            {
                _groupIndex = value;
                OnPropertyChanged();
                OnPropertyChanged("Group");
                OnPropertyChanged("GroupName");
                OnPropertyChanged("SpecialtyName");
            }
        }

        public string ShortName => $"{LastName} {FirstName[0]}. {MidName ?? ""}.";

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
