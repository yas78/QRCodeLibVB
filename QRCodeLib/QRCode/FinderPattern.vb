Imports System

Namespace Ys.QRCode

    ''' <summary>
    ''' 位置検出パターン
    ''' </summary>
    Friend Module FinderPattern

        ' 位置検出パターン
        Private _finderPattern as Integer()() = {
            ({2,  2,  2,  2,  2,  2, 2}),
            ({2, -2, -2, -2, -2, -2, 2}),
            ({2, -2,  2,  2,  2, -2, 2}),
            ({2, -2,  2,  2,  2, -2, 2}),
            ({2, -2,  2,  2,  2, -2, 2}),
            ({2, -2, -2, -2, -2, -2, 2}),
            ({2,  2,  2,  2,  2,  2, 2})
        }
        
        ''' <summary>
        ''' 位置検出パターンを配置します。
        ''' </summary>
        Public Sub Place(moduleMatrix As Integer()())
            Dim offset As Integer = moduleMatrix.Length - _finderPattern.Length

            For i As Integer = 0 To UBound(_finderPattern)
                For j As Integer = 0 To UBound(_finderPattern(i))
                    Dim v As Integer = _finderPattern(i)(j)

                    moduleMatrix(i         )(j         ) = v
                    moduleMatrix(i         )(j + offset) = v
                    moduleMatrix(i + offset)(j         ) = v

                Next
            Next
        End Sub

    End Module

End Namespace
