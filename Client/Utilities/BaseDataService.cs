using AKITE.Contingent.Helpers;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        public void Export(ExportMethod m)
        {
            switch (m)
            {
                case ExportMethod.JSON:
                {
                    var s = new SaveFileDialog();
                    s.Title = "Выберите место для экспорта";
                    s.Filter = "JSON file|*.json";
                    s.ShowDialog();

                    string data = JsonConvert.SerializeObject(this.Items);

                    if(s.FileName != "")  
                    {
                        System.IO.FileStream fs = null;

                        try
                        {
                            fs = (System.IO.FileStream)s.OpenFile();
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show($"Не удалось открыть файл для экспорта:\n{e.Message}", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                        }

                        byte[] bytes = new UTF8Encoding(true).GetBytes(data);

                        fs.Write(bytes, 0, bytes.Length);
                        fs.Close();

                        MessageBox.Show("Экспорт выполнен успешно!", "Экспорт выполнен!", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                } break;
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
