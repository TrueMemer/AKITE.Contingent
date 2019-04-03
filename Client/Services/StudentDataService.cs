using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AKITE.Contingent.Client.Models;

namespace AKITE.Contingent.Client.Services
{
    public static class StudentDataService
    {
        private static BindingList<Student> _students;

        static StudentDataService()
        {
            _students = new BindingList<Student>
            {
                new Student { CaseNum=1, Birthday=DateTime.Now, GroupIndex=1, Gender=0, FirstName="Иван", LastName="Иванов", MidName="Иванович", AttNum="1", CertNum="1"},
                new Student { CaseNum=2, Birthday=DateTime.Now, GroupIndex=1, Gender=0, FirstName="Петр", LastName="Петров", MidName="Петрович", AttNum="2", CertNum="2"},
                new Student { CaseNum=3, Birthday=DateTime.Now, GroupIndex=2, Gender=0, FirstName="Сидоров", LastName="Никита", MidName="Федорович", AttNum="3", CertNum="3"},
                new Student { CaseNum=4, Birthday=DateTime.Now, GroupIndex=3, Gender=1, FirstName="Алиса", LastName="Рейх", MidName="Руслановна", AttNum="4", CertNum="4"},
                new Student { CaseNum=5, Birthday=DateTime.Now, GroupIndex=3, Gender=1, FirstName="Анастасия", LastName="Лис", MidName="Александровна", AttNum="5", CertNum="5"},
                new Student { CaseNum=6, Birthday=DateTime.Now, GroupIndex=4, Gender=0, FirstName="Александр", LastName="Пирогов", MidName="Викторович", AttNum="6", CertNum="6"},
                new Student { CaseNum=7, Birthday=DateTime.Now, GroupIndex=4, Gender=0, FirstName="Евгений", LastName="Титаренко", MidName="Андреевич", AttNum="7", CertNum="7"},
            };

            _students.ListChanged += ListChanged;
        }

        private static void ListChanged(object s, ListChangedEventArgs e)
        {
            //_students = new BindingList<Student>(_students.OrderBy(st => st.GroupIndex).ToList());
        }

        public static void AddStudent(Student student)
        {
            if (student != null)
                _students.Add(student);
        }

        public static void DeleteStudent(Student student)
        {
            _students.Remove(student);
        }

        public static void UpdateStudent(int old, Student new_)
        {
            _students[old] = new_.Clone() as Student;
        }

        public static IEnumerable<Student> GetStudents()
        {
            return _students;
        }
    }
}