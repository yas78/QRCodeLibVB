Imports System

Namespace Ys.TypeExtension

    ''' <summary>
    ''' Int32型の拡張メソッド
    ''' </summary>
    Friend Module Int32Extensions

        ''' <summary>
        ''' オブジェクトのディープコピーを返します。
        ''' </summary>
        <System.Runtime.CompilerServices.Extension>
        Public Function CloneDeep(arg As Integer()()) As Integer()()
            Dim ret As Integer()() = New Integer(UBound(arg))() {}

            For i As Integer = 0 To UBound(arg)
                ret(i) = New Integer(UBound(arg(i))) {}
                arg(i).CopyTo(ret(i), 0)
            Next

            Return ret
        End Function

        ''' <summary>
        ''' 左に90度回転した配列を返します。
        ''' </summary>
        <System.Runtime.CompilerServices.Extension>
        Public Function Rotate90(arg As Integer()()) As Integer()()
            Dim ret As Integer()() = New Integer(UBound(arg(0)))() {}

            For i As Integer = 0 To UBound(ret)
                ret(i) = New Integer(UBound(arg)) {}
            Next

            Dim k As Integer = UBound(ret)

            For i As Integer = 0 To UBound(ret)
                For j As Integer = 0 To UBound(ret(i))
                    ret(i)(j) = arg(j)(k - i)
                Next
            Next

            Return ret
        End Function

    End Module

End Namespace