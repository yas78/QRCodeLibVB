Imports System

Namespace Ys.QRCode

    ''' <summary>
    ''' 残余ビット
    ''' </summary>
    Friend Module RemainderBit
        ''' <summary>
        ''' 残余ビットを配置します。
        ''' </summary>
        Public Sub Place(moduleMatrix As Integer()())
            For r = 0 To UBound(moduleMatrix)
                For c = 0 To UBound(moduleMatrix(r))
                    If moduleMatrix(r)(c) = Values.BLANK Then
                        moduleMatrix(r)(c) = -Values.WORD
                    End If
                Next
            Next
        End Sub

    End Module

End Namespace
