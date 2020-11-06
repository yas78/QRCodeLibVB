Imports System
Imports System.Text.RegularExpressions

Friend Module ColorCode

    Public Function IsWebColor(arg As String) As Boolean
        Dim ret As Boolean = Regex.IsMatch(arg, "^#[0-9A-Fa-f]{6}$")
        Return ret
    End Function

End Module
