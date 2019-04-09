using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using AKITE.Contingent.Helpers;

namespace AKITE.Contingent.Client.Utilities
{
    public class Navigator : BaseBindable
    {
        private Page _prevPage;

        private Page _currentPage;
        public Page CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }

        public void NavigateTo(Page p)
        {
            _prevPage = CurrentPage;
            CurrentPage = p;
        }

        public void GoBack()
        {
            if (_prevPage != null) NavigateTo(_prevPage);
        }
    }
}
