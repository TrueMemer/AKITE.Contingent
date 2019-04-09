using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AKITE.Contingent.Helpers;
using AKITE.Contingent.Models;

namespace AKITE.Contingent.Client.Services
{
    public class SpecialtyDataService : BaseBindable
    {
        private readonly HttpClient _http;

        private BindingList<Specialty> _specialties;
        public BindingList<Specialty> Specialties
        {
            get => _specialties;
            set
            {
                _specialties = value;
                OnPropertyChanged();
            }
        }

        public SpecialtyDataService()
        {
            _http = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:5001/")
            };
            _http.DefaultRequestHeaders.Accept.Clear();
            _http.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            _specialties = new BindingList<Specialty>();
        }

        public void AddSpecialty(Specialty specialty)
        {
            //if (specialty != null)
            //    Specialties.Add(specialty);
        }

        public async Task RefreshSpecialties()
        {
            var request = await _http.GetAsync("/api/specialties");

            if (!request.IsSuccessStatusCode)
            {
                Debug.WriteLine(request.StatusCode);
                Debug.WriteLine(await request.Content.ReadAsStringAsync());
                throw new System.Exception("Не удалось получить специальности! (возможно сервер недоступен)");
            }

            var resp = await request.Content.ReadAsAsync<BindingList<Specialty>>();

            _specialties = resp;
        }
    }
}
