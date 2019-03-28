using contingent_frontend.Models;
using contingent_frontend.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace contingent_frontend.Helpers
{
    public static class Statics
    {
        public static BindingList<Specialty> Specialties = new BindingList<Specialty>
        {
            new Specialty { Code=null, Name="Абитуриенты", ShortName="", Groups=new BindingList<Group>() },
            new Specialty { Code="38.02.01", Name="Экономика и бухгалтерский учет", ShortName="БУ", Groups=new BindingList<Group>() },
            new Specialty { Code="09.02.03", Name="Программирование в компьютерных системах", ShortName="ПКС", Groups=new BindingList<Group>() },
            new Specialty { Code="38.02.04", Name="Коммерция (по отраслям)", ShortName="КПО", Groups=new BindingList<Group>() },
            new Specialty { Code="11.02.01", Name="Радиоаппаратостроение", ShortName="РАС", Groups=new BindingList<Group>() }
        };

        public static List<string> Genders = new List<string>
        {
            "Мужской", "Женский"
        };

        public static List<string> PassportTypes = new List<string>
        {
            "Свидительство о рождении", "Паспорт Украины", "Паспорт ЛНР", "Паспорт ДНР", "Паспорт РФ", "Другой"
        };

        public static List<string> StudyForms = new List<string>
        {
            "Очная", "Заочная", "Экстернат", "Индивидуальная"
        };

        public static List<string> Degrees = new List<string>
        {
            "Средняя общая", "Средняя профессиональная", "Высшая"
        };

        public static BindingList<Student> Students = new BindingList<Student>();
    }
}
