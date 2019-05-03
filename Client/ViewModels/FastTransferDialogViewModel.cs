using AKITE.Contingent.Client.Utilities;
using AKITE.Contingent.Models;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TinyLittleMvvm;

namespace AKITE.Contingent.Client.ViewModels
{
    class FastTransferDialogViewModel
    {
        private readonly DataCoordinator _dataCoordinator;

        public IEnumerable<Group> Groups => _dataCoordinator.GroupDataService.Items;
        public int GroupIndex { get; set; }

        private int _studentId = -1;

        public ICommand TransferCommand { get; set; }
        private async void Transfer(object obj)
        {
            var dialog = obj as MetroWindow;
            await _dataCoordinator.StudentDataService.Transfer(_studentId, GroupIndex);
            dialog.Close();
        }

        public ICommand CancelCommand { get; set; }
        private void Cancel(object obj)
        {
            var dialog = obj as MetroWindow;
            dialog.Close();
        }

        public FastTransferDialogViewModel(DataCoordinator dataCoordinator, int studentId, int CurrentGroup = 0)
        {
            _dataCoordinator = dataCoordinator;
            _studentId = studentId;
            GroupIndex = CurrentGroup;

            TransferCommand = new RelayCommand(Transfer);
            CancelCommand = new RelayCommand(Cancel);
        }
    }
}
