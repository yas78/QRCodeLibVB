Imports System

Namespace Ys.QRCode

    ''' <summary>
    ''' 符号化モード
    ''' </summary>
    Public Enum EncodingMode
        UNKNOWN
        NUMERIC
        ALPHA_NUMERIC
        EIGHT_BIT_BYTE
        KANJI
    End Enum

    ''' <summary>
    ''' 誤り訂正レベル
    ''' </summary>
    Public Enum ErrorCorrectionLevel
        L
        M
        Q
        H
    End Enum

End Namespace

