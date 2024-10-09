﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_Dusza.Models;

namespace WPF_Dusza.Utils
{
    public static class WindowUtils
    {
        public static void ShowOtherWindow(Window main, Window other)
        {
            main.Hide();
            if(other.ShowDialog() is false) main.Show();
        }
        public static void DisplayErrorMessage(string message) 
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public static void LogoutUser(User? user, Window window)
        {
            user = null;
            window.Close();
        }
    }
}
