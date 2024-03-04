Select Count(orderID) From BookOrder Where Month(orderDateTime) = (Month(GETDATE()))-1
--Select (Month(GETDATE()))-1