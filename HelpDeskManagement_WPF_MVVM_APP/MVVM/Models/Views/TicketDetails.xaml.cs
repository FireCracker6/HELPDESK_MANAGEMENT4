using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using HelpDeskManagement_WPF_MVVM_APP.Models;
using HelpDeskManagement_WPF_MVVM_APP.MVVM.Models.ViewModels;
using HelpDeskManagement_WPF_MVVM_APP.Services;

namespace HelpDeskManagement_WPF_MVVM_APP.MVVM.Models.Views;

/// <summary>
/// Interaction logic for TicketDetails.xaml
/// </summary>
public partial class TicketDetails : UserControl
{
    #region Private Fields

    private readonly UserService _userService;
    private readonly TicketService _ticketService;
    private readonly Guid _userId;
    private DataGrid _ticketDataGrid;

    #endregion

    #region Constructor

    public TicketDetails(Guid userId)
    {
        InitializeComponent();
        DataContext = new TicketDetailModel();
        // Instantiate services and fields
        _ticketService = new TicketService();
        _userService = new UserService();
        _userId = userId;
        ShowUser(userId);

        myFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;

        // Set the data context for the view
        var viewModel = new TicketDetailModel(userId);

      //  DataContext = viewModel;
        _ticketDataGrid = ticketDataGrid;
        Ticket selectedTicket = viewModel.SelectedTicket;

        // Set the Command of the saveButton to the SaveTicketCommand of the viewModel
        saveButton.Command = viewModel.SaveTicketCommand;

        // Set the _ticketDataGrid property of the viewModel
        viewModel.TicketDataGrid = _ticketDataGrid;
    }

    #endregion

    #region Private Methods

    // Show user information associated with the selected ticket
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
            ticketsView.myDataGrid_HeaderLabel.Visibility = Visibility.Collapsed;
            ticketsView.myTicketDataGrid_HeaderLabel.Visibility = Visibility.Collapsed;

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

    // Set the selected ticket in the view model when the user selects a row in the data grid
    private void MyDetailsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

        var ticketDetailModel = (TicketDetailModel)DataContext;
        ticketDetailModel.SelectedTicket = (Ticket)ticketDataGrid.SelectedItem;
        var ticketsView = FindVisualChild<TicketsView>(Application.Current.MainWindow);

        // Set the visibility of the grids to Visible
        ticketsView.myDataGrid.Visibility = Visibility.Collapsed;
        ticketsView.myTicketDataGrid.Visibility = Visibility.Collapsed;
    }
    private async void TicketDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
    {
        // Cast the DataContext to the TicketDetailModel
        TicketDetailModel viewModel = (TicketDetailModel)DataContext;

        // Check if SelectedTicket is null and load data if it is
        if (viewModel.SelectedTicket == null)
        {
            await viewModel.LoadTicketsAsync(viewModel.SelectedUserId);
        }

        // Set the SelectedTicket property to the selected item
        viewModel.SelectedTicket = (Ticket)ticketDataGrid.SelectedItem;
        Debug.WriteLine($"{viewModel.SelectedTicket.Email}");
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

#endregion

