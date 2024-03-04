Imports Microsoft.AspNet.Identity

Public Class ApplicationUserManager
    Inherits UserManager(Of ApplicationUser)

    Public Sub New(store As IUserStore(Of ApplicationUser))
        MyBase.New(store)
    End Sub
End Class
