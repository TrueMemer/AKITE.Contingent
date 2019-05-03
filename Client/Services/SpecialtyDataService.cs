using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AKITE.Contingent.Client.Utilities;
using AKITE.Contingent.Helpers;
using AKITE.Contingent.Models;

namespace AKITE.Contingent.Client.Services
{
    public class SpecialtyDataService : BaseDataService<Specialty>
    {
        private readonly HttpClient _http;

        public SpecialtyDataService() : base()
        {
            if (SettingsManager.GetBool("LocalMode"))
            {
                Items.Add(new Specialty { Id = 0, Name = "Абитуриенты" });
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

        public override async Task Refresh()
        {
            if (SettingsManager.GetBool("LocalMode")) 
            {
                await base.Refresh();
                return;
            }

            var request = await _http.GetAsync("/api/specialties");

            if (!request.IsSuccessStatusCode)
            {
                Debug.WriteLine(request.StatusCode);
                Debug.WriteLine(await request.Content.ReadAsStringAsync());
                throw new System.Exception("Не удалось получить специальности! (возможно сервер недоступен)");
            }

            var resp = await request.Content.ReadAsAsync<BindingList<Specialty>>();

            Items = resp;
        }
    }
}
