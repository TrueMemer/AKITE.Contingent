using AKITE.Contingent.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKITE.Contingent.Models
{
    public partial class Group : BaseBindable
    {
        public int Id { get; set; }

        private int? no;
        public int? No
        {
            get => no;
            set
            {
                no = value;
                OnPropertyChanged();
                OnPropertyChanged("ShortName");
            }
        }

        private int? number;
        public int? Number
        {
            get => number;
            set
            {
                number = value;
                OnPropertyChanged();
                OnPropertyChanged("ShortName");
            }
        }

        private int specialtyId;
        public int SpecialtyId
        {
            get => specialtyId;
            set
            {
                specialtyId = value;
                OnPropertyChanged();
                OnPropertyChanged("ShortName");
                OnPropertyChanged("Specialty");
            }
        }

    }
}
