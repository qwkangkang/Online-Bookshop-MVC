Imports System.Data.SqlClient
Imports System.Xml
Imports System.IO
Imports System.Security.Cryptography
Imports System.Xml.Serialization

Public Class PaymentController
    Inherits System.Web.Mvc.Controller

    '
    ' GET: /Payment
    Private db As New DatabaseHelper()

    Private Shared ReadOnly preSharedEncryptionKey As Byte() = {
    &H12, &H34, &H56, &H78, &HAB, &HCD, &HEF, &H12,
    &H34, &H56, &H78, &HAB, &HCD, &HEF, &H12, &H34,
    &H56, &H78, &HAB, &HCD, &HEF, &H12, &H34, &H56,
    &H78, &HAB, &HCD, &HEF, &H12, &H34, &H56, &H78
}

    Private Shared ReadOnly preSharedInitializationVector As Byte() = {
        &HAB, &HCD, &HEF, &H12, &H34, &H56, &H78, &HAB,
        &HCD, &HEF, &H12, &H34, &H56, &H78, &HAB, &HCD
    }

    Public Function Payment() As ActionResult
        Dim bookID As String = Request.QueryString("bookID")
        Dim quantity As String = Request.QueryString("quantity")
        Dim userID As Integer = Session("UserID")
        Dim items As New List(Of ModelLibrary.BookCartCombined)

        'call wcf service
        Try
            Using client As New ServiceReference1.PaymentServiceClient
                'items = client.RetrievePaymentPageInfo(userID, bookID, quantity).ToList
                Dim encryptedData As String = client.RetrievePaymentPageInfo(userID, bookID, quantity)
                ' Decrypt the encrypted data using AES
                Dim decryptedData As Byte() = DecryptData(Convert.FromBase64String(encryptedData))
                items = DeserializeItems(decryptedData)
            End Using
        Catch ex As Exception

        End Try

        Try
            Using client As New ServiceReference1.PaymentServiceClient
                ViewData("Email") = client.RetrievePaymentPageInfoEmail(userID)
            End Using
        Catch ex As Exception
            ViewData("Email") = ""
        End Try


        Return View(items)
    End Function


    Private Function DeserializeItems(data As Byte()) As List(Of ModelLibrary.BookCartCombined)
        Using memoryStream As New MemoryStream(data)
            Dim serializer As New XmlSerializer(GetType(List(Of ModelLibrary.BookCartCombined)))
            Return DirectCast(serializer.Deserialize(memoryStream), List(Of ModelLibrary.BookCartCombined))
        End Using
    End Function


    Private Function DecryptData(encryptedData As Byte()) As Byte()
        Dim key As Byte() = preSharedEncryptionKey
        Dim iv As Byte() = preSharedInitializationVector

        Using rijAlg As New RijndaelManaged()
            rijAlg.Key = key
            rijAlg.IV = iv
            rijAlg.Mode = CipherMode.CBC
            rijAlg.Padding = PaddingMode.PKCS7

            Try
                Using decryptor As ICryptoTransform = rijAlg.CreateDecryptor()
                    Dim decryptedData As Byte() = decryptor.TransformFinalBlock(encryptedData, 0, encryptedData.Length)
                    Return decryptedData
                End Using
            Catch ex As CryptographicException

                Return New Byte() {}
            End Try
        End Using
    End Function

    Public Function Cart() As ActionResult
        If Not (Session("UserID") Is Nothing) Then
            ViewData("Title") = "Cart"
            ViewData("Layout") = "~/Views/Shared/_Layout.vbhtml"
            Return View(BindData())
        Else
            TempData("AlertMessage") = "Please login first"
            Return RedirectToAction("Login", "Home")
        End If
    End Function

    Protected Function BindData() As List(Of ModelLibrary.BookCartCombined)
        Dim userID As Integer = Session("UserID")
        Dim items As New List(Of ModelLibrary.BookCartCombined)()

        Using client As New ServiceReference1.PaymentServiceClient
            items = client.RetrieveCartPageInfo(userID).ToList
            Dim totalPrice As Decimal = client.CalculateTotalPrice(items.ToArray)
            ViewBag.TotalPrice = totalPrice
        End Using

        Return items
    End Function



    Public Function UpdateCartItem(ByVal bookID As Integer, ByVal quantity As Integer) As ActionResult

        SaveChangesToDatabase(quantity, bookID)
        If CheckZero(quantity, bookID) Then
            Return Json(New With {.success = False})
        Else
            Return Json(New With {.success = True})
        End If

    End Function

    Private Function CheckZero(quantity As Integer, bookID As Integer) As Boolean
        Dim userID = Session("UserID")
        If quantity = 0 Then
            Using client As New ServiceReference1.PaymentServiceClient
                client.DeleteZeroCartItem(userID, bookID)
            End Using
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub SaveChangesToDatabase(quantity As Integer, bookID As Integer)
        Using client As New ServiceReference1.PaymentServiceClient
            client.SaveCartChangesToDB(Session("UserID"), quantity, bookID)
        End Using
    End Sub

    Protected Function CheckValidity(ByVal cartNum As Integer, ByVal bookID As Integer) As Boolean

        Dim bookQuantity As Integer
        Using client As New ServiceReference1.PaymentServiceClient
            bookQuantity = client.CheckBookQuantity(bookID)
        End Using

        If (cartNum <= bookQuantity) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function Checkout(ByVal cartItems As List(Of ModelLibrary.BookCartCombined)) As ActionResult
        Dim IsValid As Boolean = True
        ' Process the cart items
        For Each item As ModelLibrary.BookCartCombined In cartItems
            Dim bookID As Integer = item.bookID
            Dim cartNum As Integer = item.cartNum

            If CheckValidity(cartNum, bookID) = False Then
                IsValid = False
            End If
        Next

        If IsValid Then
            Return Json(New With {.success = True})
        Else
            Return Json(New With {.success = False})
        End If

    End Function

    Public Function PaymentDetail(ByVal form As FormCollection) As ActionResult
        Dim email As String = form("email")
        Dim deliveryMethod As String = form("deliveryMethod")
        Dim pickupLocation As String = form("location")
        Dim totalPrice As Decimal = Decimal.Parse(form("totalPrice"))
        Dim deliveryFee As Decimal

        If pickupLocation = "ioi" Then
            pickupLocation = "BookWorm IOI Mall Puchong"
        ElseIf pickupLocation = "midvalley" Then
            pickupLocation = "BookWorm Mid Valley Megamall"
        End If

        If deliveryMethod = "selft_collect" Then
            deliveryFee = 0
        End If

        Dim model As New ModelLibrary.PaymentDetailViewModel()
        model.email = email
        model.deliveryMethod = deliveryMethod
        model.deliveryFee = deliveryFee
        model.pickupLocation = pickupLocation
        model.totalPrice = totalPrice

        Return View(model)
    End Function

    Public Function SubmitPaymentDetail(ByVal form As FormCollection, model As ModelLibrary.PaymentDetailViewModel) As ActionResult

        Dim contact As String = model.email
        Dim method As String = model.deliveryMethod
        Dim location As String = model.pickupLocation
        Dim totalPrice As Decimal = model.totalPrice
        Dim deliveryFee As Decimal = model.deliveryFee
        Dim dateTimePayment As DateTime = DateTime.Now
        Dim paymentAmount As Decimal = totalPrice + deliveryFee
        Dim country As String = form("country")
        Dim fname As String = form("fname")
        Dim lname As String = form("lname")
        Dim address As String = form("address")
        Dim postcode As String = form("postcode")
        Dim city As String = form("city")
        Dim state As String = form("state")
        Dim phone As String = form("phone")
        Dim serviceFeedback As Boolean
        Dim paymentResult As String
        Dim userID = Session("UserID")
        Dim bookIDBuyNow As String = Request.Form("bookID")
        Dim quantityBuyNow As String = Request.Form("quantity")

        'test service
        Dim xmlDoc As New XDocument()
        Dim basePath1 As String = AppDomain.CurrentDomain.BaseDirectory
        Dim parentPath As String = Directory.GetParent(Directory.GetParent(basePath1).FullName).FullName
        Dim xmlFilePath As String = parentPath + "\xml files\paymentDetail.xml"
        'Dim xmlFilePath As String = "C:\Users\CHRONOS032\Documents\Visual Studio 2013\Projects\Online Bookshop MVC\xml files\paymentDetail.xml"
        Dim settings As New XmlWriterSettings()
        settings.Indent = True

        'Create XML file and write data using XmlWriter
        Using writer As XmlWriter = XmlWriter.Create(xmlFilePath, settings)
            writer.WriteStartDocument()
            'root
            writer.WriteStartElement("Payment")
            'elements and attributes
            writer.WriteElementString("Contact", contact)
            writer.WriteElementString("Method", method)
            writer.WriteElementString("Location", location)
            writer.WriteElementString("TotalPrice", totalPrice)
            writer.WriteElementString("DeliveryFee", deliveryFee)
            writer.WriteElementString("DateTimePayment", dateTimePayment)
            writer.WriteElementString("PaymentAmount", paymentAmount)
            writer.WriteElementString("Country", country)
            writer.WriteElementString("Fname", fname)
            writer.WriteElementString("Lname", lname)
            writer.WriteElementString("Address", address)
            writer.WriteElementString("Postcode", postcode)
            writer.WriteElementString("City", city)
            writer.WriteElementString("State", state)
            writer.WriteElementString("Phone", phone)
            writer.WriteElementString("UserID", userID)
            writer.WriteElementString("BookIDBuyNow", bookIDBuyNow)
            writer.WriteElementString("QuantityBuyNow", quantityBuyNow)

            writer.WriteEndElement()
            writer.WriteEndDocument()
        End Using

        ' Read the XML file into a byte array
        Dim xmlData As Byte()
        Using fs As New FileStream(xmlFilePath, FileMode.Open, FileAccess.Read)
            Using binaryReader As New BinaryReader(fs)
                xmlData = binaryReader.ReadBytes(CInt(fs.Length))
            End Using
        End Using

        Dim key As Byte() = Encoding.UTF8.GetBytes("EncryptionKey")
        Dim encryptedData As Byte() = EncryptAes(xmlData, key)
        Dim lostServiceConn As String = String.Empty

        'call wcf service
        Try
            Using client As New ServiceReference1.PaymentServiceClient
                Using stream As New MemoryStream(encryptedData)
                    serviceFeedback = client.RetrievePaymentDetail(stream)
                End Using
            End Using
        Catch ex As Exception
            lostServiceConn = "Payment Not Successful! Order has been cancelled. Reason: There is an error in server connection"
        End Try


        'end service


        If (serviceFeedback) Then
            paymentResult = "Payment Successful! Please collect the book in one month."
        Else
            paymentResult = "Payment Not Successful! Order has been cancelled."
            If Not String.IsNullOrEmpty(lostServiceConn) Then
                paymentResult = lostServiceConn
            End If
        End If

        TempData("AlertMessage") = paymentResult

        Return RedirectToAction("Index", "Home")
    End Function

    <HandleError>
    Public Function Wishlist() As ActionResult
        If Not (Session("UserID") Is Nothing) Then
            ViewData("Title") = "Wishlist"
            ViewData("Layout") = "~/Views/Shared/_Layout.vbhtml"
            Dim id As String = Request.QueryString("bookID")


            If Not String.IsNullOrEmpty(id) Then
                Dim modify As Integer

                Using client As New ServiceReference1.PaymentServiceClient
                    modify = client.RemoveFromWishlist(Session("UserID"), id)
                End Using
                If (modify > 0) Then
                    TempData("RemovedMessage") = "Removed from Wish List Successfully!"

                End If
            End If

            Using client As New ServiceReference1.PaymentServiceClient
                Return View(client.RetrieveWishlistPageInfo(Session("UserID")))
            End Using

        Else
            TempData("AlertMessage") = "Please login first"
            Return RedirectToAction("Login", "Home")
        End If
    End Function


    Private Function EncryptAes(xmlData As Byte(), ByVal key As Byte()) As Byte()
        Dim derivedKey As Byte() = DeriveKey(key)

        Using aes As Aes = Aes.Create()
            aes.Key = derivedKey
            Dim iv = GenerateRandomIV()
            aes.IV = iv ' You can use a random IV or prepend the IV to the encrypted data

            Using memoryStream As New MemoryStream()
                Using cryptoStream As New CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write)
                    cryptoStream.Write(xmlData, 0, xmlData.Length)
                    cryptoStream.FlushFinalBlock()
                    Return memoryStream.ToArray()
                End Using
            End Using
        End Using
    End Function

    Private Function GenerateRandomIV() As Byte()
        Dim ivBytes As Byte() = New Byte(15) {} ' 16 bytes for AES-128 (Change to 32 bytes for AES-256)
        Using rng As New RNGCryptoServiceProvider()
            rng.GetBytes(ivBytes)
        End Using
        Return ivBytes
    End Function

    Private Function DeriveKey(key As Byte()) As Byte()
        Using sha256 As SHA256 = SHA256.Create()
            Return sha256.ComputeHash(key)
        End Using
    End Function
End Class