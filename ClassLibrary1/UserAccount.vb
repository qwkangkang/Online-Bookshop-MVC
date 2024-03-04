Imports System.Data.SqlClient

Public Class UserAccount
    Private db As New DatabaseHelper()

    Public Function InsertUserAcc(fname As String, lname As String, email As String, password As String,
                                  birthday As DateTime, gender As String, dateTimeCreated As DateTime) As Boolean
        Dim success As Boolean = True
        Dim MySqlCommand As New SqlCommand
        Dim strSql As String
        Try
            If db.OpenConnection() = True Then
                strSql = "INSERT into UserAcc(userFname, userLname, userEmail, userPassword," +
                "userBirthday, userGender, userCreateDateTime, userStatus, userRole) VALUES " +
                "(@userFname, @userLname, @userEmail, @userPassword," +
                "@userBirthday, @userGender, @userCreateDateTime, @userStatus, @userRole)"

                MySqlCommand = New SqlCommand(strSql, db.conn)

                MySqlCommand.Parameters.AddWithValue("@userFname", fname)
                MySqlCommand.Parameters.AddWithValue("@userLname", lname)
                MySqlCommand.Parameters.AddWithValue("@userEmail", email)
                MySqlCommand.Parameters.AddWithValue("@userPassword", password)
                MySqlCommand.Parameters.AddWithValue("@userBirthday", birthday)
                MySqlCommand.Parameters.AddWithValue("@userGender", gender)
                MySqlCommand.Parameters.AddWithValue("@userCreateDateTime", dateTimeCreated)
                MySqlCommand.Parameters.AddWithValue("@userStatus", "Active")
                MySqlCommand.Parameters.AddWithValue("@userRole", "Buyer")

                MySqlCommand.ExecuteNonQuery()

                db.CloseConnection()
            End If
        Catch ex As Exception
            success = False
        End Try
        Return success
    End Function

    Public Function SearchTop4NewArrival() As List(Of ModelLibrary.Book)
        Dim books As New List(Of ModelLibrary.Book)

        If db.OpenConnection() Then

            ' Retrieve data from the database
            Dim currentDate As DateTime = DateTime.Now
            Dim currentMonth As Integer = currentDate.Month
            Dim lastMonth As Integer = currentMonth - 1

            Dim strSqlNewArrival As String = "SELECT TOP 4 * FROM Book " &
                "WHERE MONTH(bookPublishedDateTime) BETWEEN '" & lastMonth & "' AND '" & currentMonth & "' " &
                "ORDER BY bookPublishedDateTime DESC"

            ' Execute the SQL query and populate the list of books
            Dim da As New SqlDataAdapter(strSqlNewArrival, db.conn)
            Dim ds As New DataSet()
            da.Fill(ds, "Book")

            For Each row As DataRow In ds.Tables("Book").Rows
                Dim book As New ModelLibrary.Book
                ' Populate book properties from the DataRow
                book.bookID = Convert.ToInt32(row("bookID"))
                book.bookTitle = row("bookTitle").ToString()
                book.bookAuthor = row("bookAuthor").ToString()
                book.bookImg = row("bookImg").ToString()


                books.Add(book)
            Next


            db.CloseConnection()
        End If

        ' Return the list of books
        Return books
    End Function

    Public Function SearchTop4HotPick() As List(Of ModelLibrary.Book)
        Dim books As New List(Of ModelLibrary.Book)

        If db.OpenConnection() Then


            Dim strSqlHotPick = "Select TOP 4 * From Book " &
                     "Where bookSales > 20 " &
                    "order by bookSales DESC "

            Dim da As New SqlDataAdapter(strSqlHotPick, db.conn)
            Dim ds As New DataSet()
            da.Fill(ds, "Book")

            For Each row As DataRow In ds.Tables("Book").Rows
                Dim book As New ModelLibrary.Book
                book.bookID = Convert.ToInt32(row("bookID"))
                book.bookTitle = row("bookTitle").ToString()
                book.bookAuthor = row("bookAuthor").ToString()
                book.bookImg = row("bookImg").ToString()


                books.Add(book)
            Next


            db.CloseConnection()
        End If

        Return books
    End Function

    Public Function SearchTop4Fiction() As List(Of ModelLibrary.Book)
        Dim books As New List(Of ModelLibrary.Book)

        If db.OpenConnection() Then

            Dim strSqlFiction = "Select TOP 4 * From Book " &
                    "where bookCategory ='Fiction'"

            Dim da As New SqlDataAdapter(strSqlFiction, db.conn)
            Dim ds As New DataSet()
            da.Fill(ds, "Book")

            For Each row As DataRow In ds.Tables("Book").Rows
                Dim book As New ModelLibrary.Book
                book.bookID = Convert.ToInt32(row("bookID"))
                book.bookTitle = row("bookTitle").ToString()
                book.bookAuthor = row("bookAuthor").ToString()
                book.bookImg = row("bookImg").ToString()

                books.Add(book)
            Next

            db.CloseConnection()

        End If

        Return books
    End Function

    Public Function SearchTop4NonFiction() As List(Of ModelLibrary.Book)
        Dim books As New List(Of ModelLibrary.Book)

        If db.OpenConnection() Then

            Dim strSqlNonFiction = "Select TOP 4 * From Book " &
                    "where bookCategory ='Non-Fiction'"

            Dim da As New SqlDataAdapter(strSqlNonFiction, db.conn)
            Dim ds As New DataSet()
            da.Fill(ds, "Book")

            For Each row As DataRow In ds.Tables("Book").Rows
                Dim book As New ModelLibrary.Book
                book.bookID = Convert.ToInt32(row("bookID"))
                book.bookTitle = row("bookTitle").ToString()
                book.bookAuthor = row("bookAuthor").ToString()
                book.bookImg = row("bookImg").ToString()

                books.Add(book)
            Next

            db.CloseConnection()

        End If

        Return books
    End Function

    Public Function SearchNewArrival() As List(Of ModelLibrary.Book)
        Dim books As New List(Of ModelLibrary.Book)()

        If db.OpenConnection() Then

            Dim currentDate As DateTime = DateTime.Now
            Dim currentMonth As Integer = currentDate.Month
            Dim lastMonth As Integer = currentMonth - 1

            Dim strSqlNewArrival As String = "SELECT * FROM Book " &
                "WHERE MONTH(bookPublishedDateTime) BETWEEN '" & lastMonth & "' AND '" & currentMonth & "' " &
                "ORDER BY bookPublishedDateTime DESC"

            Dim da As New SqlDataAdapter(strSqlNewArrival, db.conn)
            Dim ds As New DataSet()
            da.Fill(ds, "Book")

            For Each row As DataRow In ds.Tables("Book").Rows
                Dim book As New ModelLibrary.Book()
                book.bookID = Convert.ToInt32(row("bookID"))
                book.bookTitle = row("bookTitle").ToString()
                book.bookAuthor = row("bookAuthor").ToString()
                book.bookImg = row("bookImg").ToString()
                book.bookPrice = Decimal.Parse(row("bookPrice"))

                books.Add(book)
            Next

            db.CloseConnection()
        End If

        Return books
    End Function

    Public Function SearchHotPick() As List(Of ModelLibrary.Book)
        Dim books As New List(Of ModelLibrary.Book)()

        If db.OpenConnection() Then


            Dim strSqlHotPick = "Select * From Book " &
                     "Where bookSales > 20 " &
                    "order by bookSales DESC "

            Dim da As New SqlDataAdapter(strSqlHotPick, db.conn)
            Dim ds As New DataSet()
            da.Fill(ds, "Book")

            For Each row As DataRow In ds.Tables("Book").Rows
                Dim book As New ModelLibrary.Book()
                book.bookID = Convert.ToInt32(row("bookID"))
                book.bookTitle = row("bookTitle").ToString()
                book.bookAuthor = row("bookAuthor").ToString()
                book.bookImg = row("bookImg").ToString()
                book.bookPrice = Decimal.Parse(row("bookPrice"))

                books.Add(book)
            Next


            db.CloseConnection()
        End If

        Return books
    End Function

    Public Function SearchFiction() As List(Of ModelLibrary.Book)
        Dim books As New List(Of ModelLibrary.Book)()

        If db.OpenConnection() Then

            Dim strSqlFiction = "Select * From Book " &
                    "where bookCategory ='Fiction'"

            Dim da As New SqlDataAdapter(strSqlFiction, db.conn)
            Dim ds As New DataSet()
            da.Fill(ds, "Book")

            For Each row As DataRow In ds.Tables("Book").Rows
                Dim book As New ModelLibrary.Book()
                book.bookID = Convert.ToInt32(row("bookID"))
                book.bookTitle = row("bookTitle").ToString()
                book.bookAuthor = row("bookAuthor").ToString()
                book.bookImg = row("bookImg").ToString()
                book.bookPrice = Decimal.Parse(row("bookPrice"))
                books.Add(book)
            Next

            db.CloseConnection()

        End If

        Return books
    End Function

    Public Function SearchNonFiction() As List(Of ModelLibrary.Book)
        Dim books As New List(Of ModelLibrary.Book)()

        If db.OpenConnection() Then

            Dim strSqlNonFiction = "Select * From Book " &
                    "where bookCategory ='Non-Fiction'"

            Dim da As New SqlDataAdapter(strSqlNonFiction, db.conn)
            Dim ds As New DataSet()
            da.Fill(ds, "Book")

            For Each row As DataRow In ds.Tables("Book").Rows
                Dim book As New ModelLibrary.Book()
                book.bookID = Convert.ToInt32(row("bookID"))
                book.bookTitle = row("bookTitle").ToString()
                book.bookAuthor = row("bookAuthor").ToString()
                book.bookImg = row("bookImg").ToString()
                book.bookPrice = Decimal.Parse(row("bookPrice"))
                books.Add(book)
            Next

            db.CloseConnection()

        End If

        Return books
    End Function

    Public Function SearchBookDetail(bookID As Integer) As ModelLibrary.Book
        Dim book As New ModelLibrary.Book()
        If db.OpenConnection() Then
            Dim strSql = "Select * From Book Where bookID = @bookID"
            Dim searchCmd As New SqlCommand(strSql, db.conn)
            searchCmd.Parameters.AddWithValue("@bookID", bookID)
            Dim da As New SqlDataAdapter(searchCmd)
            Dim ds As New DataSet()
            da.Fill(ds, "Book")

            If ds.Tables("Book").Rows.Count > 0 Then
                Dim row As DataRow = ds.Tables("Book").Rows(0)
                book.bookID = Convert.ToInt32(row("bookID"))
                book.bookTitle = row("bookTitle").ToString()
                book.bookAuthor = row("bookAuthor").ToString()
                book.bookImg = row("bookImg").ToString()
                book.bookCategory = row("bookCategory").ToString()
                book.bookPrice = Decimal.Parse(row("bookPrice").ToString())
                book.bookPublishedDateTime = DateTime.Parse(row("bookPublishedDateTime").ToString())
                book.bookSales = Convert.ToInt32(row("bookSales").ToString())
                book.bookStatus = row("bookStatus").ToString()
                book.bookQuantity = Convert.ToInt32(row("bookQuantity").ToString())
                book.bookPublisher = row("bookPublisher").ToString()
                book.bookWeight = Convert.ToInt32(row("bookWeight").ToString())
                book.bookDes = row("bookDes").ToString()

            End If
            db.CloseConnection()
        End If
        Return book
    End Function

    Public Function InsertWishlist(bookID As Integer, userID As Integer) As String
        Dim result As String = String.Empty
        If db.OpenConnection = True Then
            Dim insertCmd As New SqlCommand

            Dim strFirstInsert = "Insert Into Wishlist (bookID, userID, wishlistPreference) Values (@bookID, @userID, @wishlistPreference)"
            insertCmd = New SqlCommand(strFirstInsert, db.conn)
            insertCmd.Parameters.AddWithValue("@bookID", bookID)
            insertCmd.Parameters.AddWithValue("@userID", userID)
            insertCmd.Parameters.AddWithValue("@wishlistPreference", 1)

            Dim intInsertStatus As Integer = insertCmd.ExecuteNonQuery()
            If (intInsertStatus > 0) Then
                result = "Add to Wish List Successfully!"
            Else
                result = "Failed to add to Wish List!"
            End If
            db.CloseConnection()
        Else
            result = "Failed to add to Wish List!"
        End If
        Return result
    End Function

    Public Function UpdateWishlist(userID As Integer, bookID As Integer, preference As Integer) As String
        Dim result As String = String.Empty
        If db.OpenConnection = True Then
            Dim strUpdate = "Update Wishlist SET wishlistPreference = @wishlistPreference Where userID = @userID and bookID = @bookID"

            Dim updateCmd As New SqlCommand
            updateCmd = New SqlCommand(strUpdate, db.conn)
            updateCmd.Parameters.AddWithValue("@userID", userID)
            updateCmd.Parameters.AddWithValue("@bookID", bookID)
            updateCmd.Parameters.AddWithValue("@wishlistPreference", preference)


            Dim intInsertStatus As Integer = updateCmd.ExecuteNonQuery()
            If (intInsertStatus > 0) Then
                If preference = 1 Then
                    result = "Add to Wish List Successfully!"
                ElseIf preference = 0 Then
                    result = "Removed from Wish List Successfully!"
                Else
                    result = "Something wrong to wishlist"
                End If
            End If
            db.CloseConnection()
        Else
            result = "Something wrong to wishlist"
        End If
        Return result
    End Function

    Public Function SearchWishlistExistence(userID As Integer, bookID As Integer) As Integer
        Dim data As Integer
        Dim searchCmd As New SqlCommand
        If db.OpenConnection = True Then
            Dim strSql = "Select COUNT(wishlistPreference) From Wishlist Where userID = @userID and bookID = @bookID"
            searchCmd = New SqlCommand(strSql, db.conn)
            searchCmd.Parameters.AddWithValue("@userID", userID)
            searchCmd.Parameters.AddWithValue("@bookID", bookID)

            data = Integer.Parse(searchCmd.ExecuteScalar())
            db.CloseConnection()
        End If
        Return data
    End Function

    Public Function SearchWishlistPreference(userID As Integer, bookID As Integer) As Integer
        Dim preference As Integer
        Dim searchCmd As New SqlCommand
        Dim strSql As String
        If db.OpenConnection = True Then
            strSql = "Select wishlistPreference from Wishlist Where userID = @userID and bookID = @bookID"
            searchCmd = New SqlCommand(strSql, db.conn)
            searchCmd.Parameters.AddWithValue("@userID", userID)
            searchCmd.Parameters.AddWithValue("@bookID", bookID)

            preference = Integer.Parse(searchCmd.ExecuteScalar())
        End If

        db.CloseConnection()
        Return preference
    End Function

    Public Function SearchCartNumCount(userID As Integer, bookID As Integer) As Integer
        Dim searchCmd As New SqlCommand
        Dim count As Integer
        If db.OpenConnection = True Then
            Dim strSql = "Select COUNT(cartNum) From Cart Where userID = @userID and bookID = @bookID"
            searchCmd = New SqlCommand(strSql, db.conn)
            searchCmd.Parameters.AddWithValue("@userID", userID)
            searchCmd.Parameters.AddWithValue("@bookID", bookID)
            count = searchCmd.ExecuteScalar()          
            db.CloseConnection()
        End If

        Return count
    End Function

    Public Function InsertCart(userID As Integer, bookID As Integer) As Boolean
        Dim insertCmd As New SqlCommand
        If db.OpenConnection = True Then
            Dim strFirstInsert = "Insert Into Cart (bookID, userID, cartNum) Values (@bookID, @userID, @cartNum)"
            insertCmd = New SqlCommand(strFirstInsert, db.conn)
            insertCmd.Parameters.AddWithValue("@bookID", bookID)
            insertCmd.Parameters.AddWithValue("@userID", userID)
            insertCmd.Parameters.AddWithValue("@cartNum", 1)
            Dim intInsertStatus As Integer = insertCmd.ExecuteNonQuery()
            db.CloseConnection()
            If (intInsertStatus > 0) Then
                Return True
            End If           
        End If
        Return False
    End Function

    Public Function SearchCartNum(userID As Integer, bookID As Integer) As Integer
        Dim searchCmd As New SqlCommand
        Dim num As Integer
        If db.OpenConnection = True Then
            Dim strSql = "Select cartNum from Cart Where userID = @userID and bookID = @bookID"
            searchCmd = New SqlCommand(strSql, db.conn)
            searchCmd.Parameters.AddWithValue("@userID", userID)
            searchCmd.Parameters.AddWithValue("@bookID", bookID)
            num = Integer.Parse(searchCmd.ExecuteScalar())
            db.CloseConnection()
        End If
        Return num
    End Function

    Public Function UpdateCartNum(userID As Integer, bookID As Integer, add As Integer) As Boolean
        Dim strUpdate = "Update Cart SET cartNum = @cartNum Where userID = @userID and bookID = @bookID"
        If db.OpenConnection = True Then
            Dim updateCmd As New SqlCommand
            updateCmd = New SqlCommand(strUpdate, db.conn)
            updateCmd.Parameters.AddWithValue("@userID", userID)
            updateCmd.Parameters.AddWithValue("@bookID", bookID)
            updateCmd.Parameters.AddWithValue("@cartNum", add)


            Dim intInsertStatus As Integer = updateCmd.ExecuteNonQuery()
            db.CloseConnection()
            If (intInsertStatus > 0) Then
                Return True
            End If
        End If
        Return False
    End Function

    Public Function SearchSearchEngineResult(search As String) As List(Of ModelLibrary.Book)
        Dim books As New List(Of ModelLibrary.Book)()
        If db.OpenConnection() Then

            If search.Substring(0, 1) = """" And search.Substring(search.Length - 1, 1) = """" Then
                Dim strSearch = "Select * From Book " &
                        "Where UPPER(bookTitle) = UPPER(@search) Or UPPER(bookAuthor) = UPPER(@search) "
                Dim searchCmd As New SqlCommand(strSearch, db.conn)
                Dim length = search.Length - 2
                searchCmd.Parameters.AddWithValue("@search", search.Substring(1, length))
                Dim da As New SqlDataAdapter(searchCmd)
                Dim ds As New DataSet()
                da.Fill(ds, "Book")

                For Each row As DataRow In ds.Tables("Book").Rows
                    Dim book As New ModelLibrary.Book()
                    book.bookID = Convert.ToInt32(row("bookID"))
                    book.bookTitle = row("bookTitle").ToString()
                    book.bookAuthor = row("bookAuthor").ToString()
                    book.bookImg = row("bookImg").ToString()

                    books.Add(book)
                Next
            ElseIf search.StartsWith("book:") Then
                search = search.Substring(5).Trim
                Dim strSearch = "Select * From Book " &
                        "Where UPPER(bookTitle) LIKE '%' + UPPER(@search) + '%' "
                Dim searchCmd As New SqlCommand(strSearch, db.conn)
                searchCmd.Parameters.AddWithValue("@search", search)
                Dim da As New SqlDataAdapter(searchCmd)
                Dim ds As New DataSet()
                da.Fill(ds, "Book")

                For Each row As DataRow In ds.Tables("Book").Rows
                    Dim book As New ModelLibrary.Book()
                    book.bookID = Convert.ToInt32(row("bookID"))
                    book.bookTitle = row("bookTitle").ToString()
                    book.bookAuthor = row("bookAuthor").ToString()
                    book.bookImg = row("bookImg").ToString()

                    books.Add(book)
                Next

            ElseIf search.StartsWith("author:") Then
                search = search.Substring(7).Trim
                Dim strSearch = "Select * From Book " &
                        "Where UPPER(bookAuthor) LIKE '%' + UPPER(@search) + '%' "
                Dim searchCmd As New SqlCommand(strSearch, db.conn)
                searchCmd.Parameters.AddWithValue("@search", search)
                Dim da As New SqlDataAdapter(searchCmd)
                Dim ds As New DataSet()
                da.Fill(ds, "Book")

                For Each row As DataRow In ds.Tables("Book").Rows
                    Dim book As New ModelLibrary.Book()
                    book.bookID = Convert.ToInt32(row("bookID"))
                    book.bookTitle = row("bookTitle").ToString()
                    book.bookAuthor = row("bookAuthor").ToString()
                    book.bookImg = row("bookImg").ToString()

                    books.Add(book)
                Next

            ElseIf search.StartsWith("RM") And search.Contains("..") Then
                Dim lower As Decimal = search.Substring(2, search.IndexOf("..") - 2)
                Dim higher As Decimal = search.Substring(search.IndexOf("..") + 2)

                Dim strSearch = "Select * From Book " &
                    "Where bookPrice Between @lower And @higher"

                Dim searchCmd As New SqlCommand(strSearch, db.conn)
                searchCmd.Parameters.AddWithValue("@lower", lower)
                searchCmd.Parameters.AddWithValue("@higher", higher)
                Dim da As New SqlDataAdapter(searchCmd)
                Dim ds As New DataSet()
                da.Fill(ds, "Book")

                For Each row As DataRow In ds.Tables("Book").Rows
                    Dim book As New ModelLibrary.Book()
                    book.bookID = Convert.ToInt32(row("bookID"))
                    book.bookTitle = row("bookTitle").ToString()
                    book.bookAuthor = row("bookAuthor").ToString()
                    book.bookImg = row("bookImg").ToString()

                    books.Add(book)
                Next
            Else
                Dim strSearch = "Select * From Book " &
                        "Where UPPER(bookTitle) LIKE '%' + UPPER(@search) + '%' Or UPPER(bookAuthor) LIKE '%' + UPPER(@search) + '%'"
                Dim searchCmd As New SqlCommand(strSearch, db.conn)
                searchCmd.Parameters.AddWithValue("@search", search)
                Dim da As New SqlDataAdapter(searchCmd)
                Dim ds As New DataSet()
                da.Fill(ds, "Book")

                For Each row As DataRow In ds.Tables("Book").Rows
                    Dim book As New ModelLibrary.Book()
                    book.bookID = Convert.ToInt32(row("bookID"))
                    book.bookTitle = row("bookTitle").ToString()
                    book.bookAuthor = row("bookAuthor").ToString()
                    book.bookImg = row("bookImg").ToString()

                    books.Add(book)
                Next


            End If

            db.CloseConnection()

        End If
        Return books
    End Function

    Public Function SelectCartNumIndex(userID As Integer) As Integer
        Dim cartItem As Integer
        Dim searchCmd As New SqlCommand
        If db.OpenConnection = True Then
            Try
                Dim strSql = "Select cartNum From Cart Where userID = @userID "
                Dim ds As DataSet = New DataSet
                Dim da As SqlDataAdapter
                Dim currentRow As Integer = 0
                Dim cartNum As Integer = 0

                da = New SqlDataAdapter(strSql, db.conn)
                da.SelectCommand.Parameters.AddWithValue("@userID", userID)
                ds.Clear()
                da.Fill(ds, "Cart")

                If ds.Tables("Cart").Rows.Count > 0 Then

                    For row As Integer = 0 To ds.Tables("Cart").Rows.Count - 1
                        cartNum = ds.Tables("Cart").Rows(currentRow).Item(0)

                        cartItem += cartNum

                        currentRow += 1
                    Next
                End If
            Catch ex As Exception
                cartItem = 0
            End Try
           
            db.CloseConnection()

        End If
        Return cartItem
    End Function

    Public Function SelectLikeNumIndex(userID As Integer) As Integer
        Dim likeItem As Integer
        Dim searchCmd As New SqlCommand
        If db.OpenConnection = True Then
            Try
                Dim strSql = "Select COUNT(wishlistPreference) From Wishlist Where userID = @userID And wishlistPreference = 1"
                searchCmd = New SqlCommand(strSql, db.conn)
                searchCmd.Parameters.AddWithValue("@userID", userID)

                likeItem = CInt(searchCmd.ExecuteScalar())

                db.CloseConnection()
            Catch ex As Exception
                likeItem = 0
            End Try
            
        End If
        Return likeItem
    End Function

    Public Function SelectBookOrderHistory(userID As Integer) As List(Of ModelLibrary.BookOrder)
        Dim items As New List(Of ModelLibrary.BookOrder)()

        If db.OpenConnection = True Then

            Dim strSql = "Select * From BookOrder Inner Join Payment " &
                  "On BookOrder.orderID = Payment.orderID " &
                   "And BookOrder.userID = @userID " &
                   "Order By BookOrder.orderDateTime DESC"

            Dim searchCmd As New SqlCommand
            searchCmd = New SqlCommand(strSql, db.conn)
            searchCmd.Parameters.AddWithValue("@userID", userID)
            Dim da As New SqlDataAdapter(searchCmd)
            Dim ds As DataSet = New DataSet()
            ds.Clear()
            da.Fill(ds, "BookOrder")

            If ds.Tables("BookOrder").Rows.Count > 0 Then
                For Each row As DataRow In ds.Tables("BookOrder").Rows
                    Dim bookOrder As New ModelLibrary.BookOrder()
                    Dim orderID = Convert.ToInt32(row("orderID"))
                    bookOrder.orderDateTime = DateTime.Parse(row("orderDateTime").ToString)
                    bookOrder.orderContact = row("orderContact").ToString()
                    bookOrder.orderSubtotal = Decimal.Parse(row("orderSubtotal").ToString())
                    bookOrder.orderDeliveryMethod = row("orderDeliveryMethod").ToString()
                    bookOrder.orderDeliveryFee = Decimal.Parse(row("orderDeliveryFee").ToString())
                    bookOrder.orderLocation = row("orderLocation").ToString
                    bookOrder.paymentAmount = Decimal.Parse(row("paymentAmount").ToString())
                    bookOrder.paymentDateTime = DateTime.Parse(row("paymentDateTime").ToString())


                    Dim orderDetailList As New List(Of ModelLibrary.OrderDetail)()

                    strSql = "Select * From BookOrder Inner Join OrderDetail " &
                             "On BookOrder.orderID = OrderDetail.orderID " &
                             "And BookOrder.userID = @userID And " &
                             "OrderDetail.orderID = @orderID"

                    Dim searchCmd2 As New SqlCommand
                    searchCmd2 = New SqlCommand(strSql, db.conn)
                    searchCmd2.Parameters.AddWithValue("@userID", userID)
                    searchCmd2.Parameters.AddWithValue("@orderID", orderID)
                    Dim da2 As New SqlDataAdapter(searchCmd2)
                    Dim ds2 As DataSet = New DataSet()
                    ds2.Clear()
                    da2.Fill(ds2, "OrderDetail")

                    If ds2.Tables("OrderDetail").Rows.Count > 0 Then
                        For Each row2 As DataRow In ds2.Tables("OrderDetail").Rows
                            Dim orderDetail As New ModelLibrary.OrderDetail()
                            orderDetail.odQuantity = Convert.ToInt32(row2("odQuantity").ToString)
                            Dim bookID As String = row2("bookID").ToString()

                            strSql = "Select * From Book Where bookID = @bookID"
                            Dim searchCmd3 As New SqlCommand(strSql, db.conn)
                            searchCmd3.Parameters.AddWithValue("@bookID", bookID)
                            Dim da3 As New SqlDataAdapter(searchCmd3)
                            Dim ds3 As New DataSet()
                            da3.Fill(ds3, "Book")

                            If ds3.Tables("Book").Rows.Count > 0 Then
                                Dim row3 As DataRow = ds3.Tables("Book").Rows(0)

                                Dim book As New ModelLibrary.Book()
                                book.bookTitle = row3("bookTitle").ToString()
                                book.bookImg = row3("bookImg").ToString()
                                book.bookCategory = row3("bookCategory").ToString()
                                book.bookPrice = Decimal.Parse(row3("bookPrice").ToString())
                                book.bookQuantity = Convert.ToInt32(row3("bookQuantity").ToString())

                                orderDetail.book = book
                            End If


                            orderDetailList.Add(orderDetail)
                        Next
                    End If

                    bookOrder.orderDetailList = orderDetailList


                    items.Add(bookOrder)
                Next
            End If


            db.CloseConnection()

        End If
        Return items
    End Function

    Public Function SearchAccountInfo(userID As Integer) As ModelLibrary.UserAcc
        Dim userAcc As New ModelLibrary.UserAcc

        If db.OpenConnection = True Then

            Dim strSql = "Select * From UserAcc " &
                   "Where userID = @userID "

            Dim searchCmd As New SqlCommand
            searchCmd = New SqlCommand(strSql, db.conn)
            searchCmd.Parameters.AddWithValue("@userID", userID)
            Dim da As New SqlDataAdapter(searchCmd)
            Dim ds As DataSet = New DataSet()
            ds.Clear()
            da.Fill(ds, "UserAcc")

            If ds.Tables("UserAcc").Rows.Count > 0 Then
                Dim row As DataRow = ds.Tables("UserAcc").Rows(0)

                userAcc.userFname = row("userFname").ToString
                userAcc.userLname = row("userLname").ToString
                userAcc.userEmail = row("userEmail").ToString
                Dim birthday = DateTime.Parse(row("userBirthday").ToString)
                userAcc.userBirthday = birthday.Date
                userAcc.userGender = row("userGender").ToString


            End If
        End If
        Return userAcc
    End Function

    Public Function DeleteBook(bookID As Integer) As Boolean
        Dim strSql As String
        Try
            If db.OpenConnection = True Then
                Dim deleteCmd As New SqlCommand
                strSql = "Delete from Book Where bookID = @bookID"
                deleteCmd = New SqlCommand(strSql, db.conn)
                deleteCmd.Parameters.AddWithValue("bookID", bookID)
                deleteCmd.ExecuteNonQuery()
                db.CloseConnection()
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function UpdateBookDetail(bookID As Integer, title As String, author As String, category As String, status As String,
                                     quantity As Integer, publisher As String, weight As Decimal, image As String, des As String, price As Decimal) As Boolean
        If image = "" Then
            Dim strUpdate = "Update Book SET bookTitle = @title, bookAuthor = @author, bookCategory = @category, bookStatus = @status, " +
                     "bookQuantity = @quantity, bookPublisher = @publisher, bookWeight = @weight, " +
                     "bookDes = @des, bookPrice = @price Where bookID = @bookID"
            If db.OpenConnection = True Then
                Dim updateCmd As New SqlCommand
                updateCmd = New SqlCommand(strUpdate, db.conn)
                updateCmd.Parameters.AddWithValue("@title", title)
                updateCmd.Parameters.AddWithValue("@author", author)
                updateCmd.Parameters.AddWithValue("@category", category)
                updateCmd.Parameters.AddWithValue("@status", status)
                updateCmd.Parameters.AddWithValue("@quantity", quantity)
                updateCmd.Parameters.AddWithValue("@publisher", publisher)
                updateCmd.Parameters.AddWithValue("@weight", weight)
                updateCmd.Parameters.AddWithValue("@des", des)
                updateCmd.Parameters.AddWithValue("@bookID", bookID)
                updateCmd.Parameters.AddWithValue("@bookPrice", price)

                Dim intInsertStatus As Integer = updateCmd.ExecuteNonQuery()
                db.CloseConnection()
                If (intInsertStatus > 0) Then
                    Return True
                End If
            End If

        Else
            Dim strUpdate = "Update Book SET bookTitle = @title, bookAuthor = @author, bookCategory = @category, bookStatus = @status, " +
                     "bookQuantity = @quantity, bookPublisher = @publisher, bookWeight = @weight, bookImg = @image, " +
                     "bookDes = @des, bookPrice = @price Where bookID = @bookID"
            If db.OpenConnection = True Then
                Dim updateCmd As New SqlCommand
                updateCmd = New SqlCommand(strUpdate, db.conn)
                updateCmd.Parameters.AddWithValue("@title", title)
                updateCmd.Parameters.AddWithValue("@author", author)
                updateCmd.Parameters.AddWithValue("@category", category)
                updateCmd.Parameters.AddWithValue("@status", status)
                updateCmd.Parameters.AddWithValue("@quantity", quantity)
                updateCmd.Parameters.AddWithValue("@publisher", publisher)
                updateCmd.Parameters.AddWithValue("@weight", weight)
                updateCmd.Parameters.AddWithValue("@image", image)
                updateCmd.Parameters.AddWithValue("@des", des)
                updateCmd.Parameters.AddWithValue("@bookID", bookID)
                updateCmd.Parameters.AddWithValue("@bookPrice", price)

                Dim intInsertStatus As Integer = updateCmd.ExecuteNonQuery()
                db.CloseConnection()
                If (intInsertStatus > 0) Then
                    Return True
                End If
            End If

        End If
        Return False
    End Function

    Public Function SearchUserList() As List(Of ModelLibrary.UserAcc)
        Dim userlist As New List(Of ModelLibrary.UserAcc)()

        If db.OpenConnection() Then

            Dim strSql = "Select * From UserAcc "

            Dim da As New SqlDataAdapter(strSql, db.conn)
            Dim ds As New DataSet()
            da.Fill(ds, "Book")

            For Each row As DataRow In ds.Tables("Book").Rows
                Dim userAcc As New ModelLibrary.UserAcc()
                userAcc.userRole = row("userRole").ToString
                userAcc.userID = Convert.ToInt32(row("userID"))
                userAcc.userFname = row("userFname").ToString()
                userAcc.userLname = row("userLname").ToString()
                userAcc.userEmail = row("userEmail").ToString
                userAcc.userCreateDateTime = DateTime.Parse(row("userCreateDateTime"))
                userAcc.userStatus = row("userStatus").ToString()
                userlist.Add(userAcc)
            Next

            db.CloseConnection()

        End If

        Return userlist
    End Function

    Public Function UpdateStatusToActive(userID As Integer) As Boolean
        Dim strUpdate = "Update UserAcc SET userStatus = @status Where userID = @userID"
        If db.OpenConnection = True Then
            Dim updateCmd As New SqlCommand
            updateCmd = New SqlCommand(strUpdate, db.conn)
            updateCmd.Parameters.AddWithValue("@userID", userID)
            updateCmd.Parameters.AddWithValue("@status", "Active")

            Dim intInsertStatus As Integer = updateCmd.ExecuteNonQuery()
            db.CloseConnection()
            If (intInsertStatus > 0) Then
                Return True
            End If
        End If
        Return False
    End Function

    Public Function UpdateStatusToDisable(userID As Integer) As Boolean
        Dim strUpdate = "Update UserAcc SET userStatus = @status Where userID = @userID"
        If db.OpenConnection = True Then
            Dim updateCmd As New SqlCommand
            updateCmd = New SqlCommand(strUpdate, db.conn)
            updateCmd.Parameters.AddWithValue("@userID", userID)
            updateCmd.Parameters.AddWithValue("@status", "Disabled")

            Dim intInsertStatus As Integer = updateCmd.ExecuteNonQuery()
            db.CloseConnection()
            If (intInsertStatus > 0) Then
                Return True
            End If
        End If
        Return False
    End Function

    Public Function UpdateRoleToAdmin(userID As Integer) As Boolean
        Dim strUpdate = "Update UserAcc SET userRole = @userRole Where userID = @userID"
        If db.OpenConnection = True Then
            Dim updateCmd As New SqlCommand
            updateCmd = New SqlCommand(strUpdate, db.conn)
            updateCmd.Parameters.AddWithValue("@userID", userID)
            updateCmd.Parameters.AddWithValue("@userRole", "Admin")

            Dim intInsertStatus As Integer = updateCmd.ExecuteNonQuery()
            db.CloseConnection()
            If (intInsertStatus > 0) Then
                Return True
            End If
        End If
        Return False
    End Function

    Public Function UpdateRoleToBuyer(userID As Integer) As Boolean
        Dim strUpdate = "Update UserAcc SET userRole = @userRole Where userID = @userID"
        If db.OpenConnection = True Then
            Dim updateCmd As New SqlCommand
            updateCmd = New SqlCommand(strUpdate, db.conn)
            updateCmd.Parameters.AddWithValue("@userID", userID)
            updateCmd.Parameters.AddWithValue("@userRole", "Buyer")

            Dim intInsertStatus As Integer = updateCmd.ExecuteNonQuery()
            db.CloseConnection()
            If (intInsertStatus > 0) Then
                Return True
            End If
        End If
        Return False
    End Function

    Public Function DeleteUser(userID As Integer) As Boolean
        Dim strSql As String
        Try
            If db.OpenConnection = True Then
                Dim deleteCmd As New SqlCommand
                strSql = "Delete from UserAcc Where userID = @userID"
                deleteCmd = New SqlCommand(strSql, db.conn)
                deleteCmd.Parameters.AddWithValue("userID", userID)
                deleteCmd.ExecuteNonQuery()
                db.CloseConnection()
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function InsertBook(title As String, author As String, category As String, status As String,
                                     quantity As Integer, publisher As String, weight As Decimal, image As String, des As String, price As Decimal) As Boolean
        Dim success As Boolean = True
        Dim MySqlCommand As New SqlCommand
        Dim strSql As String
        Try
            If db.OpenConnection() = True Then
                strSql = "INSERT into Book(bookTitle, bookAuthor, bookCategory, bookStatus," +
                "bookQuantity, bookPublisher, bookWeight, bookImg, bookDes, bookPrice, bookPublishedDateTime, bookSales) " +
                "VALUES (@bookTitle, @bookAuthor, @bookCategory, @bookStatus," +
                "@bookQuantity, @bookPublisher, @bookWeight, @bookImg, @bookDes, @bookPrice, @bookPublishedDateTime, @bookSales)"

                MySqlCommand = New SqlCommand(strSql, db.conn)

                MySqlCommand.Parameters.AddWithValue("@bookTitle", title)
                MySqlCommand.Parameters.AddWithValue("@bookAuthor", author)
                MySqlCommand.Parameters.AddWithValue("@bookCategory", category)
                MySqlCommand.Parameters.AddWithValue("@bookStatus", status)
                MySqlCommand.Parameters.AddWithValue("@bookQuantity", quantity)
                MySqlCommand.Parameters.AddWithValue("@bookPublisher", publisher)
                MySqlCommand.Parameters.AddWithValue("@bookWeight", weight)
                MySqlCommand.Parameters.AddWithValue("@bookImg", image)
                MySqlCommand.Parameters.AddWithValue("@bookDes", des)
                MySqlCommand.Parameters.AddWithValue("@bookPrice", price)
                MySqlCommand.Parameters.AddWithValue("@bookPublishedDateTime", DateTime.Now)
                MySqlCommand.Parameters.AddWithValue("@bookSales", 0)

                MySqlCommand.ExecuteNonQuery()

                db.CloseConnection()
            End If
        Catch ex As Exception
            success = False
        End Try
        Return success
    End Function

    Public Function InsertActionLog(userID As Integer, actionDateTime As DateTime, actionDescription As String) As Boolean
        Dim success As Boolean = True
        Dim MySqlCommand As New SqlCommand
        Dim strSql As String
        Try
            If db.OpenConnection() = True Then
                strSql = "INSERT into ActionLog(userID, actionDateTime, actionDescription) " +
                "VALUES (@userID, @actionDateTime, @actionDescription)"

                MySqlCommand = New SqlCommand(strSql, db.conn)

                MySqlCommand.Parameters.AddWithValue("@userID", userID)
                MySqlCommand.Parameters.AddWithValue("@actionDateTime", DateTime.Now)
                MySqlCommand.Parameters.AddWithValue("@actionDescription", actionDescription)


                MySqlCommand.ExecuteNonQuery()

                db.CloseConnection()
            End If
        Catch ex As Exception
            success = False
        End Try
        Return success
    End Function

    Public Function SearchActionLog(userID As Integer) As List(Of ModelLibrary.ActionLog)
        Dim actionLog As New List(Of ModelLibrary.ActionLog)()

        If db.OpenConnection() Then

            Dim strSql = "Select * From ActionLog Where userID = '" & userID & "'" +
                " Order By actionDateTime DESC"

            Dim da As New SqlDataAdapter(strSql, db.conn)
            Dim ds As New DataSet()
            da.Fill(ds, "ActionLog")

            For Each row As DataRow In ds.Tables("ActionLog").Rows
                Dim action As New ModelLibrary.ActionLog()
                action.actionID = Convert.ToInt32(row("actionID").ToString)
                action.userID = Convert.ToInt32(row("userID"))
                action.actionDescription = row("actionDescription").ToString()
                action.actionDateTime = DateTime.Parse(row("actionDateTime"))
                actionLog.Add(action)
            Next

            db.CloseConnection()

        End If

        Return actionLog
    End Function

    Public Function SearchLoginInfo(ByVal userData As ModelLibrary.UserAcc) As String()
        Dim jsonResultList As String()
        Try

            Dim found As String = "n"
            Dim active As String = "n"
            Dim admin As String = "n"
            If db.OpenConnection Then
                Dim strSql = "Select * From UserAcc"
                Dim ds As DataSet = New DataSet
                Dim da As SqlDataAdapter
                Dim currentRow As Integer = 0
                Dim dbEmail As String
                Dim dbPassword As String
                Dim dbUserID As String
                Dim dbFirstName As String
                Dim dbStatus As String
                Dim dbRole As String

                da = New SqlDataAdapter(strSql, db.conn)
                ds.Clear()
                da.Fill(ds, "UserAcc")

                If ds.Tables("UserAcc").Rows.Count > 0 Then

                    For row As Integer = 0 To ds.Tables("UserAcc").Rows.Count - 1
                        dbUserID = ds.Tables("UserAcc").Rows(currentRow).Item(0)
                        dbFirstName = ds.Tables("UserAcc").Rows(currentRow).Item(1)
                        dbEmail = ds.Tables("UserAcc").Rows(currentRow).Item(3)
                        dbPassword = ds.Tables("UserAcc").Rows(currentRow).Item(4)
                        dbStatus = ds.Tables("UserAcc").Rows(currentRow).Item(8)
                        dbRole = ds.Tables("UserAcc").Rows(currentRow).Item(9)

                        If (userData.userEmail = dbEmail And userData.userPassword = dbPassword) Then
                            found = "y"
                            Exit For
                        End If
                        currentRow += 1
                    Next

                    db.CloseConnection()

                    If dbStatus = "Active" Then
                        active = "y"
                    End If

                    If dbRole = "Admin" Then
                        admin = "y"
                    End If

                    jsonResultList = {found, active, admin, dbUserID, dbFirstName}
                    'If found And active And admin Then
                    '    Session("UserID") = dbUserID
                    '    TempData("LoginResult") = "Welcome Back, " + dbFirstName + "!"
                    'ElseIf found And Not active Then
                    '    TempData("LoginResult") = "Sorry, your account has been disabled. Please contact with the admin!"
                    'ElseIf found And Not admin Then
                    '    TempData("LoginResult") = "Sorry, you have no privilege!"
                    'End If

                End If
            End If
            Return jsonResultList
        Catch ex As Exception
            Return jsonResultList
        End Try
        
    End Function

    Public Function SearchSales(month As Integer, year As Integer) As List(Of ModelLibrary.Sales)
        Dim bookSales As New List(Of ModelLibrary.Sales)()

        If db.OpenConnection() Then
            Dim strSql = "Select Distinct bookTitle, bookAuthor, bookCategory, bookPrice, bookSales, bookPrice*bookSales As TotalRevenue from Book " +
                "inner join OrderDetail On Book.bookID = OrderDetail.bookID Inner Join BookOrder On BookOrder.orderID = OrderDetail.orderID " +
                "where MONTH(orderDateTime) = @month and YEAR(orderDateTime) = @year Order by bookSales Desc"
            Dim searchCmd As New SqlCommand(strSql, db.conn)
            searchCmd.Parameters.AddWithValue("@month", month)
            searchCmd.Parameters.AddWithValue("@year", year)
            Dim da As New SqlDataAdapter(searchCmd)
            Dim ds As New DataSet()
            da.Fill(ds, "Book")


            For Each row As DataRow In ds.Tables("Book").Rows
                Dim sales As New ModelLibrary.Sales()
                sales.bookTitle = row("bookTitle").ToString
                sales.author = row("bookAuthor").ToString
                sales.category = row("bookCategory").ToString()
                sales.price = Decimal.Parse(row("bookPrice"))
                sales.quantity = Integer.Parse(row("bookSales"))
                sales.totalRevenue = Decimal.Parse(row("TotalRevenue"))
                bookSales.Add(sales)
            Next

            db.CloseConnection()

        End If

        Return bookSales
    End Function

    Public Function SearchCustomers(month As Integer, year As Integer) As ModelLibrary.Customers
        Dim customers As New ModelLibrary.Customers
        Dim accumulatedOrderNumber As Integer = 0
        Dim accumulatedSpending As Decimal = 0

        If db.OpenConnection() Then

            Dim strSql = "Select Count(userID) From UserAcc Where userRole = 'Buyer' And MONTH(userCreateDateTime) = @month And YEAR(userCreateDateTime) = @year"
            Dim searchCmd As New SqlCommand(strSql, db.conn)
            searchCmd.Parameters.AddWithValue("@month", month)
            searchCmd.Parameters.AddWithValue("@year", year)
            Dim da As New SqlDataAdapter(searchCmd)
            Dim ds As New DataSet()
            da.Fill(ds, "UserAcc")
            customers.newCustomerNumber = searchCmd.ExecuteScalar()


            'Select userFname, userCreateDateTime, (Select Count(orderID) From BookOrder Where BookOrder.userID = UserAcc.userID) As OrderNumber  From UserAcc 
            strSql = "Select *,(Select Count(orderID) From BookOrder Where BookOrder.userID = UserAcc.userID And Month(orderDateTime) = @month) As OrderNumber, " +
                "(Select Sum(orderSubtotal) From BookOrder Where BookOrder.userID = UserAcc.userID And Month(orderDateTime) = @month) As TotalSpent from UserAcc Where userRole = 'Buyer' And userCreateDateTime <= GETDATE() Order By userID  "

            'Dim searchCmd As New SqlCommand(strSql, db.conn)
            
            'Dim da As New SqlDataAdapter(searchCmd)
            'Dim ds As New DataSet()
            searchCmd = New SqlCommand(strSql, db.conn)
            searchCmd.Parameters.AddWithValue("@month", month)
            searchCmd.Parameters.AddWithValue("@year", year)
            da = New SqlDataAdapter(searchCmd)
            ds = New DataSet()
            da.Fill(ds, "UserAcc")

            Dim customerList As New List(Of ModelLibrary.UserAcc)

            For Each row As DataRow In ds.Tables("UserAcc").Rows
                Dim customer As New ModelLibrary.UserAcc
                customer.userFname = row("userFname").ToString
                customer.userLname = row("userLname").ToString
                customer.userEmail = row("userEmail").ToString
                customer.userGender = row("userGender").ToString
                customer.userCreateDateTime = DateTime.Parse(row("userCreateDateTime")).Date
                customer.totalOrder = Integer.Parse(row("OrderNumber"))
                accumulatedOrderNumber += customer.totalOrder
                If Not IsDBNull(row("TotalSpent")) Then
                    customer.totalAmountSpent = Decimal.Parse(row("TotalSpent"))
                Else
                    customer.totalAmountSpent = 0
                End If
                accumulatedSpending += customer.totalAmountSpent
                customerList.Add(customer)
            Next

            customers.customerList = customerList
            If accumulatedOrderNumber <> 0 Then
                customers.averageOrderAmount = accumulatedSpending / accumulatedOrderNumber
            Else
                customers.averageOrderAmount = 0
            End If



            strSql = "Select count(distinct userID) from BookOrder Where Month(orderDateTime) = @month And Year(orderDateTime) = @year"
            searchCmd = New SqlCommand(strSql, db.conn)
            searchCmd.Parameters.AddWithValue("@month", month)
            searchCmd.Parameters.AddWithValue("@year", year)
            da.Fill(ds, "BookOrder")

            Dim activeCustomerNum = searchCmd.ExecuteScalar
            Dim totalCustomer = customers.customerList.Count
            customers.churnRate = (Decimal.Parse(totalCustomer - activeCustomerNum) / totalCustomer) * 100


            strSql = "Select count(userID) from UserAcc Where (Year(GETDATE())-Year(userBirthday)) < '18' And userRole = 'Buyer' And Year(userCreateDateTime) <= @year " +
                "And Month(userCreateDateTime) <= @month"
            searchCmd = New SqlCommand(strSql, db.conn)
            searchCmd.Parameters.AddWithValue("@month", month)
            searchCmd.Parameters.AddWithValue("@year", year)
            'da.Fill(ds, "UserAcc")

            customers.pBelow18 = Decimal.Parse((Integer.Parse(searchCmd.ExecuteScalar)) / totalCustomer) * 100

            strSql = "Select count(userID) from UserAcc Where (Year(GETDATE())-Year(userBirthday)) between '18' And '24' And userRole = 'Buyer' And Year(userCreateDateTime) <= @year " +
                "And Month(userCreateDateTime) <= @month"
            searchCmd = New SqlCommand(strSql, db.conn)
            searchCmd.Parameters.AddWithValue("@month", month)
            searchCmd.Parameters.AddWithValue("@year", year)

            customers.p18to24 = Decimal.Parse((Integer.Parse(searchCmd.ExecuteScalar)) / totalCustomer) * 100


            strSql = "Select count(userID) from UserAcc Where (Year(GETDATE())-Year(userBirthday)) between '25' And '34' And userRole = 'Buyer' And Year(userCreateDateTime) <= @year " +
                "And Month(userCreateDateTime) <= @month"
            searchCmd = New SqlCommand(strSql, db.conn)
            searchCmd.Parameters.AddWithValue("@month", month)
            searchCmd.Parameters.AddWithValue("@year", year)
            customers.p25to34 = Decimal.Parse((Integer.Parse(searchCmd.ExecuteScalar)) / totalCustomer) * 100

            strSql = "Select count(userID) from UserAcc Where (Year(GETDATE())-Year(userBirthday)) between '35' And '44' And userRole = 'Buyer' And Year(userCreateDateTime) <= @year " +
                "And Month(userCreateDateTime) <= @month"
            searchCmd = New SqlCommand(strSql, db.conn)
            searchCmd.Parameters.AddWithValue("@month", month)
            searchCmd.Parameters.AddWithValue("@year", year)
            customers.p35to44 = Decimal.Parse((Integer.Parse(searchCmd.ExecuteScalar)) / totalCustomer) * 100


            strSql = "Select count(userID) from UserAcc Where (Year(GETDATE())-Year(userBirthday)) between '45' And '54' And userRole = 'Buyer' And Year(userCreateDateTime) <= @year " +
                "And Month(userCreateDateTime) <= @month"
            searchCmd = New SqlCommand(strSql, db.conn)
            searchCmd.Parameters.AddWithValue("@month", month)
            searchCmd.Parameters.AddWithValue("@year", year)
            customers.p45to55 = Decimal.Parse((Integer.Parse(searchCmd.ExecuteScalar)) / totalCustomer) * 100

            strSql = "Select count(userID) from UserAcc Where (Year(GETDATE())-Year(userBirthday)) >= '55' And userRole = 'Buyer' And Year(userCreateDateTime) <= @year " +
                "And Month(userCreateDateTime) <= @month"
            searchCmd = New SqlCommand(strSql, db.conn)
            searchCmd.Parameters.AddWithValue("@month", month)
            searchCmd.Parameters.AddWithValue("@year", year)
            customers.pAbove55 = Decimal.Parse((Integer.Parse(searchCmd.ExecuteScalar)) / totalCustomer) * 100

            strSql = "Select count(userID) from UserAcc Where userGender = 'Male' And userRole = 'Buyer' And Year(userCreateDateTime) <= @year " +
                "And Month(userCreateDateTime) <= @month"
            searchCmd = New SqlCommand(strSql, db.conn)
            searchCmd.Parameters.AddWithValue("@month", month)
            searchCmd.Parameters.AddWithValue("@year", year)
            Dim maleNum = Integer.Parse(searchCmd.ExecuteScalar)
            Dim division = Decimal.Parse(maleNum) / totalCustomer
            customers.pMale = Decimal.Parse((Integer.Parse(searchCmd.ExecuteScalar)) / totalCustomer) * 100

            strSql = "Select count(userID) from UserAcc Where userGender = 'Female' And userRole = 'Buyer' And Year(userCreateDateTime) <= @year " +
                "And Month(userCreateDateTime) <= @month"
            searchCmd = New SqlCommand(strSql, db.conn)
            searchCmd.Parameters.AddWithValue("@month", month)
            searchCmd.Parameters.AddWithValue("@year", year)
            customers.pFemale = Decimal.Parse((Integer.Parse(searchCmd.ExecuteScalar)) / totalCustomer) * 100

            db.CloseConnection()



        End If

        Return customers
    End Function

    Public Function SearchDashboardInfo() As ModelLibrary.Dashboard
        Dim dashboardInfo As New ModelLibrary.Dashboard
        If db.OpenConnection() Then

            Dim strSql = "Select Count(bookID) From Book Where bookPublishedDateTime <= GETDATE()"
            Dim searchCmd As New SqlCommand(strSql, db.conn)
            dashboardInfo.totalBook = searchCmd.ExecuteScalar

            Dim lastMonBookNo As Integer
            strSql = "Select Count(bookID) from Book Where bookPublishedDateTime < DATEFROMPARTS(Year(GETDATE()), MONTH(GETDATE()), 1)"
            searchCmd = New SqlCommand(strSql, db.conn)
            lastMonBookNo = searchCmd.ExecuteScalar

            dashboardInfo.bookP = ((Decimal.Parse(dashboardInfo.totalBook - lastMonBookNo)) / dashboardInfo.totalBook) * 100


            strSql = "Select Count(userID) From UserAcc Where userRole = 'Buyer' And userCreateDateTime <= GETDATE()"
            searchCmd = New SqlCommand(strSql, db.conn)
            dashboardInfo.totalCustomer = searchCmd.ExecuteScalar

            Dim lastMonCustNo As Integer
            strSql = "Select Count(userID) from UserAcc Where userCreateDateTime < DATEFROMPARTS(Year(GETDATE()), MONTH(GETDATE()), 1) and userRole = 'Buyer'"
            searchCmd = New SqlCommand(strSql, db.conn)
            lastMonCustNo = searchCmd.ExecuteScalar

            dashboardInfo.customerP = ((Decimal.Parse(dashboardInfo.totalCustomer - lastMonCustNo)) / dashboardInfo.totalCustomer) * 100


            strSql = "Select Count(orderID) From BookOrder Where Month(orderDateTime) = Month(GETDATE())"
            searchCmd = New SqlCommand(strSql, db.conn)
            dashboardInfo.totalOrder = searchCmd.ExecuteScalar
            'Select Count(orderID) From BookOrder Where Month(orderDateTime) = (Month(GETDATE()))-1
            Dim lastMonOrderNo As Integer
            strSql = "Select Count(orderID) From BookOrder Where Month(orderDateTime) = (Month(GETDATE()))-1"
            searchCmd = New SqlCommand(strSql, db.conn)
            lastMonOrderNo = searchCmd.ExecuteScalar

            dashboardInfo.orderP = ((Decimal.Parse(dashboardInfo.totalOrder - lastMonOrderNo)) / dashboardInfo.totalOrder) * 100

            Dim intMonth As Integer = Date.Now.Month
            Dim intLastMonth As Integer = intMonth - 1
            Dim intLast2Month As Integer = intMonth - 2
            Dim intMonthList As Integer() = {intLast2Month, intLastMonth, intMonth}
            Dim i As Integer = 0
            dashboardInfo.monthList = New String(2) {}
            dashboardInfo.monthRevenueFiction = New Decimal(2) {}
            dashboardInfo.monthRevenueNonFiction = New Decimal(2) {}
            dashboardInfo.agePList = New Decimal(5) {}
            For Each mon In intMonthList

                Select Case mon
                    Case 1
                        dashboardInfo.monthList(i) = "JAN"
                    Case 2
                        dashboardInfo.monthList(i) = "FEB"
                    Case 3
                        dashboardInfo.monthList(i) = "MAR"
                    Case 4
                        dashboardInfo.monthList(i) = "APR"
                    Case 5
                        dashboardInfo.monthList(i) = "MAY"
                    Case 6
                        dashboardInfo.monthList(i) = "JUN"
                    Case 7
                        dashboardInfo.monthList(i) = "JUL"
                    Case 8
                        dashboardInfo.monthList(i) = "AUG"
                    Case 9
                        dashboardInfo.monthList(i) = "SEP"
                    Case 10
                        dashboardInfo.monthList(i) = "OCT"
                    Case 11
                        dashboardInfo.monthList(i) = "NOV"
                    Case 12
                        dashboardInfo.monthList(i) = "DEC"
                End Select
                '                Select Sum(orderSubtotal) From BookOrder Join OrderDetail On BookOrder.orderID = OrderDetail.orderID 
                'Join Book On OrderDetail.bookID = Book.bookID
                'Where MONTH(orderDateTime) = 7 And bookCategory = 'Fiction'
                

                strSql = "Select Sum(orderSubtotal) From BookOrder Join OrderDetail On BookOrder.orderID = OrderDetail.orderID " +
                    "Join Book On OrderDetail.bookID = Book.bookID " +
                    "Where MONTH(orderDateTime) = @month And bookCategory = 'Fiction'"
                searchCmd = New SqlCommand(strSql, db.conn)
                searchCmd.Parameters.AddWithValue("@month", mon)
                Dim result = searchCmd.ExecuteScalar
                If IsDBNull(result) Then
                    dashboardInfo.monthRevenueFiction(i) = 0.0
                Else
                    dashboardInfo.monthRevenueFiction(i) = result
                End If


                strSql = "Select Sum(orderSubtotal) From BookOrder Join OrderDetail On BookOrder.orderID = OrderDetail.orderID " +
                    "Join Book On OrderDetail.bookID = Book.bookID " +
                    "Where MONTH(orderDateTime) = @month And bookCategory = 'Non-Fiction'"
                searchCmd = New SqlCommand(strSql, db.conn)
                searchCmd.Parameters.AddWithValue("@month", mon)
                result = searchCmd.ExecuteScalar
                If IsDBNull(result) Then
                    dashboardInfo.monthRevenueNonFiction(i) = 0.0
                Else
                    dashboardInfo.monthRevenueNonFiction(i) = result
                End If

                i += 1
            Next


            strSql = "Select Count(userID) From UserAcc Where userRole = 'Buyer'"
            searchCmd = New SqlCommand(strSql, db.conn)
            Dim totalCustomer = searchCmd.ExecuteScalar

            strSql = "Select count(userID) from UserAcc Where (Year(GETDATE())-Year(userBirthday)) < '18' And userRole = 'Buyer'"
            searchCmd = New SqlCommand(strSql, db.conn)

            dashboardInfo.agePList(0) = Decimal.Parse((Integer.Parse(searchCmd.ExecuteScalar)) / totalCustomer) * 100

            strSql = "Select count(userID) from UserAcc Where (Year(GETDATE())-Year(userBirthday)) between '18' And '24' And userRole = 'Buyer'"
            searchCmd = New SqlCommand(strSql, db.conn)

            dashboardInfo.agePList(1) = Decimal.Parse((Integer.Parse(searchCmd.ExecuteScalar)) / totalCustomer) * 100


            strSql = "Select count(userID) from UserAcc Where (Year(GETDATE())-Year(userBirthday)) between '25' And '34' And userRole = 'Buyer'"
            searchCmd = New SqlCommand(strSql, db.conn)

            dashboardInfo.agePList(2) = Decimal.Parse((Integer.Parse(searchCmd.ExecuteScalar)) / totalCustomer) * 100

            strSql = "Select count(userID) from UserAcc Where (Year(GETDATE())-Year(userBirthday)) between '35' And '44' And userRole = 'Buyer'"
            searchCmd = New SqlCommand(strSql, db.conn)
            dashboardInfo.agePList(3) = Decimal.Parse((Integer.Parse(searchCmd.ExecuteScalar)) / totalCustomer) * 10


            strSql = "Select count(userID) from UserAcc Where (Year(GETDATE())-Year(userBirthday)) between '45' And '54' And userRole = 'Buyer'"
            searchCmd = New SqlCommand(strSql, db.conn)
            dashboardInfo.agePList(4) = Decimal.Parse((Integer.Parse(searchCmd.ExecuteScalar)) / totalCustomer) * 100

            strSql = "Select count(userID) from UserAcc Where (Year(GETDATE())-Year(userBirthday)) >= '55' And userRole = 'Buyer'"
            searchCmd = New SqlCommand(strSql, db.conn)
            dashboardInfo.agePList(5) = Decimal.Parse((Integer.Parse(searchCmd.ExecuteScalar)) / totalCustomer) * 100



            db.CloseConnection()
        End If

        Return dashboardInfo
    End Function

    Public Function SearchSearchEngineResultAdmin(search As String) As Object
        'Dim returnObject As Object
        Dim books As New List(Of ModelLibrary.Book)()
        Dim users As New List(Of ModelLibrary.UserAcc)()
        If db.OpenConnection() Then

            If search.Substring(0, 1) = """" And search.Substring(search.Length - 1, 1) = """" Then
                Dim strSearch = "Select * From Book " &
                        "Where UPPER(bookTitle) = UPPER(@search) Or UPPER(bookAuthor) = UPPER(@search) "
                Dim searchCmd As New SqlCommand(strSearch, db.conn)
                Dim length = search.Length - 2
                searchCmd.Parameters.AddWithValue("@search", search.Substring(1, length))
                Dim da As New SqlDataAdapter(searchCmd)
                Dim ds As New DataSet()
                da.Fill(ds, "Book")
                Dim recordNum As Integer = 0
                For Each row As DataRow In ds.Tables("Book").Rows
                    Dim book As New ModelLibrary.Book()
                    book.bookID = Convert.ToInt32(row("bookID"))
                    book.bookTitle = row("bookTitle").ToString()
                    book.bookAuthor = row("bookAuthor").ToString()
                    book.bookImg = row("bookImg").ToString()

                    books.Add(book)
                    recordNum += 1
                Next

                'Select * From UserAcc
                'Where UPPER(REPLACE(userFName+userLName, ' ','')) = UPPER(REPLACE('suzybae',' ','')) Or (ISNUMERIC('suzybae') = 1 and userID = CAST('suzybae' As Int))

                'try with search user by ""
                If recordNum = 0 Then
                    strSearch = "Select * From UserAcc " &
                        "Where UPPER(REPLACE(userFName+userLName, ' ','')) = UPPER(REPLACE(@search,' ',''))"
                    '  "Where UPPER(REPLACE(userFName+userLName, ' ','')) = UPPER(REPLACE(@search,' ','')) Or (ISNUMERIC(@search) = 1 and userID = CAST(@search As Int))"

                    searchCmd = New SqlCommand(strSearch, db.conn)
                    'length = search.Length - 2
                    searchCmd.Parameters.AddWithValue("@search", search.Substring(1, length))
                    da = New SqlDataAdapter(searchCmd)
                    ds = New DataSet()
                    da.Fill(ds, "UserAcc")
                    For Each row As DataRow In ds.Tables("UserAcc").Rows
                        Dim userAcc As New ModelLibrary.UserAcc
                        userAcc.userID = Convert.ToInt32(row("userID"))
                        userAcc.userFname = row("userFname").ToString()
                        userAcc.userLname = row("userLname").ToString()
                        userAcc.userEmail = row("userEmail").ToString
                        userAcc.userCreateDateTime = DateTime.Parse(row("userCreateDateTime"))
                        userAcc.userRole = row("userRole").ToString()
                        userAcc.userStatus = row("userStatus").ToString()

                        users.Add(userAcc)
                    Next
                End If

            ElseIf search.StartsWith("book:") Then
                search = search.Substring(5).Trim
                Dim strSearch = "Select * From Book " &
                        "Where UPPER(bookTitle) LIKE '%' + UPPER(@search) + '%' "
                Dim searchCmd As New SqlCommand(strSearch, db.conn)
                searchCmd.Parameters.AddWithValue("@search", search)
                Dim da As New SqlDataAdapter(searchCmd)
                Dim ds As New DataSet()
                da.Fill(ds, "Book")

                For Each row As DataRow In ds.Tables("Book").Rows
                    Dim book As New ModelLibrary.Book()
                    book.bookID = Convert.ToInt32(row("bookID"))
                    book.bookTitle = row("bookTitle").ToString()
                    book.bookAuthor = row("bookAuthor").ToString()
                    book.bookImg = row("bookImg").ToString()

                    books.Add(book)
                Next

            ElseIf search.StartsWith("author:") Then
                search = search.Substring(7).Trim
                Dim strSearch = "Select * From Book " &
                        "Where UPPER(bookAuthor) LIKE '%' + UPPER(@search) + '%' "
                Dim searchCmd As New SqlCommand(strSearch, db.conn)
                searchCmd.Parameters.AddWithValue("@search", search)
                Dim da As New SqlDataAdapter(searchCmd)
                Dim ds As New DataSet()
                da.Fill(ds, "Book")

                For Each row As DataRow In ds.Tables("Book").Rows
                    Dim book As New ModelLibrary.Book()
                    book.bookID = Convert.ToInt32(row("bookID"))
                    book.bookTitle = row("bookTitle").ToString()
                    book.bookAuthor = row("bookAuthor").ToString()
                    book.bookImg = row("bookImg").ToString()

                    books.Add(book)
                Next

            ElseIf search.StartsWith("user:") Then
                search = search.Substring(5).Trim
                Dim strSearch = "Select * From UserAcc " &
                        "Where UPPER(userFname) LIKE '%' + UPPER(@search) + '%' OR UPPER(userLname) LIKE '%' + UPPER(@search) + '%'"
                Dim searchCmd As New SqlCommand(strSearch, db.conn)
                searchCmd.Parameters.AddWithValue("@search", search)
                Dim da As New SqlDataAdapter(searchCmd)
                Dim ds As New DataSet()
                da.Fill(ds, "UserAcc")

                For Each row As DataRow In ds.Tables("UserAcc").Rows
                    Dim userAcc As New ModelLibrary.UserAcc
                    userAcc.userID = Convert.ToInt32(row("userID"))
                    userAcc.userFname = row("userFname").ToString()
                    userAcc.userLname = row("userLname").ToString()
                    userAcc.userEmail = row("userEmail").ToString
                    userAcc.userCreateDateTime = DateTime.Parse(row("userCreateDateTime"))
                    userAcc.userRole = row("userRole").ToString()
                    userAcc.userStatus = row("userStatus").ToString()

                    users.Add(userAcc)
                Next

            ElseIf search.StartsWith("RM") And search.Contains("..") Then
                Dim lower As Decimal = search.Substring(2, search.IndexOf("..") - 2)
                Dim higher As Decimal = search.Substring(search.IndexOf("..") + 2)

                Dim strSearch = "Select * From Book " &
                    "Where bookPrice Between @lower And @higher"

                Dim searchCmd As New SqlCommand(strSearch, db.conn)
                searchCmd.Parameters.AddWithValue("@lower", lower)
                searchCmd.Parameters.AddWithValue("@higher", higher)
                Dim da As New SqlDataAdapter(searchCmd)
                Dim ds As New DataSet()
                da.Fill(ds, "Book")

                For Each row As DataRow In ds.Tables("Book").Rows
                    Dim book As New ModelLibrary.Book()
                    book.bookID = Convert.ToInt32(row("bookID"))
                    book.bookTitle = row("bookTitle").ToString()
                    book.bookAuthor = row("bookAuthor").ToString()
                    book.bookImg = row("bookImg").ToString()

                    books.Add(book)
                Next
            Else
                Dim strSearch = "Select * From Book " &
                        "Where UPPER(bookTitle) LIKE '%' + UPPER(@search) + '%' Or UPPER(bookAuthor) LIKE '%' + UPPER(@search) + '%' "

                Dim searchCmd As New SqlCommand(strSearch, db.conn)
                searchCmd.Parameters.AddWithValue("@search", search)
                Dim da As New SqlDataAdapter(searchCmd)
                Dim ds As New DataSet()
                da.Fill(ds, "Book")
                Dim recordNum As Integer = 0
                For Each row As DataRow In ds.Tables("Book").Rows
                    Dim book As New ModelLibrary.Book()
                    book.bookID = Convert.ToInt32(row("bookID"))
                    book.bookTitle = row("bookTitle").ToString()
                    book.bookAuthor = row("bookAuthor").ToString()
                    book.bookImg = row("bookImg").ToString()

                    books.Add(book)
                    recordNum += 1
                Next

                If recordNum = 0 Then
                    strSearch = "Select * From UserAcc " &
                        "Where UPPER(userFname) LIKE '%' + UPPER(@search) + '%' OR UPPER(userLname) LIKE '%' + UPPER(@search) + '%' " &
                    "OR UPPER(REPLACE(userFName+userLName, ' ','')) = UPPER(REPLACE(@search,' ',''))"
                    searchCmd = New SqlCommand(strSearch, db.conn)
                    searchCmd.Parameters.AddWithValue("@search", search)
                    da = New SqlDataAdapter(searchCmd)
                    ds = New DataSet()
                    da.Fill(ds, "UserAcc")

                    For Each row As DataRow In ds.Tables("UserAcc").Rows
                        Dim userAcc As New ModelLibrary.UserAcc
                        userAcc.userID = Convert.ToInt32(row("userID"))
                        userAcc.userFname = row("userFname").ToString()
                        userAcc.userLname = row("userLname").ToString()
                        userAcc.userEmail = row("userEmail").ToString
                        userAcc.userCreateDateTime = DateTime.Parse(row("userCreateDateTime"))
                        userAcc.userRole = row("userRole").ToString()
                        userAcc.userStatus = row("userStatus").ToString()

                        users.Add(userAcc)
                    Next
                End If
            End If

            db.CloseConnection()

        End If

        If books.Count > 0 And users.Count < 1 Then
            Return books

        ElseIf users.Count > 0 And books.Count < 1 Then
            Return users
        Else
            Return Nothing
        End If

    End Function
End Class
