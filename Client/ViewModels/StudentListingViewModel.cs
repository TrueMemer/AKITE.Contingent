using AKITE.Contingent.Client.Utilities;
using AKITE.Contingent.Client.Pages;
using AKITE.Contingent.Client.Services;
using MahApps.Metro.Controls.Dialogs;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using AKITE.Contingent.Client.Dialogs;
using AKITE.Contingent.Helpers;
using AKITE.Contingent.Models;
using MahApps.Metro.Controls;

namespace AKITE.Contingent.Client.ViewModels
{
    public class StudentListingViewModel : BaseBindable
    {
        private MetroWindow _studentForm;

        public ObservableCollection<object> SelectedStudents { get; set; }

        private bool _loadingStudents;
        public bool LoadingStudents
        {
            get => _loadingStudents;
            set
            {
                _loadingStudents = value;
                OnPropertyChanged();
            }
        }

        #region Команды
        public ICommand FastTransferStudent { get; private set; }
        private void FastTransfer(object obj)
        {
            var dialog = new FastTransferDialog(SelectedStudents[0] as Student, _dataCoordinator);
            dialog.ShowDialog();
        }

        public ICommand OpenStudentCommand { get; private set; }
        private void OpenStudentForm(object obj)
        {
            if (SelectedStudents.Count > 1 || SelectedStudents.Count == 0) return;
            _studentForm = new StudentForm(SelectedStudents[0] as Student, _dataCoordinator, _dialogCoordinator);
            _studentForm.Show();
        }

        public ICommand AddStudentCommand { get; private set; }
        private void AddStudentForm(object obj)
        {
            _studentForm = new StudentForm(null, _dataCoordinator, _dialogCoordinator);
            _studentForm.Show();
        }

        public ICommand RemoveStudentCommand { get; private set; }
        private async void RemoveStudent(object obj)
        {
            string deleteString;

            if (SelectedStudents.Count == 1)
            {
                deleteString = "Вы действительно хотите удалить студента?";
            }
            else
            {
                deleteString = $"Вы действительно хотите удалить студентов:\n";
                foreach (var st in SelectedStudents)
                {
                    if (st is Student s)
                        deleteString += $"- {s.ShortName}\n";
                }
            }

            var result = await _dialogCoordinator.ShowMessageAsync(this, "Удаление студентов", deleteString,
                MessageDialogStyle.AffirmativeAndNegative,
                new MetroDialogSettings { AnimateHide = false, AnimateShow = false });

            if (result == MessageDialogResult.Negative) return;

            foreach (var st in SelectedStudents.ToList())
            {
                if (st is Student s)
                {
                    await StudentDataService.Delete(s);
                }
            }

        }

        public ICommand OnLoadedCommand { get; set; }
        // FIXME: Пофиксить утечку памяти
        private void Loaded(object s)
        {
            //LoadingStudents = true;
            //await StudentDataService.RefreshStudents();
            //LoadingStudents = false;
        }
        #endregion

        private readonly DataCoordinator _dataCoordinator;
        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly Navigator _navigator;

        public StudentDataService StudentDataService => _dataCoordinator.StudentDataService;

        public StudentListingViewModel(IDialogCoordinator dialogCoordinator, DataCoordinator dataCoordinator, Navigator navigator)
        {
            _dialogCoordinator = dialogCoordinator;
            _dataCoordinator = dataCoordinator;
            _navigator = navigator;

            SelectedStudents = new ObservableCollection<object>();

            FastTransferStudent = new RelayCommand(FastTransfer, (obj) => SelectedStudents.Count > 0);
            OpenStudentCommand = new RelayCommand(OpenStudentForm);
            RemoveStudentCommand = new RelayCommand(RemoveStudent, (obj) => SelectedStudents.Count > 0);
            AddStudentCommand = new RelayCommand(AddStudentForm);
            OnLoadedCommand = new RelayCommand(Loaded);
        }
    }
}
