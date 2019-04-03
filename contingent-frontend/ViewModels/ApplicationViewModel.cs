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
using System.Windows.Input;
using System.Windows.Interactivity;
using MahApps.Metro.IconPacks;

namespace contingent_frontend
{
    internal class MenuItem : BaseBindable
    {
        private object _icon;
        private string _text;
        private bool _isEnabled = true;
        private RelayCommand _command;
        private Page _navigationDestination;

        public object Icon
        {
            get { return this._icon; }
            set { this.SetProperty(ref this._icon, value); }
        }

        public string Text
        {
            get { return this._text; }
            set { this.SetProperty(ref this._text, value); }
        }

        public bool IsEnabled
        {
            get { return this._isEnabled; }
            set { this.SetProperty(ref this._isEnabled, value); }
        }

        public ICommand Command
        {
            get { return this._command; }
            set { this.SetProperty(ref this._command, (RelayCommand)value); }
        }

        public Page NavigationDestination
        {
            get { return this._navigationDestination; }
            set { this.SetProperty(ref this._navigationDestination, value); }
        }

        public bool IsNavigation => this._navigationDestination != null;
    }

    class ApplicationViewModel : BaseBindable
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

        private Page Dashboard;
        public Page StudentListing;

        private Page Settings;
        private Page About;
        private Page GroupManager;

        private RelayCommand windowLoaded;
        public RelayCommand WindowLoaded
        {
            get
            {
                return windowLoaded ??
                    (windowLoaded = new RelayCommand((obj) =>
                    {
                        var window = obj as MainWindow;

                        window.HambMenu.SelectedItem = AppMenu[0];
                        HambItemClicked.Execute(window.HambMenu.SelectedItem);
                    }));
            }
        }

        public BindingList<MenuItem> AppMenu { get; set; }
        public BindingList<MenuItem> OptionsMenu { get; set; }

        private RelayCommand hambItemClicked;
        public RelayCommand HambItemClicked
        {
            get
            {
                return hambItemClicked ??
                    (hambItemClicked = new RelayCommand((obj) =>
                    {
                        var item = obj as MenuItem;
                        if (item == null) return;
                        CurrentPage = item.NavigationDestination;
                    }));
            }
        }

        public ApplicationViewModel()
        {
            Dashboard = new Dashboard();
            StudentListing = new StudentListing();
            Settings = new Settings();
            About = new About();
            GroupManager = new GroupManager();

            AppMenu = new BindingList<MenuItem>();
            AppMenu.Add(new MenuItem { Icon = new PackIconFontAwesome { Kind = PackIconFontAwesomeKind.TachometerAltSolid }, Text = "Статистика", NavigationDestination = Dashboard });
            AppMenu.Add(new MenuItem { Icon = new PackIconFontAwesome { Kind = PackIconFontAwesomeKind.UserAltSolid }, Text = "Список студентов", NavigationDestination = StudentListing });
            AppMenu.Add(new MenuItem { Icon = new PackIconFontAwesome {}, Text = "Группы", NavigationDestination = GroupManager });
            OptionsMenu = new BindingList<MenuItem>();
            OptionsMenu.Add(new MenuItem { Icon = new PackIconFontAwesome { Kind = PackIconFontAwesomeKind.CogSolid }, Text = "Настройки", NavigationDestination = Settings });
            OptionsMenu.Add(new MenuItem { Icon = new PackIconFontAwesome { Kind = PackIconFontAwesomeKind.InfoSolid }, Text = "О программе", NavigationDestination = About });
        }
    }
}
