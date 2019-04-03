using AKITE.Contingent.Client.Utilities;
using AKITE.Contingent.Client.Services;
using AKITE.Contingent.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace AKITE.Contingent.Client.Models
{
    public class Passport
    {
        public int? Type { get; set; }
        public string Number { get; set; }
        public string Place { get; set; }
        public DateTime? Date { get; set; }
    }

    public class Address
    {
        public string Region { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string FlatNum { get; set; }
    }

    public class Student : BaseBindable, ICloneable
    {
        public Student()
        {
            Passport = new Passport();
            RegistrationAddress = new Address();
            FacticalAddress = new Address();
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

        private bool addressesIdentical;
        public bool AddressesIdentical
        {
            get => addressesIdentical;
            set
            {
                addressesIdentical = value;
                OnPropertyChanged();
                OnPropertyChanged("FacticalAddress");
            }
        }

        public string GenderName => Gender.HasValue ? Student.Genders[Gender.Value] : "";

        private Address facticalAddress;
        public Address FacticalAddress
        {
            get
            {
                if (AddressesIdentical) return RegistrationAddress;
                else return facticalAddress;
            }
            set
            {
                if (!AddressesIdentical) facticalAddress = value;
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

        private int groupIndex;
        public int GroupIndex
        {
            get => groupIndex;
            set
            {
                groupIndex = value;
                OnPropertyChanged();
                OnPropertyChanged("Group");
                OnPropertyChanged("GroupName");
                OnPropertyChanged("SpecialtyName");
            }
        }

        public Group Group => GroupDataService.GetGroups().ElementAt(GroupIndex);

        public string GroupName => Group.ShortName;
        public string SpecialtyName => Group.Specialty.FullName;

        public string ShortName => $"{LastName} {FirstName[0]}. {MidName[0]}.";

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
