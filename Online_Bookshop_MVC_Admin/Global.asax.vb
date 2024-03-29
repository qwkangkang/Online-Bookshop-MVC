﻿' Note: For instructions on enabling IIS6 or IIS7 classic mode, 
' visit http://go.microsoft.com/?LinkId=9394802
Imports System.Web.Http
Imports System.Web.Optimization

Public Class MvcApplication
    Inherits System.Web.HttpApplication

    Sub Application_Start()
        AreaRegistration.RegisterAllAreas()

        WebApiConfig.Register(GlobalConfiguration.Configuration)
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)

    End Sub

    Protected Sub Application_PreSendRequestHeaders(sender As Object, e As EventArgs)
        Response.
            Headers.
            Add("Content-Security-Policy",
                             "frame-ancestors 'self' https://trusted-domain.com;")

        ' script-src-elem 'self' 'unsafe-inline' localhost:3047
    End Sub

End Class
