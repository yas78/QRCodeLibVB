Imports System
Imports System.Runtime.InteropServices

Namespace Ys.Image

    ''' <summary>
    ''' RGBQUAD構造体
    ''' </summary>
    <StructLayout(LayoutKind.Sequential)>
    Friend Structure RGBQUAD

        Public rgbBlue      As Byte
        Public rgbGreen     As Byte
        Public rgbRed       As Byte
        Public rgbReserved  As Byte

        ''' <summary>
        ''' この構造体のバイト配列を返します。
        ''' </summary>
        Public Function GetBytes() As Byte()

            Dim ret As Byte() = New Byte(4 - 1) {}

            Buffer.BlockCopy(BitConverter.GetBytes(rgbBlue),     0, ret,  0, 1)
            Buffer.BlockCopy(BitConverter.GetBytes(rgbGreen),    0, ret,  1, 1)
            Buffer.BlockCopy(BitConverter.GetBytes(rgbRed),      0, ret,  2, 1)
            Buffer.BlockCopy(BitConverter.GetBytes(rgbReserved), 0, ret,  3, 1)

            Return ret

        End Function

    End Structure

End Namespace