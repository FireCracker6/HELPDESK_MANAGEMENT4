   M S B A M L   `   `   `  ????Z  VHelpDeskManagement_WPF_MVVM_APP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullDclr-namespace:HelpDeskManagement_WPF_MVVM_APP.MVVM.Models.ViewModels6HelpDeskManagement_WPF_MVVM_APP.MVVM.Models.ViewModels  5      u?clr-namespace:HelpDeskManagement_WPF_MVVM_APP.MVVM.Models.Views1HelpDeskManagement_WPF_MVVM_APP.MVVM.Models.Views  5	      u?clr-namespace:HelpDeskManagement_WPF_MVVM_APP.MVVM.Models.Views1HelpDeskManagement_WPF_MVVM_APP.MVVM.Models.Views  5
      C    =HelpDeskManagement_WPF_MVVM_APP.MVVM.Models.Views.TicketsView   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      [
viewmodelsDclr-namespace:HelpDeskManagement_WPF_MVVM_APP.MVVM.Models.ViewModels     5      Qviews?clr-namespace:HelpDeskManagement_WPF_MVVM_APP.MVVM.Models.Views     5	      Qlocal?clr-namespace:HelpDeskManagement_WPF_MVVM_APP.MVVM.Models.Views     5
      .??5      ? $??40q?6     ? RowDefinitions  5   
   ?? 5      $
??auto ?6   ?? 5      ?? 5      $
??auto ?6   .+?5   
   ?? $??0??6   $??22(?6<   $??Medium"?6J   $??0 0 0 20q?6^   ??6!   ?? *	PageTitle+6^   ? 5   
   $??1??6     5      ?? 5      $
??auto ?6    ?? 5      $??* ?6    .+?5#      ?   5$      ?? 5%      ?? 5&      ?? 5'      ?? 5(      ?? 5)      ?? 5*      ?? 5+      .+?50      ?? -   5         ??Name$ myDataGrid_HeaderLabel??50      $??Users??68   $??0,0,0,0q?6H   ???#6a839d$
??Bold"?6n   $??16(?6?   $??5q?6?   $??0??6?   ???#c3d8eb6?   &   System.Windows.Controls.DataGrid  51      -   5          Name$ 
myDataGrid??51      $??1??6/   $??20q?6<      AutoGenerateColumns$ False??6I      CanUserAddRows$ false??6?   $??0q?6?      RowHeaderWidth$ 0Q?6?   $??Stretch=?6     GridLinesVisibility$ 
Horizontal=?6#     HorizontalGridLinesBrush
 ????? ? 	Resources 52      	 ?? 
TargetType
 ?? Property ?? Value*  $System.Windows.Controls.DataGridCell ?? Triggers P? Property P? Value=  7System.Windows.Controls.Primitives.DataGridColumnHeader??%?  '        '  $    '  ?    ?? 53      "	  6    .?54      ?? 
 ? ?? ?#e6e8f26;   ?? 55      
 ? ??
 ?   ?6;   ?? 56      
 ? ??$ 0q?6@   ?? 57      
 ? ??
 ???? 6<   ?? 58       
 ?@ AlternatingRowBackground ?#e6e8f26I   ?? 5:      
 ? ??$ 10q?68   5;      ?? 5=      "	  6    .?5>      ?? 
 ? ??$ 7q?68   ?? 5?      
 ? ??$ 0q?6@   ?? 5@      
 ? ??
 ???? 6<   ?? 5A      
 ? ??
 ?   ?6;   ?? 5B      
 ? ??
 ???? 6;    5D      P? 5E   "    ?@ 
IsSelected$
 True??6@   . ?5F   &   ?? 
 ? ?? ?#80acdc6C   ?? 5G   &   
 ? ??
 ?????6C   5I   #   5K      ?? 5L      "	  6    .?5M      ?? 
 ? ??$
 Bold"?6;   ?? 5N      
 ? ?? ?#80acdc6;   ?? 5O      
 ? ??$ 7q?68   ?? 5P      
 ? ??$ Center=?6D   ?? 5Q      
 ? ??$ Stretch=?6D   ?? 5R      
 ? ??$
 Left=?6K   5S      5T         Columns 5U      0  *System.Windows.Controls.DataGridTextColumn  5V      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$	 260 6-      Header$ User ID??69      
IsReadOnly$
 True??6a   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6J   ?? *Id+6a     5W      $ * 6-   $ 
First Name??67    6K   ?? *	FirstName+  5X      $ * 6-   $ 	Last Name??67    6J   ?? *
LastName+  5Y      $ * 6-   $ Email-Address??67    6N   ?? *Email+  5Z      $	 150 6-   $ Phone Number??69   $
 True??6o    6O   ?? *PhoneNumber+6o     5[      $ * 6-   $ 
Created At??67   $
 True??6i    6K   ?? *	CreatedAt+6i   4  .System.Windows.Controls.DataGridTemplateColumn  5\      $ Remove User??61      CellTemplate 5]      ?? 5^   "   .R?5_   &   ?? -   5        $??1q?5_   -   $??5q?69   $	??100??6_   ???#dbc9da???#a7abb9$??1q?6?   .??6?   Remove Client6?   5`   #   5b        5c      $ Show Details??61    5d      ?? 5e   "   .R?5f   &   ?? -   5        $??2q?5f   -   $??5q?69   $	??100??6D   $??View Details??6P   ???#d9d6e3???#a7abb9$??1q?6?   5g   #   5i      5k      ?? 5o      -   5        $" myTicketDataGrid_HeaderLabel??5o      $??	Ticket(s)??6>   $??0,0,0,0q?6R   $
