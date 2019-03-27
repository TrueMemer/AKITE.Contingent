using contingent_frontend.Models;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace contingent_frontend.ViewModels
{
    class StudentFormViewModel : BaseBindable
    {
        public bool isEditing = false;

        public BindingList<Student> Students;
        public Student EditedStudent { get; set; }
        public Student SelectedStudent { get; set; }

        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                    (saveCommand = new RelayCommand(async obj =>
                    {
                        var mwindow = Application.Current.MainWindow as MetroWindow;

                        if (String.IsNullOrEmpty(EditedStudent.LastName) || String.IsNullOrEmpty(EditedStudent.FirstName))
                        {
                            await mwindow.ShowMessageAsync("Заполните обязательные поля", "Заполните обязательные поля!", MessageDialogStyle.Affirmative);
                            return;
                        }

                        MessageDialogResult result;

                        if (isEditing)
                        {
                            result = await mwindow.ShowMessageAsync("Подтвердите изменения", "Вы действительно хотите подтвердить изменения?", MessageDialogStyle.AffirmativeAndNegative);
                            if (result == MessageDialogResult.Negative) return;

                            var index = Students.IndexOf(SelectedStudent);
                            Students[index] = EditedStudent.Clone() as Student;
                        }
                        else
                        {
                            result = await mwindow.ShowMessageAsync("Подтвердите добавление", "Вы действительно хотите добавить нового студента?", MessageDialogStyle.AffirmativeAndNegative);
                            if (result == MessageDialogResult.Negative) return;

                            Students.Add(EditedStudent);
                        }
                        var vm = Application.Current.MainWindow.DataContext as ApplicationViewModel;

                        vm.CurrentPage = vm.StudentListing;
                    }));
            }
        }

        private RelayCommand cancelCommand;
        public RelayCommand CancelCommand => cancelCommand ??
            (cancelCommand = new RelayCommand(async obj =>
            {
                var mwindow = Application.Current.MainWindow as MetroWindow;

                var result = await mwindow.ShowMessageAsync("Подтвердите отмену", "Вы уверены что хотите отменить? Все изменения будут утеряны!", MessageDialogStyle.AffirmativeAndNegative);
                if (result == MessageDialogResult.Negative) return;

                var vm = Application.Current.MainWindow.DataContext as ApplicationViewModel;

                vm.CurrentPage = vm.StudentListing;
            }));

        public StudentFormViewModel(BindingList<Student> Students, Student SelectedStudent)
        {
            this.Students = Students;
            if (SelectedStudent != null)
            {
                this.SelectedStudent = SelectedStudent;
                EditedStudent = SelectedStudent.Clone() as Student;
                isEditing = true;
            }
            else
            {
                EditedStudent = new Student();
            }
        }
    }
}
