using AKITE.Contingent.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKITE.Contingent.Client.Models
{
    public class Group
    {
        public int? GroupNum { get; set; }
        public int? GroupID { get; set; }
        public int SpecialtyID { get; set; }
        public Specialty Specialty => SpecialtyDataService.GetSpecialties().ElementAt(SpecialtyID);
        public string ShortName => $"{GroupNum}{SpecialtyDataService.GetSpecialties().ElementAt(SpecialtyID).ShortName}-{GroupID}";
        public int StudentCount => StudentDataService.GetStudents().Count(s => s.Group == this);
    }
}
