using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;
using AKITE.Contingent.Helpers;
using AKITE.Contingent.Models;

namespace AKITE.Contingent.Client.Services
{
    public class StudentDataService : BaseBindable
    {
        private readonly HttpClient _http;

        private BindingList<Student> _students;
        public BindingList<Student> Students
        {
            get => _students;
            set
            {
                _students = value;
                OnPropertyChanged();
            }
        }

        public StudentDataService()
        {
            _http = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:5001/")
            };
            _http.DefaultRequestHeaders.Accept.Clear();
            _http.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            Students = new BindingList<Student>();
        }

        public async Task AddStudent(Student student)
        {
            var request = await _http.PostAsJsonAsync("api/students", student);

            if (!request.IsSuccessStatusCode)
            {
                MessageBox.Show("Не удалось добавить студента (сервер недоступен?)!");
                Debug.WriteLine(await request.Content.ReadAsStreamAsync());
                return;
            }

            Students.Add(await request.Content.ReadAsAsync<Student>());
        }

        public async Task DeleteStudent(Student student)
        {
            var request = await _http.DeleteAsync($"api/students/{student.Id}");

            if (!request.IsSuccessStatusCode)
            {
                MessageBox.Show("Не удалось удалить студента (возможно десинхронизация)!");
                return;
            }

            Students.Remove(student);
        }

        public async Task UpdateStudent(int id, Student student)
        {
            var request = await _http.PutAsJsonAsync($"api/students/{id}", student);

            if (!request.IsSuccessStatusCode)
            {
                MessageBox.Show("Не удалось обновить студента (сервер недоступен?)!");
                Debug.WriteLine(request.StatusCode);
                Debug.WriteLine(await request.Content.ReadAsStringAsync());
                return;
            }

            var old = Students.SingleOrDefault(s => s.Id == id);
            old = await request.Content.ReadAsAsync<Student>();
        }

        public async Task RefreshStudents()
        {
            var request = await _http.GetAsync("api/students");

            if (!request.IsSuccessStatusCode)
            {
                Debug.WriteLine(request.StatusCode);
                Debug.WriteLine(await request.Content.ReadAsStringAsync());
                throw new System.Exception("Не удалось получить студентов! (возможно сервер недоступен)");
            }

            var response = await request.Content.ReadAsAsync<BindingList<Student>>();

            Students = response;
        }
    }
}