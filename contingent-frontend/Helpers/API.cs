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

        public static List<GroupNode> GetGroupNodes()
        {
            var response = http.GetStringAsync("/students").GetAwaiter().GetResult();

            JObject res = JObject.Parse(response);

            var list = JsonConvert.DeserializeObject<List<Student>>(res["data"].ToString());

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
    }
}
