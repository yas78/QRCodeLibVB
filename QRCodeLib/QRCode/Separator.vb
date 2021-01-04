Imports System

Namespace Ys.QRCode

    ''' <summary>
    ''' 分離パターン
    ''' </summary>
    Friend Module Separator

        Const VAL As Integer = Values.SEPARATOR

        ''' <summary>
        ''' 分離パターンを配置します。
        ''' </summary>
        Public Sub Place(moduleMatrix As Integer()())
            Dim offset As Integer = UBound(moduleMatrix) - 7

            For i As Integer = 0 To 7
                moduleMatrix(i)(7)          = -VAL
                moduleMatrix(7)(i)          = -VAL

                moduleMatrix(offset + i)(7) = -VAL
                moduleMatrix(offset + 0)(i) = -VAL

                moduleMatrix(i)(offset + 0) = -VAL
                moduleMatrix(7)(offset + i) = -VAL
            Next
        End Sub

    End Module

End Namespace
