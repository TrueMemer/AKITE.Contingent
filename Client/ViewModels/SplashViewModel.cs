using System;
using System.Windows.Input;
using AKITE.Contingent.Client.Utilities;
using AKITE.Contingent.Client.Windows;
using AKITE.Contingent.Helpers;

namespace AKITE.Contingent.Client.ViewModels
{
    public class SplashViewModel : BaseBindable
    {
        private MainWindow _mainWindow;
        private DataCoordinator _dataCoordinator;

        private string _message = "Загрузка...";

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        public SplashViewModel()
        {
            WindowLoaded = new RelayCommand(OnWindowLoaded);
        }

        public ICommand WindowLoaded { get; set; }
        private async void OnWindowLoaded(object obj)
        {
            _dataCoordinator = new DataCoordinator();
            await _dataCoordinator.Init(Message);

            _mainWindow = new MainWindow(_dataCoordinator);
            _mainWindow.Show();

            var t = obj as Splash;
            t.Close();
        }
    }
}
