Imports System.Data.SqlClient

Public Class AccountController
    Inherits System.Web.Mvc.Controller
    Private db As New DatabaseHelper()
    Private ReadOnly dalUserAcc As dalInternalClassLib.UserAccount

    Public Sub New()
        dalUserAcc = New dalInternalClassLib.UserAccount
    End Sub

    Public Function OrderHistory() As ActionResult
        If Not (Session("UserID") Is Nothing) Then
            Return View(dalUserAcc.SelectBookOrderHistory(Session("UserID")))
        Else
            TempData("AlertMessage") = "Please login first"
            Return RedirectToAction("Login", "Home")
        End If
        
    End Function

    Public Function MyAccount() As ActionResult
        If Not (Session("UserID") Is Nothing) Then
            

            Return View(dalUserAcc.SearchAccountInfo(Session("UserID")))
        Else
            TempData("AlertMessage") = "Please login first"
            Return RedirectToAction("Login", "Home")
        End If

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

End Class