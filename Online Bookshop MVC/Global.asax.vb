' Note: For instructions on enabling IIS6 or IIS7 classic mode, 
' visit http://go.microsoft.com/?LinkId=9394802
Imports System.Web.Http
Imports System.Web.Optimization

Public Class MvcApplication
    Inherits Web.HttpApplication

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
                             "default-src 'self' localhost:4350; connect-src 'self' localhost:2297 ws://localhost:2297 localhost:3047 ws://localhost:3047 ws://localhost:4350 localhost:4350 localhost:31273 ws://localhost:31273 localhost:23812 ws://localhost:23812 localhost:14893 ws://localhost:14893 localhost:14643 ws://localhost:14643 localhost:3276 ws://localhost:3276 localhost:4862 ws://localhost:4862 localhost:27605 ws://localhost:27605; script-src 'self' 'unsafe-inline' 'unsafe-eval' localhost:2297 localhost:3047 code.jquery.com localhost:4350 localhost:31273 localhost:23812 localhost:14893 localhost:14643 localhost:3276 localhost:4862 localhost:27605; style-src 'self' 'unsafe-inline' cdnjs.cloudflare.com fonts.googleapis.com code.jquery.com; img-src 'self' data: i.ibb.co data: i.ibb.co wikimedia.org https://upload.wikimedia.org; font-src 'self' fonts.googleapis.com cdnjs.cloudflare.com fonts.gstatic.com  ")
        
        ' script-src-elem 'self' 'unsafe-inline' localhost:3047
    End Sub

    Protected Sub Application_Error()
        Dim exception = Server.GetLastError()
        Dim httpException = TryCast(exception, HttpException)
        Response.Clear()
        Server.ClearError()
        Dim routeData = New RouteData()
        routeData.Values("controller") = "Error"
        routeData.Values("action") = "ServerError2"
        routeData.Values("exception") = exception
        Response.StatusCode = 500
        If httpException IsNot Nothing Then
            Response.StatusCode = httpException.GetHttpCode()
            Select Case Response.StatusCode
                Case 403
                    routeData.Values("action") = "Http403"
                Case 404
                    routeData.Values("action") = "NotFound"
                Case 500
                    routeData.Values("action") = "ServerError2"
            End Select
        Else
            routeData.Values("action") = "ServerError2"
        End If
        ' Avoid IIS7 getting in the middle
        Response.TrySkipIisCustomErrors = True
        Dim errorsController As IController = New Online_Bookshop_MVC.ErrorController
        Dim wrapper As HttpContextWrapper = New HttpContextWrapper(Context)
        Dim rc As RequestContext = New RequestContext(wrapper, routeData)
        errorsController.Execute(rc)
    End Sub

End Class
