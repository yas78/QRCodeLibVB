﻿Imports System

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
            Dim ret As Integer()() = New Integer(UBound(moduleMatrix) + WIDTH * 2)() {}

            For i As Integer = 0 To UBound(ret)
                ret(i) = New Integer(UBound(ret)) {}
            Next

            For i As Integer = 0 To UBound(moduleMatrix)
                moduleMatrix(i).CopyTo(ret(i + WIDTH), WIDTH)
            Next

            Return ret
        End Function

    End Module

End Namespace
