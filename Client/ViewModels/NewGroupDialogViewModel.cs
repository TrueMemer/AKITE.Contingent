using AKITE.Contingent.Client.Utilities;
using AKITE.Contingent.Models;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AKITE.Contingent.Client.ViewModels
{
    public class NewGroupDialogViewModel
    {
        private readonly DataCoordinator _dataCoordinator;
        private readonly int _specialtyId;

        public ICommand SubmitCommand { get; set; }
        private async void Submit(object obj)
        {
            await _dataCoordinator.GroupDataService.Add(new Group { Id = _dataCoordinator.GroupDataService.Items.Last().Id + 1, No = TempGroup.No, Number = TempGroup.Number, SpecialtyId = _specialtyId });
            Cancel(obj);
        }

        public ICommand CancelCommand { get; set; }
        private void Cancel(object obj)
        {
            var dialog = obj as MetroWindow;
            dialog.Close();
        }

        public NewGroupDialogViewModel(DataCoordinator dataCoordinator, int specialtyId)
        {
            _dataCoordinator = dataCoordinator;
            _specialtyId = specialtyId;

            TempGroup = new Group();
            TempGroup.SpecialtyId = _specialtyId;

            SubmitCommand = new RelayCommand(Submit, (obj) => TempGroup.No.HasValue && TempGroup.Number.HasValue 
                && !_dataCoordinator.GroupDataService.Items.Any(g => g.No == TempGroup.No && g.Number == TempGroup.Number && TempGroup.SpecialtyId == g.SpecialtyId));
            CancelCommand = new RelayCommand(Cancel);
        }

        public Group TempGroup { get; set; }
    }
}
