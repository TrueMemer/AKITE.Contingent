using AKITE.Contingent.Client.Services;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Syncfusion.Data.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using AKITE.Contingent.Helpers;
using AKITE.Contingent.Models;

namespace AKITE.Contingent.Client.ViewModels
{
    class StudentFormViewModel : BaseBindable
    {
        public IEnumerable<Specialty> Specialties => SpecialtyDataService.GetSpecialties();
        public IEnumerable<Group> Groups => GroupDataService.GetGroups();

        public bool isEditing = false;

        #region Команды
        public Student EditedStudent { get; set; }
        public Student SelectedStudent { get; set; }

        public ICommand SaveCommand { get; private set; }
        private async void SaveStudent(object obj)
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

                var index = _studentDataService.GetStudents().IndexOf(SelectedStudent);
                await _studentDataService.UpdateStudent(index, EditedStudent);
            }
            else
            {
                result = await mwindow.ShowMessageAsync("Подтвердите добавление", "Вы действительно хотите добавить нового студента?", MessageDialogStyle.AffirmativeAndNegative);
                if (result == MessageDialogResult.Negative) return;

                await _studentDataService.AddStudent(EditedStudent);
            }
            var vm = Application.Current.MainWindow.DataContext as ApplicationViewModel;

            vm.GoBack();
        }

        public ICommand CancelCommand { get; private set; }
        private async void Cancel(object obj)
        {
                var mwindow = Application.Current.MainWindow as MetroWindow;

                var result = await mwindow.ShowMessageAsync("Подтвердите отмену", "Вы уверены что хотите отменить? Все изменения будут утеряны!", MessageDialogStyle.AffirmativeAndNegative);
                if (result == MessageDialogResult.Negative) return;

                var vm = Application.Current.MainWindow.DataContext as ApplicationViewModel;

                vm.GoBack();
        }
        #endregion

        private readonly StudentDataService _studentDataService;

        public StudentFormViewModel(Student SelectedStudent, StudentDataService studentDataService)
        {
            _studentDataService = studentDataService;

            SaveCommand = new RelayCommand(SaveStudent);
            CancelCommand = new RelayCommand(Cancel);

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
