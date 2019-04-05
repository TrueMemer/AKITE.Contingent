using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AKITE.Contingent.Helpers;
using AKITE.Contingent.Models;
using Newtonsoft.Json;

namespace AKITE.Contingent.Client.Services
{
    public class StudentDataService : BaseBindable
    {
        private readonly HttpClient Http;

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
            Http = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44378/")
            };
            Http.DefaultRequestHeaders.Accept.Clear();
            Http.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            Students = new BindingList<Student>();
        }

        public async Task AddStudent(Student student)
        {
            try
            {
                var request = await Http.PostAsJsonAsync("api/students", student);

                if (!request.IsSuccessStatusCode)
                {
                    MessageBox.Show("Не удалось добавить студента (сервер недоступен?)!");
                    Debug.WriteLine(await request.Content.ReadAsStreamAsync());
                    return;
                }

                Students.Add(await request.Content.ReadAsAsync<Student>());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task DeleteStudent(Student student)
        {
            var request = await Http.DeleteAsync($"api/students/{student.Id}");

            if (!request.IsSuccessStatusCode)
            {
                MessageBox.Show("Не удалось удалить студента (возможно десинхронизация)!");
                return;
            }

            Students.Remove(student);
        }

        public async Task UpdateStudent(int id, Student student)
        {
            var request = await Http.PutAsJsonAsync($"api/students/{id}", student);

            if (!request.IsSuccessStatusCode)
            {
                MessageBox.Show("Не удалось обновить студента (сервер недоступен?)!");
                Debug.WriteLine(request.StatusCode);
                Debug.WriteLine(await request.Content.ReadAsStreamAsync());
                return;
            }

            var old = Students.SingleOrDefault(s => s.Id == id);
            old = await request.Content.ReadAsAsync<Student>();
        }

        public IEnumerable<Student> GetStudents()
        {
            return Students;
        }

        public async Task RefreshStudents()
        {
            try
            {
                var request = await Http.GetAsync("api/students");

                var response = await request.Content.ReadAsAsync<BindingList<Student>>();

                Students = response;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace);
                throw;
            }
        }
    }
}