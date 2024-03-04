Imports System.Web.Mvc
Imports Microsoft.Owin.Security
Imports WCF_Online_Bookshop_MVC

Public Class ChallengeResult
    Inherits HttpUnauthorizedResult

    Private ReadOnly _provider As String
    Private ReadOnly _redirectUri As String
    Private ReadOnly _userId As String

    Public Sub New(provider As String, redirectUri As String, Optional userId As String = Nothing)
        _provider = provider
        _redirectUri = redirectUri
        _userId = userId
    End Sub

    Public Overrides Sub ExecuteResult(context As ControllerContext)
        Dim properties As AuthenticationProperties = New AuthenticationProperties With {
            .RedirectUri = _redirectUri
        }
        If _userId IsNot Nothing Then
            properties.Dictionary("XsrfId") = _userId
        End If

        'test
        'Dim owinContext = context.HttpContext.GetOwinContext()
        'properties.RedirectUri = owinContext.Request.Scheme + "://" + owinContext.Request.Host.ToString() + "/signin-google"

        context.HttpContext.GetOwinContext().Authentication.Challenge(properties, _provider)

    End Sub
End Class
