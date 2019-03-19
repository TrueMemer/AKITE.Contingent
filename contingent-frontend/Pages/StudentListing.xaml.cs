using contingent_frontend.ViewModels;
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

namespace contingent_frontend.Pages
{
    /// <summary>
    /// Логика взаимодействия для StudentListing.xaml
    /// </summary>
    public partial class StudentListing : Page
    {
        public StudentListing()
        {
            DataContext = new StudentListingViewModel();

            InitializeComponent();
        }
    }
}
