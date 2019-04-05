using AKITE.Contingent.Client.Dialogs;
using AKITE.Contingent.Client.Utilities;
using AKITE.Contingent.Client.Services;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AKITE.Contingent.Helpers;
using AKITE.Contingent.Models;

namespace AKITE.Contingent.Client.ViewModels
{
    class GroupManagerViewModel : BaseBindable
    {
        public IEnumerable<Specialty> Specialties => SpecialtyDataService.GetSpecialties().Skip(1);

        private IDialogCoordinator dialogCoordinator;

        public GroupManagerViewModel(IDialogCoordinator dialogCoordinator)
        {
            this.dialogCoordinator = dialogCoordinator;

            AddGroupCommand = new RelayCommand(AddGroup);
        }

        private IEnumerable<Group> selectedGroups;
        public IEnumerable<Group> SelectedGroups
        {
            get => selectedGroups;
            set
            {
                selectedGroups = value;
                OnPropertyChanged();
            }
        }

        private int selectedSpecialty = -1;
        public int SelectedSpecialty
        {
            get => selectedSpecialty;
            set
            {
                selectedSpecialty = value;
                OnPropertyChanged();
                SelectedGroups = GroupDataService.GetGroups().Where(g => g.SpecialtyId == SelectedSpecialty + 1);
            }
        }

        public ICommand AddGroupCommand { get; private set; }
        public async void AddGroup(object obj)
        {
                var dialog = new NewGroup();
                dialog.SubmitButton.Click += async (object s, RoutedEventArgs e) =>
                {
                    GroupDataService.AddGroup(new Group { GroupID = int.Parse(dialog.Num), GroupNum = int.Parse(dialog.ID), SpecialtyId = SelectedSpecialty + 1 });
                    SelectedSpecialty += 0;
                    await dialogCoordinator.HideMetroDialogAsync(this, dialog);
                };
                dialog.CancelButton.Click += async (object s, RoutedEventArgs e) =>
                {
                    await dialogCoordinator.HideMetroDialogAsync(this, dialog);
                };
                await dialogCoordinator.ShowMetroDialogAsync(this, dialog);
         }
    }
}
