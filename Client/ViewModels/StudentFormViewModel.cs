using AKITE.Contingent.Client.Services;
using AKITE.Contingent.Client.Utilities;
using AKITE.Contingent.Helpers;
using AKITE.Contingent.Models;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Syncfusion.Data.Extensions;
using System.Windows.Input;

namespace AKITE.Contingent.Client.ViewModels
{
    public class StudentFormViewModel : BaseBindable
    {
        private readonly bool _isEditing;

        #region Команды
        public Student EditedStudent { get; set; }

        public ICommand SaveCommand { get; }
        private async void SaveStudent(object obj)
        {
            var dialog = obj as MetroWindow;

            if (string.IsNullOrEmpty(EditedStudent.LastName) || string.IsNullOrEmpty(EditedStudent.FirstName))
            {
                await _dialogCoordinator.ShowMessageAsync(this, "Заполните обязательные поля (Имя и фамилия)", "Заполните обязательные поля!");
                return;
            }

            MessageDialogResult result;

            if (_isEditing)
            {
                result = await _dialogCoordinator.ShowMessageAsync(this, "Подтвердите изменения", "Вы действительно хотите подтвердить изменения?", MessageDialogStyle.AffirmativeAndNegative);
                if (result == MessageDialogResult.Negative) return;

                await _dataCoordinator.StudentDataService.Update(EditedStudent.Id, EditedStudent);
            }
            else
            {
                result = await _dialogCoordinator.ShowMessageAsync(this, "Подтвердите добавление", "Вы действительно хотите добавить нового студента?", MessageDialogStyle.AffirmativeAndNegative);
                if (result == MessageDialogResult.Negative) return;

                await _dataCoordinator.StudentDataService.Add(EditedStudent);
            }

            dialog.Close();
        }

        public ICommand CancelCommand { get; }
        private async void Cancel(object obj)
        {
            var dialog = obj as MetroWindow;

            var result = await _dialogCoordinator.ShowMessageAsync(this, "Подтвердите отмену", "Вы уверены что хотите отменить? Все изменения будут утеряны!", MessageDialogStyle.AffirmativeAndNegative);
            if (result == MessageDialogResult.Negative) return;

            dialog.Close();
        }
        #endregion

        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly DataCoordinator _dataCoordinator;

        public StudentFormViewModel(Student selectedStudent, DataCoordinator dataCoordinator, IDialogCoordinator dialogCoordinator)
        {
            _dataCoordinator = dataCoordinator;
            _dialogCoordinator = dialogCoordinator;

            SaveCommand = new RelayCommand(SaveStudent, (obj) => !(string.IsNullOrEmpty(EditedStudent.LastName) || string.IsNullOrEmpty(EditedStudent.FirstName)));
            CancelCommand = new RelayCommand(Cancel);

            if (selectedStudent != null)
            {
                EditedStudent = selectedStudent.Clone() as Student;
                _isEditing = true;
            }
            else
            {
                EditedStudent = new Student();
            }
        }
    }
}
