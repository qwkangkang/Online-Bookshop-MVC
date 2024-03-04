Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports System.Web.Routing

Public Class RouteConfig
    Public Shared Sub RegisterRoutes(ByVal routes As RouteCollection)
        routes.IgnoreRoute("{resource}.axd/{*pathInfo}")

        routes.MapRoute( _
            name:="Default", _
            url:="{controller}/{action}/{id}", _
            defaults:=New With {.controller = "Home", .action = "Login", .id = UrlParameter.Optional} _
        )

        routes.MapRoute(
            name:="ExternalLogin",
            url:="Home/ExternalLogin/{provider}",
            defaults:=New With {.controller = "Home", .action = "ExternalLogin", .provider = UrlParameter.Optional}
        )

    End Sub
End Class