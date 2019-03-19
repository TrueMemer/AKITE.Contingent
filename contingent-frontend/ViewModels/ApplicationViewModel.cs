using contingent_frontend.Helpers;
using contingent_frontend.Models;
using contingent_frontend.Pages;
using contingent_frontend.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace contingent_frontend
{
    class ApplicationViewModel : BaseViewModel
    {
        private Page currentPage;
        public Page CurrentPage
        {
            get => currentPage;
            set
            {
                currentPage = value;
                OnPropertyChanged();
            }
        }

        private Page StudentListing;

        private RelayCommand windowLoaded;
        public RelayCommand WindowLoaded
        {
            get
            {
                return windowLoaded ??
                    (windowLoaded = new RelayCommand((obj) =>
                    {
                        
                    }));
            }
        }

        private RelayCommand navigateStudentListing;
        public RelayCommand NavigateStudentListing
        {
            get
            {
                return navigateStudentListing ??
                    (navigateStudentListing = new RelayCommand((obj) =>
                    {
                        var a = API.GetGroupNodes();
                        CurrentPage = StudentListing;
                    }));
            }
        }

        public ApplicationViewModel()
        {
            StudentListing = new StudentListing();
        }
    }
}
