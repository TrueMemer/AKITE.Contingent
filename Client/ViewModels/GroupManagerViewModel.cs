using AKITE.Contingent.Client.Dialogs;
using AKITE.Contingent.Client.Utilities;
using MahApps.Metro.Controls.Dialogs;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using AKITE.Contingent.Helpers;
using AKITE.Contingent.Models;

namespace AKITE.Contingent.Client.ViewModels
{
    public class GroupManagerViewModel : BaseBindable
    {
        public IEnumerable<Specialty> Specialties => _dataCoordinator.SpecialtyDataService.Items.Skip(1);

        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly DataCoordinator _dataCoordinator;

        public GroupManagerViewModel(IDialogCoordinator dialogCoordinator, DataCoordinator dataCoordinator)
        {
            _dialogCoordinator = dialogCoordinator;
            _dataCoordinator = dataCoordinator;

            SelectedSpecialty += 0;

            AddGroupCommand = new RelayCommand(AddGroup);
        }

        private IEnumerable<Group> _selectedGroups;
        public IEnumerable<Group> SelectedGroups
        {
            get => _selectedGroups;
            set
            {
                _selectedGroups = value;
                OnPropertyChanged();
            }
        }

        private int _selectedSpecialty;
        public int SelectedSpecialty
        {
            get => _selectedSpecialty;
            set
            {
                _selectedSpecialty = value;
                OnPropertyChanged();
                SelectedGroups = _dataCoordinator.GroupDataService.Items.Where(g => g.SpecialtyId == SelectedSpecialty + 1);
            }
        }

        public ICommand AddGroupCommand { get; private set; }
        public void AddGroup(object obj)
        {
            var dialog = new NewGroupDialog(_dataCoordinator, SelectedSpecialty + 1);
            dialog.ShowDialog();
            SelectedSpecialty += 0;
         }
    }
}
