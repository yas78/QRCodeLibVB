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

            Return New Byte() {rgbBlue, rgbGreen, rgbRed, rgbReserved}

        End Function

    End Structure

End Namespace