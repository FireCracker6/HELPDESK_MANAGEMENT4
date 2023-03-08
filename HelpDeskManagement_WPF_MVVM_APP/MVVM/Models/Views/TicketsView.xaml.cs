using System;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using HelpDeskManagement_WPF_MVVM_APP.Services;

namespace HelpDeskManagement_WPF_MVVM_APP.MVVM.Models.Views
{
    /// <summary>
    /// Interaction logic for TicketsView.xaml
    /// </summary>
    public partial class TicketsView : UserControl
    {
      
        private readonly UserService _userService;


        public TicketsView()
        {
            InitializeComponent();
            _userService = new UserService();

                ShowAllUsers();
                ShowAllTickets();
           
        }
        private async Task ShowAllUsers()
        {
            var userService = new UserService(); 
            var users = await userService.GetAllAsync();
            myDataGrid.ItemsSource = users;
        }
        private async Task ShowAllTickets()
        {
            var ticketService = new TicketUserService();
            var tickets = await ticketService.GetAllAsync();
            myTicketDataGrid.ItemsSource = tickets;
        }
       

        
        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
           
            var user = ((FrameworkElement)sender).DataContext as UsersEntity;

            if (user != null)
            {
                await _userService.DeleteAsync(user.Id);

                await ShowAllUsers();
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

                // Navigate to the new UserControl with the UserEntity object
                frame.NavigationService.Navigate(new TicketDetails(userEntity.Id));
                myDataGrid.Visibility = Visibility.Collapsed;
                myTicketDataGrid.Visibility = Visibility.Collapsed;
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
        private void ShowDefaultView()
        {
            // Get a reference to the Frame control that hosts this UserControl
            var frame = (Frame)Window.GetWindow(this).FindName("myFrame");

            // Navigate to the default view
            frame.Navigate(typeof(TicketsView));
        }






    }
}
