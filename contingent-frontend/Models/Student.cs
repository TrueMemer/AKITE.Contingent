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
    

        public static List<Specialty> Specialties = new List<Specialty>
        {
            new Specialty { Code=null, Name="Абитуриенты", ShortName="" },
            new Specialty { Code="38.02.01", Name="Экономика и бухгалтерский учет", ShortName="БУ" },
            new Specialty { Code="09.02.03", Name="Программирование в компьютерных системах", ShortName="ПКС" },
            new Specialty { Code="38.02.04", Name="Коммерция (по отраслям)", ShortName="КПО" },
            new Specialty { Code="11.02.01", Name="Радиоаппаратостроение", ShortName="РАС" }
        };
    }

    public class Group
    {
        public int? GroupNum { get; set; }
        public int? GroupID { get; set; }
        public Specialty Specialty { get; set; }

        public static List<Group> Groups = new List<Group>
        {
            new Group { Specialty=Specialty.Specialties[0] }
        };

        public string ShortName
        {
            get
            {
                if (this.Specialty.Code == null)
                {
                    return Specialty.Specialties[0].Name;
                }

                return $"{GroupNum}{this.Specialty.ShortName}-{GroupID}";
            }
        }
    }

    public class Student : BaseBindable
    {
        [JsonProperty("id")]
        public int ID { get; set; }
        [JsonProperty("firstname")]
        public string FirstName { get; set; }

        private string lastName;
        [JsonProperty("lastname")]
        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged();
            }
        }
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
        [JsonProperty("languages")]
        public string Languages { get; set; }
        [JsonProperty("gender")]
        public int? Gender { get; set; }
        [JsonProperty("creation_date")]
        public DateTime CreationDate { get; set; }

        public int? CaseNum { get; set; }

        public Group Group { get; set; }
        public Specialty Specialty { get; set; }

        public string ShortName => $"{LastName} {FirstName[0]}. {MidName[0]}.";
    }
}
