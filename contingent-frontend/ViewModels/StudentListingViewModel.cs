using contingent_frontend.Helpers;
using contingent_frontend.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contingent_frontend.ViewModels
{
    class StudentListingViewModel : BaseViewModel
    {
        private BindingList<GroupNode> groups;
        public BindingList<GroupNode> Groups
        {
            get => groups;
            set
            {
                groups = value;
                OnPropertyChanged();
            }
        }

        private BindingList<Student> currentStudents;
        public BindingList<Student> CurrentStudents
        {
            get => currentStudents;
            set
            {
                currentStudents = value;
                OnPropertyChanged();
            }
        }

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

        private GroupNode selectedGroup;
        public GroupNode SelectedGroup
        {
            get => selectedGroup;
            set
            {
                selectedGroup = value;
                CurrentStudents = selectedGroup.Students;
                OnPropertyChanged();
            }
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                    (addCommand = new RelayCommand(obj =>
                    {
                        CurrentStudents.Add(new Student { FirstName = "Ivan", LastName = "Ivanov", MidName = "Ivanovich" });
                    },
                    (obj) => SelectedGroup == null ? false : true
                    ));
            }
        }

        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                    (removeCommand = new RelayCommand(obj =>
                    {
                        var a = obj as Student;
                        if (a != null) CurrentStudents.Remove(a);

                    },
                    (obj) => SelectedGroup == null || SelectedGroup.Students.Count < 1 ? false : true
                    ));
            }
        }

        private RelayCommand selectedStudentTreeChange;
        public RelayCommand SelectedStudentTreeChange
        {
            get
            {
                return selectedStudentTreeChange ??
                    (selectedStudentTreeChange = new RelayCommand(obj =>
                    {
                        dynamic a;
                        if ((a = obj as Student) != null)
                        {
                            SelectedStudent = a;
                            SelectedGroup = Groups.Where(g => g.Students.Any(s => s == a)).SingleOrDefault();
                        }
                        else
                        {
                            SelectedStudent = null;
                            SelectedGroup = obj as GroupNode;
                        }
                    }));
            }
        }

        private RelayCommand newGroupClicked;
        public RelayCommand NewGroupClicked
        {
            get
            {
                return newGroupClicked ??
                    (newGroupClicked = new RelayCommand(obj =>
                    {
                        Groups.Add(new GroupNode { Name = "Новая группа", Students = new BindingList<Student>() });
                    }));
            }
        }

        public StudentListingViewModel()
        {
            Groups = new BindingList<GroupNode>(API.GetGroupNodes());
        }
    }
}
