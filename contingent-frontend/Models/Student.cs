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
        public string FirstName { get; set; }
        public string LastName  { get; set; }
        public string MidName   { get; set; }

        public string ShortName => $"{LastName} {FirstName[0]}. {MidName[0]}.";
    }

    public class GroupNode
    {
        public string Name { get; set; }
        public BindingList<Student> Students { get; set; }
    }
}
