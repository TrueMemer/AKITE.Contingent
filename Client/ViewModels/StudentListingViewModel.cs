using AKITE.Contingent.Client.Utilities;
using AKITE.Contingent.Client.Models;
using AKITE.Contingent.Client.Pages;
using AKITE.Contingent.Client.Services;
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
using System.Windows.Input;
using AKITE.Contingent.Client.Dialogs;

namespace AKITE.Contingent.Client.ViewModels
{
    class StudentListingViewModel : BaseBindable
    {
        private Page StudentForm;

        public IEnumerable<Student> Students => StudentDataService.GetStudents();
        public ObservableCollection<object> SelectedStudents { get; set; }

        private IDialogCoordinator _dialogCoordinator;

        #region Команды
        public ICommand FastTransferStudent { get; private set; }
        private async void FastTransfer(object obj)
        {
            var dialog = new FastTransferDialog(SelectedStudents[0] as Student);

            dialog.CancelButton.Click += async (object s, RoutedEventArgs e) =>
            {
                await _dialogCoordinator.HideMetroDialogAsync(this, dialog);
            };

            dialog.TransferButton.Click += async (object s, RoutedEventArgs e) =>
            {
                await _dialogCoordinator.HideMetroDialogAsync(this, dialog);
            };

            await _dialogCoordinator.ShowMetroDialogAsync(this, dialog);
        }

        public ICommand OpenStudentCommand { get; private set; }
        private void OpenStudentForm(object obj)
        {
            if (SelectedStudents.Count > 1 || SelectedStudents.Count == 0) return;
            var window = Application.Current.MainWindow as MainWindow;

            StudentForm = new StudentForm(SelectedStudents[0] as Student);

            var ctx = window.DataContext as ApplicationViewModel;
            ctx.CurrentPage = StudentForm;
        }

        public ICommand AddStudentCommand { get; private set; }
        private void AddStudentForm(object obj)
        {
            var window = Application.Current.MainWindow as MainWindow;

            StudentForm = new StudentForm(null);

            var ctx = window.DataContext as ApplicationViewModel;
            ctx.CurrentPage = StudentForm;
        }

        public ICommand RemoveStudentCommand { get; private set; }
        private async void RemoveStudent(object obj)
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
                    StudentDataService.DeleteStudent(s);
                }
            }

        }
        #endregion

        public StudentListingViewModel(IDialogCoordinator dialogCoordinator)
        {
            _dialogCoordinator = dialogCoordinator;

            SelectedStudents = new ObservableCollection<object>();

            FastTransferStudent = new RelayCommand(FastTransfer, (obj) => SelectedStudents.Count > 0);
            OpenStudentCommand = new RelayCommand(OpenStudentForm);
            RemoveStudentCommand = new RelayCommand(RemoveStudent, (obj) => SelectedStudents.Count > 0);
            AddStudentCommand = new RelayCommand(AddStudentForm);
        }
    }
}
