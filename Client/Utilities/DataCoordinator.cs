using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AKITE.Contingent.Client.Services;
using AKITE.Contingent.Models;

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

        public async Task Init(string messageIndicator)
        {
            try
            {
                messageIndicator = "Загрузка специальностей...";
                await SpecialtyDataService.RefreshSpecialties();
                Group.SetSpecialtyService(SpecialtyDataService);
                messageIndicator = "Загрузка групп...";
                await GroupDataService.RefreshGroups();
                messageIndicator = "Загрузка студентов";
                await StudentDataService.RefreshStudents();
                Student.SetService(GroupDataService);
                Group.SetStudentService(StudentDataService);
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
