Imports System
Imports System.Text.RegularExpressions

Friend Module ColorCode
    Public Const BLACK As String = "#000000"
    Public Const WHITE As String = "#FFFFFF"
    
    Public Function IsWebColor(arg As String) As Boolean
        Dim ret As Boolean = Regex.IsMatch(arg, "^#[0-9A-Fa-f]{6}$")
        Return ret
    End Function

End Module
