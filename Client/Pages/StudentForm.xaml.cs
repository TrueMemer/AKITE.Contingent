﻿using AKITE.Contingent.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AKITE.Contingent.Client.Services;
using AKITE.Contingent.Models;

namespace AKITE.Contingent.Client.Pages
{
    public partial class StudentForm : Page
    {
        public StudentForm(Student SelectedStudent, StudentDataService studentDataService)
        {
            InitializeComponent();

            DataContext = new StudentFormViewModel(SelectedStudent, studentDataService);
        }
    }
}
