Imports System
Imports System.Drawing

Namespace Ys.Image
    Friend Module DIB

        Public Function Build1bppDIB(bitmapData As Byte(), 
                                     width As Integer, 
                                     height As Integer, 
                                     foreColor As Color, 
                                     backColor As Color) As Byte()
            Dim bfh = New BITMAPFILEHEADER() With {
                .bfType = &H4D42,
                .bfSize = 62 + bitmapData.Length,
                .bfReserved1 = 0,
                .bfReserved2 = 0,
                .bfOffBits = 62
            }

            Dim bih = New BITMAPINFOHEADER() With {
                .biSize = 40,
                .biWidth = width,
                .biHeight = height,
                .biPlanes = 1,
                .biBitCount = 1,
                .biCompression = 0,
                .biSizeImage = 0,
                .biXPelsPerMeter = 0,
                .biYPelsPerMeter = 0,
                .biClrUsed = 0,
                .biClrImportant = 0
            }

            Dim palette = New RGBQUAD() {
                New RGBQUAD() With {
                    .rgbBlue = foreColor.B,
                    .rgbGreen = foreColor.G,
                    .rgbRed = foreColor.R,
                    .rgbReserved = 0
                },
                New RGBQUAD() With {
                    .rgbBlue = backColor.B,
                    .rgbGreen = backColor.G,
                    .rgbRed = backColor.R,
                    .rgbReserved = 0
                }
            }

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

        Public Function Build24bppDIB(bitmapData As Byte(),
                                      width As Integer,
                                      height As Integer) As Byte()
            Dim bfh = New BITMAPFILEHEADER() With {
                .bfType = &H4D42,
                .bfSize = 54 + bitmapData.Length,
                .bfReserved1 = 0,
                .bfReserved2 = 0,
                .bfOffBits = 54
            }

            Dim bih = New BITMAPINFOHEADER() With {
                .biSize = 40,
                .biWidth = width,
                .biHeight = height,
                .biPlanes = 1,
                .biBitCount = 24,
                .biCompression = 0,
                .biSizeImage = 0,
                .biXPelsPerMeter = 0,
                .biYPelsPerMeter = 0,
                .biClrUsed = 0,
                .biClrImportant = 0
            }

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

End Namespace
