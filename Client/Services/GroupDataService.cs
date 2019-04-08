using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;
using AKITE.Contingent.Helpers;
using AKITE.Contingent.Models;

namespace AKITE.Contingent.Client.Services
{
    public class GroupDataService : BaseBindable
    {
        private readonly HttpClient _http;

        private BindingList<Group> _groups = new BindingList<Group>();
        public BindingList<Group> Groups
        {
            get => _groups;
            set
            {
                _groups = value;
                OnPropertyChanged();
            }
        }

        public GroupDataService()
        {
            _http = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:5001/")
            };
            _http.DefaultRequestHeaders.Accept.Clear();
            _http.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task AddGroup(Group group)
        {
            var request = await _http.PostAsJsonAsync("api/groups", group);

            if (!request.IsSuccessStatusCode)
            {
                MessageBox.Show("Не удалось добавить группу (сервер недоступен?)");
                Debug.WriteLine(request.StatusCode);
                Debug.WriteLine(await request.Content.ReadAsStringAsync());
                return;
            }

            Groups.Add(await request.Content.ReadAsAsync<Group>());
        }

        public async Task RefreshGroups()
        {
            var request = await _http.GetAsync("api/groups");

            if (!request.IsSuccessStatusCode)
            {
                Debug.WriteLine(request.StatusCode);
                Debug.WriteLine(await request.Content.ReadAsStringAsync());
                throw new System.Exception("Не удалось получить группы! (возможно сервер недоступен)");
            }

            var response = await request.Content.ReadAsAsync<BindingList<Group>>();

            Groups = response;
        }
    }
}
