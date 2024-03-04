Imports System.Web
Imports System.Web.Mvc

Public Class FilterConfig
    Public Shared Sub RegisterGlobalFilters(ByVal filters As GlobalFilterCollection)
        'filters.Add(New HandleErrorAttribute())
        filters.Add(New MyHandleErrorAttribute())
    End Sub
End Class