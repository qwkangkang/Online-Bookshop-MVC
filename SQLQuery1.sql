Select Sum(orderSubtotal) From BookOrder Join OrderDetail On BookOrder.orderID = OrderDetail.orderID 
Join Book On OrderDetail.bookID = Book.bookID
Where MONTH(orderDateTime) = 7 And bookCategory = 'Fiction'