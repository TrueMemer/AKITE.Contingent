using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKITE.Contingent.Models
{
    public class Specialty
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string FullName => $"{Code + " " ?? ""}{Name}";
    }
}
