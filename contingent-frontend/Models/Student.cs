using contingent_frontend.Helpers;
using contingent_frontend.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace contingent_frontend.Models
{

    public class Specialty
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string FullName
        {
            get
            {
                return $"{Code + " " ?? ""}{Name}";
            }
        }
    }

    public class Group
    {
        public int? GroupNum { get; set; }
        public int? GroupID { get; set; }
        public Specialty Specialty { get; set; }

        public string ShortName
        {
            get
            {
                if (this.Specialty.Code == null)
                {
                    return Statics.Specialties[0].Name;
                }

                return $"{GroupNum}{this.Specialty.ShortName}-{GroupID}";
            }
        }
    }

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
            facticalAddress = new Address();
        }

        [JsonProperty("id")]
        public int ID { get; set; }
        [JsonProperty("firstname")]
        public string FirstName { get; set; }
        [JsonProperty("lastname")]
        public string LastName { get; set; }
        [JsonProperty("midname")]
        public string MidName   { get; set; }
        [JsonProperty("birthday")]
        public DateTime? Birthday { get; set; }
        [JsonProperty("education")]
        public string Education { get; set; }
        [JsonProperty("cert_num")]
        public string CertNum { get; set; }
        [JsonProperty("att_num")]
        public string AttNum { get; set; }
        [JsonProperty("language")]
        public string Language { get; set; }
        [JsonProperty("gender")]
        public int? Gender { get; set; }
        [JsonProperty("creation_date")]
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

        public string GenderName => Gender.HasValue ? Statics.Genders[Gender.Value] : "";

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
        public string StudyFormName => StudyForm.HasValue ? Statics.StudyForms[StudyForm.Value] : "";

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
            }
        }

        public Group Group => Statics.Groups[GroupIndex];

        public string ShortName => $"{LastName} {FirstName[0]}. {MidName[0]}.";

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
