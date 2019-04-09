using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AKITE.Contingent.Helpers;

namespace AKITE.Contingent.Models
{
    public partial class Student : BaseBindable
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

        public int? PassportType { get; set; }
        public string PassportNumber { get; set; }
        public string PassportPlace { get; set; }
        public DateTime? PassportDate { get; set; }

        public string Region { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string FlatNum { get; set; }

        private string _facticalRegion;
        private string _facticalCity;
        private string _facticalStreet;
        private string _facticalHouse;
        private string _facticalFlatNum;

        public string FacticalRegion { get => AddressesIdentical ? Region : _facticalRegion; set => _facticalRegion = value; }
        public string FacticalCity { get => AddressesIdentical ? City : _facticalCity; set => _facticalCity = value;}
        public string FacticalStreet { get => AddressesIdentical ? Street : _facticalStreet; set => _facticalStreet = value; }
        public string FacticalHouse { get => AddressesIdentical ? House : _facticalHouse; set => _facticalHouse = value; }
        public string FacticalFlatNum { get => AddressesIdentical ? FlatNum : _facticalFlatNum; set => _facticalFlatNum = value; }

        private bool _addressesIdentical;
        public bool AddressesIdentical
        {
            get => _addressesIdentical;
            set
            {
                _addressesIdentical = value;
                OnPropertyChanged();
                OnPropertyChanged("FacticalRegion");
                OnPropertyChanged("FacticalCity");
                OnPropertyChanged("FacticalStreet");
                OnPropertyChanged("FacticalHouse");
                OnPropertyChanged("FacticalFlatNum");
            }
        }

        public int? CaseNum { get; set; }

        public int? StudyForm { get; set; }
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
    }
}
