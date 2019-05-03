using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AKITE.Contingent.Client.Services;
using AKITE.Contingent.Client.ViewModels;
using AKITE.Contingent.Helpers;
using AKITE.Contingent.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AKITE.Contingent.Client.Utilities
{
    public class DataCoordinator
    {
        public DataCoordinator()
        {
            SpecialtyDataService = new SpecialtyDataService();
            GroupDataService = new GroupDataService();
            StudentDataService = new StudentDataService();
        }

        public void LoadExamples()
        {
            var example = JObject.Parse(File.ReadAllText(@"exampleData.json", Encoding.UTF8));
            SpecialtyDataService.Items = JsonConvert.DeserializeObject<BindingList<Specialty>>(example["specialties"].ToString());
            GroupDataService.Items = JsonConvert.DeserializeObject<BindingList<Group>>(example["groups"].ToString());
        }

        public async Task Init()
        {
            Group.SetSpecialtyService(SpecialtyDataService);
            Student.SetService(GroupDataService);
            Group.SetStudentService(StudentDataService);
            if (SettingsManager.GetBool("LocalMode"))
            {
                LoadExamples();
                return;
            }

            try
            {
                await SpecialtyDataService.Refresh();
                await GroupDataService.Refresh();
                await StudentDataService.Refresh();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Не удалось подключиться к серверу!\n{e.Message}");
                Environment.Exit(-1);
            }
        }

        public SpecialtyDataService SpecialtyDataService;
        public GroupDataService GroupDataService;
        public StudentDataService StudentDataService;
    }
}
