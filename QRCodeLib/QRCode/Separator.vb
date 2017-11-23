Imports System

Namespace Ys.QRCode

    ''' <summary>
    ''' 分離パターン
    ''' </summary>
    Friend Module Separator
        
        ''' <summary>
        ''' 分離パターンを配置します。
        ''' </summary>
        Public Sub Place(moduleMatrix As Integer()())
            Dim offset As Integer = UBound(moduleMatrix) - 7

            For i As Integer = 0 To 7
                moduleMatrix(i)(7)          = -2
                moduleMatrix(7)(i)          = -2

                moduleMatrix(offset + i)(7) = -2
                moduleMatrix(offset + 0)(i) = -2

                moduleMatrix(i)(offset + 0) = -2
                moduleMatrix(7)(offset + i) = -2
            Next
        End Sub

    End Module

End Namespace
