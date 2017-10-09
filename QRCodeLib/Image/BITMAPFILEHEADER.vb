Imports System
Imports System.Runtime.InteropServices

Namespace Ys.Image

    ''' <summary>
    ''' BITMAPFILEHEADER構造体
    ''' </summary>
    <StructLayout(LayoutKind.Sequential, Pack:= 2)>
    Friend Structure BITMAPFILEHEADER

        Public bfType       As Short
        Public bfSize       As Integer
        Public bfReserved1  As Short
        Public bfReserved2  As Short
        Public bfOffBits    As Integer

        ''' <summary>
        ''' この構造体のバイト配列を返します。
        ''' </summary>
        Public Function GetBytes() As Byte()

            Dim ret As Byte() = New Byte(14 - 1) {}

            Buffer.BlockCopy(BitConverter.GetBytes(bfType),      0, ret,  0, 2)
            Buffer.BlockCopy(BitConverter.GetBytes(bfSize),      0, ret,  2, 4)
            Buffer.BlockCopy(BitConverter.GetBytes(bfReserved1), 0, ret,  6, 2)
            Buffer.BlockCopy(BitConverter.GetBytes(bfReserved2), 0, ret,  8, 2)
            Buffer.BlockCopy(BitConverter.GetBytes(bfOffBits),   0, ret, 10, 4)

            Return ret

        End Function

    End Structure

End Namespace