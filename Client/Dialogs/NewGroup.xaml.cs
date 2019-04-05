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
using AKITE.Contingent.Models;

namespace AKITE.Contingent.Client.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для NewGroup.xaml
    /// </summary>
    public partial class NewGroup : CustomDialog
    {
        public NewGroup()
        {
            InitializeComponent();

            DataContext = this;
        }

        public IEnumerable<Specialty> Specialties => SpecialtyDataService.GetSpecialties().Skip(1);

        public string ID { get; set; }
        public string Num { get; set; }
    }
}
