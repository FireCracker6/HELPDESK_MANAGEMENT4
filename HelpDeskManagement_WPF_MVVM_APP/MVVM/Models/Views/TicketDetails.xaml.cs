﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using CommunityToolkit.Mvvm.Input;
using HelpDeskManagement_WPF_MVVM_APP.Contexts;
using HelpDeskManagement_WPF_MVVM_APP.Models;
using HelpDeskManagement_WPF_MVVM_APP.MVVM.Models.ViewModels;
using HelpDeskManagement_WPF_MVVM_APP.Services;

namespace HelpDeskManagement_WPF_MVVM_APP.MVVM.Models.Views
{
    /// <summary>
    /// Interaction logic for TicketDetails.xaml
    /// </summary>
    public partial class TicketDetails : UserControl
    {

        private readonly UserService _userService;
        private readonly Guid _userId;



        public TicketDetails(Guid userId)
        {
            InitializeComponent();

            DataContext = new TicketDetailModel(userId);

            _userService = new UserService();
            _userId = userId;
            ShowUser(userId);
          
            contentControl.Content = myFrame;
        }
      

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var ticketDetailModel = (TicketDetailModel)DataContext;
            ticketDetailModel.SelectedTicket = (Ticket)ticketDataGrid.SelectedItem;

            var publicTicket = new Ticket()
            {
                Id = ticketDetailModel.SelectedTicket.Id,
                UsersId = ticketDetailModel.SelectedTicket.UsersId,
                Title = ticketDetailModel.SelectedTicket.Title,
                Description = ticketDetailModel.SelectedTicket.Description,
                TicketCategory = ticketDetailModel.SelectedTicket.TicketCategory,
                CreatedAt = ticketDetailModel.SelectedTicket.CreatedAt,
                LastUpdatedAt = ticketDetailModel.SelectedTicket.LastUpdatedAt,
                ClosedAt = ticketDetailModel.SelectedTicket.ClosedAt,
                Priorities = new List<TicketPriorities>(),
                Statuses = new List<TicketStatuses>(),
                Comments = new List<TicketComments>()
            };

            foreach (var priority in ticketDetailModel.SelectedTicket.Priorities)
            {
                publicTicket.Priorities.Add(new TicketPriorities
                {
                    Id = priority.Id,
                    PriorityName = priority.PriorityName
                });
            }

            foreach (var status in ticketDetailModel.SelectedTicket.Statuses)
            {
                publicTicket.Statuses.Add(new TicketStatuses
                {
                    Id = status.Id,
                    TicketId = status.TicketId,
                    StatusName = status.StatusName
                });
            }

            foreach (var comment in ticketDetailModel.SelectedTicket.Comments)
            {
                publicTicket.Comments.Add(new TicketComments
                {
                    Id = comment.Id,
                    CommentsText = comment.CommentsText,
               
                });
                Debug.WriteLine(comment.CommentsText);
            }
      

            await ticketDetailModel.SaveTicket(publicTicket);
        }




        private void ShowDefaultView()
        {
            // Get a reference to the Frame control that hosts this UserControl
            var frame = (Frame)Window.GetWindow(this).FindName("myFrame");

            // Navigate to the default view
            frame.Navigate(typeof(TicketsView));
        }

        private async Task ShowUser(Guid userId)
        {
            var item = await _userService.GetAsync(x => x.Id == userId);
            if (item != null)
            {
                Debug.WriteLine(item.FirstName);
                Debug.WriteLine("userId = " + userId);

                // Set the ItemsSource property of the DataGrid control
                var ticketService = new TicketService();
                var tickets = await ticketService.GetAsync(userId);

                Debug.WriteLine($"Number of tickets retrieved for user with id {userId}: {tickets}");

             
                ticketDataGrid.ItemsSource = tickets;
            }
            else
            {
                // Handle the case where the user with the specified ID was not found.
                // This could mean showing an error message or redirecting the user to a different page.
            }
        }
        private void MyDetailsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var ticketDetailModel = (TicketDetailModel)DataContext;
            ticketDetailModel.SelectedTicket = (Ticket)ticketDataGrid.SelectedItem;
        }
        private void TicketDetails_Loaded(object sender, RoutedEventArgs e)
        {
            myFrame.Content = new TicketDetailModel(_userId);
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Get a reference to the parent Frame control
            Frame parentFrame = null!;
            DependencyObject parent = VisualTreeHelper.GetParent(this);
            while (parent != null && parentFrame == null)
            {
                parentFrame = parent as Frame;
                parent = VisualTreeHelper.GetParent(parent);
            }

            if (parentFrame != null && parentFrame.NavigationService.CanGoBack)
            {
                // If there is, navigate back to the previous page
                parentFrame.NavigationService.GoBack();
            }
            else
            {
                // If there isn't, show an error message or do nothing
                MessageBox.Show("There is no previous page to navigate back to.");
            }
        }



        public static T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
        {
            // Get parent item
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            // We’ve reached the end of the tree
            if (parentObject == null) return null!;

            // Check if the parent matches the type we’re looking for
            if (parentObject is T parent) return parent;

            // Use recursion to proceed with next level
            return FindVisualParent<T>(parentObject);
        }





    }

}
