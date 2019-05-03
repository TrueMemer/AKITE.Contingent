using AKITE.Contingent.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKITE.Contingent.Models
{
    public partial class Student : ICloneable
    {
        private static GroupDataService _groupDataService;

        public static void SetService(GroupDataService groupDataService)
        {
            _groupDataService = groupDataService;
        }

        public Group Group => _groupDataService.Items.ElementAt(GroupIndex);

        public string GroupName => Group.ShortName;
        public string SpecialtyName => Group.Specialty.FullName;
        public string StudyFormName => StudyForm.HasValue ? Student.StudyForms[StudyForm.Value] : "";
        public string GenderName => Gender.HasValue ? Student.Genders[Gender.Value] : "";
        public string ShortName => $"{LastName} {FirstName[0]}. {MidName ?? ""}.";

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
