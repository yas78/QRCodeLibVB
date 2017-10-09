Imports System

Namespace Ys.QRCode

    ''' <summary>
    ''' タイミングパターン
    ''' </summary>
    Friend Module TimingPattern
        
        ''' <summary>
        ''' タイミングパターンを配置します。
        ''' </summary>
        Public Sub Place(moduleMatrix As Integer()())

            For i As Integer = 8 To UBound(moduleMatrix) - 8
                Dim v As Integer = If(i Mod 2 = 0, 2, -2)

                moduleMatrix(6)(i) = v
                moduleMatrix(i)(6) = v
            Next

        End Sub

    End Module

End Namespace
