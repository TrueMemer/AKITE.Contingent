using contingent_frontend.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contingent_frontend.ViewModels
{
    class StudentFormViewModel : BaseBindable
    {
        public bool isEditing = false;

        public BindingList<Student> Students;
        private Student selectedStudent;
        public Student SelectedStudent
        {
            get => selectedStudent;
            set
            {
                selectedStudent = value;
                OnPropertyChanged();
            }
        }

        public StudentFormViewModel(BindingList<Student> Students, Student SelectedStudent)
        {
            this.Students = Students;
            if (SelectedStudent != null)
            {
                this.SelectedStudent = SelectedStudent;
                isEditing = true;
            }
            else
            {
                this.SelectedStudent = new Student();
                this.SelectedStudent.LastName = "Ivanov";
                this.SelectedStudent.Birthday = DateTime.Now;
            }
        }
    }
}
