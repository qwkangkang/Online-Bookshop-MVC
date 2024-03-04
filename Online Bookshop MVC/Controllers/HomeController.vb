Imports System.Data.SqlClient
Imports System.Threading.Tasks
Imports Microsoft.Owin.Security
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin.Host.SystemWeb
Imports Microsoft.AspNet.Identity.EntityFramework
Imports System.Web
Imports Microsoft.Owin
Imports System.Web.Mvc
Imports System.Security.Claims
Imports System.Web.Script.Serialization

Public Class HomeController
    Inherits System.Web.Mvc.Controller
    '
    ' GET: /Home
    Private db As New DatabaseHelper()

    Private ReadOnly dalUserAcc As dalInternalClassLib.UserAccount

    Public Sub New()
        dalUserAcc = New dalInternalClassLib.UserAccount
    End Sub

    Function Login() As ActionResult
        'Dim userData As New UserAcc()
        Dim userData As New ModelLibrary.UserAcc
        'Dim userData = libModelUserAcc

        If Not RetrieveUserEmailByRememberMeToken() = "" Then
            userData.userEmail = RetrieveUserEmailByRememberMeToken()
        End If

        Return View(userData)
    End Function

    Private Function RetrieveUserEmailByRememberMeToken() As String
        Dim httpContext As HttpContext = System.Web.HttpContext.Current
        If httpContext.Request.Cookies("RememberMe") IsNot Nothing Then
            Dim rememberMeToken As String = httpContext.Request.Cookies("RememberMe").Value

            ' Split the token into username and token parts
            Dim tokenParts As String() = rememberMeToken.Split(":"c)
            If tokenParts.Length = 2 Then
                Dim email As String = tokenParts(0)

                Return email
            End If

        End If
        Return ""
    End Function

    Public Function ExternalLogin(provider As String, Optional returnUrl As String = Nothing) As ActionResult

        Dim redirectUri As String = "http://localhost:2993/Home/ExternalLoginCallback"
        ' Request a redirect to the external login provider
        'Return New ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Home", New With {.ReturnUrl = returnUrl}))
        Return New ChallengeResult(provider, redirectUri, returnUrl)
    End Function

    Public Async Function ExternalLoginCallback(ByVal returnUrl As String) As Task(Of ActionResult)

        Dim authenticationManager As IAuthenticationManager
        Dim loginInfo As ExternalLoginInfo
        Try
            authenticationManager = HttpContext.GetOwinContext().Authentication
            'loginInfo = Await AuthenticationManager_GetExternalLoginInfoAsync_WithExternalBearer()
            loginInfo = Await authenticationManager.GetExternalLoginInfoAsync
        Catch ex As Exception
            Dim errorMessage As String = ex.Message
            TempData("ErrorMessage") = errorMessage
            Return RedirectToAction("Login")
        End Try
      
        'Dim loginInfo = Await authenticationManager.GetExternalIdentityAsync(DefaultAuthenticationTypes.ExternalCookie)
        Dim googleAccessToken As String = Nothing
        Dim googleRefreshToken As String = Nothing

        'This line retrieves the external identity associated with the external authentication provider 
        '(in this case, Google) from the Owin context
        'Dim externalIdentity = Await HttpContext.GetOwinContext().Authentication.GetExternalIdentityAsync(DefaultAuthenticationTypes.ExternalCookie)
        'Dim externalIdentity = Await authenticationManager.GetExternalIdentityAsync(DefaultAuthenticationTypes.ExternalCookie)

        'If externalIdentity IsNot Nothing Then
        'This line retrieves the Google access token from the external identity. It uses the FindFirstValue method to search for the claim with the specified 
        'type ("urn:google:accesstoken") and retrieves its value.
        'googleAccessToken = externalIdentity.FindFirstValue("urn:google:accesstoken")
        'This line retrieves the Google refresh token from the external identity. Similar to the previous line, it searches for 
        'the claim with the specified type ("urn:google:refresh_token") and retrieves its value
        'googleRefreshToken = externalIdentity.FindFirstValue("urn:google:refresh_token")

        'End If


        If loginInfo Is Nothing Then
            Return RedirectToAction("Login")
        End If


        'compare the login email, if new user, save it in database
        'Dim userData As New UserAcc()
        'userData.userEmail = externalIdentity.FindFirstValue(ClaimTypes.Email)
        'If Not ValidateExternalLogin(userData) Then
        '    userData.userFname = externalIdentity.FindFirstValue(ClaimTypes.Surname)
        '    userData.userLname = externalIdentity.FindFirstValue(ClaimTypes.GivenName)
        '    userData.userBirthday = externalIdentity.FindFirstValue(ClaimTypes.DateOfBirth)
        '    userData.userGender = externalIdentity.FindFirstValue(ClaimTypes.Gender)
        '    StoreExternalLoginCredential(userData)
        'End If



        Dim userManager As ApplicationUserManager = HttpContext.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
        Dim signInManager As SignInManager(Of ApplicationUser, String) = New SignInManager(Of ApplicationUser, String)(userManager, authenticationManager)
        ' Check if the user is already registered and sign them in
        Dim result = Await signInManager.ExternalSignInAsync(loginInfo, isPersistent:=False)

        If result = SignInStatus.Success Then
            Return RedirectToLocal(returnUrl)
        ElseIf result = SignInStatus.LockedOut Then
            'Return View("Lockout")
            Return View("Index")
        Else
            ' If the user is not registered, you can handle the registration process or show a registration form
            ViewBag.ReturnUrl = returnUrl
            ViewBag.LoginProvider = loginInfo.Login.LoginProvider
            Return View("ExternalLoginConfirmation", New ExternalLoginConfirmationViewModel() With {
                .Email = loginInfo.Email
            })
        End If
    End Function

    Private Async Function AuthenticationManager_GetExternalLoginInfoAsync_WithExternalBearer() As Task(Of ExternalLoginInfo)
        Dim loginInfo As ExternalLoginInfo = Nothing
        Dim authenticationManager As IAuthenticationManager = HttpContext.GetOwinContext().Authentication
        Dim result = Await authenticationManager.AuthenticateAsync(DefaultAuthenticationTypes.ExternalBearer)

        If result IsNot Nothing AndAlso result.Identity IsNot Nothing Then
            Dim idClaim = result.Identity.FindFirst(ClaimTypes.NameIdentifier)
            If idClaim IsNot Nothing Then
                loginInfo = New ExternalLoginInfo() With {
                    .DefaultUserName = If(result.Identity.Name Is Nothing, "", result.Identity.Name.Replace(" ", "")),
                    .Login = New UserLoginInfo(idClaim.Issuer, idClaim.Value)
                }
            End If
        End If

        Return loginInfo
    End Function

    Private Function RedirectToLocal(returnUrl As String) As ActionResult
        If Url.IsLocalUrl(returnUrl) Then
            Return Redirect(returnUrl)
        Else
            Return RedirectToAction("Index", "Home")
        End If
    End Function

    Function Register() As ActionResult

        Return View()
    End Function

    Function Search() As ActionResult
        Dim books As List(Of ModelLibrary.Book) = GetBooksForSearching()

        Return View(books)
    End Function


    <HttpPost>
    Function SubmitRegister(ByVal form As System.Web.Mvc.FormCollection) As ActionResult
        Dim fname As String = form("fname")
        Dim lname As String = form("lname")
        Dim birthday As Date = form("birthday")
        Dim gender As String = form("gender")
        Dim email As String = form("email")
        Dim password As String = form("password")
        Dim dateTimeCreated As DateTime = DateTime.Now

        Try
            Dim success = dalUserAcc.InsertUserAcc(fname, lname, email, password, birthday, gender, dateTimeCreated)

            If success Then
                TempData("RegisterResult") = "Signed up successful. Please login first."
            Else
                TempData("RegisterResult") = "Error connecting to database server."
                Return RedirectToAction("Register", "Home")
            End If


        Catch ex As Exception
            TempData("RegisterResult") = ex.Message.ToString()
        End Try

        Return RedirectToAction("Login", "Home")
    End Function

    <HandleError>
    Function Index() As ActionResult
        ' Retrieve data from the database
        Dim newArrivalBooks As List(Of ModelLibrary.Book)
        Try
            newArrivalBooks = GetNewArrivalBooksFromDatabase()
        Catch ex As Exception
            newArrivalBooks = Nothing
        End Try

        Dim hotPickBooks As List(Of ModelLibrary.Book) = GetHotPickBooksFromDatabase()
        Dim fictionBooks As List(Of ModelLibrary.Book) = GetFictionBooksFromDatabase()
        Dim nonFictionBooks As List(Of ModelLibrary.Book) = GetNonFictionBooksFromDatabase()

        ' Create a ViewModel to pass multiple lists of books to the view
        Dim viewModel As New ModelLibrary.HomeViewModel
        viewModel.NewArrivalBooks = newArrivalBooks
        viewModel.HotPickBooks = hotPickBooks
        viewModel.FictionBooks = fictionBooks
        viewModel.NonFictionBooks = nonFictionBooks

        Return View(viewModel)
    End Function


    Private Function GetNewArrivalBooksFromDatabase() As List(Of ModelLibrary.Book)      
        Return dalUserAcc.SearchTop4NewArrival
    End Function

    Private Function GetHotPickBooksFromDatabase() As List(Of ModelLibrary.Book)
        Return dalUserAcc.SearchTop4HotPick
    End Function

    Private Function GetFictionBooksFromDatabase() As List(Of ModelLibrary.Book)
        Return dalUserAcc.SearchTop4Fiction
    End Function

    Private Function GetNonFictionBooksFromDatabase() As List(Of ModelLibrary.Book)
        Return dalUserAcc.SearchTop4NonFiction
    End Function

    Function NewArrival() As ActionResult
        Dim books As List(Of ModelLibrary.Book) = dalUserAcc.SearchNewArrival
        Return View(books)
    End Function

    Function HotPick() As ActionResult
        Dim books As List(Of ModelLibrary.Book) = dalUserAcc.SearchHotPick

        Return View(books)
    End Function

    Function Fiction() As ActionResult
        Dim books As List(Of ModelLibrary.Book) = dalUserAcc.SearchFiction

        Return View(books)
    End Function

    Function NonFiction() As ActionResult
        Dim books As List(Of ModelLibrary.Book) = dalUserAcc.SearchNonFiction

        Return View(books)
    End Function

    Public Function BookDetail(ByVal ID As Integer) As ActionResult

        If (Session("UserID") Is Nothing) Then
            Dim null As Boolean = True
        Else
            Dim what As String = Session("UserID").ToString
        End If

        If Request.HttpMethod = "GET" And Not (Session("UserID") Is Nothing) Then
            update_Like_Button(ID)
        End If

        Try
            Return View(dalUserAcc.SearchBookDetail(ID))
        Catch ex As Exception
            Return View("Book Not Found")
        End Try

    End Function

    Public Function AddToWishList(ByVal ID As Integer) As ActionResult
        If Not (Session("UserID") Is Nothing) Then
            Dim data As Integer = check_Wishlist_Existence(ID)
            Dim preference As Integer
            Dim searchCmd As New SqlCommand

            If check_Wishlist_Existence(ID) = 0 Then

                TempData("WishlistMessage") = dalUserAcc.InsertWishlist(ID, Session("UserID"))

            Else

                preference = retrieve_Wishlist_Preference(ID)

                If preference = 0 Then
                    preference = 1

                    TempData("WishlistMessage") = dalUserAcc.UpdateWishlist(Session("UserID"), ID, preference)

                ElseIf preference = 1 Then
                    preference = 0

                    TempData("WishlistMessage") = dalUserAcc.UpdateWishlist(Session("UserID"), ID, preference)

                End If

            End If

            Return Json(New With {Key .success = True})
        Else
            Return Json(New With {Key .success = False})
        End If


    End Function

    Function check_Wishlist_Existence(ByVal ID As Integer) As Integer
        Return dalUserAcc.SearchWishlistExistence(Session("UserID"), ID)
    End Function

    Private Function retrieve_Wishlist_Preference(ByVal ID As Integer) As Integer

        Return dalUserAcc.SearchWishlistPreference(Session("UserID"), ID)
    End Function

    Private Sub update_Like_Button(ByVal ID As Integer)
        Dim existed As Integer
        Dim liked As Integer

        existed = check_Wishlist_Existence(ID)
        If existed = 1 Then
            liked = retrieve_Wishlist_Preference(ID)
            If liked = 0 Then
                'change the like icon into black color
                ViewBag.LikeIconColor = "#000000"
            ElseIf liked = 1 Then
                'change the like icon into red color
                ViewBag.LikeIconColor = "#FF0000"
            End If
        End If
    End Sub

    Public Function ValidateLogin(ByVal userData As ModelLibrary.UserAcc) As ActionResult

        Dim found As Boolean = False
        Dim active As Boolean = False
        Try
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
                            found = True
                            Exit For
                        End If
                        currentRow += 1
                    Next

                    db.CloseConnection()

                    If dbStatus = "Active" Then
                        active = True
                    End If

                    If found And active Then
                        Session("UserID") = dbUserID
                        TempData("LoginResult") = "Welcome Back, " + dbFirstName + "!"

                        'Store information in cookies
                        If userData.userRememberMe Then
                            Dim rememberMeToken As String = GenerateRememberMeToken()
                            StoreRememberMeToken(userData.userEmail, rememberMeToken)

                        End If

                    ElseIf found And Not active Then

                        TempData("LoginResult") = "Sorry, your account has been disabled. Please contact with the admin!"

                    End If

                End If
            End If
        Catch ex As Exception
            TempData("LoginResult") = ex
        End Try
        
        Return Json(New With {Key .isValid = found, .loginResult = TempData("LoginResult")}, JsonRequestBehavior.AllowGet)
    End Function

    Public Function ValidateExternalLogin(ByVal userData As ModelLibrary.UserAcc) As Boolean
        Dim found As Boolean = False
        If db.OpenConnection Then
            Dim strSql = "Select * From UserAcc"
            Dim ds As DataSet = New DataSet
            Dim da As SqlDataAdapter
            Dim currentRow As Integer = 0
            Dim dbEmail As String
            Dim dbUserID As String
            Dim dbFirstName As String

            da = New SqlDataAdapter(strSql, db.conn)
            ds.Clear()
            da.Fill(ds, "UserAcc")

            If ds.Tables("UserAcc").Rows.Count > 0 Then

                For row As Integer = 0 To ds.Tables("UserAcc").Rows.Count - 1
                    dbUserID = ds.Tables("UserAcc").Rows(currentRow).Item(0)
                    dbFirstName = ds.Tables("UserAcc").Rows(currentRow).Item(1)
                    dbEmail = ds.Tables("UserAcc").Rows(currentRow).Item(3)

                    If (userData.userEmail = dbEmail) Then
                        found = True
                        Exit For
                    End If
                    currentRow += 1
                Next

                db.CloseConnection()

                If found Then
                    Session("UserID") = dbUserID
                    TempData("LoginResult") = "Welcome Back, " + dbFirstName + "!"

                End If

            End If
        End If
        Return found


    End Function

    Private Sub StoreExternalLoginCredential(userData As ModelLibrary.UserAcc)
        Dim dateTimeCreated As DateTime = DateTime.Now
        Dim fname As String = userData.userFname
        Dim lname As String = userData.userLname
        Dim birthday As Date = userData.userBirthday
        Dim gender As String = userData.userGender
        Dim email As String = userData.userEmail

        Try

            Dim MySqlCommand As New SqlCommand
            Dim strSql As String

            If db.OpenConnection() = True Then
                strSql = "INSERT into UserAcc(userFname, userLname, userEmail, userPassword," +
                "userBirthday, userGender, userCreateDateTime) VALUES (@userFname, @userLname, @userEmail, @userPassword," +
                "@userBirthday, @userGender, @userCreateDateTime)"

                MySqlCommand = New SqlCommand(strSql, db.conn)

                MySqlCommand.Parameters.AddWithValue("@userFname", fname)
                MySqlCommand.Parameters.AddWithValue("@userLname", lname)
                MySqlCommand.Parameters.AddWithValue("@userEmail", email)
                MySqlCommand.Parameters.AddWithValue("@userPassword", "")
                MySqlCommand.Parameters.AddWithValue("@userBirthday", birthday)
                MySqlCommand.Parameters.AddWithValue("@userGender", gender)
                MySqlCommand.Parameters.AddWithValue("@userCreateDateTime", dateTimeCreated)

                MySqlCommand.ExecuteNonQuery()
                db.CloseConnection()
            Else
                TempData("RegisterResult") = "Error connecting to database server."
            End If


        Catch ex As Exception
            TempData("RegisterResult") = ex.Message.ToString()
        End Try
    End Sub

    Private Function GenerateRememberMeToken() As String

        Dim token As String = Guid.NewGuid().ToString()

        Return token
    End Function


    Private Sub StoreRememberMeToken(email As String, token As String)

        ' Access the current HttpContext
        Dim httpContext As HttpContext = System.Web.HttpContext.Current
        ' Create a new cookie with the remember me token
        Dim rememberMeCookie As New HttpCookie("RememberMe")
        rememberMeCookie.Value = email & ":" & token
        rememberMeCookie.Expires = DateTime.Now.AddDays(30) ' Set the cookie expiration time (e.g., 30 days)
        rememberMeCookie.HttpOnly = True
        ' Add the cookie to the response
        httpContext.Current.Response.Cookies.Add(rememberMeCookie)
    End Sub

    Public Function AddToCart(ByVal ID As Integer) As ActionResult
        If Not (Session("UserID") Is Nothing) Then

            Dim add As Integer

            If db.OpenConnection = True Then
                add = dalUserAcc.SearchCartNumCount(Session("UserID"), ID)

                If add = 0 Then                    
                    dalUserAcc.InsertCart(Session("UserID"), ID)
                Else
                    add = dalUserAcc.SearchCartNum(Session("UserID"), ID)
                    add += 1
                    dalUserAcc.UpdateCartNum(Session("UserID"), ID, add)

                End If

            End If

            Return Json(New With {Key .success = True})
        Else
            Return Json(New With {Key .success = False})
        End If


    End Function

    Public Function BuyNow(ByVal ID As Integer) As ActionResult
        If Not (Session("UserID") Is Nothing) Then

            Return Json(New With {Key .success = True})
        Else
            Return Json(New With {Key .success = False})
        End If
    End Function

    Private Function GetBooksForSearching() As List(Of ModelLibrary.Book)
        Dim books As New List(Of ModelLibrary.Book)()
        Dim search As String = Request.QueryString("search")
        If Not String.IsNullOrEmpty(search) Then
            books = dalUserAcc.SearchSearchEngineResult(search)
        End If
        Return books
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


    Public Function UpdateCartNum() As ActionResult
        If Not (Session("UserID") Is Nothing) Then
            ' Create a JSON result and return it
            Dim result = New With {
                .CartItem = dalUserAcc.SelectCartNumIndex(Session("UserID"))
            }

            Dim jsonResult As String = New JavaScriptSerializer().Serialize(result)
            Return Content(jsonResult, "application/json")

        Else
            Dim result = New With {
                .CartItem = 0
            }

            Dim jsonResult As String = New JavaScriptSerializer().Serialize(result)
            Return Content(jsonResult, "application/json")
        End If
    End Function

    Public Function UpdateLikeNum() As ActionResult
        If Not (Session("UserID") Is Nothing) Then

            ' Create a JSON result and return it
            Dim result = New With {
                .LikeItem = dalUserAcc.SelectLikeNumIndex(Session("UserID"))
            }

            Dim jsonResult As String = New JavaScriptSerializer().Serialize(result)
            Return Content(jsonResult, "application/json")
        Else
            Dim result = New With {
                .LikeItem = 0
            }

            Dim jsonResult As String = New JavaScriptSerializer().Serialize(result)
            Return Content(jsonResult, "application/json")

        End If
    End Function

End Class