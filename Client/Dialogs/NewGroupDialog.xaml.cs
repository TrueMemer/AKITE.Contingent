using AKITE.Contingent.Client.Services;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AKITE.Contingent.Client.Utilities;
using AKITE.Contingent.Models;
using MahApps.Metro.Controls;
using AKITE.Contingent.Client.ViewModels;

namespace AKITE.Contingent.Client.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для NewGroup.xaml
    /// </summary>
    public partial class NewGroupDialog : MetroWindow
    {

        public NewGroupDialog(DataCoordinator dataCoordinator, int specialtyId)
        {
            InitializeComponent();

            DataContext = new NewGroupDialogViewModel(dataCoordinator, specialtyId);
        }
    }
}
