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
            defaults:=New With {.controller = "HomeAdmin", .action = "Login", .id = UrlParameter.Optional} _
        )

        routes.MapRoute(
            name:="EditBook",
            url:="HomeAdmin/EditBook/{bookID}",
            defaults:=New With {.controller = "HomeAdmin", .action = "EditBook"},
            constraints:=New With {.bookID = "\d+"}
            )
    End Sub
End Class