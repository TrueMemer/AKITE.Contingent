using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKITE.Contingent.Client
{
    public interface IDataService<T>
    {
        BindingList<T> Items { get; }

        Task Add(T item);
        Task Delete(T item);
        Task DeleteById(int id);
        Task Update(int id, T item);
        Task Refresh();
    }
}
