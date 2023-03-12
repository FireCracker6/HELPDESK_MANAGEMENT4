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

            _ticketService = new TicketService();
            _userService = new UserService();
            _userId = userId;
            ShowUser(userId);

            myFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;

            var viewModel = new TicketDetailModel(userId);
            DataContext = viewModel;
            _ticketDataGrid = ticketDataGrid;

            // Set the Command of the saveButton to the SaveTicketCommand of the viewModel
            saveButton.Command = viewModel.SaveTicketCommand;

            // Set the _ticketDataGrid property of the viewModel
            viewModel.TicketDataGrid = _ticketDataGrid;

            
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

                // Get the parent window of the current view
                var mainWindow = Application.Current.MainWindow;

                // Find the TicketsView control inside the main window
                var ticketsView = FindVisualChild<TicketsView>(mainWindow);

                // Set the visibility of the myDataGrid and myTicketDataGrid to Collapsed
                ticketsView.myDataGrid.Visibility = Visibility.Collapsed;
                ticketsView.myTicketDataGrid.Visibility = Visibility.Collapsed;
                Debug.WriteLine(item.FirstName);
                Debug.WriteLine("userId = " + userId);

                var ticketDetailModel = (TicketDetailModel)DataContext;
                await ticketDetailModel.LoadTicketsAsync(userId);


            }

            else
            {
                MessageBox.Show($"No ticket associated with {item?.Id}");
            }
        }


        private void MyDetailsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var ticketDetailModel = (TicketDetailModel)DataContext;
            ticketDetailModel.SelectedTicket = (Ticket)ticketDataGrid.SelectedItem;
            var ticketsView = FindVisualChild<TicketsView>(Application.Current.MainWindow);

            // Set the visibility of the grids to Visible
            ticketsView.myDataGrid.Visibility = Visibility.Collapsed;
            ticketsView.myTicketDataGrid.Visibility = Visibility.Collapsed;
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var frame = FindVisualChild<Frame>(this);
            // Navigate back to the TicketsView

            // Navigate to the new UserControl with the UserEntity object
            frame.NavigationService.Navigate(new TicketsView());

            ticketDataGrid.Visibility = Visibility.Collapsed;
            _tabControl.Visibility = Visibility.Collapsed;

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

