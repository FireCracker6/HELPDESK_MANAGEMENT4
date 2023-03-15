using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using HelpDeskManagement_WPF_MVVM_APP.Models;
using HelpDeskManagement_WPF_MVVM_APP.Services;

namespace HelpDeskManagement_WPF_MVVM_APP.MVVM.Models.Views;

/// <summary>
/// Interaction logic for TicketsView.xaml
/// </summary>
public partial class TicketsView : UserControl
{
    #region Private Fields

    private readonly UserService _userService;

    #endregion

    #region Constructor

    public TicketsView()
    {
        InitializeComponent();
        _userService = new UserService();

        ShowAllUsers();
        ShowAllTickets();

        // Hook up the SelectionChanged event for the ticketDataGrid
        ticketDataGrid.SelectionChanged += TicketDataGrid_SelectionChanged;

        // Show data grids and labels
        myDataGrid.Visibility = Visibility.Visible;
        myTicketDataGrid.Visibility = Visibility.Visible;
        myDataGrid_HeaderLabel.Visibility = Visibility.Visible;
        myTicketDataGrid_HeaderLabel.Visibility = Visibility.Visible;

        // Hide navigation controls
        myFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
    }

    #endregion

    #region Private Methods



    // Load all users into myDataGrid
    private async Task ShowAllUsers()
    {
        var userService = new UserService();
        var users = await userService.GetAllAsync();
        myDataGrid.ItemsSource = users;
    }

    // Load all tickets into myTicketDataGrid
    private async Task ShowAllTickets()
    {
        var ticketService = new TicketService();
        var tickets = await ticketService.GetAllAsync();
        myTicketDataGrid.ItemsSource = null;
        myTicketDataGrid.ItemsSource = tickets;
    //    Debug.WriteLine($"Tickets found {tickets.Count()}");
    }

  
    public async Task UpdateTickets()
    {
        var ticketService = new TicketService();
        var tickets = await ticketService.GetAllAsync();
        myTicketDataGrid.ItemsSource = null;
        myTicketDataGrid.ItemsSource = tickets;

        var userService = new UserService();
        var users = await userService.GetAllAsync();
        myDataGrid.ItemsSource = users;
    }


    // Load tickets associated with the selected user
    private async Task ShowGridTickets(Guid selectedUserId)
    {
        var ticketService = new TicketService();
        var tickets = await ticketService.GetAllAsync();
        myTicketDataGrid.ItemsSource = null;
        myTicketDataGrid.ItemsSource = tickets.Where(t => t.UsersId == selectedUserId);
    //    Debug.WriteLine($"Tickets found {tickets.Count()}");
    }

    // Navigate to TicketDetails view with the selected ticket as a parameter
    private void TicketDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var frame = (Frame)Window.GetWindow(this).FindName("myFrame");

        var ticket = (Ticket)ticketDataGrid.SelectedItem;
        frame.Navigate(typeof(TicketDetails), ticket);
    }

    // Delete selected user from the database
    private async void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        var user = ((FrameworkElement)sender).DataContext as UsersEntity;

        if (user != null)
        {
            // Delete user from the database
            await _userService.DeleteAsync(user.Id);

            // Refresh myDataGrid and myTicketDataGrid
            await ShowAllUsers();
            await ShowAllTickets();
        }
    }

    // Select the ticket(s) in the myTicketsDataGrid when admin clicks the user in myDataGrid
    private void myDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
    {
        // Clear the selection in myTicketDataGrid
        myTicketDataGrid.SelectedItems.Clear();

        foreach (var item in myDataGrid.SelectedItems)
        {
            // Get the corresponding user for the selected item
            UsersEntity user = item as UsersEntity;

            if (user != null)
            {
                // Get the GUID ID of the selected user
                Guid selectedUserId = user.Id;
                Debug.WriteLine(selectedUserId);

                // Select tickets associated with the selected user
                ShowGridTickets(selectedUserId);
            }
        }
    }
 

    private async void MyDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
    {
        UsersEntity editedRow = (UsersEntity)((DataGrid)sender).SelectedItem;

        // Get the updated values from the edited cell
        string propertyName = e.Column.SortMemberPath;
        object editedValue = ((TextBox)e.EditingElement).Text;

        // Update the entity with the new values
        PropertyInfo property = editedRow.GetType().GetProperty(propertyName) ?? null!;
        if (property != null)
        {
            property.SetValue(editedRow, Convert.ChangeType(editedValue, property.PropertyType));
            await _userService.UpdateRecordAsync(editedRow, editedRow.Id);
        }


    }




    private void TicketDetail_Click(object sender, RoutedEventArgs e)
    {
        var selectedItem = myDataGrid.SelectedItem;
   
        if (selectedItem == null)
        {
            // Handle the case where no item is selected
            MessageBox.Show("Please select an item from the DataGrid.");
            return;
        }

        // Check if the selected item can be cast to a UsersEntity object
        if (selectedItem is UsersEntity userEntity)
        {
            // Get a reference to the Frame control that hosts this UserControl
            // var frame = (Frame)Window.GetWindow(this).FindName("myFrame");
            var frame = FindVisualChild<Frame>(this);
            Debug.WriteLine($"selected item: {userEntity.LastName}");
            // Navigate to the new UserControl with the UserEntity object
            frame.NavigationService.Navigate(new TicketDetails(userEntity.Id));
            myDataGrid.Visibility = Visibility.Collapsed;
            myTicketDataGrid.Visibility = Visibility.Collapsed;
            myDataGrid_HeaderLabel.Visibility = Visibility.Collapsed;
            myTicketDataGrid_HeaderLabel.Visibility = Visibility.Collapsed;
        }
        else
        {
            // Handle the case where the selected item is not of type UsersEntity
            MessageBox.Show("Please select a row from the DataGrid.");
        }

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
    #endregion
}