Imports System
Imports System.Linq

Friend Module Charset
    Public Const SHIFT_JIS  As String = "shift_jis"
    Public Const GB2312     As String = "gb2312"
    Public Const EUC_KR     As String = "euc-kr"

    Private ReadOnly cjkCharsetNames As String() = {SHIFT_JIS, GB2312, EUC_KR}

    Public Function IsJP(ByVal charsetName As String) As Boolean
        Return LCase(charsetName) = LCase(SHIFT_JIS)
    End Function

    Public Function IsCJK(ByVal charsetName As String) As Boolean
        If String.IsNullOrEmpty(charsetName) Then
            Throw New ArgumentNullException(NameOf(charsetName))
        End If

        Return cjkCharsetNames.Contains(LCase(charsetName))
    End Function

End Module
