Imports System

Namespace Ys.QRCode

    ''' <summary>
    ''' クワイエットゾーン
    ''' </summary>
    Friend Module QuietZone

        Public Const WIDTH As Integer = 4

        ''' <summary>
        ''' クワイエットゾーンを追加します。
        ''' </summary>
        Public Function Place(moduleMatrix As Integer()()) As Integer()()
            Dim size As Integer = UBound(moduleMatrix) + WIDTH * 2
            Dim ret As Integer()() = New Integer(size)() {}

            For i As Integer = 0 To size
                ret(i) = New Integer(size) {}
            Next

            For i As Integer = 0 To UBound(moduleMatrix)
                moduleMatrix(i).CopyTo(ret(i + WIDTH), WIDTH)
            Next

            Return ret
        End Function

    End Module

End Namespace
