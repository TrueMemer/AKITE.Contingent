using AKITE.Contingent.Client.Utilities;
using AKITE.Contingent.Client.Pages;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.IconPacks;
using AKITE.Contingent.Helpers;

namespace AKITE.Contingent.Client.ViewModels
{
    public class ApplicationViewModel : BaseBindable
    {
        public class MenuItem : BaseBindable
        {
            private object _icon;
            private string _text;
            private bool _isEnabled = true;
            private RelayCommand _command;
            private Page _navigationDestination;

            public object Icon
            {
                get => _icon;
                set => SetProperty(ref _icon, value);
            }

            public string Text
            {
                get => _text;
                set => SetProperty(ref _text, value);
            }

            public bool IsEnabled
            {
                get => _isEnabled;
                set => SetProperty(ref _isEnabled, value);
            }

            public ICommand Command
            {
                get => _command;
                set => SetProperty(ref _command, (RelayCommand)value);
            }

            public Page NavigationDestination
            {
                get => _navigationDestination;
                set => SetProperty(ref _navigationDestination, value);
            }

            public bool IsNavigation => _navigationDestination != null;
        }

        #region Страницы
        // Заглушки
        private readonly Page _dashboard = new Dashboard();
        private readonly Page _settings = new Settings();
        private readonly Page _about = new About();

        private readonly Page _studentListing;
        private readonly Page _groupManager;
        #endregion

        #region Команды

        public ICommand WindowLoaded { get; }
        private void OnWindowLoaded(object obj)
        {
            if (!(obj is MainWindow window)) return;
            window.HambMenu.SelectedItem = AppMenu[0];
            HambItemClicked.Execute(window.HambMenu.SelectedItem);
        }

        public ICommand HambItemClicked { get; }
        private void OnHambItemClicked(object obj)
        {
            if (!(obj is MenuItem item)) return;
            Navigator.NavigateTo(item.NavigationDestination);
        }
        #endregion

        public BindingList<MenuItem> AppMenu { get; set; }
        public BindingList<MenuItem> OptionsMenu { get; set; }

        public Navigator Navigator { get; }

        public ApplicationViewModel(DataCoordinator dataCoordinator)
        {
            Navigator = new Navigator();

            WindowLoaded = new RelayCommand(OnWindowLoaded);
            HambItemClicked = new RelayCommand(OnHambItemClicked);

            _studentListing = new StudentListing(dataCoordinator, Navigator);
            _settings = new Settings();
            _about = new About();
            _groupManager = new GroupManager(dataCoordinator);

            AppMenu = new BindingList<MenuItem>
            {
                new MenuItem { Icon = new PackIconFontAwesome { Kind = PackIconFontAwesomeKind.UserAltSolid }, Text = "Список студентов", NavigationDestination = _studentListing },
                new MenuItem { Icon = new PackIconFontAwesome { Kind = PackIconFontAwesomeKind.UsersSolid }, Text = "Менеджер групп", NavigationDestination = _groupManager }
            };

            OptionsMenu = new BindingList<MenuItem>
            {
                new MenuItem { Icon = new PackIconFontAwesome { Kind = PackIconFontAwesomeKind.CogSolid }, Text = "Настройки", NavigationDestination = _settings },
                new MenuItem { Icon = new PackIconFontAwesome { Kind = PackIconFontAwesomeKind.InfoSolid }, Text = "О программе", NavigationDestination = _about }
            };
        }
    }
}
