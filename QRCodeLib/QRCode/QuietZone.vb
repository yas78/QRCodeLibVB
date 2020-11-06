Imports System

Namespace Ys.QRCode

    ''' <summary>
    ''' クワイエットゾーン
    ''' </summary>
    Friend Module QuietZone

        Public Const MIN_WIDTH As Integer = 4

        Private _width As Integer = MIN_WIDTH

        Public Property Width() As Integer
            Get
                Return _width
            End Get
            Set
                If _width < MIN_WIDTH Then
                    Throw New ArgumentOutOfRangeException(NameOf(value))
                End If

                _width = value
            End Set
        End Property

        ''' <summary>
        ''' クワイエットゾーンを追加します。
        ''' </summary>
        Public Function Place(moduleMatrix As Integer()()) As Integer()()
            Dim size As Integer = UBound(moduleMatrix) + Width * 2
            Dim ret As Integer()() = New Integer(size)() {}

            For i As Integer = 0 To size
                ret(i) = New Integer(size) {}
            Next

            For i As Integer = 0 To UBound(moduleMatrix)
                moduleMatrix(i).CopyTo(ret(i + Width), Width)
            Next

            Return ret
        End Function

    End Module

End Namespace
