using contingent_frontend.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace contingent_frontend.Helpers
{
    static class API
    {
        private static HttpClient http;

        static API()
        {
            http = new HttpClient();
            http.BaseAddress = new Uri("http://localhost:8080");
        }

        public static async Task<List<GroupNode>> GetGroupNodesAsync()
        {
            string response;

            try
            {
                response = await http.GetStringAsync("/students");
            }
            catch (Exception)
            {
                return null;
            }

            JObject res = JObject.Parse(response);
            var data = res["data"].ToString();
            var list = JsonConvert.DeserializeObject<List<Student>>(data);

            List<GroupNode> ret = new List<GroupNode>();
            ret.Add(new GroupNode { Name = "Абитуриенты", Students = new BindingList<Student>() });

            foreach (var s in list)
            {
                if (s.GroupName == null)
                {
                    ret.Where(g => g.Name == "Абитуриенты").Single().Students.Add(s);
                }
                else if (!ret.Any(g => g.Name == s.GroupName))
                {
                    ret.Add(new GroupNode { Name = s.GroupName, Students = new BindingList<Student> { s } });
                } else
                {
                    ret.Where(g => g.Name == s.GroupName).Single().Students.Add(s);
                }
            }

            return ret;
        }

        public static async void DeleteStudentByID(int id)
        {
            HttpResponseMessage response;
            try
            {
                response = await http.DeleteAsync($"/students/{id.ToString()}");
            }
            catch (Exception)
            {
                return;
            }
        }

        public static async Task<int> AddStudent(Student s)
        {
            var content = new StringContent(JsonConvert.SerializeObject(s), Encoding.UTF8, "application/json");
            var response = await http.PostAsync("/students/new", content);

            Console.WriteLine(await response.Content.ReadAsStringAsync());

            if (response.IsSuccessStatusCode) return int.Parse(JObject.Parse(await response.Content.ReadAsStringAsync())["data"]["id"].ToString());
            else return -1;
        }
    }
}
