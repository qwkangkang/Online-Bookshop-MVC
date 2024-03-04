
Public Class MyHandleErrorAttribute
    Inherits HandleErrorAttribute

    Public Overrides Sub OnException(ByVal filterContext As ExceptionContext)
        MyBase.OnException(filterContext)

        filterContext.HttpContext.Response.TrySkipIisCustomErrors = False
    End Sub

End Class
