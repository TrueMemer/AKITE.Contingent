using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;
using AKITE.Contingent.Client.Utilities;
using AKITE.Contingent.Helpers;
using AKITE.Contingent.Models;

namespace AKITE.Contingent.Client.Services
{
    public class GroupDataService : BaseDataService<Group>
    {
        private readonly HttpClient _http;

        public GroupDataService() : base()
        {
            if (SettingsManager.GetBool("LocalMode")) 
            {
                Items.Add(new Group { Id = 0 });
                return;
            }

            _http = new HttpClient
            {
                BaseAddress = new Uri(SettingsManager.GetString("ApiBase"))
            };
            _http.DefaultRequestHeaders.Accept.Clear();
            _http.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public override async Task Add(Group group)
        {
            if (SettingsManager.GetBool("LocalMode")) 
            {
                await base.Add(group);
                return;
            }

            var request = await _http.PostAsJsonAsync("api/groups", group);

            if (!request.IsSuccessStatusCode)
            {
                MessageBox.Show("Не удалось добавить группу (сервер недоступен?)");
                Debug.WriteLine(request.StatusCode);
                Debug.WriteLine(await request.Content.ReadAsStringAsync());
                return;
            }

            Items.Add(await request.Content.ReadAsAsync<Group>());
        }

        public override async Task Refresh()
        {
            if (SettingsManager.GetBool("LocalMode")) 
            {
                await base.Refresh();
                return;
            }

            var request = await _http.GetAsync("api/groups");

            if (!request.IsSuccessStatusCode)
            {
                Debug.WriteLine(request.StatusCode);
                Debug.WriteLine(await request.Content.ReadAsStringAsync());
                throw new System.Exception("Не удалось получить группы! (возможно сервер недоступен)");
            }

            var response = await request.Content.ReadAsAsync<BindingList<Group>>();

            Items = response;
        }
    }
}
