using System;
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
using System.Xml.Linq;
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
        private readonly TicketService _ticketService;
        private readonly Guid _userId;
        private DataGrid _ticketDataGrid;


        public TicketDetails(Guid userId)
        {
            InitializeComponent();

            DataContext = new TicketDetailModel(userId);
            

            _ticketService = new TicketService();
        
            _userService = new UserService();
            _userId = userId;
            ShowUser(userId);
         
            _ticketDataGrid = ticketDataGrid; // Assign the ticketDataGrid to the field
          
            myFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
         
        }


        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Iam clicked!");
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


            await ticketDetailModel.SaveTicket(publicTicket, _ticketDataGrid);
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

                var ticketsView = FindVisualChild<TicketsView>(Application.Current.MainWindow);

                // Set the visibility of the grids to Visible
                ticketsView.myDataGrid.Visibility = Visibility.Collapsed;
                ticketsView.myTicketDataGrid.Visibility = Visibility.Collapsed;
               

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
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var ticketDetailModel = (TicketDetailModel)DataContext;
            ticketDetailModel.SelectedTicket = (Ticket)ticketDataGrid.SelectedItem;
        }
     
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var frame = FindVisualChild<Frame>(this);
            // Navigate back to the TicketsView
          
            // Navigate to the new UserControl with the UserEntity object
            frame.NavigationService.Navigate(new TicketsView());

            ticketDataGrid.Visibility = Visibility.Collapsed;
            _tabControl.Visibility = Visibility.Collapsed;

            // Set the visibility of the grids to Visible
          
            var ticketsView = FindVisualChild<TicketsView>(Application.Current.MainWindow);

            // Set the visibility of the grids to Visible
            ticketsView.myDataGrid.Visibility = Visibility.Visible;
            ticketsView.myTicketDataGrid.Visibility = Visibility.Visible;

        }



        public static T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T)
                    return (T)child;
                else
                {
                    var childOfChild = FindVisualChild<T>(child!);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null!;
        }









    }

}
