Imports System.IO
Imports System.Threading.Tasks
Imports System.Web.Script.Serialization

Public Class HomeAdminController
    Inherits System.Web.Mvc.Controller

    '
    ' GET: /Home
    Private ReadOnly dalUserAcc As dalInternalClassLib.UserAccount

    Public Sub New()
        dalUserAcc = New dalInternalClassLib.UserAccount
    End Sub

    Public Function Index() As ActionResult
        Dim booklist As List(Of ModelLibrary.Book)
        If TempData("BooksData") IsNot Nothing Then
            ViewBag.DefaultCategory = "-"
            booklist = TempData("BooksData")
        Else
            booklist = dalUserAcc.SearchNewArrival
        End If
        Return View(booklist)
    End Function

    Public Function GetBooksByCategory(category As String) As ActionResult
        ' Retrieve the data based on the selected category
        Dim books As IEnumerable(Of ModelLibrary.Book)
        If category = "NewArrival" Then
            books = dalUserAcc.SearchNewArrival
        ElseIf category = "HotPick" Then
            books = dalUserAcc.SearchHotPick
        ElseIf category = "Fiction" Then
            books = dalUserAcc.SearchFiction
        ElseIf category = "NonFiction" Then
            books = dalUserAcc.SearchNonFiction
        End If
        ' Return the partial view with the updated data
        Return PartialView("_BookTablePartial", books)
        'Return View("Index", books)
    End Function


    Public Function DeleteBook(ByVal id As Integer) As JsonResult
        Try
            Console.WriteLine("DeleteBook action hit. Book ID: " + id.ToString())

            Dim deleted As Boolean = dalUserAcc.DeleteBook(id)
            If deleted Then
                Dim description = "1 book with book ID: " + id.ToString + " is deleted"
                dalUserAcc.InsertActionLog(Session("UserID"), DateTime.Now, description)

                Return Json(New With {.success = True, .message = "The selected book is deleted"}, JsonRequestBehavior.AllowGet)
            Else
                Return Json(New With {.success = False, .message = "The selected book is not deleted, may due to the book has been ordered"}, JsonRequestBehavior.AllowGet)

            End If

        Catch ex As Exception
            Return Json(New With {.success = False, .message = "The selected book is not deleted"}, JsonRequestBehavior.AllowGet)
        End Try
    End Function

    Public Function EditBook() As ActionResult
        Dim ID = Request.QueryString("bookID")
        Dim book As ModelLibrary.Book
        book = dalUserAcc.SearchBookDetail(ID)
        Return View(book)
    End Function

    Public Function SubmitUpdate(ByVal form As System.Web.Mvc.FormCollection, imageFile As HttpPostedFileBase) As ActionResult

        Dim fileName As String
        Dim image As String = String.Empty
        ' Check if an image file was uploaded
        If imageFile IsNot Nothing AndAlso imageFile.ContentLength > 0 Then
            Try
                ' Get the file name
                fileName = Path.GetFileName(imageFile.FileName)
                image = "Image/" + fileName
                ' Specify the path where you want to save the image (e.g., "images" folder)
                Dim pathToSave As String = Path.Combine(Server.MapPath("~/image/"), fileName)

                ' Save the image file to the specified path
                imageFile.SaveAs(pathToSave)

                ' You can then use the 'pathToSave' variable to store the image path in the database or elsewhere as needed.

            Catch ex As Exception
                ' Handle any exceptions that may occur during the file upload process
                ' You may want to show an error message to the user or log the error for troubleshooting.
            End Try
        End If


        Dim bookID As Integer = form("bookID")
        Dim title As String = form("title")
        Dim author As String = form("author")
        Dim category As String = form("category")
        Dim status As String = form("status")
        Dim quantity As Integer = Integer.Parse(form("quantity"))
        Dim publisher As String = form("publisher")
        Dim weight As Decimal = Decimal.Parse(form("weight"))
        Dim des As String = form("des")
        Dim price As Decimal = Decimal.Parse(form("price"))

        If bookID <> 0 Then
            Try
                Dim success = dalUserAcc.UpdateBookDetail(bookID, title, author, category, status, quantity, publisher, weight, image, des, price)

                If success Then
                    TempData("UpdateResult") = "Book updated!"
                    Dim description = "1 book with title: " + title + " is updated"
                    dalUserAcc.InsertActionLog(Session("UserID"), DateTime.Now, description)
                Else
                    TempData("UpdateResult") = "Updating process failed!"
                End If
            Catch ex As Exception
                TempData("UpdateResult") = ex
            End Try
        Else
            Try
                Dim success = dalUserAcc.InsertBook(title, author, category, status, quantity, publisher, weight, image, des, price)

                If success Then
                    TempData("UpdateResult") = "Book created!"
                    Dim description = "1 book with title: " + title + " is created"
                    dalUserAcc.InsertActionLog(Session("UserID"), DateTime.Now, description)

                Else
                    TempData("UpdateResult") = "Creating process failed!"
                End If
            Catch ex As Exception
                TempData("UpdateResult") = ex
            End Try
        End If



        Return RedirectToAction("Index", "HomeAdmin")

    End Function

    Function UserManagement() As ActionResult
        Dim userlist As List(Of ModelLibrary.UserAcc)
        If TempData("UsersData") Is Nothing Then
            userlist = dalUserAcc.SearchUserList
            Return View(userlist)
        Else
            userlist = TempData("UsersData")
            Return View(userlist)
        End If
    End Function

    Function ActivateUser(id As Integer) As JsonResult
        Try
            dalUserAcc.UpdateStatusToActive(id)
            Dim description = "1 user with user ID: " + id.ToString + " is activated"
            dalUserAcc.InsertActionLog(Session("UserID"), DateTime.Now, description)
            Return Json(New With {.success = True, .message = "The selected user is activated"}, JsonRequestBehavior.AllowGet)
        Catch ex As Exception
            Return Json(New With {.success = False, .message = "The selected book is failed to be activated"}, JsonRequestBehavior.AllowGet)
        End Try
    End Function

    Function DisableUser(id As Integer) As JsonResult
        Try
            dalUserAcc.UpdateStatusToDisable(id)
            Dim description As String = "1 user with user ID: " + id.ToString + " is disabled"
            dalUserAcc.InsertActionLog(Session("UserID"), DateTime.Now, description)
            Return Json(New With {.success = True, .message = "The selected user is disabled"}, JsonRequestBehavior.AllowGet)
        Catch ex As Exception
            Return Json(New With {.success = False, .message = "The selected book is failed to be disabled"}, JsonRequestBehavior.AllowGet)
        End Try
    End Function

    Function Escalate(id As Integer) As JsonResult
        Try
            dalUserAcc.UpdateRoleToAdmin(id)
            Dim description = "1 user with user ID: " + id.ToString + " is escalated to admin"
            dalUserAcc.InsertActionLog(Session("UserID"), DateTime.Now, description)
            Return Json(New With {.success = True, .message = "The selected user is escalated to admin"}, JsonRequestBehavior.AllowGet)
        Catch ex As Exception
            Return Json(New With {.success = False, .message = "The selected book is failed to be escalated"}, JsonRequestBehavior.AllowGet)
        End Try
    End Function

    Function Downgrade(id As Integer) As JsonResult
        Try
            dalUserAcc.UpdateRoleToBuyer(id)
            Dim description = "1 user with user ID: " + id.ToString + " is demoted from admin"
            dalUserAcc.InsertActionLog(Session("UserID"), DateTime.Now, description)
            Return Json(New With {.success = True, .message = "The selected user is demoted from admin"}, JsonRequestBehavior.AllowGet)
        Catch ex As Exception
            Return Json(New With {.success = False, .message = "The selected book is failed to be demoted"}, JsonRequestBehavior.AllowGet)
        End Try
    End Function

    Public Function DeleteUser(ByVal id As Integer) As JsonResult
        Try
            dalUserAcc.DeleteUser(id)
            Dim description = "1 user with user ID: " + id.ToString + " is deleted"
            dalUserAcc.InsertActionLog(Session("UserID"), DateTime.Now, description)
            Return Json(New With {.success = True, .message = "The selected user is deleted"}, JsonRequestBehavior.AllowGet)
        Catch ex As Exception
            Return Json(New With {.success = False, .message = "The selected user is not deleted"}, JsonRequestBehavior.AllowGet)
        End Try
    End Function

    Public Function MyAccount() As ActionResult
        If Not (Session("UserID") Is Nothing) Then
            Return View(dalUserAcc.SearchAccountInfo(Session("UserID")))
        Else
            TempData("AlertMessage") = "Please login first"
            Return RedirectToAction("Login", "Home")
        End If

    End Function

    Function ActionHistory() As ActionResult
        Dim actionLog As List(Of ModelLibrary.ActionLog)
        actionLog = dalUserAcc.SearchActionLog(Session("UserID"))
        Return View(actionLog)
    End Function

    Public Function SearchAutocomplete(ByVal term As String) As ActionResult
        Dim books As List(Of ModelLibrary.Book) = GetBooksForAutocomplete(term)
        Dim bookTitles As List(Of String) = books.Select(Function(b) b.bookTitle).ToList()
        Return Json(bookTitles, JsonRequestBehavior.AllowGet)
    End Function

    Private Function GetBooksForAutocomplete(ByVal term As String) As List(Of ModelLibrary.Book)

        Dim books As New List(Of ModelLibrary.Book)()
        books = dalUserAcc.SearchSearchEngineResult(term)

        Return books
    End Function

    Public Function SearchBookIDByTerm(term As String) As JsonResult
        Console.WriteLine("DeleteBook action hit. Book ID: " + term.ToString())
        Dim book As ModelLibrary.Book
        Dim bookID As Integer
        Try
            book = dalUserAcc.SearchSearchEngineResult(term).First
            bookID = book.bookID

        Catch ex As Exception
            Return Json(New With {Key .success = False}, JsonRequestBehavior.AllowGet)
        End Try

        Return Json(New With {Key .success = True, .bookID = bookID}, JsonRequestBehavior.AllowGet)
    End Function

    Public Function SearchBookUserByTerm(term As String) As ActionResult
        'Console.WriteLine("DeleteBook action hit. Book ID: " + term.ToString())
        Dim searchResult As Object
        Dim book As ModelLibrary.Book
        'Dim bookID As Integer

        Try
            searchResult = dalUserAcc.SearchSearchEngineResultAdmin(term)
            If TypeOf searchResult Is List(Of ModelLibrary.Book) Then
                TempData("BooksData") = searchResult
                Return RedirectToAction("Index", "HomeAdmin")
            ElseIf TypeOf searchResult Is List(Of ModelLibrary.UserAcc) Then
                TempData("UsersData") = searchResult
                Return RedirectToAction("UserManagement", "HomeAdmin")
            Else
                Return Redirect(Request.UrlReferrer.ToString())
            End If
            ' book = dalUserAcc.SearchSearchEngineResultAdmin(term)
            'bookID = book.bookID

        Catch ex As Exception
            'Return Json(New With {Key .success = False}, JsonRequestBehavior.AllowGet)
        End Try


    End Function

    Function Login() As ActionResult
        Return View()
    End Function

    <HttpPost>
    Public Function ValidateLogin(ByVal userData As ModelLibrary.UserAcc) As ActionResult

        'Return RedirectToAction("UserManagement", "HomeAdmin")
        Dim found As Boolean = False
        Dim active As Boolean = False
        Dim admin As Boolean = False
        Dim result = dalUserAcc.SearchLoginInfo(userData)

        If result(0) = "y" Then
            found = True
        End If
        If result(1) = "y" Then
            active = True
        End If
        If result(2) = "y" Then
            admin = True
        End If

        If found And active And admin Then
            Session("UserID") = result(3)
            TempData("LoginResult") = "Welcome Back, " + result(4) + "!"
        ElseIf found And Not active Then
            TempData("LoginResult") = "Sorry, your account has been disabled. Please contact with the admin!"
        ElseIf found And Not admin Then
            TempData("LoginResult") = "Sorry, you have no privilege!"
        End If

        Return Json(New With {Key .isValid = found, .active = active, .admin = admin, .loginResult = TempData("LoginResult")}, JsonRequestBehavior.AllowGet)
    End Function

    Public Function Logout() As ActionResult

        Dim isLogout As Boolean = False
        Try
            FormsAuthentication.SignOut()
            Session.Abandon()
            isLogout = True
        Catch ex As Exception

        End Try

        Return Json(New With {Key .success = isLogout})

    End Function

    Public Function Loading() As ActionResult
        Return View()
    End Function

    Public Function Sales() As ActionResult
        Dim bookSales As List(Of ModelLibrary.Sales)

        bookSales = dalUserAcc.SearchSales(Month(DateTime.Now), Year(DateTime.Now))

        Return View(bookSales)
    End Function

    Public Function GetMonthlySales(monthYear As String) As ActionResult
        Try
            Dim month As Integer = Integer.Parse(monthYear.Substring(5, 2))
            Dim year As Integer = Integer.Parse(monthYear.Substring(0, 4))
            Dim sales As IEnumerable(Of ModelLibrary.Sales)
            sales = dalUserAcc.SearchSales(month, year)

            'Dim partialView1 = RenderPartialViewToString("SalesTablePartial", sales)
            'Dim partialView2 = RenderPartialViewToString("_SalesReportPdf", sales)
            'Dim combinedViews = partialView1 & partialView2

            'Return Content(combinedViews)
            Return PartialView("SalesTablePartial", sales)

        Catch ex As Exception
            Return View()
        End Try

    End Function

    'Private Function RenderPartialViewToString(viewName As String, model As Object) As String
    '    ViewData.Model = model

    '    Using sw As New StringWriter()
    '        Dim viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName)
    '        Dim viewContext = New ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw)
    '        viewResult.View.Render(viewContext, sw)

    '        Return sw.ToString()
    '    End Using
    'End Function

    Public Function Customers() As ActionResult
        Dim customerList As ModelLibrary.Customers

        customerList = dalUserAcc.SearchCustomers(Month(DateTime.Now), Year(DateTime.Now))

        Return View(customerList)
    End Function

    Public Function GetMonthlyCustomers(monthYear As String) As ActionResult
        Try
            Dim month As Integer = Integer.Parse(monthYear.Substring(5, 2))
            Dim year As Integer = Integer.Parse(monthYear.Substring(0, 4))
            Dim customers As ModelLibrary.Customers
            customers = dalUserAcc.SearchCustomers(month, year)

            Return PartialView("CustomersTablePartial", customers)

        Catch ex As Exception
            Return View()
        End Try

    End Function

    Public Function TestPdf() As ActionResult
        Return View()
    End Function

    Public Function Dashboard() As ActionResult

        Dim dashboardInfo As ModelLibrary.Dashboard
        dashboardInfo = dalUserAcc.SearchDashboardInfo

        Dim serializer As New JavaScriptSerializer()
        Dim monthListJson = serializer.Serialize(dashboardInfo.monthList) ' Serialize the array
        Dim monthRevenueFictionJson = serializer.Serialize(dashboardInfo.monthRevenueFiction)
        Dim monthRevenueNonFictionJson = serializer.Serialize(dashboardInfo.monthRevenueNonFiction)
        Dim agePListJson = serializer.Serialize(dashboardInfo.agePList)

        ViewBag.MonthListJson = monthListJson
        ViewBag.MonthRevenueFictionJson = monthRevenueFictionJson
        ViewBag.MonthRevenueNonFictionJson = monthRevenueNonFictionJson
        ViewBag.AgePListJson = agePListJson

        Return View(dashboardInfo)
    End Function
End Class