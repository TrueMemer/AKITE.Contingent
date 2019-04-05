using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AKITE.Contingent.Client.Services;

namespace AKITE.Contingent.Models
{
    public partial class Group
    {
        public Specialty Specialty => SpecialtyDataService.GetSpecialties().ElementAt(SpecialtyId);
        public string ShortName => $"{GroupNum}{SpecialtyDataService.GetSpecialties().ElementAt(SpecialtyId).ShortName}-{GroupID}";
    }
}
