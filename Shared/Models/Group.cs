using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKITE.Contingent.Models
{
    public partial class Group
    {
        public int Id { get; set; }
        public int? GroupNum { get; set; }
        public int? GroupID { get; set; }
        public int SpecialtyId { get; set; }
    }
}
