using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using AKITE.Contingent.Client.Services;
using AKITE.Contingent.Client.Utilities;

namespace AKITE.Contingent.Models
{
    public partial class Group
    {
        private static SpecialtyDataService _specialtyDataService;
        private static StudentDataService _studentDataService;

        [IgnoreDataMember]
        public Specialty Specialty => _specialtyDataService.Items.ElementAt(SpecialtyId);
        [IgnoreDataMember]
        public string ShortName => $"{No}{ _specialtyDataService.Items.ElementAt(SpecialtyId).ShortName}-{Number}";
        [IgnoreDataMember]
        public int StudentCount => _studentDataService.Items.Count(student => student.Group == this);

        public static void SetSpecialtyService(SpecialtyDataService service)
        {
            _specialtyDataService = service;
        }

        public static void SetStudentService(StudentDataService service)
        {
            _studentDataService = service;
        }
    }
}
