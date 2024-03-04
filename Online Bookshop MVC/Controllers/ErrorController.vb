
Public Class ErrorController
    Inherits System.Web.Mvc.Controller

    Function Index() As ActionResult

        Return View()
    End Function

    Function NotFound() As ActionResult

        Return View()
    End Function


    Function ServerError2() As ActionResult
        Return View()
    End Function
End Class
