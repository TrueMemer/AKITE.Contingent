using AKITE.Contingent.Client.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKITE.Contingent.Client.Services
{
    public static class SpecialtyDataService
    {
        private static BindingList<Specialty> _specialties;

        static SpecialtyDataService()
        {
            _specialties = new BindingList<Specialty>
            {
                new Specialty { Code=null, Name="Абитуриенты", ShortName="" },
                new Specialty { Code="38.02.01", Name="Экономика и бухгалтерский учет", ShortName="БУ" },
                new Specialty { Code="09.02.03", Name="Программирование в компьютерных системах", ShortName="ПКС" },
                new Specialty { Code="38.02.04", Name="Коммерция (по отраслям)", ShortName="КПО" },
                new Specialty { Code="11.02.01", Name="Радиоаппаратостроение", ShortName="РАС" }
            };
        }

        public static void AddSpecialty(Specialty specialty)
        {
            if (specialty != null)
                _specialties.Add(specialty);
        }

        public static IEnumerable<Specialty> GetSpecialties()
        {
            return _specialties;
        }
    }
}
