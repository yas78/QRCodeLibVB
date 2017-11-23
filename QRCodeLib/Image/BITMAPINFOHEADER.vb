Imports System
Imports System.Runtime.InteropServices

Namespace Ys.Image

    ''' <summary>
    ''' BITMAPINFOHEADER構造体
    ''' </summary>
    <StructLayout(LayoutKind.Sequential)>
    Friend Structure BITMAPINFOHEADER

        Public biSize           As Integer
        Public biWidth          As Integer
        Public biHeight         As Integer
        Public biPlanes         As Short
        Public biBitCount       As Short
        Public biCompression    As Integer
        Public biSizeImage      As Integer
        Public biXPelsPerMeter  As Integer
        Public biYPelsPerMeter  As Integer
        Public biClrUsed        As Integer
        Public biClrImportant   As Integer

        ''' <summary>
        ''' この構造体のバイト配列を返します。
        ''' </summary>
        Public Function GetBytes() As Byte()
            Dim ret As Byte() = New Byte(40 - 1) {}

            Buffer.BlockCopy(BitConverter.GetBytes(biSize),          0, ret,  0, 4)
            Buffer.BlockCopy(BitConverter.GetBytes(biWidth),         0, ret,  4, 4)
            Buffer.BlockCopy(BitConverter.GetBytes(biHeight),        0, ret,  8, 4)
            Buffer.BlockCopy(BitConverter.GetBytes(biPlanes),        0, ret, 12, 2)
            Buffer.BlockCopy(BitConverter.GetBytes(biBitCount),      0, ret, 14, 2)
            Buffer.BlockCopy(BitConverter.GetBytes(biCompression),   0, ret, 16, 4)
            Buffer.BlockCopy(BitConverter.GetBytes(biSizeImage),     0, ret, 20, 4)
            Buffer.BlockCopy(BitConverter.GetBytes(biXPelsPerMeter), 0, ret, 24, 4)
            Buffer.BlockCopy(BitConverter.GetBytes(biYPelsPerMeter), 0, ret, 28, 4)
            Buffer.BlockCopy(BitConverter.GetBytes(biClrUsed),       0, ret, 32, 4)
            Buffer.BlockCopy(BitConverter.GetBytes(biClrImportant),  0, ret, 36, 4)

            Return ret
        End Function

    End Structure

End Namespace
