SELECT * FROM Tickets 
INNER JOIN Users ON Tickets.UsersId = Users.Id
WHERE Tickets.UsersId = userId