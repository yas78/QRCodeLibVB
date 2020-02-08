Imports System

Namespace Ys.QRCode

    ''' <summary>
    ''' 位置合わせパターン
    ''' </summary>
    Friend Module AlignmentPattern

        ''' <summary>
        ''' 位置合わせパターンを配置します。
        ''' </summary>
        Public Sub Place(version As Integer, moduleMatrix As Integer()())
            Dim centerPosArray As Integer() = _centerPosArrays(version)

            Dim maxIndex As Integer = UBound(centerPosArray)

            For i As Integer = 0 To maxIndex
                Dim r As Integer = centerPosArray(i)

                For j As Integer = 0 To maxIndex
                    Dim c As Integer = centerPosArray(j)

                    ' 位置検出パターンと重なる場合
                    If i = 0        AndAlso j = 0        OrElse
                       i = 0        AndAlso j = maxIndex OrElse
                       i = maxIndex AndAlso j = 0        Then
                        Continue For
                    End If

                    Array.Copy(New Integer() {2,  2,  2,  2,  2}, 0, moduleMatrix(r - 2), c - 2, 5)
                    Array.Copy(New Integer() {2, -2, -2, -2,  2}, 0, moduleMatrix(r - 1), c - 2, 5)
                    Array.Copy(New Integer() {2, -2,  2, -2,  2}, 0, moduleMatrix(r + 0), c - 2, 5)
                    Array.Copy(New Integer() {2, -2, -2, -2,  2}, 0, moduleMatrix(r + 1), c - 2, 5)
                    Array.Copy(New Integer() {2,  2,  2,  2,  2}, 0, moduleMatrix(r + 2), c - 2, 5)
                Next
            Next
        End Sub

        Private ReadOnly _centerPosArrays As Integer()() = {
            Nothing,
            Nothing,
            ({6, 18}),
            ({6, 22}),
            ({6, 26}),
            ({6, 30}),
            ({6, 34}),
            ({6, 22, 38}),
            ({6, 24, 42}),
            ({6, 26, 46}),
            ({6, 28, 50}),
            ({6, 30, 54}),
            ({6, 32, 58}),
            ({6, 34, 62}),
            ({6, 26, 46, 66}),
            ({6, 26, 48, 70}),
            ({6, 26, 50, 74}),
            ({6, 30, 54, 78}),
            ({6, 30, 56, 82}),
            ({6, 30, 58, 86}),
            ({6, 34, 62, 90}),
            ({6, 28, 50, 72, 94}),
            ({6, 26, 50, 74, 98}),
            ({6, 30, 54, 78, 102}),
            ({6, 28, 54, 80, 106}),
            ({6, 32, 58, 84, 110}),
            ({6, 30, 58, 86, 114}),
            ({6, 34, 62, 90, 118}),
            ({6, 26, 50, 74, 98, 122}),
            ({6, 30, 54, 78, 102, 126}),
            ({6, 26, 52, 78, 104, 130}),
            ({6, 30, 56, 82, 108, 134}),
            ({6, 34, 60, 86, 112, 138}),
            ({6, 30, 58, 86, 114, 142}),
            ({6, 34, 62, 90, 118, 146}),
            ({6, 30, 54, 78, 102, 126, 150}),
            ({6, 24, 50, 76, 102, 128, 154}),
            ({6, 28, 54, 80, 106, 132, 158}),
            ({6, 32, 58, 84, 110, 136, 162}),
            ({6, 26, 54, 82, 110, 138, 166}),
            ({6, 30, 58, 86, 114, 142, 170})
        }

    End Module

End Namespace
