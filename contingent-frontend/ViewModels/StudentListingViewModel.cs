using contingent_frontend.Helpers;
using contingent_frontend.Models;
using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace contingent_frontend.ViewModels
{
    class StudentListingViewModel : BaseBindable
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
                    (addCommand = new RelayCommand(async obj =>
                    {
                        var s = new Student { FirstName = "Ivan", LastName = "Ivanov", MidName = "Ivanovich" };
                        var result = await API.AddStudent(s);
                        
                        if (result == -1)
                        {
                            var window = Application.Current.MainWindow as MetroWindow;
                            await window.ShowMessageAsync("Ошибка!", "Не удалось добавить студента", MessageDialogStyle.Affirmative);
                            return;
                        }

                        s.ID = result;

                        CurrentStudents.Add(s);
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
                    (removeCommand = new RelayCommand(async obj =>
                    {
                        var window = Application.Current.MainWindow as MetroWindow;
                        var result = await window.ShowMessageAsync("Удаление студента", "Вы действительно хотите удалить студента?", 
                            MessageDialogStyle.AffirmativeAndNegative,
                            new MetroDialogSettings { AnimateHide = false, AnimateShow = false });

                        if (result == MessageDialogResult.Negative) return;

                        var a = obj as Student;
                        SelectedStudent = null;
                        if (a != null)
                        {
                            API.DeleteStudentByID(a.ID);
                            CurrentStudents.Remove(a);
                        }

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
                        if ((obj as Student) != null)
                        {
                            SelectedStudent = obj as Student;
                            SelectedGroup = Groups.Where(g => g.Students.Any(s => s == obj as Student)).SingleOrDefault();
                        }
                        else
                        {
                            SelectedStudent = null;
                            SelectedGroup = obj as GroupNode;
                        }
                    }));
            }
        }

        private RelayCommand gridSelectedChanged;
        public RelayCommand GridSelectedChanged
        {
            get
            {
                return gridSelectedChanged ??
                    (gridSelectedChanged = new RelayCommand(obj =>
                    {

                    }));
            }
        }

        private RelayCommand newGroupClicked;
        public RelayCommand NewGroupClicked
        {
            get
            {
                return newGroupClicked ??
                    (newGroupClicked = new RelayCommand(async obj =>
                    {
                        var window = Application.Current.MainWindow as MetroWindow;
                        var name = await window.ShowInputAsync("Создание новой группы", "Введите название новой группы", new MetroDialogSettings { AnimateHide=false, AnimateShow=false });

                        if (name == String.Empty || name == null) return;

                        Groups.Add(new GroupNode { Name = name, Students = new BindingList<Student>() });
                    }));
            }
        }

        public async Task FetchGroups()
        {
            var nodes = await API.GetGroupNodesAsync();
            if (nodes == null)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    var window = Application.Current.MainWindow as MainWindow;
                    (window.StatusBar.Items[0] as StatusBarItem).Content = "Не удалось получить список студентов!";
                });
                Groups = new BindingList<GroupNode>();
                return;
            }
            else
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    var window = Application.Current.MainWindow as MainWindow;
                    int count = nodes.Count();
                    (window.StatusBar.Items[0] as StatusBarItem).Content = $"Загружено {count.ToString()} групп";
                });
                Groups = new BindingList<GroupNode>(nodes);
            }
        }

        public StudentListingViewModel()
        {
            Task.Run(FetchGroups);
        }
    }
}
