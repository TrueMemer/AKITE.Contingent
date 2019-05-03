using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;
using AKITE.Contingent.Client.Utilities;
using AKITE.Contingent.Helpers;
using AKITE.Contingent.Models;

namespace AKITE.Contingent.Client.Services
{
    public class StudentDataService : BaseDataService<Student>
    {
        private readonly HttpClient _http;

        public StudentDataService() : base()
        {
            if (SettingsManager.GetBool("LocalMode")) return;
            _http = new HttpClient
            {
                BaseAddress = new Uri(SettingsManager.GetString("ApiBase"))
            };
            _http.DefaultRequestHeaders.Accept.Clear();
            _http.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public override async Task Add(Student student)
        {
            if (SettingsManager.GetBool("LocalMode")) 
            {
                await base.Add(student);
                return;
            }

            var request = await _http.PostAsJsonAsync("api/students", student);

            if (!request.IsSuccessStatusCode)
            {
                MessageBox.Show("Не удалось добавить студента (сервер недоступен?)!");
                Debug.WriteLine(await request.Content.ReadAsStreamAsync());
                return;
            }

            Items.Add(await request.Content.ReadAsAsync<Student>());
        }

        public override Task DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public override async Task Delete(Student student)
        {
            if (SettingsManager.GetBool("LocalMode")) 
            {
                await base.Delete(student);
                return;
            }

            var request = await _http.DeleteAsync($"api/students/{student.Id}");

            if (!request.IsSuccessStatusCode)
            {
                MessageBox.Show("Не удалось удалить студента (возможно десинхронизация)!");
                return;
            }

            Items.Remove(student);
        }

        public override async Task Update(int id, Student student)
        {
            if (SettingsManager.GetBool("LocalMode")) 
            {
                var index = Items.IndexOf(Items.Where(s => s.Id == id).SingleOrDefault());
                Items[index] = student;
                return;
            }

            var request = await _http.PutAsJsonAsync($"api/students/{id}", student);

            if (!request.IsSuccessStatusCode)
            {
                MessageBox.Show("Не удалось обновить студента (сервер недоступен?)!");
                Debug.WriteLine(request.StatusCode);
                Debug.WriteLine(await request.Content.ReadAsStringAsync());
                return;
            }

            var old = Items.SingleOrDefault(s => s.Id == id);
            old = await request.Content.ReadAsAsync<Student>();
        }

        public async Task Transfer(int id, int groupid)
        {
            if (SettingsManager.GetBool("LocalMode")) 
            {
                var index = Items.IndexOf(Items.Where(s => s.Id == id).SingleOrDefault());
                Items[index].GroupIndex = groupid;
                return;
            }

            throw new NotImplementedException();
        }

        public override async Task Refresh()
        {
            if (SettingsManager.GetBool("LocalMode")) 
            {
                await base.Refresh();
                return;
            }

            var request = await _http.GetAsync("api/students");

            if (!request.IsSuccessStatusCode)
            {
                Debug.WriteLine(request.StatusCode);
                Debug.WriteLine(await request.Content.ReadAsStringAsync());
                throw new System.Exception("Не удалось получить студентов! (возможно сервер недоступен)");
            }

            var response = await request.Content.ReadAsAsync<BindingList<Student>>();

            Items = response;
        }
    }
}