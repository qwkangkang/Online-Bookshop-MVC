Imports Microsoft.AspNet.Identity.EntityFramework

Public Class ApplicationUser
    Inherits IdentityUser

    ' Additional properties for the user
    Public Property userFname As String
    Public Property userLname As String
    Public Property userEmail As String
    Public Property userBirthday As Date

End Class
