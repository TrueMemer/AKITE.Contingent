using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contingent_frontend.Models
{
    public class Student
    {
        [JsonProperty("id")]
        public int ID { get; set; }
        [JsonProperty("firstname")]
        public string FirstName { get; set; }
        [JsonProperty("lastname")]
        public string LastName  { get; set; }
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
        [JsonProperty("group_id")]
        public int? GroupID { get; set; }
        [JsonProperty("group_name")]
        public string GroupName { get; set; }

        public string ShortName => $"{LastName} {FirstName[0]}. {MidName[0]}.";
    }

    public class GroupNode
    {
        public string Name { get; set; }
        public BindingList<Student> Students { get; set; }
    }
}
