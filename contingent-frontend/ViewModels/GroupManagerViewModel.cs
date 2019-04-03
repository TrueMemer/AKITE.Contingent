using contingent_frontend.Dialogs;
using contingent_frontend.Helpers;
using contingent_frontend.Models;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace contingent_frontend.ViewModels
{
    class GroupManagerViewModel : BaseBindable
    {
        private IDialogCoordinator dialogCoordinator;

        public GroupManagerViewModel(IDialogCoordinator dialogCoordinator)
        {
            this.dialogCoordinator = dialogCoordinator;
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

        private int selectedSpecialty = 0;
        public int SelectedSpecialty
        {
            get => selectedSpecialty;
            set
            {
                selectedSpecialty = value;
                OnPropertyChanged();
                SelectedGroups = Statics.Groups.Where(g => g.Specialty == Statics.Specialties[SelectedSpecialty == -1 ? 0 : SelectedSpecialty]);
            }
        }

        private RelayCommand addGroupCommand;
        public RelayCommand AddGroupCommand => addGroupCommand ??
            (addGroupCommand = new RelayCommand(async (obj) => {
                var dialog = new NewGroup();
                dialog.SubmitButton.Click += (object s, RoutedEventArgs e) =>
                {
                    Statics.Groups.Add(new Group { GroupID = int.Parse(dialog.Num), GroupNum = int.Parse(dialog.ID), Specialty = dialog.Specialty });
                    SelectedSpecialty += 0;
                    dialogCoordinator.HideMetroDialogAsync(this, dialog);
                };
                dialog.CancelButton.Click += (object s, RoutedEventArgs e) =>
                {
                    dialogCoordinator.HideMetroDialogAsync(this, dialog);
                };
                await dialogCoordinator.ShowMetroDialogAsync(this, dialog);
            }));
    }
}
