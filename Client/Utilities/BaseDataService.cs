using AKITE.Contingent.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKITE.Contingent.Client.Utilities
{
    public class BaseDataService<T> : BaseBindable, IDataService<T>
    {
        private BindingList<T> _items = new BindingList<T>();
        public BindingList<T> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        public virtual Task Add(T item)
        {
            Items.Add(item);
            return Task.CompletedTask;
        }

        public virtual Task Delete(T item)
        {
            Items.Remove(item);
            return Task.CompletedTask;
        }

        public virtual Task DeleteById(int id)
        {
            return Task.CompletedTask;
        }

        public virtual Task Refresh()
        {
            return Task.CompletedTask;
        }

        public virtual Task Update(int id, T item)
        {
            return Task.CompletedTask;
        }
    }
}