??Bold"?6d   $??16(?6v   $??5q?6?   $??4??6?   ???#6a839d???#c3d8eb6?     5p      -   5        $ myTicketDataGrid??5p      $??5??65   $??20q?6B   $ False??6O   $ false??6?   $??0q?6?   $ 0Q?6?   $??Center=?6?   $ 
Horizontal=?6?   
 ????? 5q      )	  #System.Windows.Controls.DataGridRow??%?  '        '	  $    '  k    ?? 5r      "	  6    .?5s      ?? 
 ? ?? ?#e6e8f26;   ?? 5t      
 ? ??
 ?   ?6;   ?? 5u      
 ? ??$ 0q?6@   ?? 5v      
 ? ??
 ???? 6<   ?? 5w       
 ?@ AlternatingRowBackground ?#e6e8f26I   ?? 5y      
 ? ??$ 10q?68   5z      ?? 5{      "	 	 6    .?5}      ?? 
 ? ??$ 0q?6@   5      ?? 5?      "	  6    .?5?   &   ?? 
 ? ??$ 10q?6@   ?? 5?      
 ? ??$ 0q?6@   ?? 5?      
 ? ??
 ?   ?6;   ?? 5?      
 ? ??
 ???? 6;    5?      P? 5?   "    ?@ 
IsSelected$
 True??6@   . ?5?   &   ?? 
 ? ?? ?#f8f1fc6C   ?? 5?   &   
 ? ??
 ?   ?6C   5?   #   5?      5?         ColumnHeaderStyle 5?      ?? 5?   "   "	  6(   .?5?   &   ?? 
 ? ??$
 Bold"?6C   ?? 5?   &   
 ? ?? ?#4A90E26C   ?? 5?   &   
 ? ??$ 7q?6@   ?? 5?   &   
 ? ??$ Center=?6L   ?? 5?   &   
 ? ??$ Center=?6S   ?? 5?   &   
 ? ??$ Stretch=?6L   ?? 5?   &   
 ? ??$
 Left=?6S   5?   #    5?        5?   "   $ 80 65   $ 	Ticket ID??6@   $
 True??6j    6S   ?? *Id+6j     5?   "   $ * 65   $ User ID??6?   $
 True??6l    6P   ?? *	UsersId+6l     5?   "   $ * 65   $ Email-Address??6?   $
 True??6p    6V   ?? *Email+6p     5?   "   $ * 65   $ Title??6?   $
 True??6h    6N   ?? *Title+6h     5?   "   $ * 65   $ Ticket Category??6?   $
 True??6{    6X   ?? *TicketCategory+6{     5?   "   $ * 65   $ Description??6?   $
 True??6t    6T   ?? *Description+   ElementStyle 5?   &   ?? 5?   *   "	 ??60   .?5?   .   ?? 
 ? ??$
 Wrap=?6M   5?   +   5?   #     5?   "   $ * 65   $ 
Created At??6?   $
 True??6q    6S   ?? *	CreatedAt+6q     5?   "   $ * 65   $ Last Updated At??6?   $
 True??6z    6X   ?? *LastUpdatedAt+6z     5?   "   $ * 65   $ 	Closed At??6?   $
 True??6o    6R   ?? *
ClosedAt+ 5?   &   ?? 5?   *   "	 ??60   .?5?   .   ?? 
 ? ?? ?#3333336K   5?   +   5?   #     5?   "   $ Remove Ticket??69    5?   &   ?? 5?   *   .R?5?   &   ?? -   5        $??2q?5?   -   $??5q?69   $	??100??6_   ???#ca938e
???????$??1q?6?   
???z???.??6?   Remove Ticket6?   5?   +   5?   #   5?      5?      5?      ? 5?   
   $??1??6   .+?5?       ? -   5          ?Name$ myFrame??5?        ? NavigationUIVisibility$ Hidden=?6%   5?        5?   
   -	   5        $ ticketDataGrid??5?      $??5??6+   $??20q?68   $ False??6E   $ false??6?   5?      5?      5?      