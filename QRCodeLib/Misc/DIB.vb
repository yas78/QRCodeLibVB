Imports System
Imports System.Drawing

Imports Ys.BitmapStructure

Friend Module DIB

    Public Function Build1bppDIB(
        bitmapData As Byte(), width As Integer, height As Integer, foreColor As Color, backColor As Color) As Byte()

        Dim bfh As BITMAPFILEHEADER
        With bfh
            .bfType         = &H4D42
            .bfSize         = 62 + bitmapData.Length
            .bfReserved1    = 0
            .bfReserved2    = 0
            .bfOffBits      = 62
        End With

        Dim bih As BITMAPINFOHEADER
        With bih
            .biSize             = 40
            .biWidth            = width
            .biHeight           = height
            .biPlanes           = 1
            .biBitCount         = 1
            .biCompression      = 0
            .biSizeImage        = 0
            .biXPelsPerMeter    = 3780 ' 96dpi
            .biYPelsPerMeter    = 3780 ' 96dpi
            .biClrUsed          = 0
            .biClrImportant     = 0
        End With

        Dim palette As RGBQUAD() = New RGBQUAD(1) {}
        With palette(0)
            .rgbBlue        = foreColor.B
            .rgbGreen       = foreColor.G
            .rgbRed         = foreColor.R
            .rgbReserved    = 0
        End With
        With palette(1)
            .rgbBlue        = backColor.B
            .rgbGreen       = backColor.G
            .rgbRed         = backColor.R
            .rgbReserved    = 0
        End With

        Dim ret     As Byte() = New Byte(62 + bitmapData.Length - 1) {}
        Dim bytes   As Byte()
        Dim offset  As Integer = 0

        bytes = bfh.GetBytes()
        Buffer.BlockCopy(bytes, 0, ret, offset, bytes.Length)
        offset += bytes.Length

        bytes = bih.GetBytes()
        Buffer.BlockCopy(bytes, 0, ret, offset, bytes.Length)
        offset += bytes.Length

        bytes = palette(0).GetBytes()
        Buffer.BlockCopy(bytes, 0, ret, offset, bytes.Length)
        offset += bytes.Length

        bytes = palette(1).GetBytes()
        Buffer.BlockCopy(bytes, 0, ret, offset, bytes.Length)
        offset += bytes.Length

        bytes = bitmapData
        Buffer.BlockCopy(bytes, 0, ret, offset, bytes.Length)

        Return ret
    End Function

    Public Function Build24bppDIB(
        bitmapData As Byte(), width As Integer, height As Integer) As Byte()

        Dim bfh As BITMAPFILEHEADER
        With bfh
            .bfType         = &H4D42
            .bfSize         = 54 + bitmapData.Length
            .bfReserved1    = 0
            .bfReserved2    = 0
            .bfOffBits      = 54
        End With

        Dim bih As BITMAPINFOHEADER
        With bih
            .biSize             = 40
            .biWidth            = width
            .biHeight           = height
            .biPlanes           = 1
            .biBitCount         = 24
            .biCompression      = 0
            .biSizeImage        = 0
            .biXPelsPerMeter    = 3780 ' 96dpi
            .biYPelsPerMeter    = 3780 ' 96dpi
            .biClrUsed          = 0
            .biClrImportant     = 0
        End With

        Dim ret     As Byte() = New Byte(54 + bitmapData .Length - 1) {}
        Dim bytes   As Byte()
        Dim offset  As Integer = 0

        bytes = bfh.GetBytes()
        Buffer.BlockCopy(bytes, 0, ret, offset, bytes.Length)
        offset += bytes.Length

        bytes = bih.GetBytes()
        Buffer.BlockCopy(bytes, 0, ret, offset, bytes.Length)
        offset += bytes.Length

        bytes = bitmapData 
        Buffer.BlockCopy(bytes, 0, ret, offset, bytes.Length)

        Return ret
    End Function

End Module
