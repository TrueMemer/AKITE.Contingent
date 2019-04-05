using AKITE.Contingent.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKITE.Contingent.Models
{
    public partial class Student
    {
        public Group Group => GroupDataService.GetGroups().ElementAt(GroupIndex);

        public string GroupName => Group.ShortName;
        public string SpecialtyName => Group.Specialty.FullName;
    }
}
