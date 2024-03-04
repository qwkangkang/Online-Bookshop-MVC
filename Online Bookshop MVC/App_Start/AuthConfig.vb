Imports Owin
Imports Microsoft.AspNet.Identity
Imports Microsoft.Owin
Imports Microsoft.Owin.Security
Imports Microsoft.Owin.Security.Cookies
Imports Microsoft.Owin.Security.Google
Imports Google.Apis.Auth.OAuth2.Responses
Imports Google.Apis.Auth.OAuth2.Flows
Imports Google.Apis.Auth.OAuth2
Imports System.Threading.Tasks

Public Class AuthConfig
    Private Shared googleAuthOptions As GoogleOAuth2AuthenticationOptions

    Public Shared Sub Configuration(app As IAppBuilder)

        app.SetDefaultSignInAsAuthenticationType(DefaultAuthenticationTypes.ExternalCookie)
        app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie)
        app.UseCookieAuthentication(New CookieAuthenticationOptions())
        Dim googleOptions = New GoogleOAuth2AuthenticationOptions() With {
           .ClientId = "824550836549-mti56d2h2tbh5ta0recq9l90aog9lfuc.apps.googleusercontent.com",
           .ClientSecret = "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"
       }
        googleOptions.Scope.Add("email") 
        Dim googleAuth = New GoogleOAuth2AuthenticationProvider() With {
            .OnAuthenticated = Function(context)
                                   ' Custom logic after successful authentication
                                   Return Task.FromResult(0)
                               End Function
        }
        googleOptions.Provider = googleAuth
        app.UseGoogleAuthentication(googleOptions)

    End Sub
End Class
