using contingent_frontend.Helpers;
using contingent_frontend.Models;
using contingent_frontend.Pages;
using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Syncfusion.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace contingent_frontend.ViewModels
{
    class StudentListingViewModel : BaseBindable
    {
        public Page StudentForm;

        private BindingList<Student> students;
        public BindingList<Student> Students
        {
            get => students;
            set
            {
                students = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<object> SelectedStudents { get; set; }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                    (addCommand = new RelayCommand(obj =>
                    {
                        var window = Application.Current.MainWindow as MainWindow;

                        StudentForm = new StudentForm(Students, null);

                        var ctx = window.DataContext as ApplicationViewModel;
                        ctx.CurrentPage = StudentForm;
                    }));
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

                        string deleteString;

                        if (SelectedStudents.Count == 1)
                        {
                            deleteString = "Вы действительно хотите удалить студента?";
                        }
                        else
                        {
                            deleteString = $"Вы действительно хотите удалить студентов:\n";
                            foreach (var _s in SelectedStudents)
                            {
                                var s = _s as Student;
                                deleteString += $"- {s.ShortName}\n";
                            }
                        }

                        var result = await window.ShowMessageAsync("Удаление студентов", deleteString, 
                            MessageDialogStyle.AffirmativeAndNegative,
                            new MetroDialogSettings { AnimateHide = false, AnimateShow = false });

                        if (result == MessageDialogResult.Negative) return;

                        foreach (var _s in SelectedStudents.ToList())
                        {
                            var s = _s as Student;
                            if (s != null)
                            {
                                //API.DeleteStudentByID(a.ID);
                                Students.Remove(s);
                            }
                        }

                    },
                    (obj) => SelectedStudents.Count < 1 ? false : true
                    ));
            }
        }

        private RelayCommand openStudentCommand;
        public RelayCommand OpenStudentCommand => openStudentCommand ??
                    (openStudentCommand = new RelayCommand(obj =>
                    {
                        if (SelectedStudents.Count > 1 || SelectedStudents.Count == 0) return;
                        var window = Application.Current.MainWindow as MainWindow;

                        StudentForm = new StudentForm(Students, SelectedStudents[0] as Student);

                        var ctx = window.DataContext as ApplicationViewModel;
                        ctx.CurrentPage = StudentForm;
                    }));

        public StudentListingViewModel()
        {
            if (!(ConfigurationManager.AppSettings["DemoMode"] == "1"))
            {
                //Task.Run(FetchGroups);
            }

            SelectedStudents = new ObservableCollection<object>();

            Statics.Groups.Add(new Group { GroupID = 17, GroupNum = 1, Specialty = Statics.Specialties[2] });
            Statics.Groups.Add(new Group { GroupID = 17, GroupNum = 2, Specialty = Statics.Specialties[2] });
            Statics.Groups.Add(new Group { GroupID = 17, GroupNum = 3, Specialty = Statics.Specialties[2] });
            Statics.Groups.Add(new Group { GroupID = 19, GroupNum = 1, Specialty = Statics.Specialties[1] });
            Statics.Groups.Add(new Group { GroupID = 19, GroupNum = 2, Specialty = Statics.Specialties[1] });

            Students = new BindingList<Student>
            {
                new Student { CaseNum=1, Birthday=DateTime.Now, GroupIndex=1, Gender=0, FirstName="Иван", LastName="Иванов", MidName="Иванович", AttNum="1", CertNum="1"},
                new Student { CaseNum=2, Birthday=DateTime.Now, GroupIndex=1, Gender=0, FirstName="Петр", LastName="Петров", MidName="Петрович", AttNum="2", CertNum="2"},
                new Student { CaseNum=3, Birthday=DateTime.Now, GroupIndex=2, Gender=0, FirstName="Сидоров", LastName="Никита", MidName="Федорович", AttNum="3", CertNum="3"},
                new Student { CaseNum=4, Birthday=DateTime.Now, GroupIndex=3, Gender=1, FirstName="Алиса", LastName="Рейх", MidName="Руслановна", AttNum="4", CertNum="4"},
                new Student { CaseNum=5, Birthday=DateTime.Now, GroupIndex=3, Gender=1, FirstName="Анастасия", LastName="Лис", MidName="Александровна", AttNum="5", CertNum="5"},
                new Student { CaseNum=6, Birthday=DateTime.Now, GroupIndex=4, Gender=0, FirstName="Александр", LastName="Пирогов", MidName="Викторович", AttNum="6", CertNum="6"},
                new Student { CaseNum=7, Birthday=DateTime.Now, GroupIndex=4, Gender=0, FirstName="Евгений", LastName="Титаренко", MidName="Андреевич", AttNum="7", CertNum="7"},
            };
        }
    }
}
