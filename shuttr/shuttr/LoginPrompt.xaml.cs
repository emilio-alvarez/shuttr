﻿using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace shuttr
{
    /// <summary>
    /// Interaction logic for LoginPrompt.xaml
    /// </summary>
    public partial class LoginPrompt : Window
    {
        private MainWindow main;

        public LoginPrompt(MainWindow main)
        {
            InitializeComponent();

            this.main = main;
            main.ChangeFill(Visibility.Visible);
        }

        public void SetMessage(string messageToDisplay)
        {
            message.Text = messageToDisplay;
        }

        public void SetConfirmText(string confirmText)
        {
            confirmButton.Content = confirmText;
        }

        /// <summary>
        /// Interaction logic for closing popup prompt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close(object sender, RoutedEventArgs e)
        {
            main.ChangeFill(Visibility.Hidden);
            this.Close();
        }

        /// <summary>
        /// Interaction logic for clicking the logout confirmation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Confirm(object sender, RoutedEventArgs e)
        {
            main.contentControl.Content = new LoginPage(main);
            main.ChangeFill(Visibility.Hidden);
            this.Close();
        }

        /// <summary>
        /// Interaction logic for clicking the cancel confirmation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel(object sender, RoutedEventArgs e)
        {
            main.ChangeFill(Visibility.Hidden);
            this.Close();
        }
    }
}
